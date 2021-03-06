﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IContainer))]
	internal interface __IContainer : IDisposable
	{
        // X:\jsc.svn\examples\javascript\android\AndroidPINForm\AndroidPINForm\Library\Form1.Designer.cs

		ComponentCollection Components { get; }

		void Add(IComponent component);
	
		void Add(IComponent component, string name);

		void Remove(IComponent component);
	}
}
