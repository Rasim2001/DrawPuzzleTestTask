using System.Collections.Generic;
using UnityEngine;

namespace Infastructure.StaticData.Window
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/Window")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}