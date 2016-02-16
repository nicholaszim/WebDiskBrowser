using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Permissions;

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
		public IEnumerable<string> ReturnFileSystemEntriesInfo(string path)
		{
			var dirInfo = new DirectoryInfo(path);
			dirInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);
			if (dirInfo.Exists)
			{
				//return dirInfo.EnumerateFileSystemEntries(path, "*.*", SearchOption.TopDirectoryOnly);
				return ConvertEntries(dirInfo.EnumerateFileSystemInfos());
			}
			else return null;
		}

		public IEnumerable<string> ReturnFilesInfo(string path)
		{
			var fileInfo = new DirectoryInfo(path);
			fileInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);
			if (fileInfo.Exists)
			{
				return ConvertEntries(fileInfo.EnumerateFiles());
			}
			return null;
		}

		public IEnumerable<string> ReturnFolders(string path)
		{
			var fileInfo = new DirectoryInfo(path);
			fileInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);
			if (fileInfo.Exists)
			{
				return ConvertEntries(fileInfo.EnumerateDirectories());
			}
			return null;
		}

		private IEnumerable<string>ConvertEntries(IEnumerable<FileSystemInfo> collection)
		{
			foreach (var item in collection)
			{
				yield return item.Name;
			}
		}
		/// <summary>
		/// Return a string that represent a name of current directory.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public string ReturnDirectoryName(string path)
		{
			var dirInfo = new DirectoryInfo(path);
			dirInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);
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
			dirInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);
			if (dirInfo.Exists)
			{
				return dirInfo.EnumerateFiles("*",SearchOption.AllDirectories).AsQueryable().Count(method);
			}
			else
			{
				return 0;
			}
		}

		public IEnumerable<string> TryReturnDirectories(string root)
		{
			var rootDirectory = new DirectoryInfo(root);
			if (!rootDirectory.Exists)
			{
				return null;
			}
			Stack<string> stackDirs = new Stack<string>(30);
			stackDirs.Push(root);

			while (stackDirs.Count > 0)
			{
				string currentDir = stackDirs.Pop();
				IEnumerable<string> subDirs;
				try
				{
					subDirs = Directory.EnumerateDirectories(currentDir);
				}
				catch (UnauthorizedAccessException e)
				{
					// implement log;
					continue;
				}
			}

			return null;
		}
	}
}