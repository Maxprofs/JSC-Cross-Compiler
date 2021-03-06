using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SyntaxAppEngineOrderByThenGroupBy;
using SyntaxAppEngineOrderByThenGroupBy.Design;
using SyntaxAppEngineOrderByThenGroupBy.HTML.Pages;

namespace SyntaxAppEngineOrderByThenGroupBy
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                 async delegate
            {
                var x = await this.WebMethod2();

                new IHTMLPre { new { x } }.AttachToDocument();

            };

            //new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
            //      delegate
            //{
            //    this.WebMethod2(
            //        x => new IHTMLPre { new { x } }.AttachToDocument());

            //};

        }

    }
}
