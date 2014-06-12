using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToldEm.Core;

namespace ToldEm.WPF
{
    class WPFInputProvider : IInputProvider
    {
        private Canvas _target;

        private Dictionary<Key, string> _commandAssignments;
        private InputState _inputState;

        private IList<string> _commands;

        public InputState InputState
        {
            get
            {
                UpdateInputPositions();
                return _inputState;
            }
        }

        public void RegisterKeyCommands(IList<string> commands)
        {
            _commands = commands;

            var defaultKeys = new Key[][] { 
                new Key[]{Key.Space},
                new Key[]{Key.LeftCtrl, Key.RightCtrl},
                new Key[]{Key.LeftShift, Key.RightShift},
                new Key[]{Key.LeftAlt, Key.RightAlt},
            };

            for (int i = 0; i < commands.Count; i++)
            {
                if (i >= defaultKeys.Length)
                {
                    break;
                }

                for (int iKey = 0; iKey < defaultKeys[i].Length; iKey++)
                {
                    _commandAssignments.Add(defaultKeys[i][iKey], commands[i]);
                }
            }
        }

        public WPFInputProvider(Canvas target)
        {
            _target = target;
            _commandAssignments = new Dictionary<Key, string>();
            _inputState = new InputState();

            _target.KeyDown += _target_KeyDown;
            _target.KeyUp += _target_KeyUp;

            _target.MouseDown += HandleInputDevice;
            _target.MouseUp += HandleInputDevice;
            _target.MouseMove += HandleInputDevice;

            //_target.TouchUp += HandleInputDevice;
            //_target.TouchDown += HandleInputDevice;
            //_target.TouchMove += HandleInputDevice;

            Touch.FrameReported += Touch_FrameReported;
        }

        private TouchFrameEventArgs _lastTouchFrame;

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            _lastTouchFrame = e;
        }

        private List<InputDevice> _devices = new List<InputDevice>();

        void UpdateInputPositions()
        {
            var toRemove = _inputState.InputValues.Where(v => !(v is KeyInputValue)).ToList();
            toRemove.ForEach(r => _inputState.InputValues.Remove(r));

            foreach (var d in _devices)
            {
                if (d is MouseDevice)
                {
                    var m = d as MouseDevice;
                    if (_target == m.Captured)
                    {
                        if (m.LeftButton == MouseButtonState.Pressed
                            || m.RightButton == MouseButtonState.Pressed
                            || m.MiddleButton == MouseButtonState.Pressed
                            || m.XButton1 == MouseButtonState.Pressed
                            || m.XButton2 == MouseButtonState.Pressed)
                        {
                            var pos = m.GetPosition(_target);
                            var screenPos = new ScreenPoint(pos.X, pos.Y);
                            _inputState.InputValues.Add(new InputValue(Core.InputType.Mouse, screenPos, null));
                        }
                    }
                }
            }

            // Touch input
            var touchPoints = _lastTouchFrame.GetTouchPoints(_target).ToList();

            touchPoints.ForEach(t =>
            {
                var pos = t.Position;
                var screenPos = new ScreenPoint(pos.X, pos.Y);
                _inputState.InputValues.Add(new InputValue(Core.InputType.Touch, screenPos, null));
            });
        }

        void HandleInputDevice(object sender, InputEventArgs e)
        {
            if (!_devices.Contains(e.Device))
            {
                _devices.Add(e.Device);
            }
        }

        class KeyInputValue : InputValue
        {
            public Key KeySource { get; private set; }

            public KeyInputValue(Key keySource, string command)
                : base(Core.InputType.Keyboard, null, new KeyValue(command))
            {
                KeySource = keySource;
            }

            public KeyInputValue(Key keySource, KeyDirection direction)
                : base(Core.InputType.Keyboard, null, new KeyValue(direction))
            {
                KeySource = keySource;
            }

        }

        void _target_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string command = null;
            KeyDirection? direction = null;

            if (_commandAssignments.ContainsKey(e.Key))
            {
                command = _commandAssignments[e.Key];
            }
            else if (
              e.Key == Key.Up
              || e.Key == Key.Down
              || e.Key == Key.Left
              || e.Key == Key.Right)
            {
                direction = e.Key == Key.Up ? KeyDirection.Up
                    : e.Key == Key.Down ? KeyDirection.Down
                    : e.Key == Key.Left ? KeyDirection.Left
                    : KeyDirection.Right;
            }

            if (!_inputState.InputValues.Where(v => v is KeyInputValue).Cast<KeyInputValue>().Any(v => v.KeySource == e.Key))
            {
                if (command != null)
                {
                    _inputState.InputValues.Add(new KeyInputValue(e.Key, command));
                }
                else if (direction != null)
                {
                    _inputState.InputValues.Add(new KeyInputValue(e.Key, direction.Value));
                }
            }
        }

        void _target_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var toRemove = _inputState.InputValues.Where(v => v is KeyInputValue).Cast<KeyInputValue>().Where(v => v.KeySource == e.Key).ToList();
            toRemove.ForEach(r => _inputState.InputValues.Remove(r));
        }


    }
}
