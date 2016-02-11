using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDiskBrowser.Managers;

namespace WebDiskBrowser.Controllers
{
    public class FileSystemController : ApiController
    {
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