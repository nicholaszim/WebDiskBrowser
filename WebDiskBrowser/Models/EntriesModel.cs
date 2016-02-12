using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebDiskBrowser.Models
{
	public class EntriesModel
	{
		public IEnumerable<FileSystemInfo> Entries { get; set; }
		public int Count10mb { get; set; }
		public int Count50mb { get; set; }
		public int Count100mb { get; set; }
	}
}