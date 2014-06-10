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
        IGraphicsEngine GraphicsEngine { get; }
        void Log(string message);
    }

    public interface IGameCore
    {
        void Setup(IHost host, IGame game);
        void Tick(double totalMS);
    }

    public interface IGame
    {
        List<Entity> Entities { get; }
        void Setup();
    }

}
