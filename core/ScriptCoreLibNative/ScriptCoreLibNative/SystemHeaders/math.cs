﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\math.h"
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Math.cs
    // X:\jsc.svn\core\discontinued\ScriptCoreLib.Alchemy\ScriptCoreLib.Alchemy\Alchemy\BCLImplementation\System\Math.cs
    // X:\jsc.svn\core\discontinued\ScriptCoreLib.Alchemy\ScriptCoreLib.Alchemy\Alchemy\Headers\math.cs

    // should we name it math_h or __math ?
    // should we use interfaces
    // like X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\GLES2\gl2ext.cs

	[Script(IsNative = true, Header = "math.h", IsSystemHeader = true)]
	public static class math
	{
        // C++ originally targetted C as it's output, not machine code
        // http://developers.slashdot.org/story/14/12/09/0418252/how-relevant-is-c-in-2014
        // C is important because it directly presents the actual machine memory model.
        // C allows you to get into regions that Java does not even know exist.

        public static float fmaxf(float e, float x)
        {
            return 0;
        }
        public static float fminf(float e, float x)
        {
            return 0;
        }

        public static float floorf(float e)
        {
            return default(float);
        }

        public static float sinf(float e)
        {
            // X:\jsc.svn\examples\c\android\hybrid\HybridGLES3JNIActivity\HybridGLES3JNIActivityNDK\RendererES3.cs

            return default(float);
        }

        public static float cosf(float e)
        {
            return default(float);
        }


        public static double sin(double e)
        {
            return default(double);
        }

        public static double cos(double e)
        {
            return default(double);
        }

        public static double sqrt(double e)
        {
            return default(double);
        }

        public static double pow(double x, double y)
        {
            return default(double);
        }

        public static float tanf(float e)
        {
            // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs

            return default(float);
        }

	}

}
