using System;

namespace SSELauncher
{
	public class AppModifiedEventArgs : EventArgs
	{
		public object tag
		{
			get;
			set;
		}

		public CApp app
		{
			get;
			set;
		}
	}
}
