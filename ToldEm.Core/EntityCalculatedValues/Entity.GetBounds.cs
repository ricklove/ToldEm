using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
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
