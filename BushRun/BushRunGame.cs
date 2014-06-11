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
                     resourceUrl: "Data/background.jpg",
                     zIndex: -1000,
                     fitType: FitType.Fill,
                     alignment: new Alignment(HorizontalAlignment.Center, VerticalAlignment.Bottom)
                 )
                 .MakePlaceable(
                     size: new GameSize(4, 2),
                     anchor: new GamePoint(0, -1),
                     position: new GamePoint(0, -1)
                     )
                 );
        }
    }
}
