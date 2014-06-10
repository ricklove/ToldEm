using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public class GameSize
    {
        public GameSize(double width = 1, double height = 1)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class GamePoint
    {
        public GamePoint(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
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
        public double Bottom { get { return Top + Height; } }

        public double Width { get; set; }
        public double Height { get; set; }
    }

    public interface IPlaceable : IBehavior
    {
        [Default("1, 1")]
        GameSize Size { get; }
        [Default("0, 0")]
        GamePoint Anchor { get; }
        [Default("0, 0")]
        GamePoint Position { get; }

        GameBounds GetBounds();
    }


    public partial class Entity
    {
        public GameBounds GetBounds()
        {
            var boxLeftToAnchor = Anchor.X + 1.0;
            var boxTopToAnchor = Anchor.Y + 1.0;

            var gameLeftToAnchor = boxLeftToAnchor * Size.Width / 2.0;
            var gameTopToAnchor = boxTopToAnchor * Size.Height / 2.0;

            var gameLeft = Position.X - gameLeftToAnchor;
            var gameTop = Position.Y - gameTopToAnchor;

            return new GameBounds(gameLeft, gameTop, Size.Width, Size.Height);
        }
    }
}
