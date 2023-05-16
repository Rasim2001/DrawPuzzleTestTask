using System;
using UnityEngine;

namespace Infastructure.StaticData.Window
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public GameObject Prefab;
    }
}