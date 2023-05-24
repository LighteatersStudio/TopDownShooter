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
    public class OffsetVector2Processor : InputProcessor<Vector2>
    {

#if UNITY_EDITOR
        static OffsetVector2Processor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            InputSystem.RegisterProcessor<OffsetVector2Processor>();
        }

        [Tooltip("Vector to add to incoming values.")]
        public float X = 0;
        public float Y = 0;

        public override Vector2 Process(Vector2 value, InputControl control)
        {
            return new Vector2(value.x + X, value.y + Y);
        }
    }
}