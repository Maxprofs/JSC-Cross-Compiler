﻿using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CoreAudio.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using starling.filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl.Library
{

    public class StarlingGameSpriteWithPedControl : StarlingGameSpriteWithPhysics
    {
        public static KeySample __keyDown = new KeySample();

        public StarlingGameSpriteWithPedControl()
        {
            var textures_ped = new StarlingGameSpriteWithPedTextures(new_tex_crop);

            //this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
            {
                var patrol1 = new PhysicalPed(textures_ped, this)
                {

                    speed = 10
                };

                var physical0 = new PhysicalPed(textures_ped, this)
                {
                    AttractZombies = true
                    //speed = 8
                };


                //physical0.visual.WalkLikeZombie = true;
                physical0.visual.StandWithVisibleGun = true;



                current = physical0;

                // 32x32 = 15FPS?
                // 24x24 35?

                stage.mouseWheel += e =>
                    {
                        if (e.delta < 0)
                        {
                            this.internalscale -= 0.05;
                        }
                        if (e.delta > 0)
                        {
                            this.internalscale += 0.05;
                        }

                    };

                #region others
                for (int ix = 0; ix < 4; ix++)
                    for (int iy = 0; iy < 4; iy++)
                    {
                        var p = new PhysicalPed(textures_ped, this);

                        p.SetPositionAndAngle(
                            32 * ix, 32 * iy, random.NextDouble()
                        );

                        if (ix == 0)
                            p.BehaveLikeZombie();
                    }
                #endregion


                #region __keyDown

                stage.keyDown +=
                   e =>
                   {
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[Keys.Alt] = true;

                       __keyDown[(Keys)e.keyCode] = true;

                       this.Text = new { e.keyCode, Keys.A }.ToString();
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[Keys.Alt] = false;

                     __keyDown[(Keys)e.keyCode] = false;
                 };

                #endregion

                bool mode_changepending = false;
                var mode_gun = false;

                var sb = new Soundboard();

                onsyncframe += delegate
                {
                    #region patrol1
                    if (syncframeid % 300 == 100)
                    {
                        patrol1.body.SetAngle(
                            45.DegreesToRadians()
                        );

                        var partol_commands = new KeySample();

                        partol_commands[Keys.Up] = true;

                        patrol1.SetVelocityFromInput(partol_commands);
                    }

                    if (syncframeid % 300 == 150)
                    {
                        var partol_commands = new KeySample();

                        //partol_commands[Keys.Left] = true;

                        patrol1.SetVelocityFromInput(partol_commands);
                    }

                    if (syncframeid % 300 == 200)
                    {
                        patrol1.body.SetAngle(
                            (180 + 45).DegreesToRadians()
                        );

                        var partol_commands = new KeySample();

                        partol_commands[Keys.Up] = true;

                        patrol1.SetVelocityFromInput(partol_commands);
                    }


                    if (syncframeid % 300 == 250)
                    {
                        var partol_commands = new KeySample();

                        //partol_commands[Keys.Left] = true;

                        patrol1.SetVelocityFromInput(partol_commands);
                    }
                    #endregion


                    current.SetVelocityFromInput(__keyDown);

                    #region simulate a weapone!
                    if (__keyDown[Keys.ControlKey])
                    {
                        mode_gun = true;
                    }
                    else
                    {
                        if (mode_gun)
                        {
                            mode_gun = false;

                            sb.snd_shotgun3.play();

                            Action<double> CreateBullet =
                                a =>
                                {
                                    var bodyDef = new b2BodyDef();

                                    bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                                    // stop moving if legs stop walking!
                                    bodyDef.linearDamping = 0;
                                    bodyDef.angularDamping = 0;
                                    //bodyDef.angle = 1.57079633;
                                    bodyDef.fixedRotation = true;


                                    var dx = Math.Cos(current.body.GetAngle() + current.CameraRotation + a);
                                    var dy = Math.Sin(current.body.GetAngle() + current.CameraRotation + a);

                                    var body = damage_b2world.CreateBody(bodyDef);
                                    body.SetPosition(
                                        new b2Vec2(
                                            current.body.GetPosition().x + dx * 0.3,
                                            current.body.GetPosition().y + dy * 0.3
                                        )
                                    );

                                    body.SetLinearVelocity(
                                           new b2Vec2(
                                             dx * 100,
                                            dy * 100
                                        )
                                    );

                                    var fixDef = new Box2D.Dynamics.b2FixtureDef();
                                    fixDef.density = 20;
                                    fixDef.friction = 0.0;
                                    fixDef.restitution = 0;


                                    fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(0.3);


                                    var fix = body.CreateFixture(fixDef);
                                };

                            CreateBullet(-6.DegreesToRadians());
                            CreateBullet(-2.DegreesToRadians());
                            CreateBullet(2.DegreesToRadians());
                            CreateBullet(6.DegreesToRadians());

                            //body.SetPosition(
                            //    new b2Vec2(0, -100 * 16)
                            //);
                        }

                    }
                    #endregion


                    #region mode
                    if (!__keyDown[Keys.Space])
                    {
                        // space is not down.
                        mode_changepending = true;
                    }
                    else
                    {
                        if (mode_changepending)
                        {
                            if (physical0.visual.LayOnTheGround)
                                physical0.visual.LayOnTheGround = false;
                            else
                                physical0.visual.LayOnTheGround = true;

                            mode_changepending = false;



                        }
                    }
                    #endregion



                };


            };



        }
    }
}
