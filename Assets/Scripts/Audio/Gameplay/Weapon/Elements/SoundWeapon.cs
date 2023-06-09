namespace Audio.Gameplay.Weapon.Elements
{
    public class SoundWeapon : SoundWeaponElement
    {
        protected void Start()
        {
            Play(Sounds.Replacement);
        }
    }
}