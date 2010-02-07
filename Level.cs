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
        private Boolean showerror = false;

        /// <summary>
        /// Constructor creating a new level obj.
        /// </summary>
        /// <param name="width">the width of the screen/level width to draw</param>
        /// <param name="height">the height of the screen/level height to draw</param>
        public Level(int lvlnr, int width, int height)
        {
            this.displayWidth = width;
            this.displayHeight = height;

            roads = new List<int>();
            rivirs = new List<int>();

            LoadDesign(lvlnr);
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
        private void LoadDesign(int lvlnr)
        {
            String appdir = Path.GetDirectoryName(Application.ExecutablePath);
            if (Directory.Exists(appdir))
            {
                string file = appdir+"\\levels\\lvl"+lvlnr+".xml";
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
                else
                {
                    if (!showerror)
                    {
                        showerror = true;
                        MessageBox.Show("Level " + file + " not found.");
                        
                    }
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

        /// <summary>
        /// Calculate the height of the rivir.
        /// </summary>
        /// <returns>the height in number of pixels</returns>
        public int GetHeightRivir(int baans)
        {
            int hrivir = (displayHeight / 10) * baans;
            return hrivir;
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
        private void DrawRiver(Graphics g, int locy, int numcourses)
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
        private void DrawRoad(Graphics g, int locy)
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
