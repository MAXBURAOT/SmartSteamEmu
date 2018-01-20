using System;
using System.Runtime.InteropServices;

namespace SSELauncher
{
	internal class NativeMethods
	{
		public const int HWND_BROADCAST = 65535;

		public static readonly int WM_SHOWME = NativeMethods.RegisterWindowMessage("SSELauncher_WM_SHOWME");

		[DllImport("user32")]
		public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

		[DllImport("user32")]
		public static extern int RegisterWindowMessage(string message);
	}
}
