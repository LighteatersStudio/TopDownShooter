# Character Example
- [Character](Character_en.md)
- [Back to Main README](../README.md)

These examples demonstrate how to integrate pure C# classes and MonoBehaviour components into the Character entity while adhering to the project's architectural approach. By using Zenject, dependencies are injected dynamically, ensuring a modular and scalable structure. This approach allows Character to remain independent while still enabling the integration of additional functionalities, such as debugging tools or AI behaviors, without modifying its core logic.

#### All examples presented in this document can be found in the corresponding version of the project in this branch: [content/characterDemo Branch](https://github.com/LighteatersStudio/TopDownShooter/tree/content/characterDemo)

## Adding a Pure C# Class to the Character Container

```csharp
Container.Bind(typeof(TimeToDebug), typeof(ITickable))
    .To<TimeToDebug>()
    .AsSingle()
    .NonLazy();
```

### **Explanation**

- **`TimeToDebug`** is a pure C# class that implements `ITickable`, allowing it to execute code every frame via **Zenject's ticking system**.
- It does not extend `MonoBehaviour`, meaning it does not need to be attached to a GameObject.

#### **Example: TimeToDebug Class**

```csharp
using Zenject;
using UnityEngine;
using System;

namespace Gameplay.Demo
{
    public class TimeToDebug : ITickable
    {
        private float _timeAccumulator = 0f;
        private float _secondsPassed = 0f;

        public void Tick()
        {
            _timeAccumulator += Time.deltaTime;

            if (_timeAccumulator >= 1.0f)
            {
                _secondsPassed += _timeAccumulator;
                Debug.Log($"Time passed: {Math.Round(_secondsPassed, 2)} seconds");
                _timeAccumulator -= 1.0f;
            }
        }
    }
}
```

---

## Adding a MonoBehaviour Component to the Enemy Entity

### **Example: TimeToDebugMono Class**

To attach a `MonoBehaviour` to an **Enemy**, we need to bind it in the **EnemyInstaller**.

#### **Example: TimeToDebugMono Class**

```csharp
using UnityEngine;
using System;
using Zenject;

namespace Gameplay.Demo
{
    public class TimeToDebugMono : MonoBehaviour
    {
        private IHaveHealth _health;
        private float _timeAccumulator = 0f;
        private float _secondsPassed = 0f;
        private DateTime _startTimePoint;

        [Inject]
        public void Construct(DateTime startTimePoint, IHaveHealth health)
        {
            _startTimePoint = startTimePoint;
            _health = health;
            Debug.Log($"Starting point: {_startTimePoint}");
        }

        private void Update()
        {
            _timeAccumulator += Time.deltaTime;

            if (_timeAccumulator >= 1.0f)
            {
                _secondsPassed += _timeAccumulator;
                Debug.Log($"Time passed: {Math.Round(_secondsPassed, 2)} seconds since {_startTimePoint}");
                Debug.Log($"Health {_health.HealthRelative}");
                _timeAccumulator -= 1.0f;
            }
        }
    }
}
```





### **Additional Binding in EnemyInstaller**

For demonstration purposes, **EnemyInstaller** includes the following binding:

```csharp
private void BindDemo()
{
    Container.Bind<DateTime>()
        .FromInstance(DateTime.Now);
}
```

This ensures that a `DateTime` instance is available as a dependency, which is used in `TimeToDebugMono` for logging the elapsed time.



Explanation

- **TimeToDebugMono** is a `MonoBehaviour`, meaning it **must** be attached to a GameObject.
- The GameObject with the `TimeToDebugMono` component is placed in a **prefab variant** of `Character`.
- It injects **DateTime** and **IHaveHealth** dependencies at runtime.