﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace OrcasJavaConsoleApplication
{

	[Script]
	public static class Extensions
	{
		public static void ToConsole(this string text)
		{
			Console.WriteLine(text);
		}
	}
}
