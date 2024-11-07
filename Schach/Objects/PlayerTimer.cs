using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Schach.Intel
{
	public class PlayerTimer
	{

		public Timer timerWhite;
		public Timer timerBlack;
		public bool isWhite;

		public PlayerTimer(int timeMaxSeconds) {
			Init();
			timerWhite.Interval = timeMaxSeconds * 60;
			timerBlack.Interval = timeMaxSeconds * 60;
		}
		public PlayerTimer()
		{
			Init();
			
		}

		private void Init()
		{
			timerWhite = new Timer();
			timerBlack = new Timer();
			isWhite = true;
		}

		public void Start() 
		{
			timerWhite.Start();
		}

		public void NextMove()
		{
			if (isWhite) 
			{
				timerBlack.Enabled = true;
				timerWhite.Enabled = false;
			} else {
				timerBlack.Enabled = false;
				timerWhite.Enabled = true;
			}
		}

		public void Pause()
		{
			timerWhite.Enabled = false;
			timerBlack.Enabled = false;
		}

		public void Unpause()
		{
			if (isWhite)
			{
				timerWhite.Enabled = true;
			}
			else
			{
				timerBlack.Enabled = true;
			}
		}
	}
}
