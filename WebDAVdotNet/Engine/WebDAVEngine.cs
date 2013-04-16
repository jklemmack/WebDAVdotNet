using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebDAVdotNet
{
    public abstract class WebDAVEngineBase
    {
        /// <summary>
        /// Enumeration of all WebDAV-defined actions
        /// </summary>
        public enum Action
        {
            PropFind_Prop,
            PropFind_Propname,
            PropFind_Allprop,
            PropPatch,
            MkCol,
            Get,
            Head,
            Post,
            Delete,
            Put,
            Copy,
            Move,
            Lock,
            Unlock
        }

        public abstract IEnumerable<IHierarchyItem> GetResources(IList<string> items, IList<PropertyName> properties, Headers.Depth depth);

        /// <summary>
        /// Writes the file to the provided output stream.  This action 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="outputStream"></param>
        public abstract void WriteFileToStream(IFile file, Stream outputStream);

        public virtual Stream GetFolderResourceStream(IFolder folder)
        {
            //Default implementation - get children & report out some basic HTML
            return null;
        }

        public Action CurrentAction { get; internal set; }
    }
}
