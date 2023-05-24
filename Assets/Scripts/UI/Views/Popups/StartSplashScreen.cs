using UnityEngine;

namespace UI
{
    public class StartSplashScreen : Popup
    {
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Close();
            }
        }
    }
}