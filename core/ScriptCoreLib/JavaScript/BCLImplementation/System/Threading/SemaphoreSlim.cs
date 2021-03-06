﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading
{
    // http://msdn.microsoft.com/en-us/library/system.threading.SemaphoreSlim(v=vs.110).aspx

    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/SemaphoreSlim.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\SemaphoreSlim.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\SemaphoreSlim.cs

    [Script(Implements = typeof(global::System.Threading.SemaphoreSlim))]
    public class __SemaphoreSlim
    {
        // https://github.com/dotnet/corert/blob/master/src/System.Private.Threading/src/System/Threading/SemaphoreSlim.cs



        // set by thread manager, async scope manager
        public string Name;

        public int CurrentCount { get; set; }


        // tested by?
        // X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
        // would this type allow to jump back from another thread?

        // X:\jsc.svn\examples\javascript\async\Test\TestGetUserMedia\TestGetUserMedia\Application.cs

        public __SemaphoreSlim(int c)
        {
            this.CurrentCount = c;
        }

        // X:\jsc.svn\examples\javascript\async\Test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs
        // set by thread hopper, to indicate, this object is living 
        // in multiple workers, and is connected.
        // for network signals, webrtc/udp needs to be available
        [Obsolete("need to support multiple entanglements, each callsite needs their own copy of this field", true)]
        public bool InternalIsEntangled;

        #region Release
        public event Action InternalVirtualRelease;

        public int Release()
        {
            // are we entangled?
            // if so then we need to send a signal to us in that other thread?
            // what if we were entangled into multiple threads? would need to do round robin
            // for the versions that are awaiting?

            //Console.WriteLine("SemaphoreSlim.Release");

            //37418ms SemaphoreSlim.Release { { InternalIsEntangled = true, ManagedThreadId = 1 } }

            if (InternalVirtualRelease != null)
            {
                // this semaphore was sent to a new worker.
                // now, we are about to signal that new thread.


                InternalVirtualRelease();
            }
            else
            {
                Console.WriteLine("otherwise, stash the release? ");

            }

            return 0;
        }

        public int Release(int releaseCount)
        {
            // X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs

            // call multiple times?
            return Release();
        }
        #endregion


        // X:\jsc.svn\examples\javascript\async\test\TestAsyncLocal\TestAsyncLocal\ApplicationControl.cs



        #region WaitAsync
        public event Action<TaskCompletionSource<object>> InternalVirtualWaitAsync;
        public TaskCompletionSource<object> InternalVirtualWaitAsync0;

        public Task WaitAsync()
        {
            // X:\jsc.svn\examples\javascript\async\test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs
            // X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs
            // X:\jsc.svn\examples\javascript\async\test\TestSemaphoreSlim\TestSemaphoreSlim\ApplicationControl.cs

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeThreadedCameraTracker\ChromeThreadedCameraTracker\Application.cs
            var c = new TaskCompletionSource<object>();

            //Console.WriteLine("SemaphoreSlim.WaitAsync " + new { Name });


            // at this point, the worker thread may not yet have connected back, entangled
            if (InternalVirtualWaitAsync != null)
                InternalVirtualWaitAsync(c);
            else
            {
                // um we need to park, until this semaphore is discovered by
                // a thread
                InternalVirtualWaitAsync0 = c;
            }

            // what if we want to await before the semaphore is to be connected with a worker?
            // SemaphoreSlim.WaitAsync {{ InternalIsEntangled = false, ManagedThreadId = 1 }}

            //:7812/view-source:51433 2873ms [10] worker2 is awaiting{{ bytes1 = [object Uint8ClampedArray] }}
            //2015-04-09 16:31:07.370 :7812/view-source:51433 2873ms [10] SemaphoreSlim.WaitAsync {{ InternalIsEntangled = true, ManagedThreadId = 10 }}
            //2015-04-09 16:31:07.370 :7812/view-source:51433 2874ms [10] worker xSemaphoreSlim.InternalVirtualWaitAsync {{ Name = bytes1sema }}


            return c.Task;
        }
        #endregion


        public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
        {

            return null;
        }


        public static implicit operator SemaphoreSlim(__SemaphoreSlim e)
        {
            return (SemaphoreSlim)(object)e;
        }
        public static implicit operator __SemaphoreSlim(SemaphoreSlim e)
        {
            return (__SemaphoreSlim)(object)e;
        }
    }
}
