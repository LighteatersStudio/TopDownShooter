using UnityEngine;

namespace Gameplay
{
    public class BloodShading : MonoBehaviour
    {
        private static readonly int SliceAmount = Shader.PropertyToID("_SliceAmount");
        
        [SerializeField] private float _bloodSpeed = 0.005f;
        [SerializeField] private float _moveSpeed = 0;
        [SerializeField] private float _speed = 4;
        [SerializeField] private float _startBloodValue = 0.2f;
        [SerializeField] private Vector3 _scaleVector;
        [SerializeField] private Vector3 _scaleSpeed;
        [SerializeField] private float _scaleModifier = 1f;

        private MeshRenderer _meshRenderer;
        private Material _material;
        private Vector3 _startPosition;
        private Vector3 _startScale;
        private float _currentBloodValue;

        private void Start()
        {
            _startPosition = transform.localPosition;
            _startScale = transform.localScale;
            _meshRenderer = GetComponentInChildren<MeshRenderer>();

            if (!_meshRenderer)
            {
                return;
            }
            
            _material = _meshRenderer.material;
            _currentBloodValue = _startBloodValue;
        }

        private void Update()
        {
            if (!_material)
            {
                return;
            }

            UpdateBloodValue();
            Movement();
            Scale();

            //Test
            if (Input.GetKeyDown(KeyCode.J))
            {
                ResetToStart();
            }
        }

        private void UpdateBloodValue()
        {
            if (_currentBloodValue < 1f)
            {
                var step = _bloodSpeed * _speed;

                _currentBloodValue = Mathf.Min(_currentBloodValue + step, 1f);
                _material.SetFloat(SliceAmount, _currentBloodValue);
            }  
        }

        private void Movement()
        {
            if (_moveSpeed != 0 && _currentBloodValue < 1f)
            {
                transform.localPosition +=  Vector3.forward * (_moveSpeed * Time.deltaTime);
            } 
        }

        private void Scale()
        {
            if (_currentBloodValue < 1f)
            {
                Vector3 deltaScale = Vector3.Scale(_scaleVector, _scaleSpeed) * _scaleModifier;
                transform.localScale += deltaScale;
            }
        }

        private void ResetToStart()
        {
            transform.localPosition = _startPosition;
            _currentBloodValue = _startBloodValue;
            transform.localScale = _startScale;

            if (_material != null)
            {
                _material.SetFloat(SliceAmount, _currentBloodValue);
            }
        }
    }
}