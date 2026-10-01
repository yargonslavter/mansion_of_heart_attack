using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace flyingRats.RunSetup
{
	public class RunParameters
	{
        public RunParameters(int width, int height, bool fullscreen, string gameversion)
		{
			Width = width;
			Height = height;
			Fullscreen = fullscreen;
            GameVersion = gameversion;
		}

		public int Width { get; set; }
		public int Height { get; set; }
		public bool Fullscreen { get; set; }
        public string GameVersion { get; set; }
	}
}
