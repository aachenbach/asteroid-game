using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFStartupDemo
{
    class BigGameText : GameText
    {
        

        public BigGameText():base()
        {
                        
            FontFamily = "Arial Black";
            Height = 500.0;
            Width = 1000.0;
            TextAlignment = TextAlignment.Center;
            Text = "Asteroid Navigator";
            FontSize = 80;

            Y = G.gameHeight * 0.125;
            X = G.gameWidth / 2.0;

            dX = 0.0;
            dY = 0.0;

            Scale = 0.5;
            ((TextBlock)(element)).Foreground = new SolidColorBrush(G.randLightColor());

            AddToGame();
        }


        public bool countDown = false;

        public int count = 0;

        

        int frameCount = 0;
        int frameCountReset = 60;
        public override void update()
        {
            if (isActive)
            {
                if (countDown)
                {
                    frameCount++;
                    Scale *= 1.005;
                    Y += 1.8;
                    if (frameCount > frameCountReset)
                    {
                        frameCountReset *= 6;
                        frameCountReset /= 7;
                        count--;
                        
                        FontColor = G.randLightColor();
                        Text = count.ToString();
                        frameCount = 0;
                    }
                }
                if ((Text == "Game Over!\nEsc to play again")  && (Scale < 2.0))
                {
                    Scale *= 1.01;
                    if(Scale>=2.0)
                    {
                        
                    }
                    Y += 3.2;
                }
            }
        }

        

    }
}
