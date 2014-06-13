using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public interface IScrollable : IBehavior
    {
        IScrolling Target { get; }
        double Ratio { get; }
        IGamePoint Position { get; set; }
        IGamePoint _InitialPosition { get; set; }
    }

    public interface IScrolling : IBehavior
    {
        IGamePoint Position { get; }
    }

}
