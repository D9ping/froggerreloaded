using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;

namespace Frogger
{
    public class Level
    {
        private const int lineDistance = 100;

        private List<int> rivirs;
        private List<int> roads;
        private int displayWidth, displayHeight;
        private bool error = false;
        private string naam;

        /// <summary>
        /// Constructor creating a new level obj.
        /// </summary>
        /// <param name="width">the width of the screen/level width to draw</param>
        /// <param name="height">the height of the screen/level height to draw</param>
        public Level(string lvlnaam, int width, int height)
        {
            this.displayWidth = width;
            this.displayHeight = height;

            roads = new List<int>();
            rivirs = new List<int>();
            this.naam = lvlnaam;
            if (!error)
            {
                LoadDesign();
            }
        }

        public Level(int width, int height)
        {
            this.displayWidth = width;
            this.displayHeight = height;
            roads = new List<int>();
            rivirs = new List<int>();
        }

        public string Naam
        {
            get
            {
                return this.naam;
            }
            set
            {
                this.naam = value;
            }
        }

        public bool HasError
        {
            get
            {
                return this.error;
            }
        }

        public int NumRoads
        {
            get
            {
                return this.roads.Count;
            }
        }

        public int NumRivirs
        {
            get
            {
                return this.rivirs.Count;
            }
        }

        public int RoadlineHeight
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Loads the xml file with the level design.
        /// </summary>
        private void LoadDesign()
        {
            string appdir = Path.GetDirectoryName(Application.ExecutablePath);
            if (Directory.Exists(appdir))
            {
                string file = appdir+"\\levels\\"+this.naam+".lvl";
                if (File.Exists(file))
                {
                    XmlReader reader = new XmlTextReader(file);
                    roads.Clear();
                    rivirs.Clear();

                    while (reader.Read())
                    {
                        if (reader.Name == "road")
                        {
                            roads.Add(this.displayHeight - GetHeightRoad() * (reader.ReadElementContentAsInt()+1));
                        }
                        else if (reader.Name == "rivir")
                        {
                            rivirs.Add(this.displayHeight - GetHeightRivir(1) * (reader.ReadElementContentAsInt()+1));
                        }
                    }
                }
                else if (!error)
                    {
                        error = true;
                        MessageBox.Show("Level " + file + " not found.");
                        
                    }
            }

            //throw new NotImplementedException();
        }

        public int GetPosRivirs(int nr)
        {
            return this.rivirs[nr];
        }

        public int GetPosRoad(int nr)
        {
            return this.roads[nr];
        }

        public void AddRivir(int y)
        {
            if ((y >= 0) && (y <= displayHeight))
            {
                if (!CheckIfAdded(this.rivirs, y))
                {
                    this.rivirs.Add(y);
                }
                
            }
        }

        public void AddRoad(int y)
        {
            if ((y >= 0) && (y <= displayHeight))
            {
                if (!CheckIfAdded(this.roads, y))
                {
                    this.roads.Add(y);
                }
            }
        }

        /// <summary>
        /// Calculate the height of the rivir.
        /// </summary>
        /// <returns>the height in number of pixels</returns>
        public int GetHeightRivir(int baans)
        {
            int hrivir = (displayHeight / 10) * baans;
            return hrivir;
        }


        private bool CheckIfAdded(List<int> itemlist, int locy)
        {
            foreach (int cury in itemlist)
            {
                if (locy == cury)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Calculate the height of the road to draw
        /// </summary>
        /// <returns></returns>
        public int GetHeightRoad()
        {
            int hroad = displayHeight / 10;
            return hroad;
        }

        /// <summary>
        /// Draw the level.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            foreach (int rivirlocY in this.rivirs)
            {
                DrawRiver(g, rivirlocY, 1);
            }
            foreach (int roadlocY in this.roads)
            {
                DrawRoad(g, roadlocY);
            }
        }

        /// <summary>
        /// Draws a river.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the river is created at</param>
        public void DrawRiver(Graphics g, int locy, int numcourses)
        {
            SolidBrush brushRiver = new SolidBrush(Color.Blue);
            if ((numcourses < 9) && (numcourses > 0))
            {
                Rectangle rectRiver = new Rectangle(0, locy, displayWidth, GetHeightRivir(numcourses));
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
        public void DrawRoad(Graphics g, int locy)
        {
            SolidBrush brushRoad = new SolidBrush(Color.Black); // the color of the road
            SolidBrush brushRoadLine = new SolidBrush(Color.White); // the color of the lines on the road
            Rectangle rectWeg;
            rectWeg = new Rectangle(0, locy, displayWidth, GetHeightRoad());
            g.FillRectangle(brushRoad, rectWeg);
            int lineloc = locy + (GetHeightRoad() / 2);
            for (int xpos = 0; xpos < displayWidth; xpos += lineDistance)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, lineloc, 20, RoadlineHeight);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            }
        }

    }
}
