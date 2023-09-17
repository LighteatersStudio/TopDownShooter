using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class OutlinePlaceholder : MonoBehaviour
    {
        private Outline3D _outline3D;
        private IFriendOrFoeTag _selfTag;
        private FriendOrFoeFactory _factory;
        private IFriendFoeSystem _friendFoeSystem;
        private IOutlineSettings _settings;


        [Inject]
        public void Construct(IFriendOrFoeTag friendOrFoeTag, FriendOrFoeFactory friendOrFoeFactory, IFriendFoeSystem friendFoeSystem, IOutlineSettings settings)
        {
            _selfTag = friendOrFoeTag;
            _factory = friendOrFoeFactory;
            _friendFoeSystem = friendFoeSystem;
            _settings = settings;
        }

        private void Start()
        {
            _outline3D = gameObject.AddComponent<Outline3D>();
            
            SetColor();
        }

        private void SetColor()
        {
            var playerTag = _factory.CreatePlayerTeam();

            if (_friendFoeSystem.CheckFriend(_selfTag, playerTag))
            {
                _outline3D.OutlineColor = _settings.Player;
            }
            else if (_friendFoeSystem.CheckFoes(_selfTag, playerTag))
            {
                _outline3D.OutlineColor = _settings.Enemy;
            }
            else
            {
                _outline3D.OutlineColor = _settings.Neutral;
            }
        }
    }
}