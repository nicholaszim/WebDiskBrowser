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
			if (dirInfo.Exists)
			{
				//FileIOPermission readPermission = new FileIOPermission(FileIOPermissionAccess.Read, path);
				//readPermission.AllLocalFiles = FileIOPermissionAccess;
				return dirInfo.EnumerateFiles("*",SearchOption.AllDirectories).AsQueryable().Count(method);
			}
			else
			{
				return 0;
			}
		}

		public IEnumerable<long> TryCountFiles(string root, out System.Collections.Specialized.StringCollection log)
		{
			log = new System.Collections.Specialized.StringCollection();
			if (!Directory.Exists(root))
			{
				return null;
			}
			Stack<string> stackDirs = new Stack<string>(30);
			stackDirs.Push(root);
			var count10mb = 0L;
			var count50mb = 0L;
			var count100mb = 0L;
			var countExceptions = 0L;
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
					/*log here*/
					var str = String.Format("Path: none; \n Attribute: none; \n Exception: UnauthorizedAccessException; \n Level: EnumerateDirectories level");
					log.Add(str);
					countExceptions += 1;
					continue;
				}
				catch (DirectoryNotFoundException e)
				{
					/*log here*/
					var str = String.Format("Path: none; \n Attribute: none; \n Exception: DirectoryNotFoundException; \n Level: EnumerateDirectories level");
					log.Add(str);
					countExceptions += 1;
					continue;
				}
				IEnumerable<string> files = null;
				try
				{
					files = Directory.EnumerateFiles(currentDir);
				}
				catch (UnauthorizedAccessException)
				{
					throw;
					var str = String.Format("Path: none; \n Attribute: none; \n Exception: UnauthorizedAccessException; \n Level: EnumerateDirectories level");
					log.Add(str);
					countExceptions += 1;
					continue;
				}
				catch (System.IO.DirectoryNotFoundException e)
				{
					countExceptions += 1;
					var str = String.Format("Path: none; \n Attribute: none; \n Exception: DirectoryNotFoundException; \n Level: EnumerateDirectories level");
					log.Add(str);
					continue;
				}
				foreach (var file in files)
				{
					try
					{
						FileInfo currentFI = new FileInfo(file);
						if (currentFI.Length < 10485760)
						{
							count10mb += 1;
						}
						else if (currentFI.Length > 10485760 && currentFI.Length < 52428800)
						{
							count50mb += 1;
						}
						else if (currentFI.Length < 104857600)
						{
							count100mb += 1;
						}
						else
						{
							//File.SetAttributes(file, attribute);
							continue;
						}
					}
					//!!
					catch (UnauthorizedAccessException e)
					{
						throw;
						var str = String.Format("Path: {0}; \n Attribute: {1}; \n Exception: UnauthorizedAccessException; \n Level: file foreach level", file, File.GetAttributes(file));
						log.Add(str);
						countExceptions += 1;
						continue;
					}
					catch (FileNotFoundException e)
					{
						var str = String.Format("Path: {0}; \n Attribute: {1}; \n Exception: FileNotFoundException; \n Level: file foreach level", file, File.GetAttributes(file));
						log.Add(str);
						countExceptions += 1;
						continue;
					}
				}
				foreach (var str in subDirs)
				{
					stackDirs.Push(str);
				}
			}
			return new List<long>(){ count10mb, count50mb, count100mb, countExceptions};
		}
	}
}