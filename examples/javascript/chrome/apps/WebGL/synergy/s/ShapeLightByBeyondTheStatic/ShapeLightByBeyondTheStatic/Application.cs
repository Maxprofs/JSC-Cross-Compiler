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
using ShapeLightByBeyondTheStatic;
using ShapeLightByBeyondTheStatic.Design;
using ShapeLightByBeyondTheStatic.HTML.Pages;

namespace ShapeLightByBeyondTheStatic
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // lets tune down the loggin
        // ChromeShaderToyColumns

        //        C:\Windows\system32>netsh wlan stop hostednetwork
        //The hosted network stopped.



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150807/shadertoy
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150908/shapelight

            // https://www.shadertoy.com/view/XtfXRX


            #region += Launched chrome.app.window
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");

                    // pass thru
                }
                else
                {
                    // should jsc send a copresence udp message?
                    chrome.runtime.UpdateAvailable += delegate
                    {
                        new chrome.Notification(title: "UpdateAvailable");

                    };

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "ChromeUDPSendAsync");

                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: null
                        );

                        //xappwindow.setAlwaysOnTop

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion


            Native.body.Clear();
            ChromeShaderToyColumns.Library.ShaderToy.AttachToDocument(
                new Shaders.ProgramFragmentShader()
            );
        }

    }
}
 