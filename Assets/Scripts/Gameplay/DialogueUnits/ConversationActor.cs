using UnityEngine;

namespace Gameplay.DialogueUnits
{
    public class ConversationActor : IConversationActor
    {
        public Transform ActorTransform { get; }
        
        public ConversationActor(Transform transform)
        {
            ActorTransform = transform;
        }
    }
}