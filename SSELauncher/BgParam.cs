using System;
using System.Drawing;

namespace SSELauncher
{
	internal class BgParam
	{
		public string HashCode;

		public CApp App;

		public Image AppIcon;

		public BgParam(string hashcode, CApp app)
		{
            HashCode = hashcode;
            App = app;
            AppIcon = null;
		}
	}
}
