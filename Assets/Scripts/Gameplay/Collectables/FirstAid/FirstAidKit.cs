using System;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.FirstAid
{
    [RequireComponent(typeof(RotateCollectables))]
    public class FirstAidKit : MonoBehaviour, ITicker
    {
        [SerializeField] private float _hpUpAmount;
        public event Action<float> Tick;

        private FirstAidKitSettings _firstAidKitSettings;
        private Cooldown.Factory _cooldownFactory;
        private Cooldown _destroyCooldown;

        [Inject]
        public void Construct(Vector3 newPosition, FirstAidKitSettings firstAidKitSettings, Cooldown.Factory cooldownFactory)
        {
            _cooldownFactory = cooldownFactory;
            _firstAidKitSettings = firstAidKitSettings;
            transform.position = newPosition;

            _destroyCooldown = _cooldownFactory.CreateFinished();
        }

        private void Start()
        {
            _destroyCooldown = _cooldownFactory.Create(_firstAidKitSettings.LifeTime, this, SelfDestroy);
            _destroyCooldown.Launch();
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }

        public void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<Player>();

            if (target == null)
            {
                return;
            }

            if (IsPlayerFullHp(target))
            {
                return;
            }

            var character = target.GetComponentInChildren<Character>();
            character.RecoverHealth(_firstAidKitSettings.HpUpAmount);

            GetComponent<RotateCollectables>().StopRotation();

            SelfDestroy();
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private bool IsPlayerFullHp(Player player)
        {
            return player.Health.HealthRelative.Equals(1f);
        }

        public class Factory : PlaceholderFactory<Vector3, FirstAidKit>
        {
        }
    }
}