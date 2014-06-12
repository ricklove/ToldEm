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

        Action<IGameInputState> HandleInputCallback { get; }
    }

    public interface IGameInputState
    {
        IList<IGameInputValue> InputValues { get; }
    }

    public interface IGameInputValue
    {
        bool IsWithinBounds { get; }
        bool HasChanged { get; }
        InputChangeType ChangeType { get; }
        TimeSpan TimeSinceDown { get; }

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
