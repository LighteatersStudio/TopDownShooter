using UnityEngine;
using UnityEngine.UI;

namespace Services.Audio
{
    [RequireComponent(typeof(Toggle))]
    public class SoundToggle : SoundUIElement
    {
        protected void OnEnable()
        {
            GetComponent<Toggle>().onValueChanged.AddListener(OnToggle);
        }
        protected void OnDisable()
        {
            GetComponent<Toggle>().onValueChanged.RemoveListener(OnToggle);
        }

        
        private void OnToggle(bool isToggle)
        {
            Play(Sounds.ToggleClick);
        }
    }
}