using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDAVMVC
{
    public class MyFile : WebDAVdotNet.IFile
    {
        private string _path = null;

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

        public override long Read(System.IO.Stream output, long startIndex, long length)
        {
            throw new NotImplementedException();
        }

        public override long Read(System.IO.Stream output)
        {
            throw new NotImplementedException();
        }

        public override bool Write(System.IO.Stream input, string contentType, long startIndex, long length)
        {
            throw new NotImplementedException();
        }
    }
}