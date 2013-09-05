﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://bugzilla.mozilla.org/show_bug.cgi?id=677638
    [Script(HasNoPrototype = true, ExternalTarget = "MessageChannel")]
    public class MessageChannel
    {
        public readonly MessagePort port1;
        public readonly MessagePort port2;

    }
}
