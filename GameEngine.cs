using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
//using System.Windows.Forms;
//using System.Threading;

namespace Frogger
{
    public class GameEngine
    {
		#region Fields (1) 

        private List<MovingObject> movingobjs;        
        private int level, sec = 0, min = 0;
        //private Timer gametime;

		#endregion Fields 

		#region Constructors (1) 

        public GameEngine(int level)
        {            
            this.level = level;
        }

		#endregion Constructors 

		#region Methods (5) 

		// Public Methods (2) 

        /// <summary>
        /// Teken het level.
        /// </summary>
        /// <param name="g"></param>
        public void DrawLevel(Graphics g)
        {
            switch (level)
            {
                case 1:
                    DrawRivir(g, 80);
                    DrawRoad(g, 250);
                    DrawRoad(g, 405);
                    break;
                case 2:
                    DrawRoad(g, 150);
                    DrawRoad(g, 300);
                    DrawRivir(g, 405);
                    break;
            }
        }

        /// <summary>
        /// Teken nieuwe dingen
        /// </summary>
        /// <param name="g"></param>
        public void DrawScreen(Graphics g)
        {
            DrawLevel(g);
            DrawTimeStr(g);
        }
		// Private Methods (3) 

        /// <summary>
        /// Teken een rivier.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="locy"></param>
        private void DrawRivir(Graphics g, int locy)
        {
            int hoogteRiver = 100;

            SolidBrush brushRiver = new SolidBrush(Color.Blue);
            Rectangle rectRiver = new Rectangle(0, locy, FrmGame.ActiveForm.Width, hoogteRiver);
            g.FillRectangle(brushRiver, rectRiver);
        }

        /// <summary>
        /// Teken een weg.        
        /// </summary>
        /// <param name="g"></param>
        /// <param name="locy">de locatie van Y cooridinaat van het venster.</param>
        private void DrawRoad(Graphics g, int locy)
        {
            int lineDistance = 100, heightRoad = 100;

            SolidBrush brushRoad = new SolidBrush(Color.Black);
            SolidBrush brushRoadLine = new SolidBrush(Color.White);
            Rectangle rectWeg = new Rectangle(0, locy, FrmGame.ActiveForm.Width, heightRoad);

            g.FillRectangle(brushRoad, rectWeg);
            for (int xpos = 0; xpos < FrmGame.ActiveForm.Height; xpos += lineDistance)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, locy + (heightRoad / 2), 20, 5);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            }
        }

        /// <summary>
        /// Teken speel tijd.
        /// </summary>
        /// <param name="g"></param>
        private void DrawTimeStr(Graphics g)
        {
            String time = this.min.ToString()+":";
            if (this.sec < 10) { time += "0" + this.sec.ToString(); }
            else { time += this.sec.ToString(); }
            
            Font myfont = new Font("Sans serif", 14.0f);
            g.DrawString(time, myfont, Brushes.Black, new Point(FrmGame.ActiveForm.Width - 80, 10));
        }

		#endregion Methods 

        
    }
}
