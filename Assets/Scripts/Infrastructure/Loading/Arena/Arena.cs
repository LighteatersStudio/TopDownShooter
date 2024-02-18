using System;
using System.Collections;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Infrastructure.Loading
{
    [Serializable]
    public class Arena : IArena
    {
        // [ValueDropdown("SelectScene", DropdownTitle = "Scene Selection")]
        // public string SceneName { get; set; }

        // private static IEnumerable SelectScene()
        // {
        //     var filesPath = Directory.GetFiles("Assets/Scenes/Levels");
        //     var fileNameList = filesPath
        //         .Select(Path.GetFileName)
        //         .Select(file => file.Split(".")[0])
        //         .Distinct()
        //         .ToList();
        //
        //     return fileNameList;
        // }

        [SerializeField] private string _sceneName;
        public string SceneName => _sceneName;
    }

    public interface IArena
    {
        string SceneName { get; }
    }
}