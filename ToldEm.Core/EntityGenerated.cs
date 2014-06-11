using System;

namespace ToldEm.Core
{

    public class GameSize : IGameSize
    {
        public Double Width { get; set; }
        public Double Height { get; set; }


        public GameSize(Double width = 1, Double height = 1)
        {
            Width = width;
            Height = height;

        }

        public object Clone()
        {
            return new GameSize(Width, Height);
        }

    }
    public class GamePoint : IGamePoint
    {
        public Double X { get; set; }
        public Double Y { get; set; }


        public GamePoint(Double x = 0, Double y = 0)
        {
            X = x;
            Y = y;

        }

        public object Clone()
        {
            return new GamePoint(X, Y);
        }

    }
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
            c.IsPlaceable = IsPlaceable;
            c.IsDrawable = IsDrawable;

            c.Size = (IGameSize)Size.Clone();
            c.Anchor = (IGamePoint)Anchor.Clone();
            c.Position = (IGamePoint)Position.Clone();
            c.ResourceUrl = (String)ResourceUrl.Clone();
            c.ResourceName = (String)ResourceName.Clone();
            c.ZIndex = ZIndex;
            c.FitType = FitType;
            c.Alignment = (Alignment)Alignment.Clone();

            return c;
        }


        // Behaviors
        public bool IsPlaceable { get; private set; }
        public bool IsDrawable { get; private set; }


        // Make Behavior 
        public Entity MakePlaceable(IGameSize size, IGamePoint anchor, IGamePoint position)
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
        public IGameSize Size { get; set; }
        public IGamePoint Anchor { get; set; }
        public IGamePoint Position { get; set; }
        public String ResourceUrl { get; set; }
        public String ResourceName { get; set; }
        public Int32 ZIndex { get; set; }
        public FitType FitType { get; set; }
        public Alignment Alignment { get; set; }
        public IScreenSize _ImageSize { get; set; }


    }
}