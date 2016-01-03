﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // Z:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\IntPtr.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\IntPtr.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\IntPtr.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IntPtr.cs


	[Script(Implements = typeof(global::System.IntPtr))]
	internal class __IntPtr
	{
        // http://www.symantec.com/content/en/us/enterprise/media/security_response/whitepapers/regin-analysis.pdf

		public static int Size
		{
			[Script(OptimizedCode = "return sizeof(void*);")]
			get
			{
				return default(int);
			}
		}

		[Script(OptimizedCode = "return a==b;")]
		static public bool operator ==(__IntPtr a, __IntPtr b)
		{
			return default(bool);
		}

		[Script(OptimizedCode = "return a!=b;")]
		static public bool operator !=(__IntPtr a, __IntPtr b)
		{
			return default(bool);
		}

		public override bool Equals(object obj)
		{
			return this == obj as __IntPtr;
		}

		public override int GetHashCode()
		{
			return default(int);
		}
	}

}
