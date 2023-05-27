using UnityEngine;

namespace Services.Application.Description
{
    [CreateAssetMenu(menuName = "LightEaters/Application/Create ApplicationDescription", fileName = "ApplicationDescription", order = 0)]
    public class ApplicationDescription : ScriptableObject, IApplicationDescription
    {
        [SerializeField] private string _name;

        public string Name => _name;
    }
}