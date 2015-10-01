﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.g
{
    class g
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /g/
            ["GoogleNewLogoByTatsunoru"] = () => new GoogleNewLogoByTatsunoru.Shaders.ProgramFragmentShader(),

            ["ChromeShaderToyGmetaballsByGermangb"] = () => new ChromeShaderToyGmetaballsByGermangb.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyGlassPolyhedronByNrx"] = () => new ChromeShaderToyGlassPolyhedronByNrx.Shaders.ProgramFragmentShader(),

            ["GalaxySpiralsByGuil"] = () => new GalaxySpiralsByGuil.Shaders.ProgramFragmentShader(),
            ["GameLogoByVladstorm"] = () => new GameLogoByVladstorm.Shaders.ProgramFragmentShader(),
            ["GammaCorrectnessByZavie"] = () => new GammaCorrectnessByZavie.Shaders.ProgramFragmentShader(),



            ["GeneratorsByKali"] = () => new GeneratorsByKali.Shaders.ProgramFragmentShader(),
            ["GhostInTheNoiseByTomkh"] = () => new GhostInTheNoiseByTomkh.Shaders.ProgramFragmentShader(),
            

            ["GlassWithCausticByAndregc"] = () => new GlassWithCausticByAndregc.Shaders.ProgramFragmentShader(),
            ["GlxgearsByBear"] = () => new GlxgearsByBear.Shaders.ProgramFragmentShader(),
            ["GoGoLegoManByIapafoto"] = () => new GoGoLegoManByIapafoto.Shaders.ProgramFragmentShader(),
            ["GoldenTunesByPassion"] = () => new GoldenTunesByPassion.Shaders.ProgramFragmentShader(),

            ["GotthardTunnelByDr2"] = () => new GotthardTunnelByDr2.Shaders.ProgramFragmentShader(),
            ["GrapheneByFabrice"] = () => new GrapheneByFabrice.Shaders.ProgramFragmentShader(),
            ["GraphingByNimitz"] = () => new GraphingByNimitz.Shaders.ProgramFragmentShader(),
            ["GreyCamoByAirtight"] = () => new GreyCamoByAirtight.Shaders.ProgramFragmentShader(),


            ["GuitarByAtyuwen"] = () => new GuitarByAtyuwen.Shaders.ProgramFragmentShader(),
            ["GyratingGyroscopeByDr2"] = () => new GyratingGyroscopeByDr2.Shaders.ProgramFragmentShader(),
            #endregion



        };

    }
}
