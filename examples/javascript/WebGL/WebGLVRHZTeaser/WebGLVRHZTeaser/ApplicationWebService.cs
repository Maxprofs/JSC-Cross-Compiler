using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebGLVRHZTeaser
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //        F/libc(27439): Fatal signal 11 (SIGSEGV), code 1, fault addr 0x8 in tid 27484 (Thread-280)
        //I/libc(27439): Suppressing debuggerd output because prctl(PR_GET_DUMPABLE)==0
        //I/WindowState(  459): WIN DEATH: Window{2f487af6 u0 WebGLVRHZTeaser.Activities/WebGLVRHZTeaser.Activities.ApplicationWebServiceActivity
        //    }
        //    I/Zygote(  218): Process 27439 exited due to signal(11)
        //I/ActivityManager(  459): Process WebGLVRHZTeaser.Activities(pid 27439) has died
        //W/ActivityManager(  459): Scheduling restart of crashed service WebGLVRHZTeaser.Activities/.ApplicationWebServiceXWidgetsWindow in 1000ms
        //W/ActivityManager(  459): Force removing ActivityRecord{2e6a3104 u0 WebGLVRHZTeaser.Activities/.ApplicationWebServiceActivity t289}: app died, no saved state
    }
}