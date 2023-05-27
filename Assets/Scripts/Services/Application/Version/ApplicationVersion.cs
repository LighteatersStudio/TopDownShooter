using UnityEngine;

namespace Services.Application.Version.Implementation
{
    [CreateAssetMenu(menuName = "LightEaters/Application/Version", fileName = "ApplicationVersion", order = 0)]
    public class ApplicationVersion : ScriptableObject, IApplicationVersion
    {
        [SerializeField] private string _version;
        [SerializeField] private string _buildNumber;
        
        public string Version => _version;
        public string BuildNumber => _buildNumber;
    }
}