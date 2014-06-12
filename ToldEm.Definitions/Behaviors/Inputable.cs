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

        public GameInputState(GameTime gameTime, IList<IGameInputValue> inputValues)
        {
            GameTime = gameTime;

            InputValues = inputValues;
            HasInput = InputValues.Any();
            HasKeyboardInput = InputValues.Any(v => v.Type == InputType.Keyboard);

            if (HasInput)
            {
                var breakdance = true;
            }
        }

        public bool HasInput { get; private set; }
        public bool HasKeyboardInput { get; private set; }
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
