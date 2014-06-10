using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public abstract class GameBase : IGame
    {
        public List<Entity> Entities { get; private set; }

        public GameBase()
        {
            Entities = new List<Entity>();
        }

        public abstract void Setup();
    }


}
