﻿using System;
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
    class Explosion : GameObject
    {

        static BitmapImage b = null;


        public static List<Explosion> list = new List<Explosion>();


        public Explosion()
        {

            UseImage("explosion.png",b);

            makeInactive();

            Scale = 1.0;
            Explosion.list.Add(this);

            AddToGame();

        }


        public static Explosion getNextAvailable()
        {
            foreach (Explosion obj in list)
            {
                if (!obj.isActive)
                {
                    return obj;
                }
            }
            return null;
        }

        

       


        int frameCount = 0;

        public int exploLength = 8;

        public override void update()
        {
            if (isActive)
            {
                frameCount++;
                Scale = 1.15 * Scale;
                Angle = G.randAngle();
                Particle.startExploParticles(10, X, Y);
                if (frameCount >= exploLength)
                {
                    exploLength = 8;
                    Scale = 1.0;
                    frameCount = 0;
                    makeInactive();
                }

            }
        }

    }
}
