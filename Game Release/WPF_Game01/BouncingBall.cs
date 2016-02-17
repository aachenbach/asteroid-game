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
    class BouncingBall : GameObject
    {

        static BitmapImage bMap = null;

        public static List<BouncingBall> list = new List<BouncingBall>();

        public static int ballCount = 0;

        public BouncingBall()
        {

            UseImage("asteroid.png", bMap);

            double scale = G.randD(10.0, 75.0);

            Scale = scale / 100.0;
            //Ellipse baseElement = new Ellipse();
            //baseElement.Fill = new SolidColorBrush(G.randColor());
            //baseElement.Height = G.randD(scale, scale);
            //baseElement.Width = G.randD(scale, scale);

            //element = baseElement;

            Y = -100.0;
            X = G.randD(0.0, G.gameWidth);

            dX = G.randD(-1.0, 1.0); ;
            dY = 5.0;

            canvas = G.canvas;

            G.gameEngine.addToDisplayList(this);
            BouncingBall.list.Add(this);
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
