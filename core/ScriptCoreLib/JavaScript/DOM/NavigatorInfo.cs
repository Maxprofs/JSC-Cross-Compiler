﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Navigator.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/base/nsIDOMNavigator.idl
    // https://github.com/adobe/webkit/blob/master/Source/WebCore/page/Navigator.idl
    // X:\opensource\github\WootzJs\WootzJs.Web\Navigator.cs
	
	// "U:\chromium\src\third_party\WebKit\Source\core\frame\Navigator.idl"
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/Navigator.idl
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/NavigatorCPU.idl
    // hardwareConcurrency

    // http://www.scala-js.org/api/scalajs-dom/0.6/index.html#org.scalajs.dom.Navigator

    // rename to INavigator ?

    [Script]
    public partial class Navigator
    {
        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/gamepad/NavigatorGamepad.idl
        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/gamepad/Gamepad.idl

        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/beacon/NavigatorBeacon.idl
    }

    [Script]
    public partial class NavigatorInfo : Navigator
    {
        // http://www.w3.org/TR/geolocation-API/
        public readonly Geolocation geolocation;


        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/credentialmanager/NavigatorCredentials.idl

        // http://w3c.github.io/netinfo/
        public INetworkInformation connection;


        // http://wiki.whatwg.org/wiki/NavigatorCores
        // tested by?
        // X:\jsc.svn\examples\javascript\async\Test\TestNavigatorCores\TestNavigatorCores\Application.cs
        //public uint hardwareConcurrency;
        public int hardwareConcurrency;



		// https://w3c.github.io/web-nfc/
		// http://www.w3.org/TR/nfc/#extensions-to-navigator-object
		// tested by ?
		public object nfc;


        // see also:
        // X:\jsc.svn\examples\javascript\Test\TestMediaCaptureAPI\TestMediaCaptureAPI\Application.cs
        // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs
        // X:\jsc.svn\examples\javascript\WebCamToGIFAnimation\WebCamToGIFAnimation\Application.cs

        // http://www.whatwg.org/specs/web-apps/current-work/multipage/offline.html#dfnReturnLink-0
        [Obsolete("ServiceWorker")]
        public bool onLine;


		// "U:\chromium\src\third_party\WebKit\Source\core\frame\NavigatorID.idl"s
		public string userAgent;
        public string appVersion;

        [Script]
        public class PluginInfo
        {
            public string description;
        }


		// "U:\chromium\src\third_party\WebKit\Source\modules\plugins\NavigatorPlugins.idl"
		// "U:\chromium\src\third_party\WebKit\Source\modules\plugins\MimeTypeArray.idl"
		// http://www.comptechdoc.org/independent/web/cgi/javamanual/javamimetype.html
		// http://www.irt.org/xref/MimeType.htm
		[Script]
        public class MimeTypeInfo
        {
            public string description;
            public string type;
        }

        // tested by
        // X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\Application.cs
        public IArray<MimeTypeInfo> mimeTypes;
        public IArray<PluginInfo> plugins;


        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/NavigatorServiceWorker.idl
        // tested by?
        // X:\jsc.svn\examples\javascript\test\TestNavigatorServiceWorker\TestNavigatorServiceWorker\Application.cs
        // chrome OS apps and server will be the first to have this tested on? then android? then app engine?
        // Chrome Canary. Type: "chrome://flags/" in the URL bar and turn on "enable-service-worker" and "experimental-web-platform-features".
        public readonly ServiceWorkerContainer serviceWorker;


        // Z:\jsc.svn\core\ScriptCoreLib\ActionScript\flash\ui\Multitouch.cs
        // http://www.w3.org/Submission/pointer-events/#the-pointermove-event

        // http://www.w3.org/TR/pointerevents/#widl-Navigator-maxTouchPoints

    }

}
