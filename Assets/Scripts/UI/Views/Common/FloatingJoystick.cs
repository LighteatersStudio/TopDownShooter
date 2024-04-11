using UnityEngine;

namespace UI.Views.Common
{
    public class FloatingJoystick : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _knob;
        
        public void SetJoystickPosition(Vector2 position, bool isActive)
        {
            _rectTransform.gameObject.SetActive(isActive);
            _rectTransform.position = position;
        }
        
        public void SetKnobPosition(Vector2 position)
        {
            _knob.localPosition = position;
        }
    }
}
