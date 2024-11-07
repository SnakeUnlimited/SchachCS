using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    public class PositionEventArgs : EventArgs
    {
        private readonly ChessPosition position;

        public PositionEventArgs(ChessPosition position)
        {
            this.position = position;
        }

        public ChessPosition Position
        {
            get { return this.position; }
        }
    }
}