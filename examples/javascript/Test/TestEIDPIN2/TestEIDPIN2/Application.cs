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
using TestEIDPIN2;
using TestEIDPIN2.Design;
using TestEIDPIN2.HTML.Pages;

namespace TestEIDPIN2
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
            // Invalid request. This video was rejected.
            // youtube videos need to be landscape
            // The video is a duplicate of a video you already uploaded.


            // X:\jsc.svn\examples\javascript\Test\TestDynamicCall\TestDynamicCall\Application.cs
            // X:\jsc.svn\examples\java\hybrid\JVMCLRTCPMultiplex\JVMCLRTCPMultiplex\Program.cs


            //  Signing software is available from https://installer.id.ee]

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141002
            //new IHTMLAnchor { href = "https://openxades.org/web_sign_demo/sign.html", innerText = "web_sign_demo" }.AttachToDocument();


            // // It is checked if the connection is https during the signing module loading

            // X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\Program.cs

            // https://code.google.com/p/chromium/issues/detail?id=412681
            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs

            // I/chromium(11925): [INFO:CONSOLE(62)] "probe() detected application/x-digidoc", source: 
            // https://id.swedbank.ee/shared/digisign/hwcrypto.js?1429603200000 (62)
            // view-source:https://id.swedbank.ee/shared/digisign/hwcrypto.js?1429603200000
            // view-source:https://id.swedbank.ee/shared/digisign/idcard.js?1429520400000

            // view-source:https://id.lhv.ee/gfxv79/js/ibank/hwcrypto-lhv.js

            //if (navigator.mimeTypes && navigator.mimeTypes.length)
            //{
            //    if (navigator.mimeTypes[pluginName])
            //    {

            //Native.window.navigator.mimeTypes

            // where else have we tested it?


            //{{ type = application/pdf, description = Portable Document Format }}
            //{{ type = application/x-google-chrome-print-preview-pdf, description = Portable Document Format }}


            // Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2369.0 Safari/537.36
            new IHTMLPre { Native.window.navigator.userAgent }.AttachToDocument();
            Native.body.style.backgroundColor = "yellow";

            // !! actually IE wont report anything here.
            new IHTMLButton { "list mimeTypes!" }.AttachToDocument().onclick +=
             e =>
             {

                 Native.window.navigator.mimeTypes.ToArray().AsEnumerable().WithEach(
                     x =>
                     {
                         new IHTMLPre {
                                new { x.type, x.description}
                            }.AttachToDocument();


                     }
                 );
             };



            new IHTMLButton { "use the API!" }.AttachToDocument().onclick +=
                e =>
                {
                    new IHTMLObject
                    {
                        type = "application/x-digidoc",
                        height = 40
                    }.AttachToDocument().With(
                        (dynamic plugin) =>
                        {
                            if (plugin.version == null)
                            {
                                // user can also right click on plugin, but this wont help us as we wont know
                                new IHTMLPre { "allow plugin, then retry." }.AttachToDocument();
                                return;
                            }

                            plugin.pluginLanguage = "en";


                            // {{ version = null }}
                            // {{ version = 3.5.5273.321 }}

                            new IHTMLPre {
                                new { plugin.version }
                            }.AttachToDocument();




                            // U:\chromium\src\third_party\WebKit\Source\core\html\HTMLObjectElement.cpp

                            new IHTMLButton { ".getCertificate()" }.AttachToDocument().onclick +=
                                ee =>
                                {
                                    dynamic cert = plugin.getCertificate();


                                    new IHTMLPre {
                                        new { cert }
                                    }.AttachToDocument();

                                    new IHTMLPre {
                                        new { cert.id, cert.CN, cert.issuerCN }
                                    }.AttachToDocument();

                                    // we get a dialog
                                    // and then NAME,SSN
                                    // it works. what else can we do?

                                    new IHTMLButton { ".sign()" }.AttachToDocument().onclick +=
                                        eee =>
                                        {
                                            var hash = "FAFA0101FAFA0101FAFA0101FAFA0101FAFA0101";

                                            dynamic signature = plugin.sign(cert.id, hash, "");

                                            // signature is a long hex string!

                                            new IHTMLCode {
                                                new { signature },

                                                //new IStyle { color = "blue" }
                                            }.AttachToDocument();

                                            // we get a dialog
                                            // and then NAME,SSN
                                            // it works. what else can we do?
                                        };
                                };



                        }
                    );




                };
        }

    }
}
