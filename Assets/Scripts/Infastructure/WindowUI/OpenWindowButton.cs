using Infastructure.Services.Window;
using Infastructure.StaticData.Window;
using UnityEngine;
using UnityEngine.UI;

namespace Infastructure.WindowUI
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public WindowId WindowId;
        
        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            Button.onClick.AddListener(Open);

        private void OnDestroy() => 
            Button.onClick.RemoveListener(Open);

        private void Open() => 
            _windowService.Open(WindowId);
    }
}