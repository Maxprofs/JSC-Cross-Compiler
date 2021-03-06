using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestFormGenerics.HTML.Pages;

namespace TestFormGenerics
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationApplet applet = new ApplicationApplet();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            applet.AutoSizeAppletTo(page.ContentSize);
            applet.AttachAppletTo(page.Content);
            @"Hello world".ToDocumentTitle();

            applet.SendStringViaGeneric +=
                e =>
                {
                    // Send data from JavaScript to the server tier
                    service.WebMethod2(
                        e,
                        value => value.ToDocumentTitle()
                    );
                };
        }

    }
}
