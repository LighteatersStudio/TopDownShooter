using UnityEngine;

namespace UI
{
    public class StartSplashScreen : PopupBase
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