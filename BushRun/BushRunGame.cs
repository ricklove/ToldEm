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
        public override void Setup()
        {
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
                     position: new GamePoint(0.5, 0)
                     )
                 );
        }
    }
}
