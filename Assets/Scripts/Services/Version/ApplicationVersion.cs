using UnityEngine;

namespace Services.AppVersion
{
    [CreateAssetMenu(menuName = "LightEaters/Version", fileName = "ApplicationVersion", order = 0)]
    public class ApplicationVersion : ScriptableObject, IApplicationVersion
    {
        [SerializeField] private string _version;
        [SerializeField] private string _buildNumber;
        
        public string Version => _version;
        public string BuildNumber => _buildNumber;
    }
}