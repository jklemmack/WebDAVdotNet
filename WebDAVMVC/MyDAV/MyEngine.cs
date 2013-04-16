using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebDAVMVC
{
    public class MyEngineFactory : WebDAVdotNet.WebDAVEngineFactoryBase
    {
        public override WebDAVdotNet.WebDAVEngineBase CreateEngine()
        {
            //return new MyEngine(Directory.GetCurrentDirectory());
            return new MyEngine(@"C:\Users\Johann\Downloads");
        }
    }

    public class MyEngine : WebDAVdotNet.WebDAVEngineBase
    {
        private string _rootPath;

        public MyEngine(string rootPath)
        {
            _rootPath = rootPath;
        }

        public override IEnumerable<WebDAVdotNet.IHierarchyItem> GetResources(IList<string> items, IList<WebDAVdotNet.PropertyName> properties, WebDAVdotNet.Headers.Depth depth)
        {

            List<WebDAVdotNet.IHierarchyItem> returnItems = new List<WebDAVdotNet.IHierarchyItem>();

            foreach (string path in items)
            {

                string mappedPath = RewriteURLToLocalPath(_rootPath, path);

                WebDAVdotNet.IHierarchyItem item = GetResource(mappedPath, properties);
                if (item is MyFolder && depth == WebDAVdotNet.Headers.Depth.One)
                {
                    returnItems.AddRange(((MyFolder)item).GetChildren(properties));
                }

                returnItems.Add(item);
            }

            foreach (WebDAVdotNet.IHierarchyItem item in returnItems)
            {
                item.Path = RewriteLocalPathToURL(_rootPath, item.Path, (item is WebDAVdotNet.IFolder));
            }
            return returnItems;
        }

        public override void WriteFileToStream(WebDAVdotNet.IFile file, Stream outputStream)
        {
            //throw new NotImplementedException();
            using (Stream inStream = File.Open(MyEngine.RewriteURLToLocalPath(this._rootPath, file.Path), FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[16 * 1024];
                int bytesRead = 0;
                while ((bytesRead = inStream.Read(buffer, 0, 16 * 1024)) > 0)
                    outputStream.Write(buffer, 0, bytesRead);
            }
            //return new BufferedStream(System.IO.File.Open(MyEngine.RewriteURLToLocalPath(this._rootPath, file.Path), FileMode.Open, FileAccess.Read), 1024 * 32);
        }

        internal static WebDAVdotNet.IHierarchyItem GetResource(string path, IList<WebDAVdotNet.PropertyName> properties)
        {
            if (File.Exists(path))
            {
                MyFile file = new MyFile();
                FileInfo fi = new FileInfo(path);
                file.Name = fi.Name;
                file.ContentLength = fi.Length;
                file.CreatedDate = fi.CreationTimeUtc;
                file.LastModified = fi.LastWriteTimeUtc;
                file.Path = fi.FullName;
                return file;
            }
            else if (Directory.Exists(path))
            {
                MyFolder folder = new MyFolder();
                DirectoryInfo di = new DirectoryInfo(path);
                folder.Name = di.Name;
                folder.CreatedDate = di.CreationTimeUtc;
                folder.LastModified = di.LastWriteTimeUtc;
                folder.Path = di.FullName;

                return folder;
            }
            return null;
        }

        internal static string RewriteURLToLocalPath(string rootPath, string path)
        {
            // make a relative path to the web server root
            if (VirtualPathUtility.IsAbsolute(path))
                path = VirtualPathUtility.MakeRelative("~", path);

            path = HttpUtility.UrlDecode(path);     //Translate any encoded chars
            path = path.Replace("/", "\\");         // flip the directory delimiters
            path = Path.Combine(rootPath, path);    // map to our root path on the file system
            path = Path.GetFullPath(path);          // cleans up wierdness
            return path;
        }

        internal static string RewriteLocalPathToURL(string rootPath, string path, bool isFolder)
        {
            Uri baseUri = new Uri(rootPath);
            Uri newPath = new Uri(path);
            string returnPath = baseUri.MakeRelativeUri(newPath).ToString();
            returnPath = returnPath.Substring(returnPath.IndexOf("/"));
            returnPath = returnPath.Replace("\\", "/");
            if (isFolder) returnPath += "/";    //tack on a trailing slash for folders
            return returnPath;
        }

    }
}