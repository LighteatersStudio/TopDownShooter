using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine;
using Zenject;

namespace Gameplay.DialogueUnits
{
    public class ConversationHandler : MonoBehaviour
    {
        [SerializeField] private DialogueSystemTrigger _dialogueSystemTrigger;
        
        [Inject]
        public void Construct(IPlayer player)
        {
            _dialogueSystemTrigger.conversationActor = player.DialogueActor;
        }
    }
}