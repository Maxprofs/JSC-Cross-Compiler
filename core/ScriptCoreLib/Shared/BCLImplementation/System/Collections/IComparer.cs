﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IComparer))]
    public interface __IComparer
    {
        int Compare(object x, object y);

    }
}
