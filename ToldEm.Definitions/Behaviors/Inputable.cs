using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public interface IInputable : IBehavior
    {
        GameBounds GetBounds();

        bool HandlesKeyboardInput { get; }
        bool HandlesGlobalTouchInput { get; }

        Action<GameInputState> HandleInputCallback { get; }
    }

    public class GameInputState
    {
        public GameTime GameTime { get; private set; }
        public IList<IGameInputValue> InputValues { get; set; }

        public GameInputState( GameTime gameTime )
        {
            InputValues = new List<IGameInputValue>();
            GameTime = gameTime;
        }
    }

    public interface IGameInputValue : IValue
    {
        bool IsWithinBounds { get; }
        bool HasChanged { get; }
        InputChangeType ChangeType { get; }
        GameTimeSpan TimeSinceDown { get; }

        InputType Type { get; }
        IGamePoint Position { get; }
        KeyValue KeyValue { get; }

    }

    public enum InputChangeType
    {
        None,
        Down,
        Up,
        Move
    }
}
