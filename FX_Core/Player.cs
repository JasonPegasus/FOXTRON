using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    internal class Player
    {
        Scanner scanner;
        public Player(Scanner scann) 
        {
            scanner = scann; 
            SetValues();

            Task.Run(() => { while (true) {Update(); Thread.Sleep(16); }} );
        }

        public void SetValues()
        {
            ptr_camX = scanner.FindCameraX()[0];
        }

        // VALUES //

        IntPtr ptr_camX;
        float camX 
        {
            get { return scanner.GetFloat(ptr_camX); }
            set { scanner.SetFloat(ptr_camX, value); }
        }

        // WORKFLOW //

        public bool EnableInput = true;
        private void Update()
        {
            if (!EnableInput) return;
            int delta = (InputManager.IsKeyDown(ConsoleKey.I) ? 1 : 0) + (InputManager.IsKeyDown(ConsoleKey.O) ? -1 : 0);
            camX += 10*delta;
        }
    }
}
