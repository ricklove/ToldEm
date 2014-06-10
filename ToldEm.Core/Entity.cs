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

    public class DefaultAttribute : Attribute
    {
        public string Value { get; set; }

        public DefaultAttribute(string value)
        {
            Value = value;
        }
    }



    

    //public interface IDrawableData : IDrawable
    //{
    //    GameSize ImageSize { get; }
    //}

    #region Entity Generator

    public static class Generator
    {
        public static string CreateEntityClass()
        {
            var type = typeof(Generator);
            var interfaces = type.Assembly.GetExportedTypes().Where(t => t.IsInterface);
            var behaviors = interfaces.Where(t => typeof(IBehavior).IsAssignableFrom(t) && t != typeof(IBehavior));

            var allProperties = behaviors.SelectMany(b => b.GetProperties()).GroupBy(p => p.Name).Select(g => new { Name = g.Key, Properties = g.ToList() }).ToList();

            // Verify all types are the same
            allProperties.ForEach(p =>
            {
                p.Properties.ForEach(p2 =>
                {
                    if (p2.PropertyType != p.Properties[0].PropertyType)
                    {
                        var message = string.Format("Incompatible Property Types: {0}.{1} is {2} and {3}.{4} is {5}",
                            p.Properties[0].DeclaringType.Name, p.Properties[0].Name, p.Properties[0].PropertyType.Name,
                            p2.DeclaringType.Name, p2.Name, p2.PropertyType.Name);

                        throw new Exception(message);
                    }
                });
            });

            // Generate the class
            var classTemplate = @"public partial class Entity: IEntity {0}{{
// Behaviors
{1}

// Properties
{2}

}}";
            var propTemplate = "public {0} {1} {{ get; set; }}\r\n";

            var behaviorList = behaviors.Aggregate(new StringBuilder(), (s, b) => s.AppendFormat(", {0}", b.Name));

            var isBehaviorList = behaviors.Aggregate(new StringBuilder(), (s, b) => s.AppendFormat(propTemplate, "bool", "Is" + b.Name.Substring(1)));
            var propList = allProperties.Aggregate(new StringBuilder(), (s, p) => s.AppendFormat(propTemplate, p.Properties[0].PropertyType.Name, p.Name));

            var classStr = string.Format(classTemplate, behaviorList, isBehaviorList, propList);

            return classStr;
        }
    }


    public partial class Entity : IEntity, IPlaceable, IDrawable
    {
        // Behaviors
        public bool IsPlaceable { get; set; }
        public bool IsDrawable { get; set; }


        // Properties
        public GameSize Size { get; set; }
        public GamePoint Anchor { get; set; }
        public GamePoint Position { get; set; }
        public IScreenSize _ImageSize { get; set; }
        public String ResourceUrl { get; set; }
        public String ResourceName { get; set; }
        public Int32 ZIndex { get; set; }
        public FitType FitType { get; set; }
        public Alignment Alignment { get; set; }


    }

    #endregion
}
