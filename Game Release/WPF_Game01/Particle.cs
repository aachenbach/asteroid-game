using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFStartupDemo
{
    class Particle : GameObject
    {


        public static List<Particle> list = new List<Particle>();

        public Particle()
        {


            Ellipse baseElement = new Ellipse();
            int loColor = 128;
            Brush b = new SolidColorBrush(Color.FromArgb((byte)255, (byte)G.randI(loColor, 256), (byte)G.randI(loColor, 256), (byte)G.randI(loColor, 256)));
            baseElement.Fill = b;
            
            baseElement.Height = 4;
            baseElement.Width = 4;


            element = baseElement;

            makeInactive();

            Particle.list.Add(this);

            AddToGame();

        }


        public static Particle getNextAvailable()
        {
            foreach (Particle gObj in list)
            {
                if(!gObj.isActive)
                {
                    return gObj;
                }
            }
            return null;
        }


        double initialSpeed = 15.0;

        public void exploInitializeSpeed()
        {
            double r = G.randD(0.0, initialSpeed);
            double a = G.randD(0.0, Math.PI * 2.0);
            dX = Math.Cos(a) * r;
            dY = Math.Sin(a) * r;
        }     


        static public void startExploParticles(int num, double x, double y)
        {
            for (int i = 0; i < num; i++)
            {
                Particle p = getNextAvailable();
                if (p != null)
                {

                    p.X = x;
                    p.Y = y;
                    p.frameCount = 15;
                    p.exploInitializeSpeed();
                }
            }
        }


        double gravity = 0.1;
        double friction = 0.999;


        int frameCount = 0;

        public override void update()
        {
            if (isActive)
            {
                frameCount--;
                dY += gravity;
                if (Y < 0.0) makeInactive();
                else if (Y > G.gameHeight) makeInactive();
                else if (X < 0.0) makeInactive();
                else if (X > G.gameWidth) makeInactive();

                dX *= friction;
                dY *= friction;

                X += dX;
                Y += dY;
                if (frameCount <= 0)
                {
                    makeInactive();
                }
            }
        }



    }
}
