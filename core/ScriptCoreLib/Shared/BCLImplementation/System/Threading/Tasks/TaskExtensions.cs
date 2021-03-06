using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Threading.Tasks
{
    // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\TaskExtensions.cs

    // http://referencesource.microsoft.com/#System.Core/System/Threading/Tasks/TaskExtensions.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskExtensionsImpl.cs
    // https://githubcom/mono/mono/blob/master/mcs/class/System.Core/System.Threading.Tasks/TaskExtensions.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskExtensions))]
    public static class __TaskExtensions
    {
        // X:\jsc.svn\examples\java\android\gles\AndroidOpenGLESLesson6Activity\AndroidOpenGLESLesson6Activity\Shaders\ApplicationSurface.cs

        // X:\jsc.svn\examples\java\hybrid\async\Test\JVMCLRUnwrap\JVMCLRUnwrap\Program.cs

        // could this be part of .async instead?
        // called by?
        //   // ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks.__Task`1+<>c__DisplayClass1_0.<ContinueWith>b__0



        public static Task<TResult> Unwrap<TResult>(Task<Task<TResult>> task)
        {
            return Unwrap(task, "");
        }

        public static Task<TResult> Unwrap<TResult>(Task<Task<TResult>> task, string Trace)
        {
            // X:\jsc.svn\examples\javascript\async\test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs

            //Console.WriteLine("enter TaskExtensions.Unwrap");

            // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

            //async worker done0:3872ms 
            //0:3872ms Task ContinueWithResult
            //0:3873ms async worker running ? { xTask = [object Object] }
            //Uncaught TypeError: undefined is not a function 
            var x = new TaskCompletionSource<TResult>();

#if FIXED
            // Z:\jsc.svn\examples\javascript\GoogleMapsMarker\GoogleMapsMarker\Application.cs


			task.ContinueWith(
				r =>
				{
					//Console.WriteLine("enter TaskExtensions.Unwrap Task<Task<TResult>> ContinueWith");

					var xResultTask = r.Result;

					//var isTaskOfT = xTask is Task<object>;
					//Console.WriteLine("async worker running ? " + new { xTask, isTaskOfT });

					// are we in a wrong function?
					if (!(((object)xResultTask) is Task))
					{
						throw new Exception("bugcheck TaskExtensions.Unwrap Task<Task> " + new { xResultTask, t = xResultTask.GetType(), Trace });
					}

					xResultTask.ContinueWith(
						rr =>
						{
							x.SetResult(
								rr.Result
							);
						}
					);
				}
			);
#endif

            return x.Task;
        }


        public static Task Unwrap(Task<Task> task)
        {
            return Unwrap(task, "");
        }

        public static Task Unwrap(Task<Task> task, string Trace)
        {
            //Console.WriteLine("enter TaskExtensions.Unwrap");

            // X:\jsc.svn\examples\javascript\async\test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs
            // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs
            // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

            //async worker done0:3872ms 
            //0:3872ms Task ContinueWithResult
            //0:3873ms async worker running ? { xTask = [object Object] }
            //Uncaught TypeError: undefined is not a function 
            var x = new TaskCompletionSource<object>();

#if FIXED
			task.ContinueWith(
				(Task<Task> r) =>
				{
					//Console.WriteLine("enter TaskExtensions.Unwrap Task<Task> ContinueWith");


					var xResultTask = r.Result;

					// are we in a wrong function?
					if (!(((object)xResultTask) is Task))
					{
						throw new Exception("bugcheck TaskExtensions.Unwrap Task<Task> " + new { xResultTask, Trace });
					}

					xResultTask.ContinueWith(
						rr =>
						{
							x.SetResult(
								new object()
							);
						}
					);
				}
			);
#endif


            return x.Task;
        }
    }
}
