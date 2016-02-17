using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFStartupDemo
{
    class Background1 : GameObject
    {

        static BitmapImage bMap = null;

        public static List<Explosion> list = new List<Explosion>();


        public Background1()
        {

            UseImage("blue-space.jpg", bMap);

            X = G.gameWidth / 2.0;
            Y = G.gameHeight / 2.0;

 
            Scale = 1.50;
            AddToGame();

        }


        public override void update()
        {
            if (isActive)
            {
                Angle += 0.009;
            }
        }

    }
}
