using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CharacterColorFeedback
    {
        private const string ColorProperty = "_BaseColor";

        private readonly List<Material> _materials = new List<Material>();
        private readonly CharacterMaterialColorChangeData _colorChangeData;

        public CharacterColorFeedback(GameObject view, CharacterMaterialColorChangeData colorChangeData)
        {
            _colorChangeData = colorChangeData;
            
            foreach (var material in view.GetComponent<SkinnedMeshRenderer>().materials)
            {
                _materials.Add(new Material(material));
            }
            
            view.GetComponent<SkinnedMeshRenderer>().sharedMaterials = _materials.ToArray();
        }

        public void ChangeColor()
        {
            foreach (var material in _materials)
            {
                material.DOKill();
                material.DOColor(_colorChangeData.Color, ColorProperty, _colorChangeData.Duration)
                    .OnComplete(() => ResetColor(material, _colorChangeData.Duration));
            }
        }

        private void ResetColor(Material material, float duration)
        {
            material.DOColor(Color.white, ColorProperty, duration);
        }
        
        public class Factory : PlaceholderFactory<GameObject, CharacterColorFeedback>
        {
        }
    }
}