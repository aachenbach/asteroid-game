using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;



namespace WPFStartupDemo
{

   

    class GameEngine
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<GameObject> displayList = new List<GameObject>();

        public bool[] keyPressed = new bool[256];


        public static BigGameText bigGameText;
        public static ScoreText scoreText;
        public static BonusText bonusText;
        public static HiScoreText hiScoreText;
        public static RoundText roundText;

        public bool autoShoot = false;
        public bool gameNotOver = true;

        public int threshold = 95; //minimum needed to spawn asteroid out of 100
        public int timerTick_counter = 0;

        public double score = 0.0;
        public double bonus = 0.0;
        public int shields = 2;

        public GameEngine()
        {
            double msPerFrame = 10.0;
            timer.Interval = new TimeSpan(0, 0, 0, 0, (int)msPerFrame);
            
            timer.Start();
            timer.Tick += timer_Tick;
        }

        public void addToDisplayList(GameObject obj)
        {
            displayList.Add(obj);
        }

        public void clearGame()
        {
            double msPerFrame = 10;
            msPerFrame *= 1920.0 / G.gameWidth;
            timer.Interval = new TimeSpan(0, 0, 0, 0, (int)msPerFrame);

            displayList.Clear();
            G.canvas.Children.Clear();
            G.canvas.Background = new SolidColorBrush(G.randDarkColor());          
            BouncingBall.list.Clear();
            BouncingBall.ballCount = 0;
            Explosion.list.Clear();

        }


        

       

        public void RestartGame()
        {
            
            score = 0.0;
            
            
            SetUpAllGameObjects();
        }

        

        Background1 background1;
        Shooter shooter;
        public void SetUpAllGameObjects(bool isFirstTime=false)
        {
            

            gameNotOver = true;
 
            clearGame();

            threshold = 95; 
            timerTick_counter = 0;
        
            //add objects to the game 

            if (isFirstTime)
            {
                background1 = new Background1();
            }
            else
            {
                background1.AddToGame();
            }
            

            if (isFirstTime)
            {
                GameEngine.hiScoreText = new HiScoreText();
            }
            else
            {
                GameEngine.hiScoreText.AddToGame();
            }
       
             
            if (isFirstTime)
            {
                shooter = new Shooter();
            }
            else
            {
                shooter.AddToGame();
                if(!shooter.isActive) shooter.SetInMiddle();
                shields = 2;
            }

            for (int i = 0; i < 100; i++)
            {
                Explosion obj = new Explosion();
            }

            if (isFirstTime)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Particle obj = new Particle();
                }
            }
            else
            {
                foreach(Particle obj in Particle.list)
                {
                    obj.AddToGame();
                }
            }
            GameEngine.scoreText = new ScoreText();
            GameEngine.bonusText = new BonusText();
            GameEngine.roundText = new RoundText();
            //GameEngine.roundText.Text = "Round " + round.ToString();
            GameEngine.bigGameText = new BigGameText();
        }

        
        void timer_Tick(object sender, EventArgs e)
        {
            int chance = 0; 
            chance = G.randI(0, 100); //roll D100 to spawn asteroid
            timerTick_counter++;
            //double threshold = 95.0; //minimum needed to spawn asteroid

            
            if ((timerTick_counter % 1000) == 0) //every ~10 seconds.. 
            {
                //Powerup bb = new Powerup();
                //Shields ss = new Shields();
                if (threshold >= 50)
                {
                    threshold -= 5; //reduce needed roll on D100 by 5
                }
            }
            

            int chanceShield = G.randI(0, 1000);
            int chanceBonus = G.randI(0, 1000);
            if (chanceShield > 998)
            {
                Shields ss = new Shields();
            }
            if (chanceBonus > 998)
            {
                Powerup ss = new Powerup();
            }
             
            if (threshold >= 80)
            {
                if ((timerTick_counter % 25) == 0)
                {
                    score += 1;
                }
            }         
            else if (threshold >= 70)
            {
                if ((timerTick_counter % 12) == 0)
                {
                    score += 1;
                }
            }
            else if (threshold >= 60)
            {
                if ((timerTick_counter % 8) == 0)
                {
                    score += 1;
                }
            }
            else if (threshold >= 50)
            {
                if ((timerTick_counter % 6) == 0)
                {
                    score += 1;
                }
            }
            else if (threshold < 50)
            {
                if ((timerTick_counter % 5) == 0)
                {
                    score += 1;
                }
            }
           
          
            if (chance > threshold)
            {
                BouncingBall bb = new BouncingBall();
            }

            if (G.isKeyPressed(Key.Q)) Quit();
            if ((G.isKeyPressed(Key.OemMinus)) && (G.isKeyPressed(Key.OemPlus))) hiScoreText.ClearHighScores();
            if (bigGameText.Text == "0")
            {
                SetUpAllGameObjects();
            }
            if (!gameNotOver)
            {                
                score = 0;
                if (G.isKeyPressed(Key.Escape)) RestartGame();
                if ((G.isKeyPressed(Key.T))&&(G.isKeyPressed(Key.F))) SetUpAllGameObjects();
            }       

            foreach (GameObject obj in displayList)
            {
                obj.update();
            }
        }

        private void Quit()
        {            
            Application.Current.Shutdown();
        }

    }
}
