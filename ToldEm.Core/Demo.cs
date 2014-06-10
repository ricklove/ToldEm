using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public class Demo : GameBase
    {
        public override void Setup()
        {
            Entities.Add(new Entity()
                .MakeDrawable(
                    resourceUrl: "snake.png",
                    resourceName: "Snake",
                    zIndex: 10,
                    fitType: FitType.Fit,
                    alignment: new Alignment(HorizontalAlignment.Center, VerticalAlignment.Middle)
                )
                .MakePlaceable(
                    size: new GameSize(1, 1),
                    anchor: new GamePoint(),
                    position: new GamePoint())
                );

            Entities.Add(new Entity()
                .MakeDrawable(
                    resourceUrl: "lion.png",
                    resourceName: "Lion",
                    zIndex: -10,
                    fitType: FitType.Fit,
                    alignment: new Alignment(HorizontalAlignment.Center, VerticalAlignment.Middle)
                )
                .MakePlaceable(
                    size: new GameSize(2, 1),
                    anchor: new GamePoint(),
                    position: new GamePoint(0.5, 0))
                );

            Entities.Add(new Entity()
               .MakeDrawable(
                   resourceUrl: "lion.png",
                   resourceName: "Lion",
                   zIndex: 0,
                   fitType: FitType.Fit,
                   alignment: new Alignment(HorizontalAlignment.Left, VerticalAlignment.Top)
               )
               .MakePlaceable(
                   size: new GameSize(0.25, 0.25),
                   anchor: new GamePoint(-1, -1),
                   position: new GamePoint(-1, -1))
               );

            //Entities.Add(new Entity()
            //{
            //    ResourceName = "Lion",
            //    ResourceUrl = "lion.png",
            //    ZIndex = 0,
            //    Size = new GameSize(0.25, 0.25),
            //    Alignment = new Alignment(HorizontalAlignment.Left, VerticalAlignment.Bottom),
            //    Anchor = new GamePoint(-1, 1),
            //    FitType = FitType.Fit,
            //    Position = new GamePoint(-1, 1)
            //});

            //Entities.Add(new Entity()
            //{
            //    ResourceName = "Lion",
            //    ResourceUrl = "lion.png",
            //    ZIndex = 0,
            //    Size = new GameSize(0.25, 0.25),
            //    Alignment = new Alignment(HorizontalAlignment.Right, VerticalAlignment.Top),
            //    Anchor = new GamePoint(1, -1),
            //    FitType = FitType.Fit,
            //    Position = new GamePoint(1, -1)
            //});

            //Entities.Add(new Entity()
            //{
            //    ResourceName = "Lion",
            //    ResourceUrl = "lion.png",
            //    ZIndex = 0,
            //    Size = new GameSize(0.25, 0.25),
            //    Alignment = new Alignment(HorizontalAlignment.Right, VerticalAlignment.Bottom),
            //    Anchor = new GamePoint(1, 1),
            //    FitType = FitType.Fit,
            //    Position = new GamePoint(1, 1)
            //});
        }
    }
}
