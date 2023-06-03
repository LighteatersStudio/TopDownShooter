using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class SoundButton: SoundUIElement, IPointerEnterHandler
    {
        protected void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        protected void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClick);
        }
        
        private void OnClick()
        {
            Play(Sounds.ButtonClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Play(Sounds.ButtonHover);
        }
    }
}