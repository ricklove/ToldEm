using System;

namespace ToldEm.Core
{

    public class Alignment : IAlignment
    {
        public HorizontalAlignment Horizontal { get; set; }
        public VerticalAlignment Vertical { get; set; }


        public Alignment(HorizontalAlignment horizontal = HorizontalAlignment.Center, VerticalAlignment vertical = VerticalAlignment.Middle)
        {
            Horizontal = horizontal;
            Vertical = vertical;

        }

        public object Clone()
        {
            return new Alignment(Horizontal, Vertical);
        }

    }

    public partial class Entity : IEntity, IPlaceable, IDrawable, IDrawableInner
    {
        // Clone
        public Entity Clone()
        {
            var c = new Entity();
            c.Size = Size;
            c.Anchor = Anchor;
            c.Position = Position;
            c.ResourceUrl = ResourceUrl;
            c.ResourceName = ResourceName;
            c.ZIndex = ZIndex;
            c.FitType = FitType;
            c.Alignment = Alignment;
            c._ImageSize = _ImageSize;

            return c;
        }


        // Behaviors
        public bool IsPlaceable { get; private set; }
        public bool IsDrawable { get; private set; }


        // Make Behavior 
        public Entity MakePlaceable(GameSize size, GamePoint anchor, GamePoint position)
        {
            Size = size;
            Anchor = anchor;
            Position = position;

            IsPlaceable = true;
            return this;
        }

        public Entity MakeDrawable(String resourceUrl, String resourceName, Int32 zIndex, FitType fitType, Alignment alignment)
        {
            ResourceUrl = resourceUrl;
            ResourceName = resourceName;
            ZIndex = zIndex;
            FitType = fitType;
            Alignment = alignment;

            IsDrawable = true;
            return this;
        }



        // Properties
        public GameSize Size { get; set; }
        public GamePoint Anchor { get; set; }
        public GamePoint Position { get; set; }
        public String ResourceUrl { get; set; }
        public String ResourceName { get; set; }
        public Int32 ZIndex { get; set; }
        public FitType FitType { get; set; }
        public Alignment Alignment { get; set; }
        public IScreenSize _ImageSize { get; set; }


    }
}