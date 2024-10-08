using System;

namespace Gameplay.Services.Input
{
    public class InputLocker : IInputLocker
    {
        private readonly Action _lockCallback;
        private readonly Action _unlockCallback;

        public InputLocker(Action lockCallback, Action unlockCallback)
        {
            _lockCallback = lockCallback;
            _unlockCallback = unlockCallback;
            _lockCallback.Invoke();
        }

        public void Dispose()
        {
            _unlockCallback.Invoke();
        }
    }
}