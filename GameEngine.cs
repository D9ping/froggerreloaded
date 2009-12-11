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

        /// <summary>
        /// Creates a GameEngine.
        /// </summary>
        /// <param name="level">The Level that should be started in the GameEngine</param>
        /// <param name="frmgame">The Form the GameEngine should use for this game</param>
        /// <param name="niv">The Niveau that is selected to use with the level</param>
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

        #region Properties (1)

        /// <summary>
        /// Returns the number of lives the player has left.
        /// </summary>
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

        #endregion

        #region Methods (10)

        // Public Methods (3) 

        /// <summary>
        /// Draws a level.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        public void DrawLevel(Graphics g)
        {
            switch (level)
            {
                case 1:
                    DrawRiver(g, 80);
                    DrawRoad(g, 240);
                    DrawRoad(g, 320);
                    DrawRoad(g, 400);
                    break;
                case 2:
                    DrawRoad(g, 150);
                    DrawRoad(g, 300);
                    DrawRiver(g, 405);
                    break;
            }
        }

        /// <summary>
        /// Shows the user that the game is over.
        /// </summary>
        public void GameOver()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// When the user has gained a highscore, the 'SaveHighscoreScreen' asks the user for his username.
        /// The highscore will be combined with the username, and will then be stored in the database.
        /// </summary>
        public void SaveHighscoreScreen()
        {
            throw new System.NotImplementedException();
        }

		// Private Methods (6) 

        /// <summary>
        /// Creates a new car with a random color.
        /// </summary>
        /// <param name="velocity">The velocity of the car</param>
        /// <param name="dir">The direction of the car</param>
        /// <returns></returns>
        private MovingObject CreateCarRandomColor(int vel, Direction dir)
        {
            int color = new Random().Next(1, 3); // color is 1 or 2
            Car car = new Car(color, vel, dir);
            return car;
        }

        /// <summary>
        /// Creates a new tree trunk.
        /// </summary>
        /// <param name="vel">The velocity of the tree trunk</param>
        /// <param name="dir">The direction of the tree trunk</param>
        /// <returns></returns>
        private MovingObject CreateTreeTrunk(int vel, Direction dir)
        {
            Tree treetrunk = new Tree(vel, dir);
            return treetrunk;
        }

        /// <summary>
        /// Draws a river.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the river is created at</param>
        private void DrawRiver(Graphics g, int locy)
        {
            int hoogteRiver = 100;

            SolidBrush brushRiver = new SolidBrush(Color.Blue);
            Rectangle rectRiver = new Rectangle(0, locy, FrmGame.ActiveForm.Width, hoogteRiver);
            g.FillRectangle(brushRiver, rectRiver);
        }

        /// <summary>
        /// Draws a road.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the road is created at</param>
        private void DrawRoad(Graphics g, int locy)
        {
            int lineDistance = 100, heightRoad = 60;

            SolidBrush brushRoad = new SolidBrush(Color.Black); // the color of the road
            SolidBrush brushRoadLine = new SolidBrush(Color.White); // the color of the lines on the road
            Rectangle rectWeg = new Rectangle(0, locy, FrmGame.ActiveForm.Width, heightRoad);

            g.FillRectangle(brushRoad, rectWeg);
            for (int xpos = 0; xpos < FrmGame.ActiveForm.Height; xpos += lineDistance)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, locy + (heightRoad / 2), 20, 5);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            }
        }

        /// <summary>
        /// Occurs when the gametime timer ticks.
        /// Every time the Adds new cars and tree trunks to the list of moving objects. 
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
