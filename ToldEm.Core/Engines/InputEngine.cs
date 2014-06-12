using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    class InputEngine
    {
        public void HandleInput(IHost _host, IGame _game, GameTime gameTime)
        {
            var inputables = _game.Entities.Cast<Entity>().Where(e => e.IsInputable).Cast<IInputable>();
            if (inputables.Any())
            {
                var state = GetState(_host.InputProvider.InputState, gameTime);
            }
        }

        private GameInputState _lastState = new GameInputState(new GameTime(0));

        static List<T> CreateEmptyList<T>(T t) { return new List<T>(); }

        private GameInputState GetState(InputState inputState, GameTime gameTime)
        {
            // Create game state
            var state = new GameInputState(gameTime);
            var timeChange = new GameTimeSpan(gameTime.TotalMilliseconds - _lastState.GameTime.TotalMilliseconds);

            inputState.InputValues.ToList().ForEach(v =>
            {
                var gamePos = GetGamePosition(v.Position);

                state.InputValues.Add(new GameInputValue(false, true, InputChangeType.Down, new GameTimeSpan(),
                    v.Type, gamePos, v.KeyValue));
            });

            // Modify game state for changes based on last game state

            // Find matches
            var oldRemaining = _lastState.InputValues.ToList();
            var newRemaining = state.InputValues.ToList();

            var maxDistanceForSame = 0.01;
            var maxDistanceForSameSq = maxDistanceForSame * maxDistanceForSame;

            var matches = CreateEmptyList(new { oldValue = (IGameInputValue)null, newValue = (GameInputValue)null });

            var iNew = 0;

            while (iNew < newRemaining.Count)
            {
                IGameInputValue match = null;
                var n = newRemaining[iNew] as GameInputValue;

                if (n.Type == InputType.Keyboard
                    || n.Type == InputType.Gamepad)
                {
                    var sameValues = oldRemaining.Where(o => o.Type == n.Type && o.KeyValue == n.KeyValue);
                    match = sameValues.FirstOrDefault();

                    if (match != null)
                    {
                        n.HasChanged = false;
                        n.ChangeType = InputChangeType.None;
                    }
                }
                else
                {
                    var sameTypes = oldRemaining.Where(o => o.Type == n.Type);
                    var comparisons = sameTypes.Select(o => new { old = o, distanceSq = GetDistanceSq(o.Position, n.Position) });
                    var cMatch = comparisons.Where(c => c.distanceSq < maxDistanceForSameSq).OrderBy(c => c.distanceSq).FirstOrDefault();

                    if (cMatch != null)
                    {
                        match = cMatch.old;

                        if (cMatch.distanceSq > 0)
                        {
                            n.HasChanged = true;
                            n.ChangeType = InputChangeType.Move;
                        }
                        else
                        {
                            n.HasChanged = false;
                            n.ChangeType = InputChangeType.None;
                        }

                    }
                }

                if (match != null)
                {
                    matches.Add(new { oldValue = match, newValue = n });

                    oldRemaining.Remove(match);
                    newRemaining.RemoveAt(iNew);
                }
                else
                {
                    // If not found increment
                    iNew++;
                }
            }


            // Matches are either moving or no change (which was set already)
            // Any that are new are already down (Do nothing)
            // Any that are missing is an up change
            oldRemaining.ForEach(o => state.InputValues.Add(new GameInputValue(
                false, true, InputChangeType.Up,
                new GameTimeSpan(o.TimeSinceDown.TotalMilliseconds + timeChange.TotalMilliseconds),
                o.Type, o.Position, o.KeyValue
                )));

            _lastState = state;
            return state;
        }

        private double GetDistanceSq(IGamePoint gamePoint1, IGamePoint gamePoint2)
        {
            throw new NotImplementedException();
        }

        private GamePoint GetGamePosition(ScreenPoint screenPoint)
        {
            throw new NotImplementedException();
        }


    }
}
