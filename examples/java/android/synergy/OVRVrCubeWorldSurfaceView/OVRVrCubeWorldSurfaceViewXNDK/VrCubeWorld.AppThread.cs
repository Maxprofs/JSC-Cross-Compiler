﻿using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c


        // converted from size_t
        // can do a sizeof for malloc?
        // sizeof not available for managed members?
        [Script]
        public partial class ovrAppThread
        {
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrApi.cs
            // set via ovrAppThread_Create
            public readonly JavaVM JavaVm;

            public readonly jobject ActivityObject;
            public readonly pthread_t Thread;

            public readonly ovrMessageQueue MessageQueue = new ovrMessageQueue();

            // set to null by onSurfaceDestroyed
            // set by onSurfaceCreated
            public native_window.ANativeWindow NativeWindow = null;

            // called by onCreate
            public ovrAppThread(JNIEnv env, jobject activityObject)
            {
                env.GetJavaVM(env, out this.JavaVm);

                this.ActivityObject = env.NewGlobalRef(env, activityObject);
                
                //// ldfda ?
                // new Thread( ?
                // await HopToWorkerThread?
                var createErr = pthread.pthread_create(out this.Thread, null, AppThreadFunction, this);


                //var createErr2 = pthread.pthread_create(out appThread.Thread, null,
                //    arg: appThread,
                //    start_routine: (ovrAppThread appThread0) =>
                //    {
                //        // scope sharing via arg0, in c! ready for roslyn?

                //        return null;
                //    }
                //);
            }







            // 1777
            static object AppThreadFunction(ovrAppThread appThread)
            {
                // new thread, jump into instance callsite
                return appThread.AppThreadFunction();
            }

            object AppThreadFunction()
            {
                // 1778

                var java = default(ovrJava);
                java.Vm = this.JavaVm;
                java.Vm.AttachCurrentThread( /* JavaVM* */ java.Vm, /* JNIEnv** */out java.Env, null);
                java.ActivityObject = this.ActivityObject;
                // 1785

                var initParms = VrApi_Helpers.vrapi_DefaultInitParms(ref java);
                VrApi.vrapi_Initialize(ref initParms);


                var appState = new ovrApp(ref java);
                // 1794
                appState.Egl.ovrEgl_CreateContext(null);


                var hmdInfo = VrApi.vrapi_GetHmdInfo(ref java);
                appState.Renderer.ovrRenderer_Create(ref hmdInfo);

                bool destroyed = false;
                while (!destroyed)
                {
                    var ok = true;
                    while (ok)
                    {
                        ovrMessage message;
                        var waitForMessages = appState.Ovr == null;

                        if (!this.MessageQueue.ovrMessageQueue_GetNextMessage(out message, waitForMessages)) break;

                        // no switch for jsc?
                        if (message.Id == MESSAGE.MESSAGE_ON_CREATE) break;
                        if (message.Id == MESSAGE.MESSAGE_ON_START) break;
                        if (message.Id == MESSAGE.MESSAGE_ON_RESUME) { appState.Resumed = true; break; }
                        if (message.Id == MESSAGE.MESSAGE_ON_PAUSE) { appState.Resumed = false; break; }
                        if (message.Id == MESSAGE.MESSAGE_ON_STOP) break;
                        if (message.Id == MESSAGE.MESSAGE_ON_DESTROY) { appState.NativeWindow = null; destroyed = true; break; }

                        if (message.Id == MESSAGE.MESSAGE_ON_SURFACE_CREATED) { appState.NativeWindow = (native_window.ANativeWindow)message.ovrMessage_GetPointerParm(0); destroyed = true; break; }
                        if (message.Id == MESSAGE.MESSAGE_ON_SURFACE_DESTROYED) { appState.NativeWindow = null; break; }

                        if (message.Id == MESSAGE.MESSAGE_ON_KEY_EVENT) { appState.ovrApp_HandleKeyEvent((keycodes.AKEYCODE)message.ovrMessage_GetIntegerParm(0), (input.AInputEventAction)message.ovrMessage_GetIntegerParm(1)); break; }
                        if (message.Id == MESSAGE.MESSAGE_ON_TOUCH_EVENT) { appState.ovrApp_HandleTouchEvent(message.ovrMessage_GetIntegerParm(0), message.ovrMessage_GetFloatParm(1), message.ovrMessage_GetFloatParm(2)); break; }

                        appState.ovrApp_HandleVrModeChanges();
                    }


                    appState.ovrApp_BackButtonAction();
                    appState.ovrApp_HandleSystemEvents();

                    // not ready yet?
                    if (appState.Ovr == null)
                    {
                        continue;
                    }



                    if (!appState.Scene.ovrScene_IsCreated())
                    {
                        // need to keep the enum typename?

                        var parms = VrApi_Helpers.vrapi_DefaultFrameParms(ref appState.Java, ovrFrameInit.VRAPI_FRAME_INIT_LOADING_ICON_FLUSH, 0);
                        parms.FrameIndex = appState.FrameIndex;
                        appState.Ovr.vrapi_SubmitFrame(ref parms);

                        appState.Scene.ovrScene_Create();
                    }

                    // 1862
                    appState.FrameIndex++;

                    var predictedDisplayTime = appState.Ovr.vrapi_GetPredictedDisplayTime(appState.FrameIndex);
                    var tracking = appState.Ovr.vrapi_GetPredictedTracking(predictedDisplayTime);

                    // like step in physics?
                    appState.Simulation.ovrSimulation_AdvanceSimulation(predictedDisplayTime);

                    {
                        //var parms = appState.Renderer.ovrRenderer_RenderFrame(ref appState, ref tracking);
                        var parms = appState.Renderer.ovrRenderer_RenderFrame(appState, ref tracking);

                        appState.Ovr.vrapi_SubmitFrame(ref parms);
                    }
                    // 1891
                }

                // 1896
                appState.Renderer.ovrRenderer_Destroy();

                // 1898
                appState.Scene.ovrScene_Destroy();
                appState.Egl.ovrEgl_DestroyContext();
                VrApi.vrapi_Shutdown();

                java.Vm.DetachCurrentThread(java.Vm);
                return null;
            }




            // called by onDestroy, then free
            public void ovrAppThread_Destroy(JNIEnv env)
            {
                pthread.pthread_join(this.Thread, null);

                //   (/* typecast */(void(*)(JNIEnv*, jobject))env->DeleteGlobalRef)(env, appThread->ActivityObject);
                env.DeleteGlobalRef(env, this.ActivityObject);
                this.MessageQueue.ovrMessageQueue_Destroy();
            }
        }





    }


}
