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
using TestTaskNotCompleteWithState;
using TestTaskNotCompleteWithState.Design;
using TestTaskNotCompleteWithState.HTML.Pages;

namespace TestTaskNotCompleteWithState
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
            //new IHTMLButton { "Invoke" }
            //    .AttachToDocument()
            //    .WhenClicked(() => base.Invoke());


            new IHTMLButton { "Invoke" }
                .AttachToDocument()
                .WhenClicked(
                    async button =>
                    {

                        await base.Invoke();
                    }
                );

        }

    }
}
