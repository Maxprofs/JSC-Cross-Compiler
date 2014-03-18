using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLLesson02.HTML.Pages;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Drawing;
using WebGLLesson02.Shaders;
using System.Collections.Generic;

namespace WebGLLesson02
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=134 by Giles
         * 
         * 01. Created a new project of type Web Application
         * 02. initGL
         * 03. initShaders
         */

        public readonly ApplicationWebService service = new ApplicationWebService();
        public bool IsDisposed;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {

            var size = 500;




            var gl = new WebGLRenderingContext();


            var canvas = gl.canvas.AttachToDocument();

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;

            var gl_viewportWidth = size;
            var gl_viewportHeight = size;




            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();


                };
            #endregion




            var shaderProgram = gl.createProgram();


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
                    throw new InvalidOperationException("shader failed");
                }

                return shader;
            };
            #endregion

            var vs = createShader(new GeometryVertexShader());
            var fs = createShader(new GeometryFragmentShader());


            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);


            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");

            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            // new in lesson 02
            var shaderProgram_vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexColorAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");



            var mvMatrix = glMatrix.mat4.create();
            var pMatrix = glMatrix.mat4.create();

            #region setMatrixUniforms
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);
                };
            #endregion




            #region init buffers
            var triangleVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
            var vertices = new[]{
                     0.0f,  1.0f,  0.0f,
                    -1.0f, -1.0f,  0.0f,
                     1.0f, -1.0f,  0.0f
                };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            var triangleVertexPositionBuffer_itemSize = 3;
            var triangleVertexPositionBuffer_numItems = 3;

            #region new in lesson 02

            var triangleVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexColorBuffer);

            var colors = new[]{
                1.0f, 0.0f, 0.0f, 1.0f,
                0.0f, 1.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f, 1.0f
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors), gl.STATIC_DRAW);
            var triangleVertexColorBuffer_itemSize = 4;
            var triangleVertexColorBuffer_numItems = 3;
            #endregion


            var squareVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
            vertices = new[]{
                     1.0f,  1.0f,  0.0f,
                    -1.0f,  1.0f,  0.0f,
                     1.0f, -1.0f,  0.0f,
                    -1.0f, -1.0f,  0.0f
                };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

            var squareVertexPositionBuffer_itemSize = 3;
            var squareVertexPositionBuffer_numItems = 4;

            #region new in lesson 02
            var squareVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
            #region loop unrolled :)
            colors = new[]{
                0.5f, 0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 1.0f, 1.0f
            };
            #endregion



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors), gl.STATIC_DRAW);
            var squareVertexColorBuffer_itemSize = 4;
            var squareVertexColorBuffer_numItems = 4;
            #endregion

            #endregion




            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.enable(gl.DEPTH_TEST);



            #region drawScene
            Action drawScene =
                delegate
                {
                    gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                    glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                    glMatrix.mat4.identity(mvMatrix);

                    glMatrix.mat4.translate(mvMatrix, new float[] { -1.5f, 0.0f, -7.0f });
                    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
                    gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, triangleVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);


                    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexColorBuffer);
                    gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, triangleVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);




                    setMatrixUniforms();
                    gl.drawArrays(gl.TRIANGLES, 0, triangleVertexPositionBuffer_numItems);


                    glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 0.0f, 0.0f });
                    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
                    gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, squareVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                    gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, squareVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);



                    setMatrixUniforms();
                    gl.drawArrays(gl.TRIANGLE_STRIP, 0, squareVertexPositionBuffer_numItems);
                };
            #endregion

            drawScene();

            #region AtResize
            Action AtResize =
                delegate
                {
                    gl_viewportWidth = Native.window.Width;
                    gl_viewportHeight = Native.window.Height;

                    canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                    canvas.width = gl_viewportWidth;
                    canvas.height = gl_viewportHeight;

                    drawScene();
                };

            Native.window.onresize +=
                e =>
                {
                    AtResize();
                };
            AtResize();
            #endregion
        }

    }
}