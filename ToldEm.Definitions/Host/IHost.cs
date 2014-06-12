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
        void TickGraphics(GameTime totalMS);
        void TickLogic(GameTime totalMS);
    }

    public class GameTime
    {
        public double TotalMilliseconds { get; private set; }
        public GameTime(double totalMilliseconds)
        {
            TotalMilliseconds = totalMilliseconds;
        }
    }

    public class GameTimeSpan
    {
        public double TotalMilliseconds { get; private set; }

        public GameTimeSpan()
        {
            TotalMilliseconds = 0;
        }

        public GameTimeSpan(double totalMilliseconds)
        {
            TotalMilliseconds = totalMilliseconds;
        }
    }

    public interface IGame
    {
        List<IEntity> Entities { get; }
        void Setup();
    }

}
