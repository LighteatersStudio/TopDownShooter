using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UIAudioBuilder
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly IUISounds _uiSounds;

        [Inject]
        public UIAudioBuilder(IAudioPlayer audioPlayer, IUISounds uiSounds)
        {
            _audioPlayer = audioPlayer;
            _uiSounds = uiSounds;
        }
        
        public void FillSounds<TView>(TView view) where TView : View
        {
            FindAllElementsAndAddSound<Button, SoundButton>(view.gameObject);
            FindAllElementsAndAddSound<Toggle, SoundToggle>(view.gameObject);
            FindAllElementsAndAddSound<PopupBase, SoundPopup>(view.gameObject);
        }

        private void FindAllElementsAndAddSound<TElement, TSound>(GameObject root)
            where TElement : MonoBehaviour
            where TSound : SoundUIElement
        {
            var elements = new List<TElement> {root.GetComponent<TElement>()};
            elements.AddRange(root.GetComponentsInChildren<TElement>());
            foreach (var element in elements)
            {
                if (!element)
                {
                    continue;
                }

                var sound = element.gameObject.GetComponent<TSound>();
                if (!sound)
                {
                    sound = element.gameObject.AddComponent<TSound>();
                }

                sound.Construct(_audioPlayer, _uiSounds);
            }
        }
    }
}