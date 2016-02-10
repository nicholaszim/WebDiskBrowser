using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebDiskBrowser.Managers
{
	public class FileSystemManager
	{
		/// <summary>
		/// Gets an IEnumerable collection of string representing the names of the logical drives on the computer.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetDrives()
		{
			return Environment.GetLogicalDrives();
		}
		/// <summary>
		/// Returns a string that represents a path to Desktop folder.
		/// </summary>
		/// <returns></returns>
		public string GetDesktopPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		/// <summary>
		/// Return an IEnumerable collection of all files (including their paths) is specified directory.
		/// </summary>
		/// <param name="path">Path to specified directory</param>
		/// <returns></returns>
		public IEnumerable<string> GetFiles(string path)
		{
			return Directory.GetFiles(path);
		}
		/// <summary>
		/// Return an IEnumerable collection of all directories (including their paths) in specified directory.
		/// </summary>
		/// <param name="path">Path to specified directory</param>
		/// <returns></returns>
		public IEnumerable<string> GetDirectories(string path)
		{
			return Directory.GetDirectories(path);
		}

		/// <summary>
		/// Helper method that incapsulates path processing and exception handling logic. 
		/// Returns IEnumerable collection of strings if processing was successfull, or returns null and warning string otherwise.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		private IEnumerable<string> TryProcess(string path, Func<string, IEnumerable<string>> method, out string message)
		{
			try
			{
				var output = method(path);
				message = "";
				return output;
			}
			catch (Exception e)
			{
				message = String.Format("Exception type: {0}; Message: {1}; Target: {2}", e.InnerException, e.Message, e.Source);
				return null;
			}
		}

		/// <summary>
		/// Returns an Inumerable collection of files (including their paths)in specified directory, if operation was unsuccessufull returns null and warning message.
		/// </summary>
		/// <param name="path">Path to specified directory</param>
		/// <returns></returns>
		public IEnumerable<string> TryGetFiles(string path, out string warning)
		{
			if (String.IsNullOrWhiteSpace(path))
			{
				warning = "String is Null or WhiteSpace or it`s Empty";
				return null;
			}
			else if (path.Length == 1)
			{
				warning = "";
				return TryProcess(path + ":\\", Directory.GetFiles, out warning);
			}
			else
			{
				warning = "";
				return TryProcess(path, Directory.GetFiles, out warning);
			}
		}
		/// <summary>
		/// Returns an Inumerable collection of directories (including their paths) in specified directory, if operation was unsuccessufull returns null and warning message.
		/// </summary>
		/// <param name="path">Path to specified directory</param>
		/// <returns></returns>
		public IEnumerable<string> TryGetDirectories(string path, out string warning)
		{
			if (String.IsNullOrWhiteSpace(path))
			{
				warning = "";
				return null;
			}
			else if (path.Length == 1)
			{
				warning = "";
				return TryProcess(path + ":\\", Directory.GetDirectories, out warning);
			}
			else
			{
				warning = "";
				return TryProcess(path, Directory.GetDirectories, out warning);
			}
		}
	}

}