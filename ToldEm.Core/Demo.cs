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
            {
                IsDrawable = true,
                ResourceName = "Snake",
                ResourceUrl = "snake.png",
                ZIndex = 10,
                Size = new GameSize(1, 1),
                Alignment = new Alignment(),
                Anchor = new GamePoint(0, 0),
                FitType = FitType.Fit,
                Position = new GamePoint()
            });

            Entities.Add(new Entity()
            {
                IsDrawable = true,
                ResourceName = "Lion",
                ResourceUrl = "lion.png",
                ZIndex = -10,
                Size = new GameSize(1, 2),
                Alignment = new Alignment(),
                Anchor = new GamePoint(0, 0),
                FitType = FitType.Fit,
                Position = new GamePoint(0.5, 0.5)
            });

            Entities.Add(new Entity()
            {
                IsDrawable = true,
                ResourceName = "Lion",
                ResourceUrl = "lion.png",
                ZIndex = 0,
                Size = new GameSize(0.25, 0.25),
                Alignment = new Alignment(HorizontalAlignment.Left, VerticalAlignment.Top),
                Anchor = new GamePoint(-1, -1),
                FitType = FitType.Fit,
                Position = new GamePoint(-1, -1)
            });

            Entities.Add(new Entity()
            {
                IsDrawable = true,
                ResourceName = "Lion",
                ResourceUrl = "lion.png",
                ZIndex = 0,
                Size = new GameSize(0.25, 0.25),
                Alignment = new Alignment(HorizontalAlignment.Left, VerticalAlignment.Bottom),
                Anchor = new GamePoint(-1, 1),
                FitType = FitType.Fit,
                Position = new GamePoint(-1, 1)
            });

            Entities.Add(new Entity()
            {
                IsDrawable = true,
                ResourceName = "Lion",
                ResourceUrl = "lion.png",
                ZIndex = 0,
                Size = new GameSize(0.25, 0.25),
                Alignment = new Alignment(HorizontalAlignment.Right, VerticalAlignment.Top),
                Anchor = new GamePoint(1, -1),
                FitType = FitType.Fit,
                Position = new GamePoint(1, -1)
            });

            Entities.Add(new Entity()
            {
                IsDrawable = true,
                ResourceName = "Lion",
                ResourceUrl = "lion.png",
                ZIndex = 0,
                Size = new GameSize(0.25, 0.25),
                Alignment = new Alignment(HorizontalAlignment.Right, VerticalAlignment.Bottom),
                Anchor = new GamePoint(1, 1),
                FitType = FitType.Fit,
                Position = new GamePoint(1, 1)
            });
        }
    }
}
