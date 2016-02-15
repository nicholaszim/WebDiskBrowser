using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDiskBrowser.Managers;
using WebDiskBrowser.Models;

namespace WebDiskBrowser.Controllers
{
    public class FileSystemController : ApiController
    {
		private FileSystemManager _fsManager;

		public FileSystemController()
		{
			_fsManager = new FileSystemManager();
		}
		[HttpGet]
		[ActionName("default")]
		public HttpResponseMessage GetAll()
		{
			var viewModel = new FileSystemViewModel();
			var drives = _fsManager.ReturnDrives();
			var defaultPath = _fsManager.ReturnDesktopPath();
			var dirName = _fsManager.ReturnDirectoryName(defaultPath);
			var fileSysEntriesInfo = _fsManager.ReturnFileSystemEntriesInfo(defaultPath);
			var files = _fsManager.ReturnFilesInfo(defaultPath);
			var folders = _fsManager.ReturnFolders(defaultPath);
			var less10mbcount = _fsManager.ReturnCount(defaultPath, entry => entry.Length < 10485760);
			var less50mbcount = _fsManager.ReturnCount(defaultPath, entry => entry.Length > 10485760 && entry.Length < 52428800);
			var morethan100mb = _fsManager.ReturnCount(defaultPath, entry => entry.Length > 104857600);
			viewModel.Drives = drives;
			viewModel.DirectoryName = dirName;
			viewModel.DirectoryPath = defaultPath;
			viewModel.Entries = fileSysEntriesInfo;
			viewModel.Files = files;
			viewModel.Folders = folders;
			viewModel.Count10mb = less10mbcount;
			viewModel.Count50mb = less50mbcount;
			viewModel.Count100mb = morethan100mb;
			return Request.CreateResponse(HttpStatusCode.OK, viewModel);
		}
		[HttpGet]
		[ActionName("current")]
		public HttpResponseMessage GetCurrentDirectory(string path)
		{
			var dirName = _fsManager.ReturnDirectoryName(path);
			if (dirName == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			else return Request.CreateResponse(HttpStatusCode.OK, dirName);
		}
		[HttpGet]
		[ActionName("new")]
		public HttpResponseMessage GetNew(string path)
		{
			var fileSysEntriesInfo = _fsManager.ReturnFileSystemEntriesInfo(path);
			var files = _fsManager.ReturnFilesInfo(path);
			var folders = _fsManager.ReturnFolders(path);
			var less10mbcount = _fsManager.ReturnCount(path, entry => entry.Length < 10485760);
			var less50mbcount = _fsManager.ReturnCount(path, entry => entry.Length > 10485760 && entry.Length < 52428800);
			var morethan100mb = _fsManager.ReturnCount(path, entry => entry.Length > 104857600);
			if (fileSysEntriesInfo == null || less10mbcount == -1 || less50mbcount == -1 || morethan100mb == -1)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			else return Request.CreateResponse(HttpStatusCode.OK, 
				new FileSystemViewModel { Entries = fileSysEntriesInfo, Files= files, Folders = folders, Count10mb = less10mbcount, Count50mb = less50mbcount, Count100mb = morethan100mb });
		}
		//private FileSystemManager _fsManager;

		//public FileSystemController()
		//{
		//	_fsManager = new FileSystemManager();
		//}

		///// <summary>
		///// Gets an array of strings (directories and files in directory) or warning (if error) + status code as output.
		///// </summary>
		///// <param name="path"></param>
		///// <returns></returns>
		//public HttpResponseMessage Get(string path)
		//{
		//	var warning = "";
		//	var files = _fsManager.TryGetFiles(path, out warning);
		//	var dirs = _fsManager.TryGetDirectories(path, out warning);
		//	if(files == null || dirs == null)
		//	{
		//		return Request.CreateResponse(HttpStatusCode.NotFound, warning);
		//	}
		//	else
		//	{
		//		var output = dirs.Concat(files);
		//		return Request.CreateResponse(HttpStatusCode.OK, output);
		//	}
		//}
		//public HttpResponseMessage GetDefaultPath()
		//{
		//	var desktopPath = _fsManager.GetDesktopPath();
		//	return Request.CreateResponse(HttpStatusCode.OK, desktopPath);
		//}
	}
}