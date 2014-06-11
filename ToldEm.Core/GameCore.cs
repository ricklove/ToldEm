using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public class GameCore : IGameCore
    {
        private IHost _host;
        private IGame _game;

        private GraphicsEngine _graphicsEngine;

        public bool IsDebugEnabled { get; set; }

        public void Setup(IHost host, IGame game)
        {
            IsDebugEnabled = true;
            _graphicsEngine = new GraphicsEngine();

            _host = host;
            _game = game;

            _host.SetGameCore(this);

            _game.Setup();

            // Create assets
            var drawables = _game.Entities.Where(e => e.IsDrawable).OrderBy(d => d.ZIndex).Cast<IDrawableInner>().ToList();
            drawables.ForEach(d =>
            {
                _host.GraphicsProvider.RegisterImageResource(d.ResourceUrl, d.ResourceUrl);
                _host.GraphicsProvider.LoadImageResource(d.ResourceUrl);
            });
        }

        public void Tick(double totalMS)
        {
            //            _host.Log("Tick " + Math.Ceiling(totalMS));

            //_game.Entities.ForEach(e => e.Size.Width += 0.001);

            _graphicsEngine.DrawGraphics(_host, _game, IsDebugEnabled);
        }

        
    }
}
