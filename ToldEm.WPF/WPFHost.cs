using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using ToldEm.Core;

namespace ToldEm.WPF
{
    class WPFHost : IHost
    {
        private DateTime _startTime;
        private DispatcherTimer _timer;

        private Action<string> _doLog;

        public IGameCore Game { get; private set; }
        public IGraphicsProvider GraphicsProvider { get; private set; }

        public WPFHost(Canvas target, Action<string> doLog)
        {
            GraphicsProvider = new WPFGraphicsProvider(target);
            _doLog = doLog;
        }

        public void SetGameCore(IGameCore game)
        {
            Game = game;

            _startTime = DateTime.Now;

            // Start ticking game
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (Game != null)
            {
                var elapsed = DateTime.Now - _startTime;
                Game.Tick(elapsed.TotalMilliseconds);
            }
        }


        public void Log(string message)
        {
            _doLog(message);
        }
    }
}
