/*
Copyright (C) 2010 Tom Postma

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

namespace Frogger
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class Level
    {
        private const int lineDistance = 100;

        private List<int> roads;
        private List<int> rivirs;
        private int displayWidth, displayHeight;
        private bool error = false;
        private string naam;

        /// <summary>
        /// Creating an new instance of the level class.
        /// </summary>
        /// <param name="lvlnaam">The name of the level (same as filename without extension)</param>
        /// <param name="width">The width of the screen/level width to draw.</param>
        /// <param name="height">The height of the screen/level height to draw.</param>
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

        /// <summary>
        /// Creating an new instance of the level class.
        /// </summary>
        /// <param name="width">The width of the screen/level width to draw.</param>
        /// <param name="height">The height of the screen/level height to draw.</param>
        public Level(int width, int height)
        {
            this.displayWidth = width;
            this.displayHeight = height;
            this.roads = new List<int>();
            this.rivirs = new List<int>();
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
            string file = Path.Combine(Program.GetLevelFolder(), this.naam + ".lvl");
            if (File.Exists(file) && !String.IsNullOrEmpty(file))
            {
                XmlReader reader = new XmlTextReader(file);
                roads.Clear();
                rivirs.Clear();

                while (reader.Read())
                {
                    if (reader.Name == "road")
                    {
                        int pos = reader.ReadElementContentAsInt();
                        roads.Add(this.displayHeight - GetHeightRoad() * (pos + 1));
                    }
                    else if (reader.Name == "rivir")
                    {
                        int pos = reader.ReadElementContentAsInt();
                        rivirs.Add(this.displayHeight - GetHeightRivir(1) * (pos + 1));
                    }
                }
            }
            else if (!error)
            {
                error = true;
               // MessageBox.Show("Level " + file + " not found."); //removed can still cause a lot of messageboxes.
            }
        }

        /// <summary>
        /// Save the level design to a xml file with a the lvl extension.
        /// </summary>
        /// <param name="levelname">the new name</param>
        /// <returns>true if succeded</returns>
        public bool SaveDesign(string levelname)
        {
            char[] forbiddenchars = "?<>:*|\\/".ToCharArray();
            for (int pos = 0; (pos < levelname.Length) && (pos <= 100); pos++)
            {
                for (int fc = 0; fc < forbiddenchars.Length; fc++)
                {
                    if (levelname[pos] == forbiddenchars[fc])
                    {
                        return false; //error has forbidden character
                    }
                }
            }
            string lvlsdir = Program.GetLevelFolder();
            if (Directory.Exists(lvlsdir))
            {
                string filenamepath = Path.Combine(lvlsdir, levelname + ".lvl");
                XmlWriter xmlwr = null;
                try
                {
                    xmlwr = new XmlTextWriter(filenamepath, System.Text.Encoding.UTF8);
                    xmlwr.WriteStartDocument();

                    xmlwr.WriteStartElement("level");
                    xmlwr.WriteString("\r\n");

                    int placeheight = this.displayHeight / 10;

                    foreach (int locy in this.roads)
                    {
                        try
                        {
                            int posroad = (10 - (locy / placeheight)) - 1;
                            xmlwr.WriteStartElement("road");
                            xmlwr.WriteValue(posroad);
                            xmlwr.WriteEndElement();
                            xmlwr.WriteString("\r\n");
                        }
                        catch (Exception)
                        {
                            return false;//error  placeheight prob.
                        }
                    }
                    foreach (int locy in this.rivirs)
                    {
                        try
                        {
                            int posrivir = (10 - (locy / placeheight)) - 1;
                            xmlwr.WriteStartElement("rivir");
                            xmlwr.WriteValue(posrivir);
                            xmlwr.WriteEndElement();
                            xmlwr.WriteString("\r\n");
                        }
                        catch (Exception)
                        {
                            return false;//error  placeheight prob.
                        }
                    }

                    xmlwr.WriteEndElement();
                    xmlwr.WriteEndDocument();
                }
                catch (IOException)
                {
                    return false;//error: no write access/permission prob.
                }
                finally
                {
                    xmlwr.Flush();
                    xmlwr.Close();
                }

                return true;
            }
            else
            {
                return false;//error level folder gona.
            }
        }

        /// <summary>
        /// Sets the level width and height.
        /// </summary>
        /// <param name="width">The new level width.</param>
        /// <param name="height">The new level height.</param>
        public void SetLevelSize(int width, int height, bool reloaddesign)
        {
            this.displayWidth = width;
            this.displayHeight = height;
            if (!this.error)
            {
                if (reloaddesign)
                {
                    LoadDesign();
                }
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
        /// Adds a road to the road list
        /// if the road at pos. is not already added.
        /// and there is no rivir at this position, if so
        /// remove the rivir first.
        /// </summary>
        /// <param name="y">y position to add road</param>
        public void AddRoad(int y)
        {
            if ((y >= 0) && (y <= displayHeight))
            {
                if (CheckIfAdded(this.rivirs, y))
                {
                    this.rivirs.Remove(y);
                }
                if (!CheckIfAdded(this.roads, y))
                {
                    this.roads.Add(y);
                }
            }
        }

        /// <summary>
        /// Adds a rivir to the rivir list
        /// if the rivir at pos. is not aleady added.
        /// And there is no road at the same y position, if so
        /// remove the road first.
        /// </summary>
        /// <param name="y">y position to add rivir</param>
        public void AddRivir(int y)
        {
            if ((y >= 0) && (y <= displayHeight))
            {
                if (CheckIfAdded(this.roads, y))
                {
                    this.roads.Remove(y);
                }
                if (!CheckIfAdded(this.rivirs, y))
                {
                    this.rivirs.Add(y);
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

        /// <summary>
        /// Check if item at locy is in list 
        /// </summary>
        /// <param name="itemlist">the list to check</param>
        /// <param name="locy">to location y pos.</param>
        /// <returns>true if it is in the list</returns>
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
            if (!error)
            {
                foreach (int rivirlocY in this.rivirs)
                {
                    
                    DrawRiver(g, rivirlocY, 1);
                }
                
                /* optimizing stuff, work in process
                for (int i = 0; i < this.rivirs.Count; i++)
                {
                    if (i >= 1)
                    {
                        if (rivirs[i] - (this.displayHeight / 10) == rivirs[i - 1]) 
                        {
                            DrawRiver(g, rivirs[i-1], 2);
                            i++;
                        }
                    }
                    else
                    {
                        DrawRiver(g, rivirs[i], 1);
                    }
                }
                */
                foreach (int roadlocY in this.roads)
                {
                    DrawRoad(g, roadlocY);
                }
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
