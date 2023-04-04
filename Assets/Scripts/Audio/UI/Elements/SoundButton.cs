using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class SoundButton: SoundUIElement
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
    }
}