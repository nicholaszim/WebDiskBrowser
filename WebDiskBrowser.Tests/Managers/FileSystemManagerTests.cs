using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDiskBrowser.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebDiskBrowser.Managers.Tests
{
	[TestClass()]
	public class FileSystemManagerTests
	{
		[TestMethod()]
		public void ReturnDesktopPathTest()
		{
			string expectedPath = @"C:\Users\<!!!!>\Desktop";

			FileSystemManager fs = new FileSystemManager();
			string actual = fs.ReturnDesktopPath();
			Assert.AreEqual(expectedPath, actual);
			Console.WriteLine("expected path: {0}, \n actual path: {1}", expectedPath, actual);
		}

		[TestMethod()]
		public void ReturnDrivesTest()
		{
			FileSystemManager fs = new FileSystemManager();
			var drives = fs.ReturnDrives();
			Assert.IsNotNull(drives);
		}

		[TestMethod()]
		public void ReturnFileSystemEntriesTest()
		{
			var invalidPath = "";
			var invalidPath2 = " ";
			var invalidPath3 = "     ";
			var invalidPath4 = "sdfodvove iifvbdibda";
			var invalidPath5 = "C";
			var invalidPath6 = @"C:/";
			FileSystemManager fs = new FileSystemManager();
			var entries = fs.ReturnFileSystemEntries(invalidPath);
			var entries2 = fs.ReturnFileSystemEntries(invalidPath2);
			var entries3 = fs.ReturnFileSystemEntries(invalidPath3);
			var entries4 = fs.ReturnFileSystemEntries(invalidPath4);
			var entries5 = fs.ReturnFileSystemEntries(invalidPath5);
			var entries6 = fs.ReturnFileSystemEntries(invalidPath6);
			Assert.IsNull(entries);
			Assert.IsNull(entries2);
			Assert.IsNull(entries3);
			Assert.IsNull(entries4);
			Assert.IsNull(entries5);
			Assert.IsNotNull(entries6);
			foreach (var entry in entries6)
			{
				Console.WriteLine(entry);
			}
		}

		[TestMethod()]
		public void ReturnDirectoryNameTest()
		{
			var path = @"C:\Program Files";
			var path2 = "C:\\Program Files";
			var path3 = @"C:\ ProgramFiles";

			FileSystemManager fs = new FileSystemManager();
			var name = fs.ReturnDirectoryName(path);
			var name2 = fs.ReturnDirectoryName(path2);
			var name3 = fs.ReturnDirectoryName(path3);

			var expectedname = "Program Files";

			Assert.IsNotNull(name);
			Assert.IsNotNull(name2);
			Assert.IsNull(name3);
			Assert.AreEqual(expectedname, name);
			Assert.AreEqual(expectedname, name2);
			Assert.AreNotEqual(expectedname, name3);
		}

		[TestMethod()]
		public void ReturnCountTest()
		{
			// create test folder
			// create test file/files with different size
			// test method
			if (!Directory.Exists(@"C:\Users\<!!!!>\Desktop\TestFolder\"))
			{
				Directory.CreateDirectory(@"C:\Users\<!!!!>\Desktop\TestFolder\");
				File.Create(@"C:\Users\<!!!!>\Desktop\TestFolder\testfile.txt");
				FileInfo fi = new FileInfo(@"C:\Users\<!!!!>\Desktop\TestFolder\testfile.txt");
				var fileSize = fi.Length;

				FileSystemManager fs = new FileSystemManager();
				var count = fs.ReturnCount(@"C:\Users\<!!!!>\Desktop\TestFolder", p => p.Length == fileSize);
				Assert.IsTrue(count == 1);
			}

		}

		[TestMethod()]
		public void TryCountFilesTest()
		{
			var log = new System.Collections.Specialized.StringCollection();
			FileSystemManager fs = new FileSystemManager();
			IEnumerable<long> countsList;
			var path = @"C:\Users\Nick";
			var path2 = @"C:\Program Files\Internet Explorer";
			countsList = fs.TryCountFiles(path, out log);
			Assert.IsNotNull(countsList);
			foreach (var item in log)
			{
				Console.WriteLine(item);
			}
		}
	}
}