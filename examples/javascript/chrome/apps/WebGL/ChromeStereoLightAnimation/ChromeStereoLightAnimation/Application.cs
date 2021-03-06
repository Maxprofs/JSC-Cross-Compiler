#define FWebGLHZBlendCharacter
// can we animate our characters?

// F5 to run in CLR mode, on server
//#define AsWEBSERVER
// webserver cannot save images to foler tho
// as webserver imgs will have saves context menu



using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeStereoLightAnimation;
using ChromeStereoLightAnimation.Design;
using ChromeStereoLightAnimation.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace ChromeStereoLightAnimation
{
    using ChromeStereoLightAnimation.HTML.Images.FromAssets;
    using gl = WebGLRenderingContext;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20140815/webglcannonphysicsengine

        // http://youtu.be/Lo1IU8UAutE
        // 60hz 2160 4K!

        // The equirectangular projection was used in map creation since it was invented around 100 A.D. by Marinus of Tyre. 

        //        C:\Users\Arvo> "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hzsky.png" "/sdcard/oculus/360photos/"
        //1533 KB/s(3865902 bytes in 2.461s)

        //C:\Users\Arvo> "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape360globe1\0000.png" "/sdcard/oculus/360photos/tape360globe1.png"
        //1556 KB/s(2714294 bytes in 1.703s)

        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hz2048c3840x2160.png" "/sdcard/oculus/360photos/"
        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push   "X:\vr\tape360globe1\0000.png" "/sdcard/oculus/360photos/tape360globe2.png"
        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push   "X:\vr\tape360globe1\0000.png" "/sdcard/oculus/360photos/tape360globenight.png"



        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809/chrome360hz

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809

        // the eye nor the display will be able to do any stereo
        // until tech is near matrix capability. 2019?

        // cubemap can be used for all long range scenes
        // http://www.imdb.com/title/tt0112111/?ref_=nv_sr_1


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/cubemapcamera
        // subst /D b:
        // subst b: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeStereoLightAnimation\ChromeStereoLightAnimation\bin\Debug\staging\ChromeStereoLightAnimation.Application\web
        // subst a: z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeStereoLightAnimation\ChromeStereoLightAnimation\bin\Debug\staging\ChromeStereoLightAnimation.Application\web
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeStereoLightAnimation\ChromeStereoLightAnimation\bin\Debug\staging\ChromeStereoLightAnimation.Application\web
        // what if we want to do subst in another winstat or session?

        // ColladaLoader: Empty or non-existing file (assets/ChromeStereoLightAnimation/S6Edge.dae)

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://stemkoski.github.io/Three.js/Shadow.html

#if AsWEBSERVER
            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                // if we run as a server. we can open up on android.

                //chrome.Notification.DefaultTitle = "Nexus7";
                //chrome.Notification.DefaultIconUrl = new x128().src;
                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                     AppSource.Text
                //, AtFormCreated: FormStyler.AtFormCreated

                //AtFormConstructor:
                //    f =>
                //    {
                //        //arg[0] is typeof System.Int32
                //        //script: error JSC1000: No implementation found for this native method, please implement [static System.Drawing.Color.FromArgb(System.Int32)]

                //        // X:\jsc.svn\examples\javascript\forms\Test\TestFromArgb\TestFromArgb\ApplicationControl.cs

                //        f.BackColor = System.Drawing.Color.FromArgb(0xA26D41);
                //    }
                );
                return;
            }
            #endregion
#else

            #region += Launched chrome.app.window
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");

                    // pass thru
                }
                else
                {
                    // should jsc send a copresence udp message?
                    //chrome.runtime.UpdateAvailable += delegate
                    //{
                    //    new chrome.Notification(title: "UpdateAvailable");

                    //};

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "ChromeStereoLightAnimation");

                        // https://developer.chrome.com/apps/app_window#type-CreateWindowOptions
                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: new
                               {
                                   alwaysOnTop = true,
                                   visibleOnAllWorkspaces = true
                               }
                        );

                        //xappwindow.setAlwaysOnTop

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion


#endif

            Native.body.Clear();
            Native.body.style.margin = "0px";
            Native.body.style.padding = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;





            // Severity	Code	Description	Project	File	Line
            //Error CS0201  Only assignment, call, increment, decrement, and new object expressions can be used as a statement ChromeStereoLightAnimation  Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeStereoLightAnimation\ChromeStereoLightAnimation\Application.cs   187


            //new Action<int>(

            var camera0 = default(THREE.PerspectiveCamera);






            foreach (var y in
                from eyeid in new[] { 0, 1 }
                select (Action)
                delegate
                {
                    var scene = new THREE.Scene();


                    var floorTexture = THREE.ImageUtils.loadTexture(
             new HTML.Images.FromAssets.checkerboard().src
             //"images/checkerboard.jpg"
             );


                    #region  function init()
                    // SCENE
                    // CAMERA
                    float SCREEN_WIDTH = Native.window.Width / 2;
                    //float SCREEN_WIDTH = Native.screen.width / 2;
                    float SCREEN_HEIGHT = Native.window.Height;
                    var VIEW_ANGLE = 45;
                    float ASPECT = SCREEN_WIDTH / SCREEN_HEIGHT;
                    var NEAR = 0.1;
                    var FAR = 20000;



                    // RENDERER
                    // WebGLDeferredRenderer
                    var renderer = new THREE.WebGLRenderer(new { antialias = true });

                    //renderer.setSize((int)SCREEN_WIDTH, (int)SCREEN_HEIGHT);

                    renderer.domElement.AttachToDocument();
                    //renderer.domElement.style.SetLocation(Native.window.Width / 2 * (1 - eyeid), 0);

                    // EVENTS
                    // CONTROLS
                    // STATS


                    // LIGHT
                    //var light = new THREE.PointLight(0xffffff);
                    //light.position.set(0,250,0);
                    //scene.add(light);

                    ////////////
                    // CUSTOM //
                    ////////////

                    // must enable shadows on the renderer 
                    renderer.shadowMapEnabled = true;





                    // SKYBOX/FOG
                    var skyBoxGeometry = new THREE.CubeGeometry(10000, 10000, 10000);
                    var skyBoxMaterial = new THREE.MeshBasicMaterial(new { color = 0x9999ff, side = THREE.BackSide });
                    var skyBox = new THREE.Mesh(skyBoxGeometry, skyBoxMaterial);
                    // scene.add(skyBox);
                    scene.fog = new THREE.FogExp2(0x9999ff, 0.00025);

                    // "shadow cameras" show the light source and direction

                    // spotlight #1 -- yellow, dark shadow
                    var spotlight = new THREE.SpotLight(0xffff00);
                    spotlight.position.set(-60, 150, -30);
                    spotlight.shadowCameraVisible = true;
                    spotlight.shadowDarkness = 0.95;
                    spotlight.intensity = 2;
                    // must enable shadow casting ability for the light
                    spotlight.castShadow = true;
                    scene.add(spotlight);

                    // spotlight #2 -- red, light shadow
                    var spotlight2 = new THREE.SpotLight(0xff0000);
                    //var spotlight2 = new THREE.SpotLight(0xffff00);
                    spotlight2.position.set(60, 150, -60);
                    scene.add(spotlight2);
                    spotlight2.shadowCameraVisible = true;
                    spotlight2.shadowDarkness = 0.70;
                    spotlight2.intensity = 2;
                    spotlight2.castShadow = true;


                    // THREE.WebGLProgram: gl.getProgramInfoLog() Pixel shader sampler count exceeds MAX_TEXTURE_IMAGE_UNITS (16).
                    for (int i = 0; i < 8; i++)
                    {

                        // spotlight #3 blue
                        var spotlight3 = new THREE.SpotLight(0x0000ff);
                        //var spotlight3 = new THREE.SpotLight(0x00ffff);
                        spotlight3.position.set(150 * i, 80, -100);
                        //spotlight3.shadowCameraVisible = true;
                        spotlight3.shadowDarkness = 0.95;
                        spotlight3.intensity = 2;
                        spotlight3.castShadow = true;
                        scene.add(spotlight3);
                        // change the direction this spotlight is facing
                        var lightTarget = new THREE.Object3D();
                        lightTarget.position.set(150 * i, 10, -100);
                        scene.add(lightTarget);
                        spotlight3.target = lightTarget;



                    }



                    // cube: mesh to cast shadows
                    #region castShadow
                    var cubeGeometry = new THREE.CubeGeometry(50, 50, 50);
                    var cubeMaterial = new THREE.MeshLambertMaterial(new { color = 0x888888 });

                    var cube0 = new THREE.Mesh(cubeGeometry, cubeMaterial);
                    //cube.position.set(0, 50, 0);
                    cube0.position.set(0, 100, 0);
                    // Note that the mesh is flagged to cast shadows
                    cube0.castShadow = true;
                    scene.add(cube0);

                    // floor: mesh to receive shadows


                    // public static ImageUtilsType ImageUtils;
                    //var floorTexture = new THREE.ImageUtils.loadTexture("images/checkerboard.jpg");


                    floorTexture.wrapS = THREE.RepeatWrapping;
                    floorTexture.wrapT = THREE.RepeatWrapping;
                    floorTexture.repeat.set(10, 10);
                    #endregion




                    // Note the change to Lambert material.
                    #region receiveShadow
                    var floorMaterial = new THREE.MeshLambertMaterial(new { map = floorTexture, side = THREE.DoubleSide });
                    //var floorGeometry = new THREE.PlaneGeometry(1000 * 8, 1000 * 8, 100, 100);
                    var floorGeometry = new THREE.PlaneGeometry(1000, 1000, 100, 100);

                    for (int i = 0; i < 8; i++)
                    {

                        var floor0 = new THREE.Mesh(floorGeometry, floorMaterial);
                        floor0.position.y = -0.5;

                        floor0.position.x = 1000 * i;

                        floor0.rotation.x = Math.PI / 2;
                        // Note the mesh is flagged to receive shadows
                        floor0.receiveShadow = true;
                        scene.add(floor0);

                    }
                    #endregion


                    // create "light-ball" meshes
                    var sphereGeometry = new THREE.SphereGeometry(10, 16, 8);
                    var darkMaterial = new THREE.MeshBasicMaterial(new { color = 0x000000 });


                    {
                        var wireframeMaterial = new THREE.MeshBasicMaterial(
                        new { color = 0xffff00, wireframe = true, transparent = true });
                        var shape = THREE.SceneUtils.createMultiMaterialObject(
                        sphereGeometry, new[] { darkMaterial, wireframeMaterial });
                        shape.position = spotlight.position;
                        scene.add(shape);
                    }


                    {
                        var wireframeMaterial = new THREE.MeshBasicMaterial(
                        new { color = 0xff0000, wireframe = true, transparent = true });
                        var shape = THREE.SceneUtils.createMultiMaterialObject(
                        sphereGeometry, new[] { darkMaterial, wireframeMaterial });
                        shape.position = spotlight2.position;
                        scene.add(shape);
                    }

                    //{
                    //    var wireframeMaterial = new THREE.MeshBasicMaterial(
                    //    new { color = 0x0000ff, wireframe = true, transparent = true });
                    //    var shape = THREE.SceneUtils.createMultiMaterialObject(
                    //    sphereGeometry, new[] { darkMaterial, wireframeMaterial });
                    //    shape.position = spotlight3.position;
                    //    scene.add(shape);
                    //}

                    #endregion










                    var camera = new THREE.PerspectiveCamera(VIEW_ANGLE, ASPECT, NEAR, FAR);
                    scene.add(camera);
                    camera.position.set(0, 150, 400);
                    camera.lookAt(scene.position);



                    if (eyeid == 0)
                    {
                        camera0 = camera;

                        var controls = new THREE.OrbitControls(camera0, renderer.domElement);
                        //var controls = new THREE.OrbitControls(camera0, Native.document.body);

                        Native.window.onframe += delegate
                        {
                            renderer.render(scene, camera0);
                            controls.update();
                            //stats.update();
                        };
                    }
                    else
                    {
                        Native.window.onframe += delegate
                        {
                            camera.rotation.set(camera0.rotation.x, camera0.rotation.y, camera0.rotation.z);
                            camera.position.set(camera0.position.x, camera0.position.y, camera0.position.z);

                            // lets not add any stereo effect. as we may need to run in 360 mono mode anyway to gain speed?
                            //camera.translateX()

                            // scene is unique to renderer!!!!!
                            // we are doing everything twice for now.
                            renderer.render(scene, camera);
                            //controls.update();
                            //stats.update();
                        };
                    }



                    #region onresize
                    new { }.With(
                        async delegate
                        {
                            do
                            {
                                // allow webview to behave in S6 multiview  mode
                                //SCREEN_WIDTH = Native.screen.width / 2;
                                SCREEN_WIDTH = Native.window.Width / 2;

                                //camera.aspect = Native.window.aspect;
                                camera.aspect = SCREEN_WIDTH / Native.window.Height;
                                camera.updateProjectionMatrix();

                                //renderer0.setSize(Native.window.Width / 2, Native.window.Height);
                                renderer.setSize((int)SCREEN_WIDTH, Native.window.Height);
                                //renderer0.domElement.style.SetLocation(Native.window.Width / 2 * eyeid, 0);
                                renderer.domElement.style.SetLocation((int)SCREEN_WIDTH * (1 - eyeid), 0);

                            }
                            while (await Native.window.async.onresize);
                        });
                    #endregion
                }
             )
                y();






























            Console.WriteLine("do you see it?");
        }

    }
}

//{ Message = Could not load file or assembly 'Chrome Web Store, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified. }
//1c48:02:01:14 after worker yield...

//Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.