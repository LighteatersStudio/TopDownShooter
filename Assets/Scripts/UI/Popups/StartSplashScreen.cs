using UnityEngine;

namespace UI
{
    public class StartSplashScreen : View
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