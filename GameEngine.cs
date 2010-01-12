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
using System.Runtime.InteropServices;

namespace Frogger
{
    public class GameEngine
    {
        #region Fields (11)

        private FrmGame frmgame;
        private Niveau tier;
        private Boolean setup = false;
        public Frog frog;
        private Timer gameupdate;
        public List<MovingObject> movingobjs;
        private List<int> rivirs;
        private List<int> roads;

        private int timesecnewobj = 0, level = -1, lives = 0, tick = 0;


        #endregion Fields

        #region game const settings
        private const int roadlineheight = 5; //not supposed to change integers without recompile.
        private const int frogbottommargin = 16;
        private const int lineDistance = 100;
        #endregion

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

            roads = new List<int>();
            rivirs = new List<int>();
            movingobjs = new List<MovingObject>();
            ResizesResources.images = new Dictionary<String, Bitmap>();

            gameupdate = new Timer
            {
                Enabled = true,
                Interval = 50
            };
            gameupdate.Tick += new EventHandler(gameupdate_Tick);

            frog = CreateFrog();
            frmgame.Controls.Add(frog);
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

        public int NumObjects
        {
            get
            {
                return this.movingobjs.Count;
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

        #region Methods (18)

        // Public Methods (13) 

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
        /// Resize a picture/bitmap
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="width">new width</param>
        /// <param name="height">new height</param>
        /// <returns>the resized image</returns>
        private Bitmap ResizeImage(Bitmap picture, int width, int height)
        {
            Bitmap resizedpic = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedpic))
            {
                graphics.DrawImage(picture, 0, 0, width, height);
            }
            return resizedpic;
        }

        /// <summary>
        /// Creates a new car with a random color.
        /// </summary>
        /// <param name="velocity">The velocity of the car</param>
        /// <param name="dir">The direction of the car</param>
        /// <param name="dir">The number of the road to added the car to</param>
        /// <param name="dir">The random generator this to prevent getting right and left always the same color.</param>
        /// <returns>a car moving object</returns>
        public MovingObject CreateCarRandomColor(int vel, Direction dir, int locX, int roadLocY, Random rndgen)
        {
            int color = rndgen.Next(1, 5);
            Car car = new Car(color, vel, dir);

            int initheightcar = CalcHeightRoad() / 2 - roadlineheight;

            int initwidthcar = frmgame.ClientRectangle.Width / 12;
            if (car.IsTruck)
            {
                initwidthcar = frmgame.ClientRectangle.Width / 8;
            }

            if (dir == Direction.East)
            {
                int locY = roadLocY + 2 * roadlineheight + initheightcar;
                car.Location = new Point(locX, locY);
            }
            else if (dir == Direction.West)
            {
                car.Location = new Point(locX, roadLocY);
            }
            car.SetSize(initwidthcar, initheightcar);
            return car;
        }

        /// <summary>
        /// Create a frog (the player) and calculate start position.
        /// The start position is the middle of the width of the form
        /// and the 
        /// </summary>
        /// <returns>a frog moving object</returns>
        public Frog CreateFrog()
        {
            int sizeX = frmgame.ClientSize.Width / 20;
            int sizeY = frmgame.ClientSize.Height / 20;
            
            int space = frmgame.ClientSize.Height / 20;
            frog = new Frog(0, Direction.North, space);
            int locX = 0;
            int locY = 0;
            if (Program.fullscreen)
            {
                locX = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (frog.Width / 2);
                locY = Screen.PrimaryScreen.WorkingArea.Height - frog.Height - frogbottommargin;
            }
            else
            {
                locX = (frmgame.ClientSize.Width / 2) - (frog.Width / 2);
                locY = frmgame.ClientSize.Height - frog.Height - frogbottommargin;
            }

            frog.Location = new Point(locX, locY);
            frog.Size = new Size(sizeX, sizeY);

            if (frog == null) { throw new Exception("frog not created."); }
            return frog;
        }

        /// <summary>
        /// Creates a new tree trunk.
        /// </summary>
        /// <param name="vel">The velocity of the tree trunk</param>
        /// <param name="direction">The direction of the tree trunk</param>dd
        /// <returns>a tree trunk moving object</returns>
        public MovingObject CreateTreeTrunk(int vel, Direction direction, int locX, int locY)
        {
            Tree treetrunk = new Tree(vel, direction);
            treetrunk.Location = new Point(locX, locY);
            int htree = CalcHeightRivir(1);
            int wtree = CalcHeightRivir(1) * 3;
            treetrunk.Size = new Size(wtree, htree);

            return treetrunk;
        }

        /// <summary>
        /// Detects collision when Frogger collides.
        /// </summary>
        /// <returns>Whether or not Frogger collides with a moving object</returns>
        public Boolean DetectCollision(MovingObject mvobj)
        {
            if (frog == null) { throw new Exception("no frog created."); }

            int frogXrechtsboven = frog.Location.X + frog.Size.Width;
            int frogYlinksboven = frog.Location.Y;
            int mvobjXrechtsboven = mvobj.Location.X + mvobj.Size.Width;
            int frogXlinksboven = frog.Location.X - 5;

            if ((mvobj.Location.X <= frogXrechtsboven) && (mvobjXrechtsboven >= frogXlinksboven))
            {
                int frogYlinksonder = frog.Location.Y + frog.Size.Height;
                int mvobjYlinksonder = mvobj.Location.Y + mvobj.Size.Height + 10;

                if ((mvobj.Location.Y <= frogYlinksboven) && (mvobjYlinksonder >= frogYlinksonder))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Draws a level.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        public void RenderScreen(Graphics g)
        {
            int space = frmgame.ClientSize.Height / 10;
            switch (level)
            {
                case 1:
                    DrawRiver(g, space * 1, 1); //this is the level design.
                    DrawRoad(g, space * 3);
                    DrawRoad(g, space * 5);
                    DrawRoad(g, space * 7);
                    break;
                case 2:
                    DrawRiver(g, space * 1, 2);
                    DrawRoad(g, space * 5);
                    DrawRoad(g, space * 7);
                    break;
                case 3:
                    DrawRiver(g, space * 1, 4);
                    DrawRoad(g, space / 2 * 11);
                    DrawRoad(g, space * 7);
                    break;
            }

            if (!setup)
            {
                SetupEngine();
            }
            DrawNumLives(g);
        }

        /// <summary>
        /// set the number of lives based on the tier.
        /// Create some object on startup.
        /// </summary>
        private void SetupEngine()
        {
            ResizesResources.images.Clear();
            //pre loading..
            int kikkersizeX = frmgame.ClientSize.Width / 20;
            int kikkersizeY = frmgame.ClientSize.Height / 20;
            ResizesResources.images.Add("kikker_west", ResizeImage(Frogger.Properties.Resources.kikker_west, kikkersizeX, kikkersizeY));
            ResizesResources.images.Add("kikker_east", ResizeImage(Frogger.Properties.Resources.kikker_east, kikkersizeX, kikkersizeY));
            ResizesResources.images.Add("frogdead_east", ResizeImage(Frogger.Properties.Resources.frogdead_east, kikkersizeX, kikkersizeY));
            ResizesResources.images.Add("frogdead_west", ResizeImage(Frogger.Properties.Resources.frogdead_west, kikkersizeX, kikkersizeY));
            int treesizeX = CalcHeightRivir(1);
            int treesizeY = CalcHeightRivir(1) * 3;
            ResizesResources.images.Add("treetrunk", ResizeImage(Frogger.Properties.Resources.treetrunk, treesizeX, treesizeY));
            int carsizeX = frmgame.ClientRectangle.Width / 12;
            int carsizeY = CalcHeightRoad() / 2 - roadlineheight;
            ResizesResources.images.Add("car_grey_east", ResizeImage(Frogger.Properties.Resources.car_grey_east, carsizeX, carsizeY));
            ResizesResources.images.Add("car_grey_west", ResizeImage(Frogger.Properties.Resources.car_grey_west, carsizeX, carsizeY));
            ResizesResources.images.Add("car_yellow_east", ResizeImage(Frogger.Properties.Resources.car_yellow_east, carsizeX, carsizeY));
            ResizesResources.images.Add("car_yellow_west", ResizeImage(Frogger.Properties.Resources.car_yellow_west, carsizeX, carsizeY));
            ResizesResources.images.Add("car_green_east", ResizeImage(Frogger.Properties.Resources.car_green_east, carsizeX, carsizeY));
            ResizesResources.images.Add("car_green_west", ResizeImage(Frogger.Properties.Resources.car_green_west, carsizeX, carsizeY));
            int trunksizeX = frmgame.ClientRectangle.Width / 8;
            ResizesResources.images.Add("truck_east", ResizeImage(Frogger.Properties.Resources.truck_east, trunksizeX, carsizeY));
            ResizesResources.images.Add("truck_west", ResizeImage(Frogger.Properties.Resources.truck_west, trunksizeX, carsizeY));

            switch (tier)
            {
                case Niveau.easy:
                    lives = 3;
                    break;
                case Niveau.medium:
                    lives = 3;
                    break;
                case Niveau.hard:
                    lives = 2;
                    break;
                case Niveau.elite:
                    lives = 1;
                    break;
            }

            Random rndgen = new Random();
            int middlescreenx = frmgame.ClientSize.Width / 2;

            for (int curroad = 0; curroad < roads.Count; curroad++)
            {
                int rnddir = rndgen.Next(0, 2);
                if (rnddir == 0)
                {
                    movingobjs.Add(CreateCarRandomColor(2, Direction.East, middlescreenx, roads[curroad], rndgen));
                }
                else
                {
                    movingobjs.Add(CreateCarRandomColor(2, Direction.West, middlescreenx, roads[curroad], rndgen));
                }
            }

            for (int i = 0; i < rivirs.Count; i++)
            {
                if (rivirs[i] % 2 == 0) //even
                {
                    movingobjs.Add(CreateTreeTrunk(4, Direction.West, middlescreenx, rivirs[i]));
                }
                else //odd
                {
                    movingobjs.Add(CreateTreeTrunk(4, Direction.East, middlescreenx, rivirs[i]));
                }
            }

            setup = true;
        }

        /// <summary>
        /// Draws the number of lives pictures.
        /// </summary>
        /// <param name="g"></param>
        private void DrawNumLives(Graphics g)
        {
            int locX = 5;
            for (int i = 0; i < lives; i++)
            {
                g.DrawImage(Frogger.Properties.Resources.live, new Point(locX, 10));
                locX += Frogger.Properties.Resources.live.Size.Width + 5;
            }
        }



        /// <summary>
        /// Draw a box with text.
        /// </summary>
        /// <param name="g">graphics object</param>
        /// <param name="textregel1">the first line, big text</param>
        public void DrawTextbox(Graphics g, String textline1, String textline2)
        {
            frmgame.Controls.Remove(frog);

            int margincentre = textline1.Length * 20;
            Font fontregel1 = new Font("Flubber", 64);
            Font fontregel2 = new Font("Flubber", 24);
            SolidBrush sbdarkorange = new SolidBrush(System.Drawing.Color.DarkOrange);
            Rectangle box = new Rectangle(new Point(50, 50), new Size(frmgame.ClientRectangle.Width - 100, frmgame.ClientRectangle.Height - 100));
            g.DrawRectangle(Pens.Black, box);
            g.FillRectangle(sbdarkorange, box);
            g.DrawString(textline1, fontregel1, Brushes.Red, new PointF(frmgame.ClientRectangle.Width / 2 - margincentre, frmgame.ClientRectangle.Height / 2 -50));
            g.DrawString(textline2, fontregel2, Brushes.Black, new PointF(frmgame.ClientRectangle.Width / 2 - 100, frmgame.ClientRectangle.Height / 2 +20));

            HoverButton hovbtnBack = new HoverButton("Back");
            hovbtnBack.Location = new Point(frmgame.ClientSize.Width / 2 - hovbtnBack.Width / 2, frmgame.Height - 200);
            hovbtnBack.Click += new EventHandler(hovbtnBack_Click);
            frmgame.Controls.Add(hovbtnBack);
        }

        private void hovbtnBack_Click(object sender, EventArgs e)
        {
            frmgame.CloseGame();
        }

        /// <summary>
        /// Shows the user that the game is over.
        /// </summary>
        public void GameOver(Graphics g, bool timeup, bool nomorelive)
        {
            if (timeup)
            {
                StopEngine();
                DrawTextbox(g, "Game Over", "time is up.");
            }
        }

        /// <summary>
        /// When the user has gained a highscore, the 'SaveHighscoreScreen' asks the user for his username.
        /// The highscore will be combined with the username, and will then be stored in the database.
        /// </summary>
        public void SaveHighscoreScreen()
        {
            throw new System.NotImplementedException();
        }

        [DllImport("winmm.dll")]
        public static extern int sndPlaySound(string sFile, int sMode);

        /// <summary>
        /// disable the gameupdate timer.
        /// </summary>
        public void StopEngine()
        {
            gameupdate.Enabled = false;
            foreach (MovingObject mvobj in movingobjs)
            {
                mvobj.Dispose();
            }
        }

        /// <summary>
        /// Updates the position of every moving object.
        /// </summary>
        public void UpdatePositionMovingObjects()
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

                    if (DetectCollision(obj))
                    {
                        gameupdate.Enabled = false;
                        switch (obj.Dir)
                        {
                            case Direction.East:
                                frog.pic = ResizesResources.images["frogdead_east"];//Frogger.Properties.Resources.frogdead_east;
                                break;
                            case Direction.West:
                                frog.pic = ResizesResources.images["frogdead_west"];//Frogger.Properties.Resources.frogdead_west;
                                break;
                        }
                        if (Program.sound)
                        {
                            sndPlaySound(Application.StartupPath + @"\sounds\carcrash.wav", 1); //1 = Async
                        }
                    }
                }
                else
                {
                    frmgame.Controls.Add(obj);
                }
            }
        }
        // Private Methods (5) 

        /// <summary>
        /// Calculate the height of the rivir.
        /// </summary>
        /// <returns>the height in number of pixels</returns>
        private int CalcHeightRivir(int baans)
        {
            int hrivir = (frmgame.ClientSize.Height / 10) * baans;
            if (Program.fullscreen)
            {
                hrivir = Screen.PrimaryScreen.WorkingArea.Height / 10 * baans;
            }
            return hrivir;
        }

        /// <summary>
        /// Calculate the height of the road to draw
        /// </summary>
        /// <returns></returns>
        private int CalcHeightRoad()
        {
            int hroad = frmgame.ClientSize.Height / 10;
            if (!Program.fullscreen)
            {
                hroad = Screen.PrimaryScreen.WorkingArea.Height / 10;
            }
            return hroad;
        }

        /// <summary>
        /// Draws a river.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the river is created at</param>
        private void DrawRiver(Graphics g, int locy, int numcourses)
        {
            if (!setup)
            {
                for (int curcourse = 0; curcourse < numcourses; curcourse++)
                {
                    rivirs.Add(locy + curcourse * CalcHeightRivir(1));
                }
            }

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

            SolidBrush brushRoad = new SolidBrush(Color.Black); // the color of the road
            SolidBrush brushRoadLine = new SolidBrush(Color.White); // the color of the lines on the road
            Rectangle rectWeg;
            rectWeg = new Rectangle(0, locy, frmgame.ClientSize.Width, CalcHeightRoad());
            g.FillRectangle(brushRoad, rectWeg);
            int lineloc = locy + (CalcHeightRoad() / 2);
            for (int xpos = 0; xpos < frmgame.ClientSize.Width; xpos += lineDistance)
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
            int maxtick = 1000 / gameupdate.Interval; //1s

            if (tick >= maxtick * timesecnewobj)
            {
                Random rndgen = new Random();
                //if (this.NumObjects < 255) //protects against heavy cpu stress on hardest tier and settings.
                //{
                for (int curroad = 0; curroad < roads.Count; curroad++)
                {
                    int rnddir = rndgen.Next(0, 2);
                    if (rnddir == 0)
                    {

                        movingobjs.Add(CreateCarRandomColor(2, Direction.East, 0, roads[curroad], rndgen));

                    }
                    else
                    {
                        movingobjs.Add(CreateCarRandomColor(2, Direction.West, frmgame.ClientSize.Width, roads[curroad], rndgen));
                    }
                }

                for (int curriver = 0; curriver < rivirs.Count; curriver++)
                {
                    if (curriver % 2 == 0) //even
                    {
                        movingobjs.Add(CreateTreeTrunk(4, Direction.East, 0,rivirs[curriver]));
                    }
                    else if (curriver != 0) //odd and not 0
                    {
                        movingobjs.Add(CreateTreeTrunk(4, Direction.West, frmgame.ClientRectangle.Width, rivirs[curriver]));
                    }
                }
                tick = 0;
                //}
            }
            else
            {
                tick++;
            }

            switch (level)
            {
                case 1:
                    timesecnewobj = 5;
                    break;
                case 2:
                    timesecnewobj = 4;
                    break;
                case 3:
                    timesecnewobj = 3;
                    break;
            }

            UpdatePositionMovingObjects();
        }

        #endregion Methods
    }
}
