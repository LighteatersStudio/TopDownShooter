using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class FriendOrFoeComponent : MonoBehaviour, IFriendOrFoeTag
    {
        private IFriendOrFoeTag _tag;
        public string TeamTag => _tag.TeamTag;

        [Inject]
        public void Construct(IFriendOrFoeTag friendOrFoeTag)
        {
            _tag = friendOrFoeTag;
        }
    }
}