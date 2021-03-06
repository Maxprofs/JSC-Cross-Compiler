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
using TestTypeOfArray;
using TestTypeOfArray.Design;
using TestTypeOfArray.HTML.Pages;

namespace TestTypeOfArray
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
			// X:\jsc.svn\examples\javascript\test\TestDynamicToArray\TestDynamicToArray\Application.cs
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401

			// Uncaught ReferenceError: type$AAAAAAAAAAAAAAAAAAAAAA is not defined
			var a = typeof(object[]);

			// like rewrite cache, we need to reconstruct the typeinfo at runtime
			//var e = typeof(object);
			//var a = e.MakeArrayType();

			// {{ a = Object[] }}

			// if dynamic [] is to be used from ScriptCoreLIB,
			// then, the fix cannot be at rewriter level?

			new IHTMLPre { new { a } }.AttachToDocument();

		}

	}
}
