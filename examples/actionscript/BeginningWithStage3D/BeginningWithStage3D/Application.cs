using BeginningWithStage3D.Design;
using BeginningWithStage3D.HTML.Pages;
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
using ScriptCoreLib.ActionScript.flash.display;

namespace BeginningWithStage3D
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // Initialize ApplicationSprite
            //new IHTMLDiv { innerText = "wmode set" }.AttachToDocument();

            //sprite.wmode();
            //sprite.AtMessage +=
            //    e =>
            //    {
            //        new IHTMLDiv { innerText = e }.AttachToDocument();
            //    };

            sprite.AttachSpriteTo(page.Content);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    //static class X
    //{
    //    public static void wmode(this Sprite s, string value = "direct")
    //    {
    //        var x = s.ToHTMLElement();

    //        var p = x.parentNode;
    //        if (p != null)
    //        {
    //            // if we continue, element will be reloaded!
    //            return;
    //        }

    //        x.setAttribute("wmode", value);


    //    }
    //}
}
