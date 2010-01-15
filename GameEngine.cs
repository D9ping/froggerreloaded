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
using System.Data;

namespace Frogger
{
    public class GameEngine
    {
        #region Fields (10)

        //public voor testen
        //private List<MovingObject> movingobjs;
        private FrmGame frmgame;
        public Frog frog;
        private Timer gameupdate;
        private int level = -1, lives = 0, secnewcar = 3, secnewtree = 3, tickcar = 0, ticktree;
        private List<PictureBox> livesimgs;
        public List<MovingObject> movingobjs;
        private List<int> rivirs;
        private List<int> roads;
        private Boolean setup = false, ishit = false, livesup = false, freeplay = false, win = false, screendraw = false;
        private Niveau tier;

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

            roads = new List<int>();
            rivirs = new List<int>();
            movingobjs = new List<MovingObject>();
            livesimgs = new List<PictureBox>();

            ResizesResources.images = new Dictionary<String, Bitmap>();

            switch (tier)
            {
                case Niveau.freeplay:
                    freeplay = true;
                    break;
                case Niveau.easy:
                    frmgame.min = 3;
                    frmgame.sec = 0;
                    break;
                case Niveau.medium:
                    frmgame.min = 1;
                    frmgame.sec = 30;
                    break;
                case Niveau.hard:
                    frmgame.min = 0;
                    frmgame.sec = 45;
                    break;
                case Niveau.elite:
                    frmgame.min = 0;
                    frmgame.sec = 20;
                    break;
                default: throw new Exception("Tier not found..");
            }

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

        #region Methods (26)

        // Public Methods (11) 

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
            if ((currentLives < 1) && (tier != Niveau.freeplay))
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
        /// Creates a new car with a random color.
        /// </summary>
        /// <param name="velocity">The velocity of the car</param>
        /// <param name="dir">The direction of the car</param>
        /// <param name="locX">The number of the road to added the car to</param>
        /// <param name="roadLocY">The random generator this to prevent getting right and left always the same color.</param>
        /// <returns>a car moving object</returns>
        public MovingObject CreateCarRandomColor(int vel, Direction dir, int locX, int roadLocY, Random rndgen)
        {
            int carcolor = rndgen.Next(1, 5);
            int initcarheight = CalcHeightRoad() / 2 - roadlineheight;
            int initcarwidth = frmgame.ClientRectangle.Width / 12;
            Car car = new Car(carcolor, vel, dir, initcarwidth, initcarheight);
            if (car.IsTruck)
            {
                initcarwidth = frmgame.ClientRectangle.Width / 8;
                car.Size = new Size(initcarwidth, initcarheight);
            }
            if (dir == Direction.East)
            {
                int locY = roadLocY + 2 * roadlineheight + initcarheight;
                car.Location = new Point(locX, locY);
            }
            else if (dir == Direction.West)
            {
                car.Location = new Point(locX, roadLocY);
            }
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
            int space = frmgame.ClientSize.Height / 20;
            int initfrogwidth = frmgame.ClientSize.Width / 20;
            int initfrogheight = frmgame.ClientSize.Height / 20;
            frog = new Frog(0, Direction.North, space, initfrogwidth, initfrogheight);
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
            //frog.pic = ResizesResources.images["frog_east"];
            frog.Location = new Point(locX, locY);

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
            int inittreewidth = CalcHeightRivir(1) * 3;
            int inittreeheight = CalcHeightRivir(1);
            Tree treetrunk = new Tree(vel, direction, inittreewidth, inittreeheight);

            treetrunk.Location = new Point(locX, locY);

            return treetrunk;
        }

        /// <summary>
        /// Detects collision when Frogger collides.
        /// </summary>
        /// <returns>Whether or not Frogger collides with a moving object</returns>
        public Boolean DetectCollision(MovingObject mvobj)
        {
            if (frog == null) { throw new Exception("no frog created."); }

            Point mvobjbovenlinks = new Point(mvobj.Location.X, mvobj.Location.Y);
            Point mvobjbovenrechts = new Point(mvobj.Location.X + mvobj.Size.Width, mvobj.Location.Y);
            Point mvobjonderlinks = new Point(mvobj.Location.X, mvobj.Location.Y + mvobj.Size.Height);

            if ((mvobjbovenlinks.X <= frog.Location.X + frog.Size.Width) && (mvobjbovenrechts.X >= frog.Location.X)) //X location okay?
            {
                if ((mvobjbovenlinks.Y <= frog.Location.Y + frog.Size.Height) && (mvobjonderlinks.Y >= frog.Location.Y)) //Y location okay?
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Shows the user that the game is over.
        /// </summary>
        public void GameOver(Graphics g, bool timeup, bool nomorelive)
        {
            StopEngine();

            if (timeup)
            {
                DrawGameOverScreen(g, "time is up.");
            }
            else if (nomorelive)
            {
                DrawGameOverScreen(g, "no more lives left.");
            }
            else if (win)
            {
                bool entername = false;
                if (!screendraw)
                {
                    string query = "SELECT * FROM HIGHSCORES WHERE LEVEL = " + level + " ORDER BY SPEELTIJD ASC";
                    DataTable dt = DBConnection.ExecuteQuery(query, 4);
                    if (dt.Rows.Count >= 10)
                    {
                        object[] row = dt.Rows[9].ItemArray;
                        int minspeeltijdhighscore = Convert.ToInt32(row[2]);
                        //MessageBox.Show(Convert.ToInt32(row[2]).ToString() + " gametime:" + GetGameTime());
                        if (GetGameTime() < minspeeltijdhighscore)
                        {
                            entername = true;
                        }
                    }
                    else if (dt.Rows.Count < 10)
                    {
                        entername = true;
                    }
                }
                DrawWinScreen(g, entername);
            }
            frmgame.Invalidate();
        }

        /// <summary>
        /// Draws a level./REnders the screen.
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
            if ((livesup) && !freeplay)
            {
                GameOver(g, false, true);
            }
            else if (win)
            {
                GameOver(g, false, false);
            }
        }

        [DllImport("winmm.dll")]
        public static extern int sndPlaySound(string sFile, int sMode);

        /// <summary>
        /// disable the gameupdate timer.
        /// </summary>
        public void StopEngine()
        {
            gameupdate.Enabled = false;
            frmgame.timerTime.Enabled = false;
            if (frog != null)
            {
                frmgame.Controls.Remove(frog);
                frog.Dispose();
            }
            for (int i = 0; i < movingobjs.Count; i++)
            {
                frmgame.Controls.Remove(movingobjs[i]);
                movingobjs[i].Location = new Point(-200, -200); //dont bother the game. with not yet gc objects.
                movingobjs[i].Dispose();
            }
            GC.Collect(); //soon
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
                        obj.Invalidate();
                    }

                    if (DetectCollision(obj))
                    {
                        //car:
                        if (obj is Car)
                        {
                            ishit = true;
                            switch (obj.Dir)
                            {
                                case Direction.East:
                                    frog.pic = ResizesResources.images["frogdead_west"];//Frogger.Properties.Resources.frogdead_east;
                                    break;
                                case Direction.West:
                                    frog.pic = ResizesResources.images["frogdead_east"];//Frogger.Properties.Resources.frogdead_west;
                                    break;
                            }
                            frog.Invalidate();
                            if (Program.sound)
                            {
                                sndPlaySound(Application.StartupPath + @"\sounds\punch.wav", 1); //1 = Async
                            }
                        }
                        //tree:
                        else if (obj is Tree)
                        {
                            frog.OnTree = true;
                            frog.TreeDir = obj.Dir;
                            frog.TreeVelocity = obj.Velocity;
                        }
                    }
                    else
                    {
                        frog.OnTree = false;
                    }
                }
                else if ((obj != null) && (obj.IsDisposed == false))
                {
                    frmgame.Controls.Add(obj);
                }
            }
            if (frog.OnTree == true)
            {
                switch (frog.TreeDir)
                {
                    case Direction.East:
                        frog.Location = new Point(frog.Location.X + frog.TreeVelocity, frog.Location.Y);
                        break;
                    case Direction.West:
                        frog.Location = new Point(frog.Location.X - frog.TreeVelocity, frog.Location.Y);
                        break;
                }
            }
            if (frog.Location.Y <= frog.Height)
            {
                win = true;
            }
            frog.CanMove = true;
        }
        // Private Methods (15) 

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
        /// Draw a box with the text "Game over" and a reason and display enter highscore if entername is true.
        /// </summary>
        /// <param name="g">graphics object</param>
        /// <param name="textregel1">the first line, big text</param>
        private void DrawGameOverScreen(Graphics g, String textline)
        {
            Font fontregel1 = new Font("Flubber", 64);
            Font fontregel2 = new Font("Flubber", 24);
            SolidBrush sbdarkorange = new SolidBrush(System.Drawing.Color.DarkOrange);
            Rectangle box = new Rectangle(new Point(50, 50), new Size(frmgame.ClientRectangle.Width - 100, frmgame.ClientRectangle.Height - 100));
            g.DrawRectangle(Pens.Black, box);
            g.FillRectangle(sbdarkorange, box);
            g.DrawString("Game Over", fontregel1, Brushes.Red, new PointF(frmgame.ClientRectangle.Width / 2 - 200, frmgame.ClientRectangle.Height / 2 - 50));
            g.DrawString(textline, fontregel2, Brushes.Black, new PointF(frmgame.ClientRectangle.Width / 2 - 100, frmgame.ClientRectangle.Height / 2 + 20));

            if (!this.screendraw)
            {
                HoverButton hovbtnBack = new HoverButton("Back");
                hovbtnBack.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                hovbtnBack.Location = new Point(frmgame.ClientSize.Width / 2 - hovbtnBack.Width / 2, frmgame.Height - 200);
                hovbtnBack.Click += new EventHandler(hovbtnBack_Click);
                frmgame.Controls.Add(hovbtnBack);
                hovbtnBack.Refresh();
                screendraw = true;
            }
        }

        /// <summary>
        /// Draws the number of lives pictures.
        /// </summary>
        /// <param name="g"></param>
        private void DrawNumLives()
        {
            foreach (PictureBox curpb in livesimgs)
            {
                curpb.Dispose();
            }

            int locX = 5;
            for (int i = 0; i < lives; i++)
            {
                PictureBox pbLive = new PictureBox();
                pbLive.Image = Frogger.Properties.Resources.live;
                pbLive.Location = new Point(locX, 3);
                pbLive.SizeMode = PictureBoxSizeMode.AutoSize;
                livesimgs.Add(pbLive);
                pbLive.BackColor = Color.Transparent;
                frmgame.Controls.Add(pbLive);
                locX += Frogger.Properties.Resources.live.Size.Width + 5;
            }
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
        /// Draw a screen with the text "win" and display enter highscore if entername is true.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="entername">is allow to enter highscore.</param>
        private void DrawWinScreen(Graphics g, bool entername)
        {
            Font fontregel1 = new Font("Flubber", 64);
            SolidBrush bl = new SolidBrush(System.Drawing.Color.GreenYellow);
            Rectangle box = new Rectangle(new Point(50, 50), new Size(frmgame.ClientRectangle.Width - 100, frmgame.ClientRectangle.Height - 100));
            g.DrawRectangle(Pens.Black, box);
            g.FillRectangle(bl, box);
            g.DrawString("Win", fontregel1, Brushes.Red, new PointF(frmgame.ClientRectangle.Width / 2 - 100, frmgame.ClientRectangle.Height / 2 - 200));

            if ((entername) && (!screendraw))
            {
                ShowEnterHighscore();
            }
            screendraw = true;
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
            int maxtickcar = (1000 / gameupdate.Interval) * secnewcar;
            if (tickcar >= maxtickcar)
            {
                if (!ishit)
                {
                    Random rndgen = new Random();
                    int carwidth = ResizesResources.images["car_yellow_east"].Size.Width;
                    for (int curroad = 0; curroad < roads.Count; curroad++)
                    {
                        int rnddir = rndgen.Next(0, 2);
                        if (rnddir == 0)
                        {
                            movingobjs.Add(CreateCarRandomColor(2, Direction.East, -carwidth, roads[curroad], rndgen));
                        }
                        else
                        {
                            movingobjs.Add(CreateCarRandomColor(2, Direction.West, frmgame.ClientSize.Width + carwidth, roads[curroad], rndgen));
                        }
                    }
                }
                else if (ishit)
                {
                    StopEngine();
                    if (!CheckLives(lives))
                    {
                        DrawNumLives();
                        Frog newfrog = CreateFrog();
                        frmgame.Controls.Add(newfrog);

                        InitSomeMvobjs();
                        ishit = false;
                        frmgame.timerTime.Enabled = true;
                        gameupdate.Enabled = true;
                    }
                    else
                    {
                        livesup = true;
                    }
                }
                tickcar = 0;
            }
            else
            {
                tickcar++;
            }
            int maxticktree = (1000 / gameupdate.Interval) * secnewtree;
            if (ticktree >= maxticktree)
            {
                int treetrunkwidth = ResizesResources.images["treetrunk"].Size.Width;
                for (int curriver = 0; curriver < rivirs.Count; curriver++)
                {
                    if (curriver % 2 == 0) //even
                    {
                        movingobjs.Add(CreateTreeTrunk(4, Direction.East, -treetrunkwidth, rivirs[curriver]));
                    }
                    else if (curriver != 0) //odd and not 0
                    {
                        movingobjs.Add(CreateTreeTrunk(4, Direction.West, frmgame.ClientRectangle.Width + treetrunkwidth, rivirs[curriver]));
                    }
                }
                ticktree = 0;
            }
            else
            {
                ticktree++;
            }
            if (!ishit)
            {
                UpdatePositionMovingObjects();
            }
            else
            {
                frog.CanMove = false;
                frog.BringToFront();
            }

        }

        /// <summary>
        /// De speel tijd berekenen en terug geven.
        /// </summary>
        /// <returns></returns>
        private int GetGameTime()
        {
            switch (tier)
            {
                case Niveau.easy:
                    return 180 - (frmgame.min * 60 + frmgame.sec);
                case Niveau.medium:
                    return 90 - (frmgame.min * 60 + frmgame.sec);
                case Niveau.hard:
                    return 45 - (frmgame.min * 60 + frmgame.sec);
                case Niveau.elite:
                    return 20 - (frmgame.min * 60 + frmgame.sec);
                default:
                    return 99999;
            }
        }

        private void hovbtnBack_Click(object sender, EventArgs e)
        {
            frmgame.CloseGame();
        }

        /// <summary>
        /// Added highscore to database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnSubmit_Click(object sender, EventArgs e)
        {
            String insertquery = "INSERT INTO HIGHSCORES VALUES (\"" + DateTime.Now.ToString() + "\", \"" + frmgame.TbEnterName + "\", " + GetGameTime() + "," + level + ")";
            DBConnection.SetData(insertquery);
            frmgame.VisibleTbEnterName = false;
            frmgame.CloseGame();
        }

        /// <summary>
        /// Create some object to start with.
        /// </summary>
        private void InitSomeMvobjs()
        {
            int carsperroad = 4;
            switch (tier)
            {
                case Niveau.freeplay:
                    carsperroad = 5;
                    break;
                case Niveau.easy:
                    carsperroad = 5;
                    break;
                case Niveau.medium:
                    carsperroad = 6;
                    break;
                case Niveau.hard:
                    carsperroad = 8;
                    break;
                case Niveau.elite:
                    carsperroad = 10;
                    break;
            }
            Random rndgen = new Random();

            int screenwidth = frmgame.ClientSize.Width;
            if (Program.fullscreen)
            {
                screenwidth = Screen.PrimaryScreen.WorkingArea.Width;
            }
            int truckwidth = ResizesResources.images["truck_east"].Width + 3; //3px distance at least
            for (int curroad = 0; curroad < roads.Count; curroad++)
            {
                for (int i = 0; i < carsperroad; i++)
                {
                    int rnddir = rndgen.Next(0, 2);
                    int locX = screenwidth - (i * truckwidth);
                    if (rnddir == 0)
                    {
                        if (locX > 0)
                        {
                            movingobjs.Add(CreateCarRandomColor(2, Direction.East, locX, roads[curroad], rndgen));
                        }
                        else if (locX < screenwidth)
                        {
                            movingobjs.Add(CreateCarRandomColor(2, Direction.West, locX, roads[curroad], rndgen));
                        }

                    }
                    else
                    {
                        if (locX < screenwidth)
                        {
                            movingobjs.Add(CreateCarRandomColor(2, Direction.West, locX, roads[curroad], rndgen));
                        }
                        else if (locX > 0)
                        {
                            movingobjs.Add(CreateCarRandomColor(2, Direction.East, locX, roads[curroad], rndgen));
                        }
                    }
                }
            }

            for (int i = 0; i < rivirs.Count; i++)
            {
                if (rivirs[i] % 2 == 0) //even
                {
                    movingobjs.Add(CreateTreeTrunk(4, Direction.West, screenwidth/2, rivirs[i]));
                }
                else //odd
                {
                    movingobjs.Add(CreateTreeTrunk(4, Direction.East, screenwidth/2, rivirs[i]));
                }
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
            int treesizeheight = CalcHeightRivir(1);
            int treesizewidth = treesizeheight * 3;
            ResizesResources.images.Add("treetrunk", ResizeImage(Frogger.Properties.Resources.treetrunk, treesizewidth, treesizeheight));
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
                case Niveau.freeplay:
                    lives = -1;
                    secnewcar = 4;
                    secnewtree = 3;
                    break;
                case Niveau.easy:
                    lives = 3;
                    secnewcar = 4;
                    secnewtree = 3;
                    break;
                case Niveau.medium:
                    lives = 3;
                    secnewcar = 3;
                    secnewtree = 4;
                    break;
                case Niveau.hard:
                    lives = 2;
                    secnewcar = 2;
                    secnewtree = 5;
                    break;
                case Niveau.elite:
                    lives = 1;
                    secnewcar = 2;
                    secnewtree = 6;
                    break;
                default:
                    throw new Exception("tier unknow.");
            }

            DrawNumLives();

            InitSomeMvobjs();

            livesup = false;
            setup = true;
        }

        /// <summary>
        /// Maak label en textbox en button zichtbaar/aan.
        /// </summary>
        private void ShowEnterHighscore()
        {
            frmgame.VisibleTbEnterName = true;
            HoverButton hovbtnSubmit = new HoverButton("submit");
            hovbtnSubmit.Location = new Point(frmgame.ClientSize.Width / 2 - hovbtnSubmit.Width / 2, frmgame.Height - 200);
            hovbtnSubmit.Click += new EventHandler(hovbtnSubmit_Click);
            frmgame.Controls.Add(hovbtnSubmit);

            Label lblText = new Label();
            lblText.Font = new Font("Flubber", 24);
            lblText.Text = "Enter your name:";
            lblText.AutoSize = true;
            lblText.BackColor = Color.Transparent;
            lblText.Location = new Point(hovbtnSubmit.Location.X, hovbtnSubmit.Location.Y - 250);
            frmgame.Controls.Add(lblText);

            lblText.Refresh();
            hovbtnSubmit.Refresh();
            frmgame.tbHighscoreName.Refresh();
        }

        #endregion Methods



        #region Properties (3)

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

        #region game const settings
        private const int roadlineheight = 5; //not supposed to change integers without recompile.
        private const int frogbottommargin = 12;
        private const int lineDistance = 100;
        #endregion
    }
}
