using UnityEngine;

namespace Services.Audio
{
    [CreateAssetMenu(menuName = "LightEaters/Audio/Create MusicList", fileName = "MusicList", order = 0)]
    public class MusicList : ScriptableObject, IMusicList
    {
        [SerializeField] private AudioClip[] _musicList;
        
        public MusicTrack GetRandomTrack()
        {
            var index = Random.Range(0, _musicList.Length);
            return new MusicTrack(_musicList[index], 0.6f);
        }
    }
}