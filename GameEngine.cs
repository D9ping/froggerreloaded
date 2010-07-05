/*
Copyright (C) 2009-2010  Tom Postma, Gertjan Buijs

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
#define windows //platform

namespace Frogger
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class GameEngine
    {
        #region Fields (11)

        private BigTextbox bigtbName;
        private Form frmgame;
        public Frog frog;
        private Timer gameupdate, timeupdate;
        private Level level;
        private Niveau tier;
        private List<PictureBox> livesimgs;
        private List<MovingObject> movingobjs;//make public for tests
        private string lvlname;
        public int min = 1, sec = 0;
        private int lives = 0, secnewcar, secnewtree, tickcar = 0, ticktree = 0, maxtickcar = 100, maxticktree = 100, carspeed = 10;
        private bool timeup = false, ishit = false, livesup = false, freeplay = false, win = false, screendraw = false, setup = false;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Creating a new instance of GameEngine class.
        /// </summary>
        /// <param name="level">The Level that should be started in the GameEngine</param>
        /// <param name="frmgame">The Form the GameEngine should use for this game</param>
        /// <param name="niv">The Niveau that is selected to use with the level</param>
        public GameEngine(string lvlname, Form frmgame, Niveau tier)
        {
            this.tier = tier;
            this.frmgame = frmgame;
            this.lvlname = lvlname;

            this.SetupEngine(true);

            this.DrawNumLives();

            
            bigtbName = new BigTextbox();
            bigtbName.Visible = false;
            frmgame.Controls.Add(bigtbName);

            this.StartEngine();
            frog = CreateFrog();
            frmgame.Controls.Add(frog);
        }

        #endregion Constructors

        #region Properties (3)

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

        #endregion Properties

        #region Methods (24)

        // Public Methods (5) 

        /// <summary>
        /// Renders the screen/draw the level.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        public void RenderScreen(Graphics g)
        {
            if (!freeplay)
            {
                if (livesup)
                {
                    DrawGameOverScreen(g, "no more lives left.");
                }
                else if (timeup)
                {
                    DrawGameOverScreen(g, "time is up.");
                }
            }

            if (win)
            {
                StopEngine(true);
                DrawWinScreen(g, CheckEnterName());
            }

            if (!screendraw)
            {
                level.Draw(g);
            }
        }

        /// <summary>
        /// set the number of lives based on the tier.
        /// Create some object on startup.
        /// </summary>
        public void SetupEngine(bool initsettings)
        {
            //--start loading--
            ResizesResources.images = new Dictionary<String, Bitmap>();
            ResizesResources.images.Clear();

            if (initsettings)
            {
                if (Program.fullscreen)
                {
                    level = new Level(this.lvlname, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                }
                else
                {
                    level = new Level(this.lvlname, frmgame.ClientSize.Width, frmgame.ClientSize.Height);
                }
            }
            else if (level != null)
            {
                level.SetLevelSize(this.frmgame.ClientRectangle.Width, this.frmgame.ClientRectangle.Height, true);
            }

            int kikkersizeX = frmgame.ClientSize.Width / 20;
            int kikkersizeY = frmgame.ClientSize.Height / 20;
            if (Program.fullscreen)
            {
                kikkersizeX = Screen.PrimaryScreen.WorkingArea.Width / 20;
                kikkersizeY = Screen.PrimaryScreen.WorkingArea.Height / 20;
            }
            ResizesResources.images.Add("kikker_west", ResizeImage(Frogger.Properties.Resources.kikker_west, kikkersizeX, kikkersizeY));
            ResizesResources.images.Add("kikker_east", ResizeImage(Frogger.Properties.Resources.kikker_east, kikkersizeX, kikkersizeY));
            ResizesResources.images.Add("frogdead_east", ResizeImage(Frogger.Properties.Resources.frogdead_east, kikkersizeX, kikkersizeY));
            ResizesResources.images.Add("frogdead_west", ResizeImage(Frogger.Properties.Resources.frogdead_west, kikkersizeX, kikkersizeY));
            ResizesResources.images.Add("frogdead_drunk", ResizeImage(Frogger.Properties.Resources.frogdead_drunk, kikkersizeX, kikkersizeY));
            int treesizeheight = level.GetHeightRivir(1);
            int treesizewidth = treesizeheight * 3;
            ResizesResources.images.Add("treetrunk", ResizeImage(Frogger.Properties.Resources.treetrunk, treesizewidth, treesizeheight));
            int carsizeX = frmgame.ClientRectangle.Width / 12;
            if (Program.fullscreen)
            {
                carsizeX = Screen.PrimaryScreen.WorkingArea.Width / 12;
            }
            int carsizeY = level.GetHeightRoad() / 2 - this.level.RoadlineHeight;
            ResizesResources.images.Add("car_grey_east", ResizeImage(Frogger.Properties.Resources.car_grey_east, carsizeX, carsizeY));
            ResizesResources.images.Add("car_grey_west", ResizeImage(Frogger.Properties.Resources.car_grey_west, carsizeX, carsizeY));
            ResizesResources.images.Add("car_yellow_east", ResizeImage(Frogger.Properties.Resources.car_yellow_east, carsizeX, carsizeY));
            ResizesResources.images.Add("car_yellow_west", ResizeImage(Frogger.Properties.Resources.car_yellow_west, carsizeX, carsizeY));
            ResizesResources.images.Add("car_green_east", ResizeImage(Frogger.Properties.Resources.car_green_east, carsizeX, carsizeY));
            ResizesResources.images.Add("car_green_west", ResizeImage(Frogger.Properties.Resources.car_green_west, carsizeX, carsizeY));
            int trunksizeX = frmgame.ClientRectangle.Width / 10;
            if (Program.fullscreen)
            {
                trunksizeX = Screen.PrimaryScreen.WorkingArea.Width / 10;
            }
            ResizesResources.images.Add("truck_east", ResizeImage(Frogger.Properties.Resources.truck_east, trunksizeX, carsizeY));
            ResizesResources.images.Add("truck_west", ResizeImage(Frogger.Properties.Resources.truck_west, trunksizeX, carsizeY));
            if (initsettings)
            {
                movingobjs = new List<MovingObject>();
                livesimgs = new List<PictureBox>();

                switch (tier)
                {
                    case Niveau.freeplay:
                        lives = -1;
                        secnewcar = 4;
                        secnewtree = 3;
                        carspeed = 6;
                        freeplay = true;
                        this.min = 60;
                        this.sec = 0;
                        break;
                    case Niveau.easy:
                        lives = 3;
                        secnewcar = 4;
                        secnewtree = 3;
                        carspeed = 6;
                        this.min = 3;
                        this.sec = 0;
                        break;
                    case Niveau.medium:
                        lives = 2;
                        secnewcar = 2;
                        secnewtree = 4;
                        carspeed = 8;
                        this.min = 1;
                        this.sec = 30;
                        break;
                    case Niveau.hard:
                        lives = 1;
                        secnewcar = 1;
                        secnewtree = 5;
                        carspeed = 10;
                        this.min = 0;
                        this.sec = 45;
                        break;
                    case Niveau.elite:
                        lives = 0;
                        secnewcar = 1;
                        secnewtree = 6;
                        carspeed = 12;
                        this.min = 0;
                        this.sec = 20;
                        break;
                    default:
                        throw new Exception("tier unknow.");
                }
                gameupdate = new Timer();
                gameupdate.Interval = 50;
                gameupdate.Tick += new EventHandler(gameupdate_Tick);

                timeupdate = new Timer();
                timeupdate.Interval = 1000;
                timeupdate.Tick += new EventHandler(timeupdate_Tick);

                maxtickcar = (1000 / gameupdate.Interval) * secnewcar;
                maxticktree = (1000 / gameupdate.Interval) * secnewtree;
                livesup = false;
            }
            else
            {
                //todo: resize all current objects.
                for (int i = 0; i < movingobjs.Count; i++)
                {
                    //movingobjs[i]
                }
            }

            //--done loading--
        }

        /// <summary>
        /// Start the timers: gameupdate and timeupdate
        /// </summary>
        public void StartEngine()
        {
            gameupdate.Enabled = true;
            timeupdate.Enabled = true;
        }

        /// <summary>
        /// Disable the gameupdate timer and timerTime timer.
        /// If destroyobjs is true than remove all objs from the screen.
        /// </summary>
        public void StopEngine(bool destroyobjs)
        {
            gameupdate.Enabled = false;
            timeupdate.Enabled = false;

            if (destroyobjs)
            {
                if (frog != null)
                {
                    frmgame.Controls.Remove(frog);
                    frog.Dispose();
                }
                for (int i = 0; i < movingobjs.Count; i++)
                {
                    frmgame.Controls.Remove(movingobjs[i]);
                    movingobjs[i].Location = new Point(0, -200); //dont bother the game with not yet garbagecollected objects.
                    movingobjs[i].Dispose();
                }
                GC.Collect(); //soon
            }
        }

        /// <summary>
        /// Updates the position of every moving object.
        /// It also detects if object is hit, and make the right sound if enabled.
        /// And it detects if the frog is on the other side(win)
        /// </summary>
        private void UpdatePositionMovingObjects()
        {
            frog.CanMove = false;
            foreach (MovingObject obj in movingobjs)
            {
                //if (frmgame.Controls.Contains(obj))
                //{
                    switch (obj.Dir)
                    {
                        case Direction.East:
                            obj.Location = new Point(obj.Location.X + obj.Velocity, obj.Location.Y);
                            break;
                        case Direction.West:
                            obj.Location = new Point(obj.Location.X - obj.Velocity, obj.Location.Y);
                            break;
                    }
                    if ((obj.Location.X + obj.Width < 0) || (obj.Location.X > frmgame.ClientRectangle.Width + obj.Width))
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
                            if (frog.Location.Y + frog.Size.Height >= obj.Location.Y + frogbottommargin)
                            {
                                ishit = true;
                                frog.CanMove = false;
                                switch (obj.Dir)
                                {
                                    case Direction.East:
                                        frog.Pic = ResizesResources.images["frogdead_west"];//Frogger.Properties.Resources.frogdead_east;
                                        break;
                                    case Direction.West:
                                        frog.Pic = ResizesResources.images["frogdead_east"];//Frogger.Properties.Resources.frogdead_west;
                                        break;
                                }
                                frog.Invalidate();
                                if (Program.sound)
                                {
#if windows
                                    sndPlaySound(Application.StartupPath + @"\sounds\punch.wav", 1); //1 = Async
#elif linux
                                    String soundbeep = Application.StartupPath + @"/sounds/punch.wav";
                                    if (File.Exists(soundbeep))
                                    {
                                        System.Media.SoundPlayer playsnd = new System.Media.SoundPlayer(soundbeep);
                                        playsnd.Play(); //issue cannot mix sound.
                                    }
                                    else
                                    {
                                        MessageBox.Show("Soundfile not found.");
                                    }
#endif
                                }
                            }
                        }
                        //tree:
                        else if (obj is Tree)
                        {
                            int widthmargin = frog.Width / 2;
                            if ((obj.Location.X < frog.Location.X + widthmargin) && (obj.Location.X + obj.Width > frog.Location.X + widthmargin))
                            {
                                int heightmargin = frog.Height / 2;
                                if ((obj.Location.Y < frog.Location.Y + heightmargin) && (obj.Location.Y + obj.Height > frog.Location.Y + heightmargin))
                                {
                                    frog.OnTree = true;
                                    frog.TreeDir = obj.Dir;
                                    frog.TreeVelocity = obj.Velocity;
                                }
                            }
                        }
                    }
                //}
                //else if ((obj != null) && (obj.IsDisposed == false))
                //{
                //    frmgame.Controls.Add(obj);
                //}
            }

            if (frog.OnTree == false)
            {
                int heightrivir = level.GetHeightRivir(1);
                //foreach (int rivYtop in this.rivirs)
                for (int i = 0; i < level.NumRivirs; i++)
                {
                    int rivYbottom = level.GetPosRivirs(i) + heightrivir;
                    if ((level.GetPosRivirs(i) < frog.Location.Y) && (rivYbottom > (frog.Location.Y + (frog.Height / 2))))
                    {
                        ishit = true;
                        frog.CanMove = false;
                        frog.Pic = ResizesResources.images["frogdead_drunk"];
                        frog.Invalidate();
                        if (Program.sound)
                        {
#if windows
                            sndPlaySound(Application.StartupPath + @"\sounds\sink.wav", 1); //1 = Async
#elif linux
                            String sinkwav = Application.StartupPath + @"/sounds/sink.wav";
                            System.Media.SoundPlayer sndply = new System.Media.SoundPlayer(sinkwav);
                            sndply.Play();
#endif
                        }
                    }
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
                frog.OnTree = false;
            }

            if (frog.Location.Y <= frog.Height)
            {
                win = true;
            }
            frog.CanMove = true;
        }
        // Private Methods (19) 

        /// <summary>
        /// Check if the player needs to enter a name for the highscore or not.
        /// </summary>
        /// <returns>true if the player can enter their name.</returns>
        private Boolean CheckEnterName()
        {
            bool entername = false;
            if (!screendraw)
            {
                string query = "SELECT * FROM HIGHSCORES WHERE LEVEL = '" + this.level.Naam + "' ORDER BY SPEELTIJD ASC";
                DataTable dt = DBConnection.ExecuteQuery(query, 4);
                if (dt.Rows.Count >= 10)
                {
                    object[] row = dt.Rows[9].ItemArray;
                    int minspeeltijdhighscore = Convert.ToInt32(row[2]);
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
            return entername;
        }

        /// <summary>
        /// Checks if game time is up for the current tier.
        /// if so then excute the GameOver methode
        /// </summary>
        /// <param name="min"></param>
        /// <returns>true if time is up.</returns>
        private bool CheckGameTime(int min)
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
        private bool CheckLives(int currentLives)
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
        /// Added a back hoverbuton to return to the main menu.
        /// </summary>
        private void CreateBackBtn()
        {
            HoverButton hovbtnBack = new HoverButton("Back");
            hovbtnBack.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            hovbtnBack.Location = new Point(frmgame.ClientSize.Width / 2 - hovbtnBack.Width / 2, frmgame.Height - 200);
            hovbtnBack.Click += new EventHandler(hovbtnBack_Click);
            frmgame.Controls.Add(hovbtnBack);
        }

        /// <summary>
        /// Creates a new car with a random color.
        /// </summary>
        /// <param name="velocity">The velocity of the car</param>
        /// <param name="dir">The direction of the car</param>
        /// <param name="locX">The number of the road to added the car to</param>
        /// <param name="roadLocY">The random generator this to prevent getting right and left always the same color.</param>
        /// <returns>a car moving object</returns>
        private MovingObject CreateCarRandomColor(int vel, Direction dir, int locX, int roadLocY, Random rndgen)
        {
            int carcolor = rndgen.Next(1, 5);
            int initcarheight = level.GetHeightRoad() / 2 - level.RoadlineHeight;
            int initcarwidth = frmgame.ClientRectangle.Width / 12;
            if (Program.fullscreen)
            {
                initcarwidth = Screen.PrimaryScreen.WorkingArea.Width / 12;
            }

            Car car = new Car(carcolor, vel, dir, initcarwidth, initcarheight);
            if (car.IsTruck)
            {
                initcarwidth = frmgame.ClientRectangle.Width / 8;
                car.Size = new Size(initcarwidth, initcarheight);
            }

            if (dir == Direction.East)
            {
                int locY = roadLocY + 2 * level.RoadlineHeight + initcarheight;
                car.Location = new Point(locX, locY);
            }
            else if (dir == Direction.West)
            {
                car.Location = new Point(locX, roadLocY);
            }
            frmgame.Controls.Add(car);
            return car;
        }

        /// <summary>
        /// Create a frog (the player) and calculate start position.
        /// The start position is the middle of the width of the form
        /// and the 
        /// </summary>
        /// <returns>a frog moving object</returns>
        private Frog CreateFrog()
        {
            int initfrogheight = frmgame.ClientSize.Height / 20;
            int initfrogwidth = frmgame.ClientSize.Width / 20;
            if (Program.fullscreen)
            {
                initfrogheight = Screen.PrimaryScreen.WorkingArea.Height / 20;
                initfrogwidth = Screen.PrimaryScreen.WorkingArea.Width / 20;
            }

            frog = new Frog(0, Direction.North, initfrogheight, initfrogwidth, initfrogheight, frmgame);
            int locX;
            int locY = 0;
            if (Program.fullscreen)
            {
                locX = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (frog.Width / 2);
                locY = (Screen.PrimaryScreen.WorkingArea.Height - frog.Height) - frogbottommargin;
            }
            else
            {
                locX = (frmgame.ClientSize.Width / 2) - (frog.Width / 2);
                locY = frmgame.ClientSize.Height - frog.Height - frogbottommargin;
            }
            frog.Location = new Point(locX, locY);

//#if DEBUG
            //MessageBox.Show("x:"+locX+" y:"+locY);
//#endif

            //frog.Anchor = AnchorStyles.None;
            if (frog == null) { throw new Exception("frog not created."); }
            return frog;
        }

        /// <summary>
        /// Creates a new tree trunk.
        /// </summary>
        /// <param name="vel">The velocity of the tree trunk</param>
        /// <param name="direction">The direction of the tree trunk</param>dd
        /// <returns>a tree trunk moving object</returns>
        private MovingObject CreateTreeTrunk(int vel, Direction direction, int locX, int locY)
        {
            int inittreewidth = level.GetHeightRivir(1) * 3;
            int inittreeheight = level.GetHeightRivir(1);
            Tree treetrunk = new Tree(vel, direction, inittreewidth, inittreeheight);
            treetrunk.Location = new Point(locX, locY);
            frmgame.Controls.Add(treetrunk);
            return treetrunk;
        }

        /// <summary>
        /// Detects collision when Frogger collides.
        /// </summary>
        /// <returns>Whether or not Frogger collides with a moving object</returns>
        private bool DetectCollision(MovingObject mvobj)
        {
            if (!mvobj.Disposing)
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
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Draw a box with the text "Game over" and a reason and display enter highscore if entername is true.
        /// </summary>
        /// <param name="g">graphics object</param>
        /// <param name="textregel1">the first line, big text</param>
        private void DrawGameOverScreen(Graphics g, string textline)
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
                screendraw = true;
                StopEngine(true);
                CreateBackBtn();
                frmgame.Refresh();
            }
            this.min = 0;
            this.sec = 0;
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

            if (this.screendraw == false)
            {
                if (entername)
                {
                    ShowEnterHighscore();
                }
                else
                {
                    CreateBackBtn();
                }
                this.screendraw = true;
                frmgame.Refresh();
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
            if (!setup)
            {
                InitSomeMvobjs();
                setup = true;
            }

            if (tickcar >= maxtickcar)
            {
                if (!ishit)
                {
                    Random rndgen = new Random();
                    int carwidth = ResizesResources.images["car_yellow_east"].Size.Width;
                    for (int curroad = 0; curroad < level.NumRoads; curroad++)
                    {
                        int rnddir = rndgen.Next(0, 2);
                        if (rnddir == 0)
                        {
                            movingobjs.Add(CreateCarRandomColor(carspeed, Direction.East, -carwidth, level.GetPosRoad(curroad), rndgen));
                        }
                        else
                        {
                            movingobjs.Add(CreateCarRandomColor(carspeed, Direction.West, frmgame.ClientSize.Width + carwidth, level.GetPosRoad(curroad), rndgen));
                        }
                    }
                }
                else if (ishit)
                {
                    StopEngine(true);
                    tickcar = 0;
                    if (!CheckLives(lives))
                    {
                        DrawNumLives();
                        Frog newfrog = CreateFrog();
                        frmgame.Controls.Add(newfrog);

                        InitSomeMvobjs();
                        ishit = false;
                        this.StartEngine();
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

            int treetrunkwidth = ResizesResources.images["treetrunk"].Size.Width;
            if (ticktree >= maxticktree)
            {
                for (int curriver = 0; curriver < level.NumRivirs; curriver++)
                {
                    if (curriver == 0)
                    {
                        movingobjs.Add(CreateTreeTrunk(4, Direction.East, -treetrunkwidth, level.GetPosRivirs(curriver)));
                    }
                    else if (curriver == 1)
                    {
                        movingobjs.Add(CreateTreeTrunk(5, Direction.West, frmgame.ClientRectangle.Width, level.GetPosRivirs(curriver)));
                    }
                    else if (curriver == 2)
                    {
                        movingobjs.Add(CreateTreeTrunk(6, Direction.East, -treetrunkwidth, level.GetPosRivirs(curriver)));
                    }
                    else if (curriver == 3)
                    {
                        movingobjs.Add(CreateTreeTrunk(4, Direction.West, frmgame.ClientRectangle.Width, level.GetPosRivirs(curriver)));
                    }

                    else if (curriver > 3 && curriver % 2 == 0) //even
                    {
                        movingobjs.Add(CreateTreeTrunk(3, Direction.East, -treetrunkwidth, level.GetPosRivirs(curriver)));
                    }
                    else if (curriver > 3)//odd and not 1 or 3
                    {
                        movingobjs.Add(CreateTreeTrunk(5, Direction.West, frmgame.ClientRectangle.Width + treetrunkwidth, level.GetPosRivirs(curriver)));
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
                    return 180 - (this.min * 60 + this.sec);
                case Niveau.medium:
                    return 90 - (this.min * 60 + this.sec);
                case Niveau.hard:
                    return 45 - (this.min * 60 + this.sec);
                case Niveau.elite:
                    return 20 - (this.min * 60 + this.sec);
                default:
                    return 99999;
            }
        }

        /// <summary>
        /// Close the game form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnBack_Click(object sender, EventArgs e)
        {
            frmgame.Close(); //Form fires closing event.
        }

        /// <summary>
        /// Added highscore to database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnSubmit_Click(object sender, EventArgs e)
        {
            string insertquery = "INSERT INTO HIGHSCORES VALUES (\"" + DateTime.Now.ToString() + "\", \"" +
                this.bigtbName.Text.Trim() + "\", " + this.GetGameTime() + ", '" + this.level.Naam + "')";
            DBConnection.SetData(insertquery);
            frmgame.Close();
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
                    carsperroad = 7;
                    break;
                case Niveau.hard:
                    carsperroad = 9;
                    break;
                case Niveau.elite:
                    carsperroad = 12;
                    break;
            }
            Random rndgen = new Random();

            int screenwidth = frmgame.ClientSize.Width;
            if (Program.fullscreen)
            {
                screenwidth = Screen.PrimaryScreen.WorkingArea.Width;
            }
            int truckwidth = ResizesResources.images["truck_east"].Width + mindistanceobjs;
            for (int curroad = 0; curroad < level.NumRoads; curroad++)
            {
                for (int i = 0; i < carsperroad; i++)
                {
                    int rnddir = rndgen.Next(0, 2);
                    int locX = screenwidth - (i * truckwidth);
                    if (rnddir == 0)
                    {
                        if (locX > 0)
                        {
                            movingobjs.Add(CreateCarRandomColor(carspeed, Direction.East, locX, level.GetPosRoad(curroad), rndgen));
                        }
                        else if (locX < screenwidth)
                        {
                            movingobjs.Add(CreateCarRandomColor(carspeed, Direction.West, locX, level.GetPosRoad(curroad), rndgen));
                        }

                    }
                    else
                    {
                        if (locX < screenwidth)
                        {
                            movingobjs.Add(CreateCarRandomColor(carspeed, Direction.West, locX, level.GetPosRoad(curroad), rndgen));
                        }
                        else if (locX > 0)
                        {
                            movingobjs.Add(CreateCarRandomColor(carspeed, Direction.East, locX, level.GetPosRoad(curroad), rndgen));
                        }
                    }
                }
            }

            for (int currivir = 0; currivir < level.NumRivirs; currivir++)
            {
                if (currivir % 2 == 0) //even
                {
                    movingobjs.Add(CreateTreeTrunk(5, Direction.East, screenwidth / 2, level.GetPosRivirs(currivir)));
                }
                else //odd
                {
                    if (tier != Niveau.elite && tier != Niveau.hard)
                    {
                        movingobjs.Add(CreateTreeTrunk(3, Direction.West, screenwidth / 2, level.GetPosRivirs(currivir)));
                    }
                }
            }
        }

        /// <summary>
        /// Resize a picture/bitmap
        /// </summary>
        /// <param name="picture">the bitmap to be resized</param>
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
        /// Maak label, bigtextbox en button aan voor invullen van Naam voor in highscore lijst
        /// </summary>
        private void ShowEnterHighscore()
        {
            HoverButton hovbtnSubmit = new HoverButton("submit");
            hovbtnSubmit.Location = new Point(frmgame.ClientSize.Width / 2 - hovbtnSubmit.Width / 2, frmgame.Height - 200);
            hovbtnSubmit.Click += new EventHandler(hovbtnSubmit_Click);

            Label lblText = new Label();
            lblText.Font = new Font("Flubber", 24);
            lblText.Text = "Enter your name:";
            lblText.AutoSize = true;
            lblText.BackColor = Color.Transparent;
            lblText.Location = new Point(hovbtnSubmit.Location.X, hovbtnSubmit.Location.Y - 250);

            bigtbName.Location = new Point(hovbtnSubmit.Location.X, hovbtnSubmit.Location.Y - 200);
            bigtbName.Visible = true;

            try
            {
                frmgame.Controls.Add(lblText);
                frmgame.Controls.Add(hovbtnSubmit);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }

        }

        /// <summary>
        /// Decreace the availible game time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeupdate_Tick(object sender, EventArgs e)
        {
            this.sec--;
            if (this.sec < 0)
            {
                this.min--;
                if (this.CheckGameTime(this.min))
                {
                    this.timeup = true;
                }
                else
                {
                    this.sec = 59;
                }
            }
        }

#if windows
        [DllImport("winmm.dll")]
        public static extern int sndPlaySound(string sFile, int sMode);
#endif


        #endregion


        #region game const settings
        private const int frogbottommargin = 6;
        private const int mindistanceobjs = 4;
        #endregion
    }
}
