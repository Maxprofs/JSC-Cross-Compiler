﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Components;

namespace ScriptCoreLib.JavaScript.Components
{
	public class SolutionDocumentViewerTab : ISupportsActivation
	{
		string InternalText;
		public string Text
		{
			get
			{
				return InternalText;
			}
			set
			{
				InternalText = value;
				if (Changed != null)
					Changed();
			}
		}

		public event Action Activated;
		public void RaiseActivated()
		{
			if (Activated != null)
				Activated();
		}

		public event Action Deactivated;
		public void RaiseDeactivated()
		{
			if (Deactivated != null)
				Deactivated();
		}


		public event Action Changed;
		public static implicit operator SolutionDocumentViewerTab(string Text)
		{
			return new SolutionDocumentViewerTab { Text = Text };
		}

		public Action Activate;


		public IHTMLElement TabElement;
	}

}
