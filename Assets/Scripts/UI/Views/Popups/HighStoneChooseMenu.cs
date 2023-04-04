using UnityEngine;

namespace UI
{
    public class HighStoneChooseMenu : PopupBase
    {
        public void ActivateHighMode()
        {
            Debug.Log("High");
            Close();
        }

        public void ActivateStoneMode()
        {
            Debug.Log("Stone");
            Close();
        }
    }
}