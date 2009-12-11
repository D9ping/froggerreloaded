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
        private List<int> roads;
        private List<int> rivirs;
        private Niveau tier;
        private int tick = 0;
        private int lives;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creates a GameEngine.
        /// </summary>
        /// <param name="level">The Level that should be started in the GameEngine</param>
        /// <param name="frmgame">The Form the GameEngine should use for this game</param>
        /// <param name="niv">The Niveau that is selected to use with the level</param>
        public GameEngine(int level, FrmGame frmgame, Niveau tier)
        {
            this.level = level;
            this.tier = tier;
            this.frmgame = frmgame;

            movingobjs = new List<MovingObject>();
            roads = new List<int>();
            rivirs = new List<int>();

            gameupdate = new Timer
            {
                Enabled = true,
                Interval = 50
            };
            gameupdate.Tick += new EventHandler(gameupdate_Tick);

            movingobjs.Add(CreateFrog());
        }

		#endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Returns the number of lives the player has left.
        /// </summary>
        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                lives = value;
            }
        }

        #endregion

        #region Methods (10)

        // Public Methods (3) 

        /// <summary>
        /// disable the gameupdate timer.
        /// </summary>
        public void StopEngine()
        {
            gameupdate.Enabled = false;
        }

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
                    break;
            }
        }

        /// <summary>
        /// Checks if game time is up for the current tier.
        /// if so then excute the GameOver methode
        /// </summary>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        public void CheckGameTime(int min)
        {
            switch (tier)
            {
                case Niveau.freeplay:
                    //never gameover
                    break;
                case Niveau.easy:
                    if (min == 10) GameOver(true, false);
                    break;
                case Niveau.medium:
                    if (min == 6) GameOver(true, false);
                    break;
                case Niveau.hard:
                    if (min == 3) GameOver(true, false);
                    break;
                case Niveau.elite:
                    if (min == 2) GameOver(true, false);
                    break;
            }
        }

        /// <summary>
        /// Shows the user that the game is over.
        /// </summary>
        public void GameOver(bool timeup, bool nomorelive)
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
        /// <param name="dir">The number of the road to added the car to</param>
        /// <returns></returns>
        private MovingObject CreateCarRandomColor(int vel, Direction dir, int numroad)
        {
            int color = new Random().Next(1, 3); // color is 1 or 2
            Car car = new Car(color, vel, dir);
            if (dir == Direction.East)
            {
                car.X = 0;
            }
            else if (dir == Direction.West)
            {
                car.X = frmgame.Width;
            }
            car.Y = roads[numroad];

            return car;
        }

        /// <summary>
        /// Create a frog (the player) and calculate start position.
        /// </summary>
        /// <returns></returns>
        private MovingObject CreateFrog()
        {
            int bottommargin = 5;
            Frog frog = new Frog(0, Direction.North);
            frog.X = (frmgame.Width / 2) - (frog.Width/2);
            frog.Y = frmgame.Height - frog.Height - bottommargin;
            return frog;
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
            if (dir == Direction.East)
            {
                treetrunk.X = 0;
            }
            else if (dir == Direction.West)
            {
                treetrunk.X = frmgame.Width;
            }
            return treetrunk;
        }

        /// <summary>
        /// Draws a river.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the river is created at</param>
        private void DrawRiver(Graphics g, int locy)
        {
            rivirs.Add(locy);
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
            roads.Add(locy);
            int lineDistance = 100, heightRoad = 60;

            SolidBrush brushRoad = new SolidBrush(Color.Black); // the color of the road
            SolidBrush brushRoadLine = new SolidBrush(Color.White); // the color of the lines on the road
            Rectangle rectWeg = new Rectangle(0, locy, FrmGame.ActiveForm.Width, heightRoad);

            g.FillRectangle(brushRoad, rectWeg);
            for (int xpos = 0; xpos < frmgame.Width; xpos += lineDistance)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, locy + (heightRoad / 2), 20, 5);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            }
        }

        /// <summary>
        /// Occurs when the gameupdate timer ticks.
        /// A car with a random color is being added to the list of movingobjects, every 20 times this method is recalled.
        /// Finally this method will update the position of every moving object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameupdate_Tick(object sender, EventArgs e)
        {
            tick++;
            
            switch (level)
            {
                case 1:
                    if (tick == 20)
                    {


                        //todo
                        movingobjs.Add(CreateCarRandomColor(3, Direction.East, 0));
                        tick = 0;
                    }
                    break;
                default:
                    break;
            }
            UpdatePositionMovingObjects();
        }

        /// <summary>
        /// Updates the position of every moving object.
        /// </summary>
        private void UpdatePositionMovingObjects()
        {
            foreach (MovingObject obj in movingobjs)
            {
                obj.Location = new Point(obj.X, obj.Y);

                //first find it, if not add.
                frmgame.Controls.Add(obj);
                
            }
        }

        /// <summary>
        /// Detects collision when Frogger collides.
        /// </summary>
        /// <returns>Whether or not Frogger collides with a moving object</returns>
        public Boolean DetectCollision()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks the amount of lives the player has.
        /// If this amount is less than 1, it will inform the player that the game is over.
        /// If that is the case, the GameEngine will close, and the main menu will open.
        /// </summary>
        private void CheckLives()
        {
            int currentLives = this.Lives;
            if (currentLives < 1)
            {
                GameOver(false, true);
            }
            else
            {
                Lives--;
            }
        }

        #endregion Methods
    }
}
