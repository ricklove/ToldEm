
using System;
using System.Collections.Generic;

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
    public class GameInputValue : IGameInputValue
    {
        public Boolean IsWithinBounds { get; set; }
        public Boolean HasChanged { get; set; }
        public InputChangeType ChangeType { get; set; }
        public GameTimeSpan TimeSinceDown { get; set; }
        public InputType Type { get; set; }
        public IGamePoint Position { get; set; }
        public KeyValue KeyValue { get; set; }


        public GameInputValue(Boolean isWithinBounds, Boolean hasChanged, InputChangeType changeType, GameTimeSpan timeSinceDown, InputType type, IGamePoint position, KeyValue keyValue)
        {
            IsWithinBounds = isWithinBounds;
            HasChanged = hasChanged;
            ChangeType = changeType;
            TimeSinceDown = timeSinceDown;
            Type = type;
            Position = position;
            KeyValue = keyValue;

        }

        public object Clone()
        {
            return new GameInputValue(IsWithinBounds, HasChanged, ChangeType, TimeSinceDown, Type, Position, KeyValue);
        }

    }
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

    public partial class Entity : IEntity, IDrawable, ITileable, IInputable, IPlaceable
    {
        // Clone
        public Entity Clone()
        {
            var c = new Entity();
            c.IsDrawable = IsDrawable;
            c.IsTileable = IsTileable;
            c.IsInputable = IsInputable;
            c.IsPlaceable = IsPlaceable;

            c.ResourceUrl = (String)ResourceUrl.Clone();
            c.ZIndex = ZIndex;
            c.FitType = FitType;
            c.Alignment = (IAlignment)Alignment.Clone();
            c.TileDirection = TileDirection;
            c.HandlesKeyboardInput = HandlesKeyboardInput;
            c.HandlesGlobalTouchInput = HandlesGlobalTouchInput;
            c.HandleInputCallback = (Action<GameInputState>)HandleInputCallback.Clone();
            c.Size = (IGameSize)Size.Clone();
            c.Anchor = (IGamePoint)Anchor.Clone();
            c.Position = (IGamePoint)Position.Clone();

            return c;
        }


        // Behaviors
        public bool IsDrawable { get; private set; }
        public bool IsTileable { get; private set; }
        public bool IsInputable { get; private set; }
        public bool IsPlaceable { get; private set; }


        // Make Behavior 
        public Entity MakeDrawable(String resourceUrl, Int32 zIndex, FitType fitType, IAlignment alignment)
        {
            ResourceUrl = resourceUrl;
            ZIndex = zIndex;
            FitType = fitType;
            Alignment = alignment;

            IsDrawable = true;
            return this;
        }

        public Entity MakeTileable(TileDirection tileDirection)
        {
            TileDirection = tileDirection;

            IsTileable = true;
            return this;
        }

        public Entity MakeInputable(Boolean handlesKeyboardInput, Boolean handlesGlobalTouchInput, Action<GameInputState> handleInputCallback)
        {
            HandlesKeyboardInput = handlesKeyboardInput;
            HandlesGlobalTouchInput = handlesGlobalTouchInput;
            HandleInputCallback = handleInputCallback;

            IsInputable = true;
            return this;
        }

        public Entity MakePlaceable(IGameSize size, IGamePoint anchor, IGamePoint position)
        {
            Size = size;
            Anchor = anchor;
            Position = position;

            IsPlaceable = true;
            return this;
        }



        // Properties
        public String ResourceUrl { get; set; }
        public Int32 ZIndex { get; set; }
        public FitType FitType { get; set; }
        public IAlignment Alignment { get; set; }
        public IScreenSize _ImageSize { get; set; }
        public TileDirection TileDirection { get; set; }
        public Boolean HandlesKeyboardInput { get; set; }
        public Boolean HandlesGlobalTouchInput { get; set; }
        public Action<GameInputState> HandleInputCallback { get; set; }
        public IGameSize Size { get; set; }
        public IGamePoint Anchor { get; set; }
        public IGamePoint Position { get; set; }


    }
}