#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Services.Input
{

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class LeftHalfTouchProcessor : InputProcessor<Vector2>
    {
#if UNITY_EDITOR
        static LeftHalfTouchProcessor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            InputSystem.RegisterProcessor<LeftHalfTouchProcessor>();
        }

        public override Vector2 Process(Vector2 value, InputControl control)
        {
            return value;
        }
    }
}