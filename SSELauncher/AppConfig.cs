using System;

namespace SSELauncher
{
	public class AppConfig
	{
		public string Path;

		public string Folder;

		public string Exe;

		public bool Matched;

		public AppConfig(string path, string folder, string exe)
		{
            Path = path;
            Folder = folder;
            Exe = exe;
		}
	}
}
