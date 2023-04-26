using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.Controls
{
    public class ControlsManager : MonoBehaviour
    {
        private InputActionAsset _inputActionAsset;
    
        [Inject]
        public void Construct(InputActionAsset inputActionAsset)
        {
            Debug.Log("Controls manager construct");
        
            _inputActionAsset = inputActionAsset;
        }


        private void OnEnable()
        {
            if (_inputActionAsset == null)
            {
                return;
            }

            _inputActionAsset.FindAction("Move").performed += ctx => Debug.Log("Move action performed");
            _inputActionAsset.FindAction("Fire").performed += ctx => Debug.Log("Fire action performed");
            _inputActionAsset.FindAction("Special").performed += ctx => Debug.Log("Special action performed");
            _inputActionAsset.FindAction("Use").performed += ctx => Debug.Log("Use action performed");
            _inputActionAsset.FindAction("Reload").performed += ctx => Debug.Log("Reload action performed");
            _inputActionAsset.FindAction("Melee").performed += ctx => Debug.Log("Melee action performed");
        }

        private void OnDisable()
        {
            if (_inputActionAsset == null)
            {
                return;
            }

            _inputActionAsset.FindAction("Move").performed -= ctx => Debug.Log("Move action performed");
            _inputActionAsset.FindAction("Fire").performed -= ctx => Debug.Log("Fire action performed");
            _inputActionAsset.FindAction("Special").performed -= ctx => Debug.Log("Special action performed");
            _inputActionAsset.FindAction("Use").performed -= ctx => Debug.Log("Use action performed");
            _inputActionAsset.FindAction("Reload").performed -= ctx => Debug.Log("Reload action performed");
            _inputActionAsset.FindAction("Melee").performed -= ctx => Debug.Log("Melee action performed");
        }
    }
}
