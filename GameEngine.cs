/*
Copyright (C) 2009  Tom Postma, Gertjan Buijs

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    public class GameEngine
    {
        #region Fields (10)

        private FrmGame frmgame;
        private Timer gameupdate;
        private int level = -1;
        private int lives;
        private List<MovingObject> movingobjs;
        private List<int> rivirs;
        private List<int> roads;
        private int tick = 0;
        private Niveau tier;
        private List<Bitmap> prescaledimages;

        public const int frogbottommargin = 5;
        public const int roadlineheight = 5;

        #endregion Fields

        #region Constructors (1)

        //not supposed to change without recompile.
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

        /// <summary>
        /// Returns if GameUpdate timer is still running. Needed for tests.
        /// </summary>
        public bool GameUpdateStatus
        {
            get
            {
                return gameupdate.Enabled;
            }
        }

        #endregion

        #region Methods (15)

        // Public Methods (9) 

        /// <summary>
        /// Checks if game time is up for the current tier.
        /// if so then excute the GameOver methode
        /// </summary>
        /// <param name="min"></param>
        /// <returns>true if time is up.</returns>
        public bool CheckGameTime(int min)
        {
            if (min < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new car with a random color.
        /// </summary>
        /// <param name="velocity">The velocity of the car</param>
        /// <param name="dir">The direction of the car</param>
        /// <param name="dir">The number of the road to added the car to</param>
        /// <param name="dir">The random generator this to prevent getting right and left always the same color.</param>
        /// <returns>a car moving object</returns>
        public MovingObject CreateCarRandomColor(int vel, Direction dir, int roadLocY, Random rndgen)
        {
            int color = rndgen.Next(1, 3); // color is 1 or 2
            Car car = new Car(color, vel, dir);

            int posX = 0;
            int initheightcar = CalcHeightRoad() / 2 - roadlineheight;
            int initwidthcar = frmgame.ClientRectangle.Width / 10;

            if (dir == Direction.East)
            {
                posX = 0;
                int posY = roadLocY + 2*roadlineheight + initheightcar;
                car.Location = new Point(posX, posY);
            }
            else if (dir == Direction.West)
            {
                posX = frmgame.Width;
                car.Location = new Point(posX, roadLocY);
            }
            //car.Size = new Size(wcar, hcar);
            car.SetSize();
            return car;
        }

        /// <summary>
        /// Create a frog (the player) and calculate start position.
        /// The start position is the middle of the width of the form
        /// and the 
        /// </summary>
        /// <returns>a frog moving object</returns>
        public MovingObject CreateFrog()
        {
            Frog frog = new Frog(0, Direction.North);

            int locX = (frmgame.ClientRectangle.Width / 2) - (frog.Width / 2);
            int locY = frmgame.ClientRectangle.Height - frog.Height - frogbottommargin;
            //int sizeX = frmgame.ClientRectangle.Width / 10;
            //int sizeY = frmgame.ClientRectangle.Height / 10;

            frog.Location = new Point(locX, locY);
            //frog.Size = new Size(sizeX, sizeY);

            frog.SetSize();
            return frog;
        }

        /// <summary>
        /// Creates a new tree trunk.
        /// </summary>
        /// <param name="vel">The velocity of the tree trunk</param>
        /// <param name="dir">The direction of the tree trunk</param>
        /// <returns>a tree trunk moving object</returns>
        public MovingObject CreateTreeTrunk(int vel, Direction dir, int locY)
        {
            Tree treetrunk = new Tree(vel, dir);
            
            int locX = -treetrunk.Width;
            /*
            if (dir == Direction.East)
            {
                locX = -treetrunk.Width;
            }
            else
             */
            if (dir == Direction.West)
            {
                locX = frmgame.ClientRectangle.Width + treetrunk.Width;
            }
            treetrunk.Location = new Point(locX, locY);
            int htree = CalcHeightRivir(1);
            int wtree = CalcHeightRivir(1) * 2;
            treetrunk.Size = new Size(wtree, htree);
            treetrunk.SetSize();
            return treetrunk;
        }

        /// <summary>
        /// Detects collision when Frogger collides.
        /// </summary>
        /// <returns>Whether or not Frogger collides with a moving object</returns>
        public Boolean DetectCollision()
        {
            //throw new System.NotImplementedException();
            return false;
        }

        /// <summary>
        /// Draws a level.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        public void DrawLevel(Graphics g)
        {
            int space = frmgame.ClientRectangle.Height / 10;
            switch (level)
            {
                case 1:
                    DrawRiver(g, space* 1, 1);
                    DrawRoad(g, space * 3);
                    DrawRoad(g, space * 5);
                    DrawRoad(g, space * 7);
                    break;
                case 2:
                    break;
            }
        }

        public void DrawTextbox(Graphics g, String text)
        {
            int trycentre = text.Length * 10;
            Font font = new Font("Arial", 24);
            SolidBrush sbred = new SolidBrush(System.Drawing.Color.Red);
            SolidBrush sbdarkorange = new SolidBrush(System.Drawing.Color.DarkOrange);
            Rectangle box = new Rectangle(new Point(50, 50), new Size(frmgame.ClientRectangle.Width - 100, frmgame.ClientRectangle.Height - 100));
            g.DrawRectangle(Pens.Black, box);
            g.FillRectangle(sbdarkorange, box); 
            g.DrawString("Game Over", font, sbred, new PointF(frmgame.ClientRectangle.Width / 2 - trycentre, frmgame.ClientRectangle.Height / 2));
        }

        /// <summary>
        /// Shows the user that the game is over.
        /// </summary>
        public void GameOver(Graphics g, bool timeup, bool nomorelive)
        {
            if (timeup)
            {
                DrawTextbox(g, "Game Over");
            }
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// When the user has gained a highscore, the 'SaveHighscoreScreen' asks the user for his username.
        /// The highscore will be combined with the username, and will then be stored in the database.
        /// </summary>
        public void SaveHighscoreScreen()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// disable the gameupdate timer.
        /// </summary>
        public void StopEngine()
        {
            gameupdate.Enabled = false;
        }
        // Private Methods (6) 

        /// <summary>
        /// Calculate the height of the rivir.
        /// </summary>
        /// <returns>the height in number of pixels</returns>
        private int CalcHeightRivir(int baans)
        {
            int hrivir = (frmgame.ClientRectangle.Height / 10) * baans;
            return hrivir;
        }

        private int CalcHeightRoad()
        {
            int hroad = frmgame.ClientRectangle.Height / 10;
            return hroad;
        }

        /// <summary>
        /// Checks the amount of lives the player has.
        /// If this amount is less than 1, it will inform the player that the game is over.
        /// If that is the case, the GameEngine will close, and the main menu will open.
        /// </summary>
        public bool CheckLives(int currentLives)
        {
            if (currentLives < 1)
            {
                return true;
            }
            else
            {
                Lives--;
                return false;
            }
        }

        /// <summary>
        /// Draws a river.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the river is created at</param>
        private void DrawRiver(Graphics g, int locy, int numcourses)
        {
            rivirs.Add(locy);

            SolidBrush brushRiver = new SolidBrush(Color.Blue);
            if ((numcourses < 9) && (numcourses > 0))
            {
                Rectangle rectRiver = new Rectangle(0, locy, frmgame.Width, CalcHeightRivir(numcourses));
                g.FillRectangle(brushRiver, rectRiver);
            }
            else
            {
                throw new Exception("number of courses not valid.");
            }
        }

        /// <summary>
        /// Draws a road.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the road is created at</param>
        private void DrawRoad(Graphics g, int locy)
        {
            //roads.Add(locy);
            bool roadexist = false;
            foreach (int curroad in roads)
            {
                if (curroad == locy) roadexist = true;
            }
            if ((!roadexist) && (locy != 0))
            {
                roads.Add(locy);
            }

            int lineDistance = 100, heightRoad = 60;

            SolidBrush brushRoad = new SolidBrush(Color.Black); // the color of the road
            SolidBrush brushRoadLine = new SolidBrush(Color.White); // the color of the lines on the road
            Rectangle rectWeg = new Rectangle(0, locy, frmgame.ClientRectangle.Width, heightRoad);

            g.FillRectangle(brushRoad, rectWeg);
            int lineloc = locy + (heightRoad / 2);
            for (int xpos = 0; xpos < frmgame.ClientRectangle.Width; xpos += lineDistance)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, lineloc, 20, roadlineheight);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            }
        }

        /// <summary>
        /// Occurs when the gameupdate timer ticks.
        /// A car with a random color is being added to the list of movingobjects,
        /// Finally this method will update the position of every moving object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameupdate_Tick(object sender, EventArgs e)
        {
            tick++;
            int maxtick = 1000 / gameupdate.Interval; //1s

            switch (level)
            {
                case 1:
                    int sectime = 5;

                    if (tick == maxtick * sectime)
                    {
                        Random rndgen = new Random();

                        for (int curroad = 0; curroad < roads.Count; curroad++)
                        {
                            int rnddir = rndgen.Next(0, 2);
                            if (rnddir == 0)
                            {
                                movingobjs.Add(CreateCarRandomColor(2, Direction.East, roads[curroad], rndgen));
                            }
                            else
                            {
                                movingobjs.Add(CreateCarRandomColor(2, Direction.West,  roads[curroad], rndgen));
                            }
                        }

                        movingobjs.Add(CreateTreeTrunk(3, Direction.East, rivirs[0]));
                        /*
                        foreach (int riviry in rivirs)
                        {
                            movingobjs.Add(CreateTreeTrunk(2, Direction.East, riviry));
                        }
                         */
                        
                        //todo

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
                if (frmgame.Controls.Contains(obj))
                {

                    switch (obj.Dir)
                    {
                        case Direction.East:
                            obj.Location = new Point(obj.Location.X + obj.Velocity, obj.Location.Y);
                            break;
                        case Direction.West:
                            obj.Location = new Point(obj.Location.X - obj.Velocity, obj.Location.Y);
                            break;
                    }
                    if ((obj.Location.X + obj.Width < 0) || (obj.Location.X > frmgame.Width + obj.Width))
                    {
                        frmgame.Controls.Remove(obj);
                    }
                    else
                    {
                        //obj.Refresh();
                        obj.Invalidate();
                    }
                }
                else
                {
                    frmgame.Controls.Add(obj);
                }

            }
        }

        #endregion Methods
    }
}
