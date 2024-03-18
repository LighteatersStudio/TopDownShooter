using UnityEngine;

namespace UI.Views
{
    public class FloatingJoystick : MonoBehaviour
    {
        [SerializeField] private RectTransform _knob;

        public RectTransform Knob => _knob;
    }
}
