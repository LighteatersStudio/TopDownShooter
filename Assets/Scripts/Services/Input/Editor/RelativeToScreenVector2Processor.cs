using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class RelativeToScreenVector2Processor : InputProcessor<Vector2>
    {

#if UNITY_EDITOR
        static RelativeToScreenVector2Processor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()    
        {
            InputSystem.RegisterProcessor<RelativeToScreenVector2Processor>();
        }

        public override Vector2 Process(Vector2 value, InputControl control)
        {
            return new Vector2(value.x / Screen.width, value.y / Screen.height);
        }
    }
}