using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Permissions;
using WebDiskBrowser.Models;

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
			if (fileInfo.Exists)
			{
				return ConvertEntries(fileInfo.EnumerateFiles());
			}
			return null;
		}

		public IEnumerable<string> ReturnFolders(string path)
		{
			var fileInfo = new DirectoryInfo(path);
			if (fileInfo.Exists)
			{
				return ConvertEntries(fileInfo.EnumerateDirectories());
			}
			return null;
		}

		private IEnumerable<string> ConvertEntries(IEnumerable<FileSystemInfo> collection)
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
				return dirInfo.EnumerateFiles().Count(method);
			}
			else
			{
				return 0;
			}
		}

//--------------------------------------------------

		/// <summary>
		/// Determines if file/directory located on specified path exists/available or not.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool ExistsOrAvialable(string path)
		{
			return Directory.Exists(path) || File.Exists(path);
		}

		/// <summary>
		/// Returns only collection of available file or directory names.
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public IEnumerable<string> TryConvertEntries(IEnumerable<FileSystemInfo> collection)
		{
			foreach (var item in collection)
			{
				if (item.Exists)
				{
					yield return item.Name;
				}
				else continue;
			}
		}
		/// <summary>
		/// Returns collection of only available directory names.
		/// This method is not exception-safe. 
		/// Use ExistOrAvailable method to check path value before using this method.
		/// </summary>
		/// <param name="path">path to a root directory</param>
		/// <returns></returns>
		public IEnumerable<string> ReturnAvailableDirectories(string path)
		{
			var getInfo = new DirectoryInfo(path);
			var getSubDirs = getInfo.EnumerateDirectories();
			return TryConvertEntries(getSubDirs);
		}
		/// <summary>
		/// Returns collection of only available file names.
		/// This method is not exception-safe. 
		/// Use ExistOrAvailable method to check path value before using this method.
		/// </summary>
		/// <param name="path">path to a root directory</param>
		/// <returns></returns>
		public IEnumerable<string> ReturnAvailableFiles(string path)
		{
			var getInfo = new DirectoryInfo(path);
			var getFiles = getInfo.EnumerateFiles();
			return TryConvertEntries(getFiles);
		}
		/// <summary>
		/// Only counts files in subdirectories.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		public int CountAvailableFiles(string path, Func<FileInfo, bool> method)
		{
			var getInfo = new DirectoryInfo(path);
			var getFiles = getInfo.EnumerateFiles();
			var counter = 0;
			foreach (var item in getFiles)
			{
				if (item.Exists)
				{
					if (method(item))
					{
						counter += 1;
					}
					else continue;
				}
				else continue;
			}
			return counter;
		}
		/// <summary>
		/// Traverses a directory tree starting with root specified root directory. Files in directories are processed with a mthods in delegates list.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="methods"></param>
		/// <returns></returns>
		public Dictionary<Func<FileInfo, bool>, int> TraverseAvailableFiles(string path, List<Func<FileInfo, bool>> methods)
		{
			Dictionary<Func<FileInfo, bool>, int> delegateCounters = new Dictionary<Func<FileInfo, bool>, int>();
			foreach (var item in methods)
			{
				delegateCounters.Add(item, 0);
			}
			//var counter = 0;
			Stack<string> dirs = new Stack<string>(20);
			dirs.Push(path);

			while(dirs.Count > 0)
			{
				string currentDir = dirs.Pop();
				IEnumerable<DirectoryInfo> subDirs;
				try
				{
					var dirInfo = new DirectoryInfo(currentDir);
					subDirs = dirInfo.EnumerateDirectories();
				}
				catch (UnauthorizedAccessException e) { continue; }
				catch (DirectoryNotFoundException e) { continue; }

				IEnumerable<FileInfo> files = null;
				try
				{
					var dirInfo = new DirectoryInfo(currentDir);
					files = dirInfo.EnumerateFiles();
				}
				catch (UnauthorizedAccessException e) { continue; }
				catch (DirectoryNotFoundException e) { continue; }
				foreach (var file in files)
				{
					try
					{
						foreach (var method in methods)
						{
							if (method(file))
							{
								delegateCounters[method] += 1;
							}
							else continue;
						}
					}
					catch (FileNotFoundException e)
					{
						continue;
					}
				}
				foreach (var dir in subDirs)
				{
					dirs.Push(dir.FullName);
				}
			}
			return delegateCounters;
		}

		/// <summary>
		/// Exception-safe method wrapper for ReturnAvailableFiles and ReturnAvailableDirectories.
		/// </summary>
		/// <param name="path">path to root directory</param>
		/// <returns></returns>
		public FileSystemViewModel ReturnAvailableDirInfo(string path)
		{
			switch (ExistsOrAvialable(path))
			{
				case false:
					return null;
				case true:
					var model = new FileSystemViewModel();
					model.Folders = ReturnAvailableDirectories(path);
					model.Files = ReturnAvailableFiles(path);
					model.Count10mb = CountAvailableFiles(path, entry => entry.Length < 10485760);
					model.Count50mb = CountAvailableFiles(path, entry => entry.Length > 10485760 && entry.Length < 52428800);
					model.Count100mb = CountAvailableFiles(path, entry => entry.Length > 104857600);
					return model;
			}
			return null;
		}
		public FileSystemViewModel TraverseAvailableDirInfo(string path, List<Func<FileInfo, bool>> delegates)
		{
			switch (ExistsOrAvialable(path))
			{
				case false:
					return null;
				case true:
					var model = new FileSystemViewModel();
					model.Folders = ReturnAvailableDirectories(path);
					model.Files = ReturnAvailableFiles(path);
					var filesCount = TraverseAvailableFiles(path, delegates);
					model.Count10mb = filesCount[delegates[0]];
					model.Count50mb = filesCount[delegates[1]];
					model.Count100mb = filesCount[delegates[2]];
					//model.Count10mb = TraverseAvailableFiles(path, delegates);
					//model.Count50mb = TraverseAvailableFiles(path, delegates);
					//model.Count100mb = TraverseAvailableFiles(path, delegates);
					return model;
			}
			return null;
		}


	}
}