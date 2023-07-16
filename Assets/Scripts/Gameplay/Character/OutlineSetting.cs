using System;
using UnityEngine;

namespace Gameplay
{
    public class OutlineSetting : MonoBehaviour
    {
        private readonly Color _green = new Color(0.42f, 1, 0); 
        private readonly Color _red = new Color(1f, 0.1f, 0);
        
        private Outline3D _outline3D;


        public void ChangeOutlineColor(TypeGameplayObject type)
        {
            _outline3D = gameObject.AddComponent<Outline3D>();

            if (type == TypeGameplayObject.Player)
            {
                _outline3D.OutlineColor = _green;
            }
            
            if (type == TypeGameplayObject.Enemy)
            {
                _outline3D.OutlineColor = _red;
            }
        }
    }
}