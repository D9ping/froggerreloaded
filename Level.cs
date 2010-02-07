using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Frogger
{
    public class Level
    {
        private const int lineDistance = 100;

        private List<int> rivirs;
        private List<int> roads;
        private int displayWidth, displayHeight;

        /// <summary>
        /// Constructor creating a new level obj.
        /// </summary>
        /// <param name="width">the width of the screen/level width to draw</param>
        /// <param name="height">the height of the screen/level height to draw</param>
        public Level(int width, int height)
        {
            this.displayWidth = width;
            this.displayHeight = height;

            roads = new List<int>();
            rivirs = new List<int>();
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
        /// Draws a river.
        /// </summary>
        /// <param name="g">The graphics component that should be used</param>
        /// <param name="locy">The y-coördinate the river is created at</param>
        public void DrawRiver(Graphics g, int locy, int numcourses)
        {
            //if (!setup)
            //{
                bool rivirexist = false;
                foreach (int currivir in rivirs)
                {
                    if (currivir == locy) rivirexist = true;
                }
                if ((!rivirexist) && (locy != 0))
                {
                    for (int curcourse = 0; curcourse < numcourses; curcourse++)
                    {
                        rivirs.Add(locy + curcourse * GetHeightRivir(1));
                    }
                }
            //}

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
            //if (!setup)
            //{
                bool roadexist = false;
                foreach (int curroad in roads)
                {
                    if (curroad == locy) roadexist = true;
                }
                if ((!roadexist) && (locy != 0))
                {
                    roads.Add(locy);
                }
            //}

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
    }
}
