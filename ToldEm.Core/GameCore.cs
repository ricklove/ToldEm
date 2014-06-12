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
        private InputEngine _inputEngine;

        public bool IsDebugEnabled { get; set; }

        public void Setup(IHost host, IGame game)
        {
            IsDebugEnabled = true;
            _graphicsEngine = new GraphicsEngine();
            _inputEngine = new InputEngine();

            _host = host;
            _game = game;

            _host.SetGameCore(this);

            _game.Setup();

            // Create assets
            var drawables = _game.Entities.Cast<Entity>().Where(e => e.IsDrawable).OrderBy(d => d.ZIndex).Cast<IDrawable>().ToList();
            drawables.ForEach(d =>
            {
                _host.GraphicsProvider.RegisterImageResource(d.ResourceUrl, d.ResourceUrl);
                _host.GraphicsProvider.LoadImageResource(d.ResourceUrl);
            });
        }

        public void TickGraphics(GameTime gameTime)
        {
            _graphicsEngine.DrawGraphics(_host, _game, IsDebugEnabled);
        }

        public void TickLogic(GameTime gameTime)
        {
            //_host.Log("Tick " + Math.Ceiling(totalMS));
            //_game.Entities.ForEach(e => e.Position.X -= 0.01);
            _inputEngine.HandleInput(_host, _game, gameTime);
        }
    }
}
