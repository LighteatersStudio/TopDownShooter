using System;
using System.Collections.Generic;

namespace UI.Framework.Implementation
{
    internal class WindowSystemOrderObserver
    {
        private readonly List<IWindowsSystemController> _systems = new(6);
        private readonly Stack<IWindowsSystemController> _stack = new();
        private IWindowsSystemController _activeSystem;

        public void Register(IWindowsSystemController system)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            _systems.Add(system);
            system.ViewOpen += OnViewOpened;
            system.ViewClosed += OnViewClosed;
        }

        private void OnViewOpened(IWindowsSystemController newSystem)
        {
            if (_activeSystem == null)
            {
                _activeSystem = newSystem;
                return;
            }

            if (_activeSystem == newSystem)
                return;

            if (newSystem.WindowsSystemOrder > _activeSystem.WindowsSystemOrder)
            {
                _stack.Push(_activeSystem);
                _activeSystem.SetActive(false);

                _activeSystem = newSystem;
            }
        }

        private void OnViewClosed(IWindowsSystemController closedSystem)
        {
            if (closedSystem != _activeSystem)
            {
                RemoveFromStack(closedSystem);
                return;
            }

            if (_stack.Count > 0)
            {
                var previous = _stack.Pop();
                previous.SetActive(true);
                _activeSystem = previous;
            }
            else
            {
                _activeSystem = null;
            }
        }

        private void RemoveFromStack(IWindowsSystemController system)
        {
            if (_stack.Count == 0) return;

            var temp = new Stack<IWindowsSystemController>();

            while (_stack.Count > 0)
            {
                var top = _stack.Pop();
                if (top != system)
                    temp.Push(top);
            }

            while (temp.Count > 0)
                _stack.Push(temp.Pop());
        }

        public void UnregisterAll()
        {
            foreach (var system in _systems)
            {
                system.ViewOpen -= OnViewOpened;
                system.ViewClosed -= OnViewClosed;
            }

            _systems.Clear();
            _stack.Clear();
            _activeSystem = null;
        }
    }
}