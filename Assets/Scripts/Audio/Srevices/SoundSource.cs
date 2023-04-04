using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        public AudioSource Source => _source;

        protected void Awake()
        {
            if (!_source)
            {
                _source = GetComponent<AudioSource>();
            }
        }
    }
}