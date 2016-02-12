using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebDiskBrowser.Models
{
	[DataContract(IsReference=true)]
	public class FileSystemViewModel
	{
		[DataMember]
		public IEnumerable<string> Drives { get; set; }
		[DataMember]
		public string DirectoryName { get; set; }
		[DataMember]
		public string DirectoryPath { get; set; }
		[DataMember]
		public IEnumerable<string> Entries { get; set; }
		[DataMember]
		public int Count10mb { get; set; }
		[DataMember]
		public int Count50mb { get; set; }
		[DataMember]
		public int Count100mb { get; set; }
	}
}