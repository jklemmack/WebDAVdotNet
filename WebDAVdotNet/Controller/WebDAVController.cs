using System;
using System.Collections.Generic;
using System.Xml;
using System.Web.Mvc;

namespace WebDAVdotNet
{
    public class WebDAVController : Controller
    {
        [AcceptVerbs("GET"), ActionName("WebDAVMethod")]
        public virtual ActionResult Get(string resource, WebDAVEngineFactoryBase factory)
        {
            WebDAVEngineBase engine = factory.CreateEngine();
            engine.CurrentAction = WebDAVEngineBase.Action.Get;
            //return ActionResults.GetResult.CreateGetResult(engine, resource);
            return new ActionResults.GetResult(engine);
        }

        [AcceptVerbs("HEAD"), ActionName("WebDAVMethod")]
        public virtual ActionResult Head(string resource, WebDAVEngineFactoryBase factory)
        {
            WebDAVEngineBase engine = factory.CreateEngine();
            engine.CurrentAction = WebDAVEngineBase.Action.Head;
            return new ActionResults.HeadResult(engine);
        }

        [AcceptVerbs("POST"), ActionName("WebDAVMethod")]
        public virtual ActionResult Post(string resource, WebDAVEngineFactoryBase factory)
        {
            return null;
        }



        [AcceptVerbs("PROPFIND"), ActionName("WebDAVMethod")]
        public virtual ActionResult PropFind(string resource, WebDAVEngineFactoryBase factory)
        {
            WebDAVEngineBase engine = factory.CreateEngine();
            engine.CurrentAction = WebDAVEngineBase.Action.PropFind_Prop;

            return new ActionResults.PropFindResult(engine);
        }

        [AcceptVerbs("PROPPATCH"), ActionName("WebDAVMethod")]
        public virtual ActionResult PropPatch()
        {
            return null;
        }

        [AcceptVerbs("MKCOL"), ActionName("WebDAVMethod")]
        public virtual ActionResult MkCol()
        {
            return null;
        }

        [AcceptVerbs("DELETE"), ActionName("WebDAVMethod")]
        public virtual ActionResult Delete()
        {
            return null;
        }

        [AcceptVerbs("PUT"), ActionName("WebDAVMethod")]
        public virtual ActionResult Put()
        {
            return null;
        }

        [AcceptVerbs("COPY"), ActionName("WebDAVMethod")]
        public virtual ActionResult Copy()
        {
            return null;
        }

        [AcceptVerbs("MOVE"), ActionName("WebDAVMethod")]
        public virtual ActionResult Move()
        {
            return null;
        }

        [AcceptVerbs("LOCK"), ActionName("WebDAVMethod")]
        public virtual ActionResult Lock()
        {
            return null;
        }

        [AcceptVerbs("UNLOCK"), ActionName("WebDAVMethod")]
        public virtual ActionResult Unlock()
        {
            return null;
        }


    }
}
