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
		/// Returns IEnumerable collection of string that represent names of all logical drives.
		/// Not exception-safe
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> ReturnDrives()
		{
			return Environment.GetLogicalDrives();
		}

		/// <summary>
		/// Returns a string that represents a path to a desktop folder.
		/// </summary>
		/// <returns></returns>
		public string ReturnDesktopPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}

		/// <summary>
		/// Returns IEnumerable collection of string, that represent full names of all files and directories 
		/// located in specified directory
		/// Not exception-safe
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public IEnumerable<string> ReturnFileSystemEntries(string path)
		{
			if (Directory.Exists(path))
			{
				return Directory.EnumerateFileSystemEntries(path, "*.*", SearchOption.TopDirectoryOnly);
			}
			else return null;
		}
		/// <summary>
		/// Returns IEnumerable collection of FileSystemInfo that represent names of all files and directories 
		/// located in specified directory
		/// Not exception-safe
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public IEnumerable<FileSystemInfo> ReturnFileSystemEntriesInfo(string path)
		{
			var dirInfo = new DirectoryInfo(path);
			if (dirInfo.Exists)
			{
				//return dirInfo.EnumerateFileSystemEntries(path, "*.*", SearchOption.TopDirectoryOnly);
				return dirInfo.EnumerateFileSystemInfos();
			}
			else return null;
		}
		/// <summary>
		/// Return a string that represent a name of current directory.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public string ReturnDirectoryName(string path)
		{
			var dirInfo = new DirectoryInfo(path);
			if (dirInfo.Exists)
			{
				return dirInfo.Name;
			}
			else return null;
		}
		/// <summary>
		/// Counts a number of files located on a specified directory, that satisfy a condition.
		/// </summary>
		/// <param name="path">Path to specified directory</param>
		/// <param name="method">Method that represents a condition</param>
		/// <returns></returns>
		public int ReturnCount(string path, Func<FileInfo, bool> method)
		{
			var dirInfo = new DirectoryInfo(path);
			if (dirInfo.Exists)
			{
				return dirInfo.EnumerateFiles().AsQueryable().Count(method);
			}
			else
			{
				return 0;
			}
		}
		///// <summary>
		///// Gets an IEnumerable collection of string representing the names of the logical drives on the computer.
		///// </summary>
		///// <returns></returns>
		//public IEnumerable<string> GetDrives()
		//{
		//	return Environment.GetLogicalDrives();
		//}
		///// <summary>
		///// Returns a string that represents a path to Desktop folder.
		///// </summary>
		///// <returns></returns>
		//public string GetDesktopPath()
		//{
		//	return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//}
		///// <summary>
		///// Return an IEnumerable collection of all files (including their paths) is specified directory.
		///// </summary>
		///// <param name="path">Path to specified directory</param>
		///// <returns></returns>
		//public IEnumerable<string> GetFiles(string path)
		//{
		//	return Directory.GetFiles(path);
		//}
		///// <summary>
		///// Return an IEnumerable collection of all directories (including their paths) in specified directory.
		///// </summary>
		///// <param name="path">Path to specified directory</param>
		///// <returns></returns>
		//public IEnumerable<string> GetDirectories(string path)
		//{
		//	return Directory.GetDirectories(path);
		//}

		///// <summary>
		///// Helper method that incapsulates path processing and exception handling logic. 
		///// Returns IEnumerable collection of strings if processing was successfull, or returns null and warning string otherwise.
		///// </summary>
		///// <param name="path"></param>
		///// <param name="method"></param>
		///// <returns></returns>
		//private IEnumerable<string> TryProcess(string path, Func<string, IEnumerable<string>> method, out string message)
		//{
		//	try
		//	{
		//		var output = method(path);
		//		message = "";
		//		return output;
		//	}
		//	catch (Exception e)
		//	{
		//		message = String.Format("Exception type: {0}; Message: {1}; Target: {2}", e.InnerException, e.Message, e.Source);
		//		return null;
		//	}
		//}

		///// <summary>
		///// Returns an Inumerable collection of files (including their paths)in specified directory, if operation was unsuccessufull returns null and warning message.
		///// </summary>
		///// <param name="path">Path to specified directory</param>
		///// <returns></returns>
		//public IEnumerable<string> TryGetFiles(string path, out string warning)
		//{
		//	if (String.IsNullOrWhiteSpace(path))
		//	{
		//		warning = "String is Null or WhiteSpace or it`s Empty";
		//		return null;
		//	}
		//	else if (path.Length == 1)
		//	{
		//		warning = "";
		//		return TryProcess(path + ":\\", Directory.GetFiles, out warning);
		//	}
		//	else
		//	{
		//		warning = "";
		//		return TryProcess(path, Directory.GetFiles, out warning);
		//	}
		//}
		///// <summary>
		///// Returns an Inumerable collection of directories (including their paths) in specified directory, if operation was unsuccessufull returns null and warning message.
		///// </summary>
		///// <param name="path">Path to specified directory</param>
		///// <returns></returns>
		//public IEnumerable<string> TryGetDirectories(string path, out string warning)
		//{
		//	if (String.IsNullOrWhiteSpace(path))
		//	{
		//		warning = "";
		//		return null;
		//	}
		//	else if (path.Length == 1)
		//	{
		//		warning = "";
		//		return TryProcess(path + ":\\", Directory.GetDirectories, out warning);
		//	}
		//	else
		//	{
		//		warning = "";
		//		return TryProcess(path, Directory.GetDirectories, out warning);
		//	}
		//}

		//public int CountFiles(string path)
		//{
		//	DirectoryInfo di = new DirectoryInfo(path);
		//	var collection = di.EnumerateFiles();
		//	var result = collection.Count(p => p.Length <= 10);
		//	return result;
		//}

		//public int CountFiles2(string path)
		//{
		//	var files = Directory.EnumerateFileSystemEntries(path);
		//	var result = files.Count(p => p.Length <= 10);
		//	return result;
		//}
	}

}