using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
//using System.Timers;
using System.Drawing;
//using System.Windows.Forms;
//using System.Threading;

namespace Frogger
{
    public class GameEngine
    {
		#region Fields (3) 

        private Timer gameupdate;
        private int level;
        private List<MovingObject> movingobjs;

		#endregion Fields 

		#region Constructors (1) 

        public GameEngine(int level)
        {
            gameupdate.Tick += new System.EventHandler(gameupdate_Tick);
            gameupdate.Enabled = true;
            this.level = level;
        }

		#endregion Constructors 

        /// <summary>
        /// game logic, auto e.d. verplaatsen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameupdate_Tick(object sender, System.EventArgs e)
        {
            //todo
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

        public void DrawScreen(Graphics g)
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
    }
}
