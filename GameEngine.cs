using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    public class GameEngine
    {
		#region Fields (7) 

        private FrmGame frmgame;
        private Timer gameupdate;
        private int level = -1;
        private List<MovingObject> movingobjs;
        private Niveau niveau;
        private int tick = 0;

		#endregion Fields 

		#region Constructors (1) 

        public GameEngine(int level, FrmGame frmgame, Niveau niv)
        {
            this.level = level;
            this.frmgame = frmgame;
            this.niveau = niv;

            movingobjs = new List<MovingObject>();

            gameupdate = new Timer
            {
                Enabled = true,
                Interval = 50
            };
            gameupdate.Tick += new EventHandler(gametime_Tick);
        }

		#endregion Constructors 

        public int lives
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

		#region Methods (10) 

		// Public Methods (4) 

        /// <summary>
        /// Teken het level.
        /// </summary>
        /// <param name="g"></param>
        public void DrawLevel(Graphics g)
        {
            switch (level)
            {
                case 1:
                    DrawRivir(g, 80);
                    DrawRoad(g, 240);
                    DrawRoad(g, 320);
                    DrawRoad(g, 400);
                    break;
                case 2:
                    DrawRoad(g, 150);
                    DrawRoad(g, 300);
                    DrawRivir(g, 405);
                    break;
            }
        }

        /// <summary>
        /// Toon gameover scherm
        /// </summary>
        public void GameOver()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Er is een nieuwe highscore behaald.
        /// Teken een textbox vragen voor je naam etc.
        /// </summary>
        public void SaveHighscoreScreen()
        {
            throw new System.NotImplementedException();
        }
		// Private Methods (6) 

        /// <summary>
        /// Create a new car.with random color
        /// </summary>
        /// <returns>the car obj.</returns>
        private MovingObject CreateCarRandomColor(int speed, Direction dir)
        {
            int color = new Random().Next(1, 3);
            Car car = new Car(color, speed, dir);
            return car;
        }

        /// <summary>
        /// Create a new tree trunk
        /// </summary>
        /// <returns></returns>
        private MovingObject CreateTreeTrunk(int speed, Direction dir)
        {
            Tree treetrunk = new Tree(3, dir);
            return treetrunk;
        }

        /// <summary>
        /// Teken een rivier.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="locy"></param>
        private void DrawRivir(Graphics g, int locy)
        {
            int hoogteRiver = 100;

            SolidBrush brushRiver = new SolidBrush(Color.Blue);
            Rectangle rectRiver = new Rectangle(0, locy, FrmGame.ActiveForm.Width, hoogteRiver);
            g.FillRectangle(brushRiver, rectRiver);
        }

        /// <summary>
        /// Teken een weg
        /// </summary>
        /// <param name="g"></param>
        /// <param name="locy">de locatie van Y cooridinaat van het venster.</param>
        private void DrawRoad(Graphics g, int locy)
        {
            int lineDistance = 100, heightRoad = 60;

            SolidBrush brushRoad = new SolidBrush(Color.Black);
            SolidBrush brushRoadLine = new SolidBrush(Color.White);
            Rectangle rectWeg = new Rectangle(0, locy, FrmGame.ActiveForm.Width, heightRoad);

            g.FillRectangle(brushRoad, rectWeg);
            for (int xpos = 0; xpos < FrmGame.ActiveForm.Height; xpos += lineDistance)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, locy + (heightRoad / 2), 20, 5);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            }
        }

        /// <summary>
        /// Teken nieuwe autos / boomstammen. Roep UpdatePosObject aan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gametime_Tick(object sender, EventArgs e)
        {
            tick++;
            switch (level)
            {
                case 1:
                    if (tick == 20)
                    {
                        movingobjs.Add(CreateCarRandomColor(3, Direction.East));
                    }
                    break;
                default:
                    break;
            }

            UpdatePosObjecten();
        }

        private void UpdatePosObjecten()
        {
        }

        /// <summary>
        /// kikker bots met ..
        /// </summary>
        public Boolean DetectCollision()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Controlleer levens, als minder dan 1 dan voer gameover uit
        /// </summary>
        private void CheckLives()
        {
            throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}
