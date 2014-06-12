using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public interface IEntity
    {
    }

    public interface IBehavior
    {
    }

    public interface IValue : ICloneable
    {

    }

    public class DefaultAttribute : Attribute
    {
        public string Value { get; set; }

        public DefaultAttribute(string value)
        {
            Value = value;
        }
    }

    #region Entity Generator

    public static class Generator
    {
        public static string CreateAll()
        {
            var allTemplate = @"using System;

namespace ToldEm.Core
{{
{0}
}}";

            var content = CreateEntityValues() + "\r\n\r\n" + CreateEntityClass();

            var all = string.Format(allTemplate, content);

            return all;
        }

        public static string GetFormattedTypeName(Type type)
        {
            var t = type;

            if (!t.IsGenericType)
                return t.Name;

            StringBuilder sb = new StringBuilder();

            sb.Append(t.Name.Substring(0, t.Name.LastIndexOf("`")));

            var genericArguments = t.GetGenericArguments().ToList();
            if (genericArguments.Any())
            {
                sb.Append("<");

                var argList = genericArguments.Aggregate(new StringBuilder(), (sb2, a) => sb2.Append(GetFormattedTypeName(a) + ", "));

                sb.Append(argList.ToString().Trim(", ".ToCharArray()));

                sb.Append(">");
            }

            return sb.ToString();
        }

        public static string CreateEntityValues()
        {
            var type = typeof(Generator);
            var interfaces = type.Assembly.GetTypes().Where(t => t.IsInterface);
            var values = interfaces.Where(t => typeof(IValue).IsAssignableFrom(t) && t != typeof(IValue));

            var classTemplate = @"
    public class {1} : {0}
    {{
{5}   

        public {1}({2})
        {{
            {3}
        }}
   
        public object Clone()
        {{
            return new {1}({4});
        }}

    }}";
            // 0 = Prop Type
            // 1 = Prop Name
            // 2 = Lowercase Name
            // 3 = Default Value String
            var paramsTemplate = "{0} {2} {3}, ";
            var setValuesTemplates = "{1} = {2};\r\n";
            var argsTemplare = "{1}, ";
            var propTemplate = "public {0} {1} {{get; set;}}\r\n";

            return values.Aggregate(new StringBuilder(), (s, v) =>
            {
                var pInfos = from p in v.GetProperties()
                             let lowerName = char.ToLower(p.Name[0]) + p.Name.Substring(1)
                             let attributes = p.GetCustomAttributes(typeof(DefaultAttribute), false)
                             let defaultValue = attributes.Any() ? " = " + attributes.Cast<DefaultAttribute>().First().Value : ""
                             select new { property = p, type = GetFormattedTypeName(p.PropertyType), name = p.Name, lowerName = lowerName, defaultValue = defaultValue };

                Func<string, string> doFormat = (template) =>
                {
                    var text = pInfos.Aggregate(new StringBuilder(), (s2, p) => s2.AppendFormat(template, p.type, p.name, p.lowerName, p.defaultValue)).ToString();

                    return text.Trim(" ,".ToCharArray());
                };

                var paramsStr = doFormat(paramsTemplate);
                var setValuesStr = doFormat(setValuesTemplates);
                var argsStr = doFormat(argsTemplare);
                var propStr = doFormat(propTemplate);

                return s.AppendFormat(classTemplate, v.Name, v.Name.Substring(1), paramsStr, setValuesStr, argsStr, propStr);
            }).ToString();
        }

        public static string CreateEntityClass()
        {
            var type = typeof(Generator);
            var interfaces = type.Assembly.GetTypes().Where(t => t.IsInterface);
            var behaviors = interfaces.Where(t => typeof(IBehavior).IsAssignableFrom(t) && t != typeof(IBehavior));

            var allProperties = behaviors.SelectMany(b => b.GetProperties()).GroupBy(p => p.Name).Select(g => new
            {
                Name = g.Key,
                Properties = g.ToList()
            }).ToList();

            // Verify all types are the same
            allProperties.ForEach(p =>
            {
                p.Properties.ForEach(p2 =>
                {
                    if (p2.PropertyType != p.Properties[0].PropertyType)
                    {
                        var message = string.Format("Incompatible Property Types: {0}.{1} is {2} and {3}.{4} is {5}",
                            GetFormattedTypeName(p.Properties[0].DeclaringType), p.Properties[0].Name, GetFormattedTypeName(p.Properties[0].PropertyType),
                            GetFormattedTypeName(p2.DeclaringType), p2.Name, GetFormattedTypeName(p2.PropertyType));

                        throw new Exception(message);
                    }
                });
            });

            // Generate the class
            var classTemplate = @"public partial class Entity: IEntity {0}{{
// Clone
{1}

// Behaviors
{2}

// Make Behavior 
{3}

// Properties
{4}

}}";
            var propTemplate = "public {0} {1} {{ get; set; }}\r\n";

            var cloneTemplate = @"public Entity Clone()
{{
var c = new Entity();
{0}
{1}
return c;
}}
";

            var cloneItemTemplate = "c.{0} = {1} {0}{2};\r\n";
            var cloneIsItemTemplate = "c.Is{0} = Is{0};\r\n";

            var isTemplate = "public bool Is{0} {{ get; private set; }}\r\n";
            var makeTemplate = @"public Entity Make{0}({1})
{{
    {2}
    Is{0} = true;
    return this;
}}

";

            //var makeArgumentTemplate = "{0} {1} {2}";
            // Ignore default values for now
            var makeArgumentTemplate = "{0} {1}, ";
            var makeSetValueTemplate = "{0} = {1};\r\n";


            // Behavior List
            var behaviorList = behaviors.Aggregate(new StringBuilder(), (s, b) => s.AppendFormat(", {0}", b.Name));

            // Is Behavior List
            var isList = behaviors
                //.Where(b => b.IsPublic)
                .Aggregate(new StringBuilder(), (s, b) => s.AppendFormat(isTemplate, b.Name.Substring(1)));

            // Make Behavior List
            var makeList = behaviors
                //.Where(b => b.IsPublic)
                .Aggregate(new StringBuilder(), (s, b) =>
            {
                var pInfos = from p in b.GetProperties()
                             where !p.Name.StartsWith("_")
                             let lowerName = char.ToLower(p.Name[0]) + p.Name.Substring(1)
                             let attributes = p.GetCustomAttributes(typeof(DefaultAttribute), false)
                             let defaultValue = attributes.Any() ? attributes.Cast<DefaultAttribute>().First().Value : ""
                             select new { property = p, type = GetFormattedTypeName(p.PropertyType), name = p.Name, lowerName = lowerName, defaultValue = defaultValue };

                var args = pInfos.Aggregate(new StringBuilder(), (s2, p) =>
                {
                    return s2.AppendFormat(makeArgumentTemplate, p.type, p.lowerName, p.defaultValue != "" ? (" = " + p.defaultValue) : "");
                });

                var setValues = pInfos.Aggregate(new StringBuilder(), (s3, p) => s3.AppendFormat(makeSetValueTemplate, p.name, p.lowerName));

                return s.AppendFormat(makeTemplate, b.Name.Substring(1), args.ToString().Trim(' ', ','), setValues);
            });

            // Prop List
            var propList = allProperties.Aggregate(new StringBuilder(), (s, p) => s.AppendFormat(propTemplate, GetFormattedTypeName(p.Properties[0].PropertyType), p.Name));

            // Clone Props
            var cloneIsValues = behaviors
                //.Where(b => b.IsPublic)
                .Aggregate(new StringBuilder(), (s, b) => s.AppendFormat(cloneIsItemTemplate, b.Name.Substring(1)));

            var cloneStr = string.Format(cloneTemplate, cloneIsValues,
                allProperties
                //.Where(p => p.Properties.First().DeclaringType.IsPublic)
                .Where(p => !p.Name.StartsWith("_"))
                .Aggregate(new StringBuilder(), (s, p) =>
                {
                    var prop = p.Properties[0];
                    var isClonable = typeof(ICloneable).IsAssignableFrom(prop.PropertyType);
                    var castStr = isClonable ? ("(" + GetFormattedTypeName(prop.PropertyType) + ")") : "";
                    var dotCloneStr = isClonable ? ".Clone()" : "";

                    return s.AppendFormat(cloneItemTemplate, p.Name, castStr, dotCloneStr);
                }));

            var classStr = string.Format(classTemplate, behaviorList, cloneStr, isList, makeList, propList);

            return classStr;
        }
    }





    #endregion
}
