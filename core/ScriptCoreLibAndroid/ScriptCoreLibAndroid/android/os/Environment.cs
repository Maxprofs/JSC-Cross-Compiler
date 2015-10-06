﻿using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{

    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Environment.java
    // http://developer.android.com/reference/android/os/Environment.html

    [Script(IsNative = true)]
    public class Environment
    {
        // tested by
        // X:\jsc.svn\examples\javascript\android\AndroidEnvironmentWebActivity\AndroidEnvironmentWebActivity\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs
        // X:\jsc.svn\examples\java\android\AndroidCameraActivity\AndroidCameraActivity\ApplicationActivity.cs

        public static string DIRECTORY_DCIM = "DCIM";

        // X:\jsc.svn\examples\javascript\android\AndroidListApplications\AndroidListApplications\ApplicationWebService.cs
        public static string DIRECTORY_DOWNLOADS;

        public static string DIRECTORY_PICTURES = "Pictures";

        public static File getExternalStoragePublicDirectory(string type)
        {
            return default(File);
        }

        public static File getExternalStorageDirectory() { throw null; }
        public static File getDataDirectory() { throw null; }

    }
}
