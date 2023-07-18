using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class OutlinePlaceholder : MonoBehaviour
    {
        private readonly Color _green = new Color(0.42f, 1, 0); 
        private readonly Color _red = new Color(1f, 0.1f, 0);
        
        private Outline3D _outline3D;
        private TypeGameplayObject _typeGameplayObject;


        [Inject]
        public void Construct(TypeGameplayObject typeGameplayObject)
        {
            _typeGameplayObject = typeGameplayObject;
        }

        private void Start()
        {
            _outline3D = gameObject.AddComponent<Outline3D>();

            if (_typeGameplayObject == TypeGameplayObject.Player)
            {
                _outline3D.OutlineColor = _green;
            }
            
            if (_typeGameplayObject == TypeGameplayObject.Enemy)
            {
                _outline3D.OutlineColor = _red;
            } 
        }
    }
}