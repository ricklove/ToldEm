using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    class ScrollEngine
    {
        public void Scroll(IHost _host, IGame _game)
        {
            var scrollables = _game.Entities.Cast<Entity>().Where(e => e.IsScrollable).Cast<IScrollable>();

            scrollables.ToList().ForEach(s =>
            {
                if (s._InitialPosition == null)
                {
                    s._InitialPosition = s.Position;
                }

                var target = s.Target;
                s.Position = new GamePoint(
                    s._InitialPosition.X + (target.Position.X * s.Ratio),
                    s._InitialPosition.Y + (target.Position.Y * s.Ratio)
                    );
            });
        }
    }
}
