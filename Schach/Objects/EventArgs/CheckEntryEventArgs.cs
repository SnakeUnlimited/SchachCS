using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Schach
{
    public class CheckEntryEventArgs : EventArgs
    {
        private readonly AbstPiece.Color kingColor;

        public CheckEntryEventArgs(AbstPiece.Color color)
        {
            this.kingColor = color;
        }

        public AbstPiece.Color KingColor
        {
            get { return this.KingColor; }
        }
    }
}
