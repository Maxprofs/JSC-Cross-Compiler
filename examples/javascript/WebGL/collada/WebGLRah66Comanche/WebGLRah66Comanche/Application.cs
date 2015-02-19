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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLRah66Comanche;
using WebGLRah66Comanche.Design;
using WebGLRah66Comanche.HTML.Pages;
using WebGLRah66Comanche.Library;

namespace WebGLRah66Comanche
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;

            // https://3dwarehouse.sketchup.com/model.html?id=e78dca4863e8572d86ea4fa6bd93bc43
            // https://3dwarehouse.sketchup.com/model.html?id=38d1045b8de1cf12b08e958a32ef3184

            var oo = new List<THREE.Object3D>();

            #region scene
            var window = Native.window;

            var camera = new THREE.PerspectiveCamera(
                45,
                window.aspect,
                1,
                10000
                );
            camera.position.z = 400;

            // scene

            var scene = new THREE.Scene();

            var ambient = new THREE.AmbientLight(0x101030);
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);






            var planeGeometry = new THREE.CubeGeometry(512, 512, 1);
            var plane = new THREE.Mesh(planeGeometry,
                    new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })

                );
            plane.receiveShadow = true;


            {

                var parent = new THREE.Object3D();
                parent.add(plane);
                parent.rotation.x = -Math.PI / 2;
                parent.scale.set(10, 10, 10);

                // where the fk is it?
                scene.add(parent);
            }




            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(window.Width, window.Height);

            renderer.domElement.AttachToDocument();
            renderer.domElement.style.SetLocation(0, 0);


            //var mouseX = 0;
            //var mouseY = 0;
            //var st = new Stopwatch();
            //st.Start();


            //Native.window.document.onmousemove +=
            //    e =>
            //    {
            //        mouseX = e.CursorX - Native.window.Width / 2;
            //        mouseY = e.CursorY - Native.window.Height / 2;
            //    };

            var controls = new THREE.OrbitControls(camera);

            Native.window.onframe +=
                delegate
                {

                    //oo.WithEach(
                    //    x =>
                    //        x.rotation.y = (st.ElapsedMilliseconds + mouseX * 100) * 0.00001
                    //);


                    //camera.position.x += (mouseX - camera.position.x) * .05;
                    //camera.position.y += (-mouseY - camera.position.y) * .05;

                    //camera.lookAt(scene.position);

                    controls.update();
                    camera.position = controls.center.clone();

                    renderer.render(scene, camera);
                };

            Native.window.onresize +=
                delegate
                {
                    camera.aspect = window.aspect;
                    camera.updateProjectionMatrix();

                    renderer.setSize(window.Width, window.Height);

                };
            #endregion

            #region THREE_ColladaAsset
            // why isnt it being found?
            new global::WebGLColladaExperiment.THREE_ColladaAsset(

                // we get purple small thingy
                "assets/WebGLRah66Comanche/RAH-66-Comanche-by-decten.dae"

            // maybe sketchup doesnt know how to export colors?
            //"assets/WebGLHeatZeekerColladaExperiment/sam_site.dae"
            ).Source.Task.ContinueWithResult(
                dae =>
                {

                    dae.position.y = -40;
                    //dae.position.z = 280;
                    scene.add(dae);
                    oo.Add(dae);

                    // this wont work!?
                    //dae.scale = new THREE.Vector3(40, 40, 40);

                }
            );
            #endregion


            var f = new ZeProperties();

            f.Show();


            f.Add(nameof(controls), () => controls);

            //f.treeView1.Nodes.Add("controls : " + typeof(THREE.OrbitControls)).Tag = controls;


        }

    }
}
