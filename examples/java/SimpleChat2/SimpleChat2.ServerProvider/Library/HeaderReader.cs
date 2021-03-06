﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleChat2.ServerProvider.Library
{
	public class HeaderReader
	{
		public delegate void KeyValuePairDelegate(string key, string value);

		public event KeyValuePairDelegate Method;
		public event KeyValuePairDelegate Header;

		public StreamReader Reader;

		public void Read(Stream s)
		{
			var r = new StreamReader(s);
			this.Reader = r;

			var h = r.ReadLine();

			if (string.IsNullOrEmpty(h))
				return;

			var Method_i = h.IndexOf(" ");
			var Method_key = h.Substring(0, Method_i);
			var Method_value = h.Substring(Method_i + 1, h.IndexOf(" ", Method_i + 1) - (Method_i + 1));

			if (Method != null)
				Method(Method_key, Method_value);

			while (!string.IsNullOrEmpty(h))
			{
				h = r.ReadLine();

				if (!string.IsNullOrEmpty(h))
				{
					var Header_key = h.Substring(0, h.IndexOf(":"));
					var Header_value = h.Substring(h.IndexOf(":") + 1).Trim();

					if (Header != null)
						Header(Header_key, Header_value);
				}
			}
		}
	}

}
