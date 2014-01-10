﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using ScriptCoreLib.JavaScript.DOM;
using System.Threading.Tasks;
using System.Collections.Specialized;
using ScriptCoreLib.JavaScript.WebGL;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.WebClient))]
    public class __WebClient
    {
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\JavaScript\InternalWebMethodRequest.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\WebClient.cs
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Net\WebClient.cs

        public event UploadValuesCompletedEventHandler UploadValuesCompleted;

        public void UploadValuesAsync(Uri address, NameValueCollection data)
        {
            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.POST, address.ToString(), async: true);
            x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");


            var xx = ToFormDataString(data);

            //Uncaught InvalidStateError: Failed to execute 'send' on 'XMLHttpRequest': the object's state must be OPENED.
            // X:\jsc.svn\examples\javascript\Test\TestUploadValuesAsync\TestUploadValuesAsync\Application.cs

            // UploadValuesAsync { responseType = , response = <document><TaskComplete><TaskResult>13</TaskResult></TaskComplete></document> }

            x.InvokeOnComplete(
                delegate
                {
                    var response = default(byte[]);

                    // UploadValuesAsync { status = 204, responseType = arraybuffer, response = [object Uint8ClampedArray] }


                    Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType });


                    if (x.response != null)
                    {
                        // http://stackoverflow.com/questions/8022425/getting-blob-data-from-xhr-request

                        var a = (ArrayBuffer)x.response;

                        Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, a.byteLength });

                        // IE?
                        //var u8 = new Uint8Array(array: a);
                        var u8c = new Uint8ClampedArray(array: a);

                        response = u8c;
                    }

                    Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, response });

                    var e = new __UploadValuesCompletedEventArgs { Result = response };



                    if (UploadValuesCompleted != null)
                        UploadValuesCompleted(null, (UploadValuesCompletedEventArgs)(object)e);

                }
               );

            x.responseType = "arraybuffer";
            x.send(xx);



        }

        public static string ToFormDataString(NameValueCollection data)
        {
            #region AllKeys
            var xx = "";

            foreach (var item in data.AllKeys)
            {
                if (xx != "")
                {
                    xx += "&";
                }

                var evalue = Native.window.escape(data[item]).Replace("+", "%" + ((byte)'+').ToString("x2"));
                xx += item + "=" + evalue;
            }


            #endregion
            return xx;
        }

        #region DownloadString
        public event DownloadStringCompletedEventHandler DownloadStringCompleted;

        [Obsolete("this will not work if the baseURI has changed and worker is using blob: workaround")]
        public string DownloadString(string address)
        {

            // X:\jsc.svn\examples\javascript\Test\TestDownloadStringAsync\TestDownloadStringAsync\Application.cs

            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.GET, address.ToString(), async: false);

            //Console.WriteLine("DownloadStringAsync");
            x.send();

            return x.responseText;
        }

        public void DownloadStringAsync(Uri address)
        {
            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.GET, address.ToString());

            x.InvokeOnComplete(
                r =>
                {
                    //var e = new __DownloadStringCompletedEventArgs { Error = new Exception("Not implemented. (__WebClient.DownloadStringAsync)") };
                    var e = new __DownloadStringCompletedEventArgs { Result = r.responseText };

                    if (DownloadStringCompleted != null)
                        DownloadStringCompleted(null, (DownloadStringCompletedEventArgs)(object)e);
                }
            );

            //Console.WriteLine("DownloadStringAsync");
            x.send();

        }

        public Task<string> DownloadStringTaskAsync(string address)
        {
            var z = new TaskCompletionSource<string>();

            var x = new IXMLHttpRequest();


            x.open(Shared.HTTPMethodEnum.GET, address.ToString());

            x.InvokeOnComplete(
                r =>
                {
                    z.SetResult(r.responseText);
                }
            );

            //Console.WriteLine("DownloadStringAsync");
            x.send();

            return z.Task;
        }
        #endregion

    }
}
