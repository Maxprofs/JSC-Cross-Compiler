using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CSSAzimuthMapViz;
using CSSAzimuthMapViz.Design;
using CSSAzimuthMapViz.HTML.Pages;
using CSSAzimuthMapViz.HTML.Images.FromAssets;

namespace CSSAzimuthMapViz
{
    public class side
    {
        public IHTMLImage img;
        public THREE.Vector3 position;
        public THREE.Vector3 rotation;
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // example broken by sf data loss?
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151016/azimuthal


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // chrome 48 opens too many connections to download pngs?
            // Version 48.0.2536.0 canary (64-bit)

            //TCP enter https { ClientCounter = 395 }
            //{ RemoteEndPoint = 192.168.1.199:65133, isPeerProxy = False }
            //{ certificate = , chain =  }

            //Unhandled Exception: OutOfMemoryException.

            // view-source:file:///X:/opensource/github/three.js/examples/css3d_panorama.html

            // http://security.stackexchange.com/questions/53765/router-detecting-port-scan-and-ack-flood-attack
            // what causes the port flood

            ////TCP enter https { ClientCounter = 12 }
            ////{ RemoteEndPoint = 192.168.1.199:65188, isPeerProxy = False }
            //new azi_ny().AttachToDocument();
            //// what happens if we add two of them?

            //var __threejs = new THREE.Vector3(-512, 0, 0);




            //var camera = new THREE.PerspectiveCamera(75, Native.window.aspect, 1, 1000);
            //var scene = new THREE.Scene();

            //var renderer = new THREE.CSS3DRenderer();

            Console.WriteLine("about to init sides...");
            #region sides
            var sides = new[]
            {
                new side
                {
                    img=  new azi_px(),

                    // glsl, clr46, Vector3?
                    position= new THREE.Vector3( -512, 0, 0 ),
                    rotation= new THREE.Vector3( 0, Math.PI / 2, 0 )
                },
                new side {
                    img=  new azi_nx(),
                    position= new THREE.Vector3( 512, 0, 0 ),
                    rotation= new THREE.Vector3( 0, -Math.PI / 2, 0 )
                },
                new side{
                    img=  new azi_py(),
                    position= new THREE.Vector3( 0,  512, 0 ),
                    rotation= new THREE.Vector3( Math.PI / 2, 0, Math.PI )
                },
                new side{
                    img=  new azi_ny(),
                    position= new THREE.Vector3( 0, -512, 0 ),
                    rotation= new THREE.Vector3( - Math.PI / 2, 0, Math.PI )
                },
                new side{
                    img=  new azi_pz(),
                    position= new THREE.Vector3( 0, 0,  512 ),
                    rotation= new THREE.Vector3( 0, Math.PI, 0 )
                },
                new side{
                    img=  new azi_nz(),
                    position= new THREE.Vector3( 0, 0, -512 ),
                    rotation= new THREE.Vector3( 0, 0, 0 )
                }
            };
            #endregion
            Console.WriteLine("about to init CSS3DObject sides... did chrome just abuse TCP ?");

            var camera = new THREE.PerspectiveCamera(75, Native.window.aspect, 1, 1000);
            var scene = new THREE.Scene();

            var renderer = new THREE.CSS3DRenderer();

            for (var i = 0; i < sides.Length; i++)
            {
                var side = sides[i];

                var element = side.img;

                element.style.SetSize(1026, 1026);

                //element.width = 1026; // 2 pixels extra to close the gap.

                var xobject = new THREE.CSS3DObject(element);
                xobject.position.set(side.position.x, side.position.y, side.position.z);
                xobject.rotation.set(side.rotation.x, side.rotation.y, side.rotation.z);
                scene.add(xobject);

            }

            //// c48 floods the ports by now.
            //TCP enter https { ClientCounter = 344 }
            //{ RemoteEndPoint = 192.168.1.199:49221, isPeerProxy = False }

            //Unhandled Exception: OutOfMemoryException.
            //return;


            //<div style="-webkit-transform-style: preserve-3d; width: 978px; height: 664px; -webkit-transform: translate3d(0px, 0px, 432.6708237832803px) matrix3d(0.34382355213165283, -0.024581052362918854, -0.938712477684021, 0, 0, -0.9996572732925415, 0.026176948100328445, 0, 0.9390342831611633, 0.00900025200098753, 0.34370577335357666, 0, 0, 0, 0, 0.9999999403953552) translate3d(489px, 332px, 0px);">
            //        <img src="assets/CSSAzimuthMapViz/posx.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, -512, 0, 0, 1);"><img src="assets/CSSAzimuthMapViz/negx.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 512, 0, 0, 1);"><img src="assets/CSSAzimuthMapViz/posy.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 512, 0, 1);"><img src="assets/CSSAzimuthMapViz/negy.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, -512, 0, 1);"><img src="assets/CSSAzimuthMapViz/posz.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, 512, 1);"><img src="assets/CSSAzimuthMapViz/negz.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, -512, 1);"></div>
            //<div style="-webkit-transform-style: preserve-3d; width: 978px; height: 664px; -webkit-transform: translate3d(0px, 0px, 432.6708237832803px) matrix3d(-0.4524347484111786, 0, 0.8917974829673767, 0, 0, -1, 0, 0, -0.8917974829673767, 0, -0.4524347484111786, 0, 0, 0, 0, 1) translate3d(489px, 332px, 0px);">
            // <img width="1026" src="textures/cube/Bridge2/posx.jpg"                                             style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(0, 0, -1, 0, 0, -1, 0, 0, 1, 0, 0, 0, -512, 0, 0, 1);"><img width="1026" src="textures/cube/Bridge2/negx.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, 0, 512, 0, 0, 1);"><img width="1026" src="textures/cube/Bridge2/posy.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(-1, 0, 0, 0, 0, 0, 1, 0, 0, -1, 0, 0, 0, 512, 0, 1);"><img width="1026" src="textures/cube/Bridge2/negy.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(-1, 0, 0, 0, 0, 0, -1, 0, 0, 1, 0, 0, 0, -512, 0, 1);"><img width="1026" src="textures/cube/Bridge2/posz.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(-1, 0, 0, 0, 0, -1, 0, 0, 0, 0, -1, 0, 0, 0, 512, 1);"><img width="1026" src="textures/cube/Bridge2/negz.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, -512, 1);"></div> 


            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.domElement.AttachToDocument();

            #region onresize
            Native.window.onresize +=
                delegate
                {
                    camera.aspect = Native.window.aspect;
                    camera.updateProjectionMatrix();

                    renderer.setSize(Native.window.Width, Native.window.Height);
                };
            #endregion

            var target = new THREE.Vector3();

            var lon = 90.0;
            var lat = 0.0;
            var phi = 0.0;
            var theta = 0.0;





            var drag = false;


            Native.window.onframe +=
                delegate
                {
                    if (Native.document.pointerLockElement == Native.document.body)
                        lon += 0.00;
                    else
                        lon += 0.01;

                    lat = Math.Max(-85, Math.Min(85, lat));

                    //Native.document.title = new { lon, lat }.ToString();


                    phi = THREE.Math.degToRad(90 - lat);
                    theta = THREE.Math.degToRad(lon);

                    target.x = Math.Sin(phi) * Math.Cos(theta);
                    target.y = Math.Cos(phi);
                    target.z = Math.Sin(phi) * Math.Sin(theta);

                    camera.lookAt(target);

                    renderer.render(scene, camera);

                };

            #region ontouchmove
            var touchX = 0;
            var touchY = 0;

            Native.document.body.ontouchstart +=
                e =>
                {
                    e.preventDefault();

                    var touch = e.touches[0];

                    touchX = touch.screenX;
                    touchY = touch.screenY;

                };


            Native.document.body.ontouchmove +=
              e =>
              {
                  e.preventDefault();

                  var touch = e.touches[0];

                  lon -= (touch.screenX - touchX) * 0.1;
                  lat += (touch.screenY - touchY) * 0.1;

                  touchX = touch.screenX;
                  touchY = touch.screenY;

              };
            #endregion






            #region camera rotation
            Native.document.body.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        lon += e.movementX * 0.1;
                        lat -= e.movementY * 0.1;

                        //Console.WriteLine(new { lon, lat, e.movementX, e.movementY });
                    }
                };


            Native.document.body.onmouseup +=
              e =>
              {
                  drag = false;
                  e.preventDefault();
              };

            Native.document.body.onmousedown +=
                e =>
                {
                    //e.CaptureMouse();

                    drag = true;
                    e.preventDefault();
                    Native.document.body.requestPointerLock();

                };


            Native.document.body.ondblclick +=
                e =>
                {
                    e.preventDefault();

                    Console.WriteLine("requestPointerLock");
                };

            #endregion



            Native.document.body.onmousewheel +=
                e =>
                {
                    camera.fov -= e.WheelDirection * 5.0;
                    camera.updateProjectionMatrix();
                };


        }

    }
}

//..0388:01:01:0f RewriteToAssembly error: System.IO.FileNotFoundException: Could not load file or assembly 'ScriptCoreLib.Async, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependenc
