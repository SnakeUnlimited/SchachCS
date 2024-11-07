using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    public class MoveEntryEventArgs : EventArgs
    {
        private readonly string move;

        public MoveEntryEventArgs(string move)
        {
            this.move = move;
        }

        public string Move
        {
            get { return this.move; }
        }
    }
}
