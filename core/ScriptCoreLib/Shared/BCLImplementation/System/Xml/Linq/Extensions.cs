﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/Extensions.cs

    [Script(Implements = typeof(global::System.Xml.Linq.Extensions))]
	internal static class __Extensions
	{
        public static IEnumerable<XElement> Elements<T>(IEnumerable<T> source) where T : XContainer
        {
            return source.SelectMany(k => k.Elements());
        }

		public static IEnumerable<XElement> Elements<T>(IEnumerable<T> source, XName name) where T : XContainer
		{
			return source.SelectMany(k => k.Elements(name));
		}
	}
}
