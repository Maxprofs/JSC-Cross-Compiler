﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public static class IHTMLButtonAsyncExtensions
    {
        // tested by
        // X:\jsc.svn\examples\javascript\AsyncButtonSequence\AsyncButtonSequence\Application.cs
        // "X:\jsc.svn\examples\javascript\async\AsyncButtonSequence\AsyncButtonSequence.sln"

        //[Obsolete("use button.async.onclick instead?")]
        public static TaskAwaiter<IEvent<IHTMLButton>> GetAwaiter(this IHTMLButton button)
        {
            return button.async.onclick.GetAwaiter();
        }


        // Error	80	Cannot implicitly convert type 'System.Runtime.CompilerServices.TaskAwaiter<ScriptCoreLib.JavaScript.DOM.IEvent>' to 
        // 'System.Runtime.CompilerServices.TaskAwaiter<ScriptCoreLib.JavaScript.DOM.IEvent<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton>>'	Z:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\HTML\IHTMLButtonAsyncExtensions.cs	20	20	ScriptCoreLib.Async




        public static IHTMLButton WhenClicked(this IHTMLButton e, Func<Task> h)
        {
            // X:\jsc.svn\examples\javascript\appengine\StopwatchTimetravelExperiment\StopwatchTimetravelExperiment\Application.cs
            return e.WhenClicked(button => h());
        }

        public static IHTMLButton WhenClicked(this IHTMLButton e, Func<IHTMLButton, Task> h)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\css\CSSFontFaceExperiment\CSSFontFaceExperiment\Application.cs

            var busy = false;

            e.onclick +=
                async delegate
            {
                if (busy)
                    return;

                busy = true;

                e.disabled = true;

                await h(e);

                e.disabled = false;
                busy = false;
            };

            return e;
        }

        //[Obsolete("experimental, what about reentry? signal the previous via scope?")]
        public static IHTMLAnchor Historic(
            this IHTMLAnchor e,
            Action<HistoryScope<object>> yield,
            bool replace = false
            )
        {
            // X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs
            //X:\jsc.svn\examples\javascript\Test\TestHistoricWithBaseElement\TestHistoricWithBaseElement\Application.cs
            // X:\jsc.svn\examples\javascript\test\TestHistoryForwardEvent\TestHistoryForwardEvent\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131222-form
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517

            //var url = "/#/" + e.innerText.Replace(" ", "+").ToLower();


            // Historic: { domain = 192.168.1.75, baseURI = http://otherhost/, href = http://192.168.1.75:10007/ }


            // http://otherhost/http://192.168.1.75:24121/#/click+to+enter+a+new+historic+state
            var url = "";

            var xlocation = Native.document.location.href.TakeUntilIfAny("#");
            var xbaseURI = Native.document.baseURI.TakeUntilIfAny("#");

            Console.WriteLine(
                "enter Historic: " + new
                {
                    Native.document.domain,
                    Native.document.baseURI,

                    location = Native.document.location.href,
                    xlocation,
                    href = e.href
                }
            );


            // http://otherhost/#/click+to+enter+a+new+historic+state

            if (string.IsNullOrEmpty(e.href))
            {
                //0:9ms HistoryExtensions enter view-source:35994
                //0:15ms Foo.Historic view-source:35994
                //0:16ms enter Historic: { domain = 192.168.1.91,
                // baseURI = http://192.168.1.91:20443/#/bar, 
                //location = http://192.168.1.91:20443/#/bar, href = http://192.168.1.91:20443/#/foo } view-source:35994

                //0:17ms update { href = http://192.168.1.91:20443/#/foo, url = http://192.168.1.91:20443/#/bar } 

                var z = e.innerText;

                // http://192.168.1.75:22130/#/click+to+enter+a+new+historic+state

                url = xlocation + "#/" + z.Replace(" ", "+").ToLower().Trim();

                // enable new tab click
                // start from root

                //Console.WriteLine("update " + new { e.href, url, e.innerText });
                e.href = url;
            }

            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs
            else
            {
                // reusing jsc server redirector
                // Historic enter. activate? { url = #/http://192.168.43.252:19360, length = 1, hash = #/fake-right }
                //Console.WriteLine(
                //    new { e.href, location = Native.document.location.href }
                //);

                // will this support offline reload?
                // { href = http://192.168.43.252:22188/#/zTop, location = http://192.168.43.252:22188/ }



                //:20ms enter Historic: { domain = 192.168.1.91, baseURI = http://192.168.1.91:6393/#/bar, location = http://192.168.1.91:6393/#/bar, xlocation = http://192.168.1.91:6393/, href = http://192.168.1.91:6393/#/bar } view-source:35994
                //0:20ms update { href = http://192.168.1.91:6393/#/bar, url = http://192.168.1.91:6393/ } 

                // http://otherhost/#/pre
                url = xlocation
                    + e.href.SkipUntilLastOrEmpty(xbaseURI);

                //Console.WriteLine("update " + new { e.href, url });
                e.href = url;
            }

            url = url.SkipUntilBeforeOrEmpty("#");

            e.title = url;

            //Console.WriteLine("Historic enter. activate? " + new { url, Native.window.history.length, Native.document.location.hash });


            #region onclick
            e.onclick +=
                ev =>
                {
                    //Console.WriteLine("onclick: " + new { e.href, ev.MouseButton, IEvent.MouseButtonEnum.Left });

                    if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                    {
                        // tested by
                        // X:\jsc.svn\examples\javascript\Test\TestImplicitTimelineRecordingEvents\TestImplicitTimelineRecordingEvents\Application.cs

                        Console.WriteLine("event: onclick " + new { e.href, ev.MouseButton });

                        ev.preventDefault();
                        ev.stopPropagation();


                        var xreplace = replace;

                        // reentry shall reload?
                        if (Native.document.location.hash == url)
                            xreplace = true;


                        //Console.WriteLine("Historic onclick " + new { url, Native.window.history.length, Native.document.location.hash });

                        if (xreplace)
                        {

                            Native.window.history.replaceState(
                                  state: new object(),
                                  url: e.href,
                                  // exlusive replace means current state will be forgotten
                                  exclusive: true,
                                  yield: yield
                              );
                        }
                        else
                        {
                            Native.window.history.pushState(
                                state: new object(),
                                url: e.href,
                                exclusive: true,
                                yield: yield
                            );
                        }
                    }
                };
            #endregion

            if (Native.document.location.hash == url)
            {
                //Console.WriteLine("activate after onpopstate!");

                //HistoryExtensions.yield(
                //    delegate
                //    {
                Console.WriteLine("event: activate! " + new { Native.document.location.hash, url });

                // safari?
                e.click();
                //    }
                //);

            }



            return e;
        }
    }
}
