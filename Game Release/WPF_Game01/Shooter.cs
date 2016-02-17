using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFStartupDemo
{
    class Shooter : GameObject
    {

        static BitmapImage bMap = null;

        
        static MediaPlayer bigExploSound = null;
        static MediaPlayer powerupSound = null;

        public Shooter()
        {
            
            G.SetupSound(ref bigExploSound, "player_explode.mp3");
            G.SetupSound(ref powerupSound, "shooting_star.mp3");

             UseImage("spaceship.png",bMap);

             Scale = 0.3;

            
            SetInMiddle();

            dX = 0.0;
            dY = 0.0;
            

            AddToGame();

        }

     
        

        public void SetInMiddle()
        {
            X = G.gameWidth / 2.0;
            Y = G.gameHeight / 2.0;
        }



        double friction = 0.99;
        double acceleration = 0.2;

        int frameCount = 0;

        public override void update()
        {
            if (isActive)
            {
                frameCount++;
                if (Y > (G.gameHeight - Height + 200)) dY = -0.2;//-Math.Abs(dY);
                if (X > (G.gameWidth - Width + 100)) dX = -0.2;//-Math.Abs(dX);
                if (X < 0) dX = 0.2;//Math.Abs(dX);
                if (Y < 0) dY = 0.2;//Math.Abs(dY);
                X += dX;
                Y += dY;
                dX *= friction;
                dY *= friction;

                if (dX > 8)
                {
                    dX = 8;
                }
                if (dY > 8)
                {
                    dY = 8;
                }
                
                if (G.isKeyPressed(Key.Left))
                {
                    if (dX > 0.0) dX = 0.0;
                    dX -= acceleration;
                }
                if (G.isKeyPressed(Key.Right))
                {
                    if (dX < 0.0) dX = 0.0;
                    dX += acceleration;
                }
                if (G.isKeyPressed(Key.Up))
                {
                    if (dY > 0.0) dY = 0.0;
                    dY -= acceleration;
                }
                if (G.isKeyPressed(Key.Down))
                {
                    if (dY < 0.0) dY = 0.0;
                    dY += acceleration;
                }
                if(G.isKeyPressed(Key.Space))
                {
                    dY = 0;
                    dX = 0;
                }
                
                
                
                
                foreach (BouncingBall bb in BouncingBall.list)
                {
                    if (G.checkCollision(this, bb))
                    {
                        if (G.gameEngine.shields == 0)
                        {                          
                            bigExploSound.Stop();
                            bigExploSound.Play();
                            Explosion explo = Explosion.getNextAvailable();
                            if (explo != null)
                            {
                                explo.Scale = 1.0;
                                explo.exploLength = 30;
                                explo.X = X;
                                explo.Y = Y;
                            }
                            makeInactive();
                            GameEngine.bigGameText.Text = "Game Over!\nEsc to play again";
                            G.gameEngine.gameNotOver = false;
                            GameEngine.hiScoreText.addNewScore(G.gameEngine.score);
                        }
                        else
                        {
                            bigExploSound.Stop();
                            bigExploSound.Play();
                            bb.makeInactive();
                            G.gameEngine.shields = G.gameEngine.shields - 1;
                        }
                    }
                }
                
                
                foreach (Powerup bb in Powerup.list)
                {
                    if (G.checkCollision(this, bb))
                    {
                        powerupSound.Stop();
                        powerupSound.Play();
                        G.gameEngine.score += 100;
                        bb.makeInactive();                                                                       
                    }
                }

                foreach (Shields bb in Shields.list)
                {
                    if (G.checkCollision(this, bb))
                    {
                        powerupSound.Stop();
                        powerupSound.Play();
                        G.gameEngine.shields++;
                        bb.makeInactive();
                    }
                }
                
            }
        }

    }
}
