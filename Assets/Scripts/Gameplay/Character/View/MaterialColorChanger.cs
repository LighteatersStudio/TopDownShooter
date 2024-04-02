using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class MaterialColorChanger
    {
        private const string ColorProperty = "_BaseColor";

        private readonly List<Material> _materials = new List<Material>();

        public MaterialColorChanger(GameObject view)
        {
            foreach (var material in view.GetComponent<SkinnedMeshRenderer>().materials)
            {
                _materials.Add(new Material(material));
            }
            
            view.GetComponent<SkinnedMeshRenderer>().sharedMaterials = _materials.ToArray();
        }

        public void ChangeColor(Color targetColor, float duration)
        {
            foreach (var material in _materials)
            {
                material.DOKill();
                material.DOColor(targetColor, ColorProperty, duration).OnComplete(() => ResetColor(material, duration));
            }
        }

        private void ResetColor(Material material, float duration)
        {
            material.DOColor(Color.white, ColorProperty, duration);
        }
    }
}