﻿using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OVRWindWheelNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c


        // field of ovrApp
        public struct ovrSimulation
        {
            public ovrVector3f CurrentRotation;

            // set default?
            public void ovrSimulation_Clear()
            {
                // 965
                this.CurrentRotation.x = 0.0f;
                this.CurrentRotation.y = 0.0f;
                this.CurrentRotation.z = 0.0f;
            }

            // called by AppThreadFunction, would we benefit if jsc marked no branch methods as inline?
            public void ovrSimulation_AdvanceSimulation(double predictedDisplayTime)
            {
                // 972
                // Update rotation.
                this.CurrentRotation.x = (float)(predictedDisplayTime);
                this.CurrentRotation.y = (float)(predictedDisplayTime);
                this.CurrentRotation.z = (float)(predictedDisplayTime);
            }
        }


        public enum ovrBackButtonState
        {
            BACK_BUTTON_STATE_NONE,
            BACK_BUTTON_STATE_PENDING_DOUBLE_TAP,
            BACK_BUTTON_STATE_PENDING_SHORT_PRESS,
            BACK_BUTTON_STATE_SKIP_UP
        }


        // stackalloc at X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRWindWheelNDK\VrCubeWorld.AppThreadFunction.cs

        // created by AppThreadFunction
        // ref used by ovrRenderer_RenderFrame
        public class ovrApp
        {
            public ovrAppThread AppThread;

            // defined at vrapi.h?

            // cant make it readonly
            // sent to vrapi_DefaultFrameParms VRAPI_FRAME_INIT_DEFAULT VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            public ovrJava Java;

            public ovrEgl Egl = new ovrEgl();
            public native_window.ANativeWindow NativeWindow = null;

            // other activites could be selected too...
            public bool Resumed = false;

            // set by?
            public ovrMobile Ovr = default(ovrMobile);

            public ovrSimulation Simulation;

            public long FrameIndex = 1;
            public int MinimumVsyncs = 1;

            public ovrBackButtonState BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
            public bool BackButtonDown = false;
            public double BackButtonDownStartTime = 0;
#if MULTI_THREADED
	ovrRenderThread		RenderThread;
#else
            // set by?
            public ovrRenderer Renderer = new ovrRenderer();
#endif


            public ovrScene Scene;

            // called by AppThreadFunction
            public ovrApp(ref ovrJava java)
            {
                this.Scene = new ovrScene { App = this };

                ConsoleExtensions.trace("enter ovrApp, set ovrJava, call ovrSimulation_Clear");
                this.Java = java;

                // 1408

                this.Simulation.ovrSimulation_Clear();
            }

            // called by AppThreadFunction
            public void ovrApp_HandleVrModeChanges()
            {
                //ConsoleExtensions.tracei("enter ovrApp_HandleVrModeChanges, FrameIndex: ", (int)FrameIndex);

                // 1432
                if (this.NativeWindow != null && this.Egl.MainSurface == egl.EGL_NO_SURFACE)
                {
                    ConsoleExtensions.trace("ovrApp_HandleVrModeChanges, ovrEgl_CreateSurface");
                    this.Egl.ovrEgl_CreateSurface(this.NativeWindow);
                }

                if (this.Resumed != false && this.NativeWindow != null)
                {
                    if (this.Ovr == null)
                    {
                        var parms = VrApi_Helpers.vrapi_DefaultModeParms(ref Java);
                        parms.CpuLevel = 2;
                        parms.GpuLevel = 3;
                        parms.MainThreadTid = unistd.gettid();
#if MULTI_THREADED
			// Also set the renderer thread to SCHED_FIFO.
			parms.RenderThreadTid = ovrRenderThread_GetTid( &app->RenderThread );
#endif

                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));

                        ConsoleExtensions.trace("ovrApp_HandleVrModeChanges, vrapi_EnterVrMode");
                        this.Ovr = VrApi.vrapi_EnterVrMode(ref parms);

                        //ALOGV("        vrapi_EnterVrMode()");
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));
                    }
                }
                else
                {
                    if (this.Ovr != null)
                    {
#if MULTI_THREADED
			// Make sure the renderer thread is no longer using the ovrMobile.
			ovrRenderThread_Wait( &app->RenderThread );
#endif
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));

                        ConsoleExtensions.trace("ovrApp_HandleVrModeChanges, vrapi_LeaveVrMode");
                        VrApi.vrapi_LeaveVrMode(this.Ovr);
                        this.Ovr = null;

                        //ALOGV("        vrapi_LeaveVrMode()");
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));
                    }
                }

                if (this.NativeWindow == null && this.Egl.MainSurface != egl.EGL_NO_SURFACE)
                {
                    ConsoleExtensions.trace("ovrApp_HandleVrModeChanges, ovrEgl_DestroySurface");
                    this.Egl.ovrEgl_DestroySurface();
                }
            }

            // called by AppThreadFunction
            public void ovrApp_BackButtonAction()
            {
                // 1484

                if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_PENDING_DOUBLE_TAP)
                {
                    //ALOGV("back button double tap");
                    this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP;
                }
                else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_PENDING_SHORT_PRESS && !this.BackButtonDown)
                {
                    if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) > VrApi_Android.BACK_BUTTON_DOUBLE_TAP_TIME_IN_SECONDS)
                    {
                        ConsoleExtensions.trace("back button short press");
                        //ALOGV("        ovr_StartSystemActivity( %s )", PUI_CONFIRM_QUIT);
                        VrApi_Android.ovr_StartSystemActivity(ref Java, VrApi.PUI_CONFIRM_QUIT, default(string));
                        this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                    }
                }
                else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_NONE && this.BackButtonDown)
                {
                    if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) > VrApi_Android.BACK_BUTTON_LONG_PRESS_TIME_IN_SECONDS)
                    {
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/pui_global_menu
                        ConsoleExtensions.trace("back button long press, ovr_StartSystemActivity PUI_GLOBAL_MENU");
                        //ALOGV("        ovr_StartSystemActivity( %s )", PUI_GLOBAL_MENU);
                        VrApi_Android.ovr_StartSystemActivity(ref Java, VrApi.PUI_GLOBAL_MENU, null);
                        this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP;
                    }
                }
            }

            // java UI sends over to native, which the uses MQ to send over to bg thread? SharedMemory would be nice?
            // onKeyEvent
            public void ovrApp_HandleKeyEvent(keycodes.AKEYCODE keyCode, input.AInputEventAction action)
            {
                // 1513
                // cannot do this aliasing?
                //var app = this;

                // Handle GearVR back button.
                if (keyCode == keycodes.AKEYCODE.AKEYCODE_BACK)
                {
                    if (action == input.AInputEventAction.AKEY_EVENT_ACTION_DOWN)
                    {
                        if (!this.BackButtonDown)
                        {
                            if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) < VrApi_Android.BACK_BUTTON_DOUBLE_TAP_TIME_IN_SECONDS)
                            {
                                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_PENDING_DOUBLE_TAP;
                            }
                            this.BackButtonDownStartTime = VrApi.vrapi_GetTimeInSeconds();
                        }
                        this.BackButtonDown = true;
                    }
                    else if (action == input.AInputEventAction.AKEY_EVENT_ACTION_UP)
                    {
                        if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_NONE)
                        {
                            if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) < VrApi_Android.BACK_BUTTON_SHORT_PRESS_TIME_IN_SECONDS)
                            {
                                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_PENDING_SHORT_PRESS;
                            }
                        }
                        else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP)
                        {
                            this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                        }
                        this.BackButtonDown = false;
                    }
                    //return 1;
                }
                //return 0;
            }

            // onTouchEvent
            public void ovrApp_HandleTouchEvent(int action, float x, float y)
            {
                // ??? not used

            }



            const int MAX_EVENT_SIZE = 4096;
            readonly byte[] ovrApp_HandleSystemEvents_eventBuffer = new byte[MAX_EVENT_SIZE];

            // sent by?
            // called by AppThreadFunction
            public void ovrApp_HandleSystemEvents()
            {
                // 1568

                //var eventBuffer = new byte[MAX_EVENT_SIZE];
                //var eventBuffer = stackalloc byte[MAX_EVENT_SIZE];

                for (var status = VrApi_Android.ovr_GetNextPendingEvent(ovrApp_HandleSystemEvents_eventBuffer, MAX_EVENT_SIZE); status >= eVrApiEventStatus.VRAPI_EVENT_PENDING; status = VrApi_Android.ovr_GetNextPendingEvent(ovrApp_HandleSystemEvents_eventBuffer, MAX_EVENT_SIZE))
                {
                    if (status != eVrApiEventStatus.VRAPI_EVENT_PENDING)
                    {
                        if (status != eVrApiEventStatus.VRAPI_EVENT_CONSUMED)
                        {
                            //ALOGE("Error %i handing System Activities Event", status);
                        }
                        continue;
                    }
                }

            }




            public unsafe void trace60(
           string message = "",
           [CallerFilePath] string sourceFilePath = "",
           [CallerLineNumber] int sourceLineNumber = 0
           )
            {
                //if (this.FrameIndex > 300)
                //    return;

                if (this.FrameIndex % 60 == 1)
                {
                    ConsoleExtensions.trace(
                        message,
                        //value,
                        sourceFilePath,
                        sourceLineNumber

                    );
                }
            }

            public unsafe void tracei60(
            string message = "",
            int value = 0,
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
            {
                //if (this.FrameIndex > 300)
                //    return;

                if (this.FrameIndex % 60 == 1)
                {
                    ConsoleExtensions.tracei(
                        message,
                        value,
                        sourceFilePath,
                        sourceLineNumber

                    );
                }
            }


            public unsafe void tracef60(
            string message = "",
            float value = 0,
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
            {
                //if (this.FrameIndex > 300)
                //    return;

                if (this.FrameIndex % 60 == 1)
                {
                    ConsoleExtensions.tracef(
                        message,
                        value,
                        sourceFilePath,
                        sourceLineNumber

                    );
                }
            }
        }



    }


}
