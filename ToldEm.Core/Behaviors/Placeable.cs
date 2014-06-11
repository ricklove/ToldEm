﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public interface IGameSize : IValue
    {
        [Default("1")]
        double Width { get; set; }
        [Default("1")]
        double Height { get; set; }
    }

    public interface IGamePoint : IValue
    {
        [Default("0")]
        double X { get; set; }
        [Default("0")]
        double Y { get; set; }
    }

    public class GameBounds
    {
        public GameBounds(double left = 0, double top = 0, double width = 0, double height = 0)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Right { get { return Left + Width; } }
        public double Bottom { get { return Top - Height; } }

        public double Width { get; set; }
        public double Height { get; set; }
    }

    public interface IPlaceable : IBehavior
    {
        [Default("1, 1")]
        IGameSize Size { get; }
        [Default("0, 0")]
        IGamePoint Anchor { get; }
        [Default("0, 0")]
        IGamePoint Position { get; }

        GameBounds GetBounds();
    }


    public partial class Entity
    {
        public GameBounds GetBounds()
        {
            var boxLeftToAnchor = Anchor.X + 1.0;
            var boxBottomToAnchor = Anchor.Y + 1.0;

            var gameLeftToAnchor = boxLeftToAnchor * Size.Width / 2.0;
            var gameBottomToAnchor = boxBottomToAnchor * Size.Height / 2.0;

            var gameLeft = Position.X - gameLeftToAnchor;
            var gameBottom = Position.Y - gameBottomToAnchor;

            var gameTop = gameBottom + Size.Height;

            return new GameBounds(gameLeft, gameTop, Size.Width, Size.Height);
        }
    }
}
