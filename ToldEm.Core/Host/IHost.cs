using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public interface IHost
    {
        void SetGameCore(IGameCore game);
        IGraphicsProvider GraphicsProvider { get; }
        IInputProvider InputProvider { get; }
        void Log(string message);
    }

    public interface IGameCore
    {
        void Setup(IHost host, IGame game);
        void TickGraphics(double totalMS);
        void TickLogic(double totalMS);
    }

    public interface IGame
    {
        List<Entity> Entities { get; }
        void Setup();
    }

}
