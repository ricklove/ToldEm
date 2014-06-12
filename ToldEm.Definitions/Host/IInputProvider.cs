using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public interface IInputProvider
    {
        void RegisterKeyCommands(IList<string> commands);
        InputState InputState { get; }
    }

    public class InputState
    {
        public IList<InputValue> InputValues { get; private set; }

        public InputState()
        {
            InputValues = new List<InputValue>();
        }
    }


    public class InputValue
    {
        public InputType Type { get; private set; }
        public ScreenPoint Position { get; private set; }
        public KeyValue KeyValue { get; private set; }

        public InputValue(InputType type, ScreenPoint position, KeyValue keyValue)
        {
            Type = type;
            Position = position;
            KeyValue = keyValue;
        }
    }

    public enum InputType
    {
        Mouse,
        Touch,
        Keyboard,
        Gamepad
    }

    public class KeyValue
    {
        public KeyType KeyType { get; private set; }
        public KeyDirection Direction { get; private set; }
        public string Command { get; private set; }

        public KeyValue(KeyDirection direction)
        {
            KeyType = Core.KeyType.Direction;
            Direction = direction;
            Command = "";
        }

        public KeyValue(string command)
        {
            KeyType = Core.KeyType.Command;
            Direction = KeyDirection.None;
            Command = command;
        }
    }

    public enum KeyType
    {
        Direction,
        Command
    }

    public enum KeyDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}
