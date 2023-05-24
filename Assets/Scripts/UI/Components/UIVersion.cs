using Services.AppVersion;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIVersion : MonoBehaviour
    {
        [SerializeField] private TMP_Text _versionText;
        private IApplicationVersion _applicationVersion;
        
        [Inject]
        public void Construct(IApplicationVersion applicationVersion)
        {
            _applicationVersion = applicationVersion;
        }
        
        private void Start()
        {
            _versionText.text = $"v{_applicationVersion.Version} ({_applicationVersion.BuildNumber})";
        }
    }
}