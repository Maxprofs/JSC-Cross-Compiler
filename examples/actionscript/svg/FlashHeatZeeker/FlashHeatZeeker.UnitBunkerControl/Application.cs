using FlashHeatZeeker.UnitBunkerControl.Design;
using FlashHeatZeeker.UnitBunkerControl.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
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

namespace FlashHeatZeeker.UnitBunkerControl
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
        public Application(IApp page)
        {
            sprite.wmode();

            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.window.Width, Native.window.Height);

                       Native.window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.window.Width, Native.window.Height);
                           };
                   }
               );
        }

    }


    public static class XX
    {


        public static void wmode(this Sprite s, string value = "direct")
        {
            var x = s.ToHTMLElement();

            var p = x.parentNode;
            if (p != null)
            {
                // if we continue, element will be reloaded!
                return;
            }

            x.setAttribute("wmode", value);


        }
    }
}
