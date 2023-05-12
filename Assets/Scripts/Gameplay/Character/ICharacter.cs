using System;

namespace Gameplay
{
    public interface ICharacter
    {
        event Action Damaged;
        event Action Dead;
        
        CharacterModelRoots ModelRoots { get; }
    }
}