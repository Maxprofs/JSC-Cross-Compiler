﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;

namespace Designer1Forms
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            DesktopFormsExtensions.Launch(() => new ApplicationControl());
#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

       
    }
}
