using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitTank.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using starling.core;
using System;

namespace FlashHeatZeeker.UnitTank
{
    [SWF(backgroundColor = 0xA26D41)]
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
              delegate
              {
                  // http://gamua.com/starling/first-steps/
                  // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                  Starling.handleLostContext = true;



                  // Error	1	'.ctor' is not supported by the language	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.UnitTank\ApplicationSprite.cs	24	27	FlashHeatZeeker.UnitTank
                  // partial build?
                  var s = new Starling(
                      typeof(StarlingGameSpriteWithTank).ToClassToken(),
                      this.stage
                  );


                  //Starling.current.showStats

                  s.showStats = true;

                  #region atresize
                  Action atresize = delegate
                  {
                      // http://forum.starling-framework.org/topic/starling-stage-resizing

                      s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                          0, 0, this.stage.stageWidth, this.stage.stageHeight
                      );

                      s.stage.stageWidth = this.stage.stageWidth;
                      s.stage.stageHeight = this.stage.stageHeight;


                      //b2stage_centerize();
                  };

                  atresize();
                  #endregion

                  StarlingGameSpriteBase.onresize =
                      yield =>
                      {
                          this.stage.resize += delegate
                          {
                              atresize();

                              yield(this.stage.stageWidth, this.stage.stageHeight);
                          };

                          yield(this.stage.stageWidth, this.stage.stageHeight);
                      };




                  this.stage.enterFrame +=
                      delegate
                      {




                          StarlingGameSpriteBase.onframe(this.stage, s);
                      };

                  s.start();

              }
          );
        }

    }
}
