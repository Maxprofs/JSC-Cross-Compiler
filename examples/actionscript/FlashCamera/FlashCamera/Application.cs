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
using FlashCamera.Components;
using FlashCamera.HTML.Pages;

namespace FlashCamera
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // Initialize MySprite1
            new MySprite1().AttachSpriteTo(page.Content);

        }

    }
}
