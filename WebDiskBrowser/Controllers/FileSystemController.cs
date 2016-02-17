using System;
using System.Collections.Generic;
using System.IO;
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
			var defaultPath = _fsManager.ReturnDesktopPath();
			var methods = new List<Func<FileInfo, bool>>()
			{
				entry => entry.Length < 10485760,
				entry => entry.Length > 10485760 && entry.Length < 52428800,
				entry => entry.Length > 104857600
			};
			viewModel = _fsManager.TraverseAvailableDirInfo(defaultPath, methods);
			if (viewModel == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Failed to find requested file/category");
			}
			viewModel.Drives = _fsManager.ReturnDrives();
			return Request.CreateResponse(HttpStatusCode.OK, viewModel);
		}

		[HttpGet]
		[ActionName("new")]
		public HttpResponseMessage GetNew(string path)
		{
			var viewModel = new FileSystemViewModel();
			var methods = new List<Func<FileInfo, bool>>()
			{
				entry => entry.Length < 10485760,
				entry => entry.Length > 10485760 && entry.Length < 52428800,
				entry => entry.Length > 104857600
			};
			viewModel = _fsManager.TraverseAvailableDirInfo(path, methods);
			if (viewModel == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Failed to find requested file/category");
			}
			else return Request.CreateResponse(HttpStatusCode.OK, viewModel);
		}
	}
}