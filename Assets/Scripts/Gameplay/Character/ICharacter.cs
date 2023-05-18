using System;

namespace Gameplay
{
    public interface ICharacter
    {
        event Action Damaged;
        event Action Attacked;
        event Action Dead;
        
        CharacterModelRoots ModelRoots { get; }
    }
}