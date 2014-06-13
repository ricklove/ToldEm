using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToldEm.Core;

namespace BushRun
{
    public class BushRunGame : GameBase
    {
        private Entity Create()
        {
            var entity = new Entity();
            Entities.Add(entity);
            return entity;
        }

        private Entity Clone(Entity c)
        {
            var entity = c.Clone();
            Entities.Add(entity);
            return entity;
        }

        public override void Setup()
        {
            double? lastGameTimeMS = null;

            var scroller = Create();
            scroller
                .MakeScrolling(new GamePoint())
                .MakeInputable(true, true, s =>
                {
                    if (lastGameTimeMS.HasValue && s.InputValues[0].ChangeType != InputChangeType.Down)
                    {
                        var changePerSecond = 2.0;
                        var diffSeconds = (s.GameTime.TotalMilliseconds - lastGameTimeMS.Value) / 1000.0;


                        if (s.InputValues.Any(v => v.KeyValue.Direction == KeyDirection.Right))
                        {
                            scroller.Position.X -= changePerSecond * diffSeconds;
                        }
                        else if (s.InputValues.Any(v => v.KeyValue.Direction == KeyDirection.Left))
                        {
                            scroller.Position.X += changePerSecond * diffSeconds;
                        }
                    }

                    lastGameTimeMS = s.GameTime.TotalMilliseconds;
                });

            var background = Create()
                .MakeDrawable(
                    resourceUrl: "Data/background.jpg",
                    zIndex: -1000,
                    fitType: FitType.Fill,
                    alignment: new Alignment(HorizontalAlignment.Center, VerticalAlignment.Bottom)
                ).MakePlaceable(
                    size: new GameSize(6, 2),
                    anchor: new GamePoint(0, -1),
                    position: new GamePoint(0, -1)
                ).MakeTileable(
                    tileDirection: TileDirection.Horizontal
                ).MakeScrollable(scroller, 0.25, position: new GamePoint(0, -1));

            var ground = Clone(background);
            ground.ResourceUrl = "Data/ground.png";
            ground.ZIndex = -900;
            ground.Alignment = new Alignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            ground.Size = new GameSize(6, 0.5);
            ground.Anchor = new GamePoint(0, 1);
            ground.Position = new GamePoint(0, -0.5);
            ground.Ratio = 1;

        }
    }
}
