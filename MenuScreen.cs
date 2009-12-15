using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Frogger
{
    public abstract class MenuScreen
    {
		#region Fields (2) 
        
        private FrmMenu frmmenu;        

		#endregion Fields 

		#region Constructors (1) 

        public MenuScreen(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;            
        }

		#endregion Constructors 

		#region Methods (3) 

		// Public Methods (1) 

        public abstract void ClearScreen();
		// Private Methods (2) 


		#endregion Methods 
    }
}
