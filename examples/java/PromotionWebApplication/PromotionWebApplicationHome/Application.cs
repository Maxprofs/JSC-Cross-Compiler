// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using PromotionWebApplicationHome.HTML.Pages;
using PromotionWebApplicationHome;
using ScriptCoreLib.Shared.Drawing;
using PromotionWebApplicationHome.Components;

namespace PromotionWebApplicationHome
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page)
        {
            @"jsc".ToDocumentTitle();

            page.AnimateHomePage();

            new ApplicationWebService().WebMethod2(
                new XElement(@"Document",
                    new object[] {
						new XElement(@"Data", 
							new object[] {
								@"Hello world"
							}
						),
						new XElement(@"Client", 
							new object[] {
								@"Unchanged text"
							}
						)
					}
                ),
                delegate(XElement doc)
                {
                    doc.Element(@"Data").Value.ToDocumentTitle();
                }
            );
        }


    }
}
