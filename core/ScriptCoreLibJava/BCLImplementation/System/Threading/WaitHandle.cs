﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/WaitHandle.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/WaitHandle.cs
    // https://github.com/dot42/api/blob/master/System/Threading/WaitHandle.cs

    [Script(Implements = typeof(global::System.Threading.WaitHandle))]
    internal class __WaitHandle
    {
        // tested by?

        public virtual bool WaitOne(int millisecondsTimeout)
        {
            return false;
        }

        public virtual bool WaitOne()
        {
            return false;
        }
    }
}
