using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    class InputEngine
    {
        public void HandleInput(IHost _host, IGame _game)
        {
            var inputables = _game.Entities.Where(e => e.IsInputable).Cast<IInputable>();
            //inputables.
        }
    }
}
