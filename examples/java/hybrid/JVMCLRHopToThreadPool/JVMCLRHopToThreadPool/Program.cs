using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace JVMCLRHopToThreadPool
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    struct HopToThreadPoolAwaitable : INotifyCompletion
    {
        public HopToThreadPoolAwaitable GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }


    static class Program
    {
		// need 2012 compiler!
		// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\actionscript\Test\TestUploadValuesTaskAsync\TestUploadValuesTaskAsync\ApplicationSprite.cs

            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            #region  roslyn 2015? 4.6 ?
            //0001 0200001c JVMCLRHopToThreadPool__i__d.jvm::<module>.SHA1e980dfe21ddfc8bf9c27f3cbb3709e5a011bd3ba@1175797185$000000db
            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRHopToThreadPool\Program.java
            //Y:\staging\web\java\JVMCLRHopToThreadPool\Program___c___Main_b__0_0_d__MoveNext_06000028.java:44: error: cannot find symbol
            //        Program___c___Main_b__0_0_d__MoveNext_06000028.__workflow(next_060000280, ref_awaitable1, ref_awaitable2, ref_main_b__0_0_d3);
            #endregion


            new { }.With(
                // c# 5? yes, works with 2012 c# 5
                async scope =>
                {
                    //java.lang.Object, rt
                    //{{ ManagedThreadId = 1 }}
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                    //{{ ManagedThreadId = 8 }}


                    Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });

                    // will this compile for roslyn 2015? 4.6 ?
                    await default(HopToThreadPoolAwaitable); // computationally-intensive operation follows, so force execution to run asynchronously

                    Thread.Sleep(400);

                    Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });
                }
            );



            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );


            MessageBox.Show("click to close");

        }
    }


}
