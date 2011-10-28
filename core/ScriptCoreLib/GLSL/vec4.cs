﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using genType = Single;
    using System.Runtime.InteropServices;

    // http://ioctl.eu/browser/opengl/base/src/vec4.h
    [Script]
    [StructLayout(LayoutKind.Explicit)]
    public struct vec4
    {
        #region value[0]
        [FieldOffset(0)]
        public genType x;
        [FieldOffset(0)]
        public genType r;
        [FieldOffset(0)]
        public genType s;
        #endregion

        #region value[1]
        [FieldOffset(sizeof(genType) * 1)]
        public genType y;
        [FieldOffset(sizeof(genType) * 1)]
        public genType g;
        [FieldOffset(sizeof(genType) * 1)]
        public genType t;
        #endregion

        #region value[2]
        [FieldOffset(sizeof(genType) * 2)]
        public genType z;
        [FieldOffset(sizeof(genType) * 2)]
        public genType b;
        [FieldOffset(sizeof(genType) * 2)]
        public genType p;
        #endregion

        #region value[3]
        [FieldOffset(sizeof(genType) * 2)]
        public genType w;
        [FieldOffset(sizeof(genType) * 2)]
        public genType a;
        [FieldOffset(sizeof(genType) * 2)]
        public genType q;
        #endregion


        // see
        // http://www.opengl.org/wiki/GLSL_:_common_mistakes
        //  fvalue1.x + fvalue1.y 
        // Dot products are to be to be generated!
        public vec2 xy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        // Dot products are to be to be generated!
        public vec3 rgb { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        // Dot products are to be to be generated!
        public vec3 xyz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public static vec4 operator *(mat4 x, vec4 y)
        {
            throw new NotImplementedException();
        }

        public static vec4 operator +(vec4 x, vec4 y)
        {
            throw new NotImplementedException();
        }
    }
}
