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

        public override void Setup()
        {
            var background = Create();
            background.MakeDrawable(
                resourceUrl: "Data/background.jpg",
                zIndex: -1000,
                fitType: FitType.Fill,
                alignment: new Alignment(HorizontalAlignment.Center, VerticalAlignment.Bottom)
            )
            .MakePlaceable(
                size: new GameSize(6, 2),
                anchor: new GamePoint(0, -1),
                position: new GamePoint(0, -1)
                )
            .MakeTileable(
                tileDirection: TileDirection.Horizontal
            )
            .MakeInputable(true, true, s =>
            {
                if (s.InputValues.Any(v => v.KeyValue.Direction == KeyDirection.Right))
                {
                    background.Position.X -= 0.01;
                }
                else if (s.InputValues.Any(v => v.KeyValue.Direction == KeyDirection.Left))
                {
                    background.Position.X += 0.01;
                }
            });
        }
    }
}
