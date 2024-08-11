using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Gameplay
{
    public class StartPoint : MonoBehaviour
    {
        private const float MaxDistance = 5f;

        private IPlayer _player;

        [Inject]
        public void Construct(IPlayer player)
        {
            _player = player;
        }

        private void Start()
        {
            if (!NavMesh.SamplePosition(transform.position, out var hit, MaxDistance, NavMesh.AllAreas))
            {
                Debug.LogError("Start Point: nearest NavMesh point didn't find");
                hit = new NavMeshHit()
                {
                    position = transform.position
                };
            }

            _player.SetPosition(hit.position);
        }
    }
}