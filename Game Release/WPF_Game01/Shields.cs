using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WPFStartupDemo
{
    class Shields : GameObject
    {

        static BitmapImage bMap = null;

        public static List<Shields> list = new List<Shields>();




        public Shields()
        {

            UseImage("shield.png", bMap); 

            double scale = G.randD(30.0, 30.0);

            Scale = scale / 100.0;

            Y = -100.0;
            X = G.randD(0.0, G.gameWidth);

            dX = G.randD(-1.0, 1.0); ;
            dY = 2.0;

            canvas = G.canvas;

            G.gameEngine.addToDisplayList(this);
            Shields.list.Add(this);
        }


        double gravity = 0.0; //0.1
        double friction = 1.0; //0.99999

        public override void update()
        {
            dY += gravity;

            //if (Y > G.gameHeight - element.Height) dY = -Math.Abs(dY * 0.99);

            //if (X > G.gameWidth - element.Width) dX = -Math.Abs(dX);
            if (X < 0) dX = Math.Abs(dX);
            dX *= friction;
            dY *= friction;
            X += dX;
            Y += dY;

        }

    }
}
