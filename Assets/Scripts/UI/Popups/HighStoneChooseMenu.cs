using UnityEngine;

namespace UI
{
    public class HighStoneChooseMenu: View
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