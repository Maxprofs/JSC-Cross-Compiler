﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Avalon;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class INodeExtensions
	{


        //public static T SizeTo<T>(this T e, int w, int h) where T : IHTMLElement
        //{
        //    e.style.width = w + "px";
        //    e.style.height = w + "px";
        //    return e;
        //}

        public static IHTMLOption GetSelection(this IHTMLSelect c)
        {
            return c[c.selectedIndex];
        }

        public static string GetSelectionText(this IHTMLSelect c)
        {
            return c.GetSelection().value;
        }


        public static void SetMatrixTransform(this IHTMLElement c, AffineTransformBase matrix)
        {
            c.style.SetMatrixTransform(matrix);
        }

		public static IHTMLDiv WithinContainer(this INode e)
		{
			var x = new IHTMLDiv { e };

			x.style.width = "100%";
			x.style.height = "100%";

			return x;
		}


		public static IHTMLIFrame WhenContentReady(this IHTMLIFrame that, Action<IHTMLBody> y)
		{
			return that.WhenDocumentReady(
				doc =>
				{
					doc.WhenContentReady(y);
				}
			);
		}


		public static void WhenContentReady(this IHTMLDocument doc, Action<IHTMLBody> y)
		{
			if (doc.body != null)
			{
				y(doc.body);
			}
			else
			{
				new Timer(
					t =>
					{
						if (doc.body == null)
							return;

						t.Stop();

						y(doc.body);
					}
				).StartInterval(15);
			}
		}

		public static IHTMLIFrame WhenDocumentReady(this IHTMLIFrame that, Action<IHTMLDocument> y)
		{
			new Timer(
				t =>
				{
					if (that.contentWindow == null)
						return;

					if (that.contentWindow.document == null)
						return;

					t.Stop();

					y(that.contentWindow.document);
				}
			).StartInterval(15);

			return that;
		}



        public static void ReplaceContentWith(this IHTMLElement parent, INode value)
		{
			parent.Clear();
			parent.Add(value);
		}

        public static T AttachToHead<T>(this T Content) where T : IHTMLElement
        {
            var h = Native.Document.getElementsByTagName("head");

            if (h.Length > 0)
                h[0].appendChild(Content);
            else
                Content.AttachToDocument();

            return Content;
        }
	}


}
