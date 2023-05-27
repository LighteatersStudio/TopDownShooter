using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "LightEaters/UI/Create MainMenuBackgrounds", fileName = "MainMenuBackgrounds", order = 0)]
    public class MainMenuBackgrounds : ScriptableObject
    {
        
        [SerializeField] private List<Sprite> _sprites;

        public Sprite GetRandom()
        {
            if (!_sprites.Any())
            {
                Debug.LogError("MainMenuBackgrounds does not contain any pictures");
                return null;
            }
            
            return _sprites[Random.Range(0, _sprites.Count)];
        }
    }
}