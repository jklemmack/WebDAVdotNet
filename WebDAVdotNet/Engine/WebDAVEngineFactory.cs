using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDAVdotNet
{
    public abstract class WebDAVEngineFactoryBase
    {
        public abstract WebDAVEngineBase CreateEngine();
    }
}
