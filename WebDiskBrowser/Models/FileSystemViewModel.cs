using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebDiskBrowser.Models
{
	public class FileSystemViewModel
	{
		public IEnumerable<string> Drives { get; set; }
		public string DirectoryName { get; set; }
		public string DirectoryPath { get; set; }
		public IEnumerable<string> Entries { get; set; }
		public int Count10mb { get; set; }
		public int Count50mb { get; set; }
		public int Count100mb { get; set; }
	}
}