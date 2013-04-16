using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebDAVMVC
{
    public class MyFolder : WebDAVdotNet.IFolder
    {
        private string _path;
        public override string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        public override IEnumerable<WebDAVdotNet.IHierarchyItem> GetChildren()
        {
            return GetChildren(null);
        }

        public override IEnumerable<WebDAVdotNet.IHierarchyItem> GetChildren(IList<WebDAVdotNet.PropertyName> properties)
        {
            List<WebDAVdotNet.IHierarchyItem> returnItems = new List<WebDAVdotNet.IHierarchyItem>();
            string[] directories = Directory.GetDirectories(_path);
            foreach (string directory in directories)
            {
                MyFolder myFolder = (MyFolder)MyEngine.GetResource(directory, properties);
                returnItems.Add(myFolder);
            }

            string[] files = Directory.GetFiles(_path);
            foreach (string file in files)
            {
                MyFile myFile = (MyFile)MyEngine.GetResource(file, properties);
                returnItems.Add(myFile);
            }

            return returnItems;
        }

        public MyFolder()
            : base()
        {

        }
    }
}