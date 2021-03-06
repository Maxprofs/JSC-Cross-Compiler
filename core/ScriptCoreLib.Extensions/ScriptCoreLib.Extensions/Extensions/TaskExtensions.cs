﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{


    // name clash!
    public static class ScriptCoreLib_TaskExtensions
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150911/mysql
        // Z:\jsc.svn\examples\javascript\appengine\XSLXAssetWithXElement\XSLXAssetWithXElement\ApplicationWebService.cs




        //[Obsolete]
        //public static void await<T>(this Task<T> x, Action<T> y)
        public static void ContinueWithResult<T>(this Task<T> x, Action<T> y)
        {
            x.ContinueWith(z => y(z.Result));
        }

        sealed class InternalTaskExtensionsScope<TSource, TResult> where TSource : class
        {
            [Obsolete("Special hint for JavaScript runtime, until scope sharing is implemented..")]
            public Func<TSource, TResult> InternalTaskExtensionsScope_function;

            public TResult f(object e)
            {
                return this.InternalTaskExtensionsScope_function((TSource)e);
            }
        }




        //[Obsolete]
        //public static Task<TResult> StartNew<TSource, TResult>(this TaskFactory<TResult> that, TSource state, Func<TSource, TResult> function) where TSource : class
        //{
        //    // tested by
        //    // X:\jsc.svn\examples\javascript\forms\TaskRunExperiment\TaskRunExperiment\ApplicationControl.cs


        //    var x = new InternalTaskExtensionsScope<TSource, TResult> { InternalTaskExtensionsScope_function = function };

        //    return Task<TResult>.Factory.StartNew(x.f, (object)state);
        //}



        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160108
        // called by
        // Z:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Extensions\TaskAsyncExtensions.cs

        public static Task<TResult> StartNew<TSource, TResult>(this TaskFactory that,
            TSource state,
            Func<TSource, TResult> function

            ) where TSource : class
        {
            if (function == null)
                throw new Exception("function missing");

            // You need to pass a state object to the delegate to reduce memory pressure from lambda variable capture.
            // http://blog.stephencleary.com/2013/08/startnew-is-dangerous.html
            // X:\jsc.svn\examples\javascript\async\test\TestDelayInsideWorker\TestDelayInsideWorker\Application.cs

            // tested by
            // X:\jsc.svn\examples\javascript\forms\TaskRunExperiment\TaskRunExperiment\ApplicationControl.cs
            // X:\jsc.svn\examples\javascript\WebCamToGIFAnimation\WebCamToGIFAnimation\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs

            var x = new InternalTaskExtensionsScope<TSource, TResult> { InternalTaskExtensionsScope_function = function };

            //return Task<TResult>.Factory.StartNew(x.f, (object)state);
            return Task.Factory.StartNew<TResult>(x.f, (object)state);
        }

        //cancellationToken: default(CancellationToken),
        //         creationOptions: TaskCreationOptions.LongRunning,
        //         scheduler: TaskScheduler.Default

        public static Task<TResult> StartNew<TSource, TResult>(this TaskFactory that,
            TSource state,
            Func<TSource, TResult> function,
            CancellationToken cancellationToken,
            TaskCreationOptions creationOptions,
            TaskScheduler scheduler
            ) where TSource : class
        {
            if (function == null)
                throw new Exception("function missing");

            // tested by
            // X:\jsc.svn\examples\javascript\forms\MandelbrotFormsControl\MandelbrotFormsControl\Library\MandelbrotComponent.cs
            // X:\jsc.svn\examples\javascript\forms\TaskRunExperiment\TaskRunExperiment\ApplicationControl.cs


            var x = new InternalTaskExtensionsScope<TSource, TResult> { InternalTaskExtensionsScope_function = function };

            return Task<TResult>.Factory.StartNew(
                x.f,
                (object)state,
                cancellationToken,
                creationOptions,
                scheduler
            );
        }

    }
}
