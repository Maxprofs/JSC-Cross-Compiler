﻿using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitRocket.Library
{
    public partial class PhysicalRocket : IPhysicalUnit
    {
        public double Altitude { get; set; }
        public string Identity { get; set; }
        public double CameraRotation { get; set; }
        public DriverSeat driverseat { get; set; }

        // ??
        public Image visual;

        StarlingGameSpriteWithRocketTextures textures_rocket;
        StarlingGameSpriteWithPhysics Context;

        public PhysicalRocket(
            StarlingGameSpriteWithRocketTextures textures_rocket,
            StarlingGameSpriteWithPhysics Context,
            bool issmoke = false,
            Image Explosion1 = null
            )
        {
            this.issmoke = issmoke;

            this.Context = Context;
            this.textures_rocket = textures_rocket;

            this.CurrentInput = new KeySample();

            visual = new Image(textures_rocket.rocket1());
            visual.AttachTo(Context.Content);


            //this.CameraRotation = Math.PI / 2;

            #region smoke_b2world




            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 0;
                bodyDef.angularDamping = 6;
                //bodyDef.angle = 1.57079633;
                //bodyDef.fixedRotation = true;

                if (issmoke)
                {
                    body = Context.smoke_b2world.CreateBody(bodyDef);
                }
                else
                {
                    body = Context.damage_b2world.CreateBody(bodyDef);
                }


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.0;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(0.5);

                // 
                var fix = body.CreateFixture(fixDef);

                var fix_data = new Action<double>(
                    jeep_forceA =>
                    {
                        this.body.SetActive(false);
                        this.visual.visible = false;

                        // explode?
                        this.speed = 0;
                        this.CurrentInput = new KeySample();

                        Context.CreateExplosion(
                             this.body.GetPosition().x,
                              this.body.GetPosition().y
                        );


                    }
                );

                // this does NOT work!
                fix.SetUserData(fix_data);
            }


            #endregion





            Context.internalunits.Add(this);
        }

        public Queue<PhysicalRocket> CreateSmokeRecycleCache = new Queue<PhysicalRocket>();

        public void CreateSmoke()
        {
            // how much is this huring FPS?

            if (issmoke)
                return;

            PhysicalRocket smoke = null;

            if (CreateSmokeRecycleCache.Count < 8)
            {

                smoke = new PhysicalRocket(textures_rocket, Context, issmoke: true);

            }
            else
            {
                smoke = CreateSmokeRecycleCache.Dequeue();
                smoke.body.SetActive(true);
                smoke.visual.visible = true;
            }


            smoke.smokerandom = Context.random.NextDouble() * Math.PI * 2;
            smoke.smoketime = Context.gametime.ElapsedMilliseconds;

            if (this.body.GetLinearVelocity().Length() > 0)
            {
                smoke.smokescale = 0.7 + 0.7 * Context.random.NextDouble();
                CreateSmokeRecycleCache.Enqueue(smoke);
            }
            else
            {
                smoke.smokescale = 2.0;
            }

            {
                var up = new KeySample();
                up[Keys.Up] = true;
                smoke.speed = 5;
                smoke.SetVelocityFromInput(up);
            }

            var a = this.body.GetAngle() + (175 + Context.random.Next(10)).DegreesToRadians();

            smoke.SetPositionAndAngle(
                this.body.GetPosition().x + Math.Cos(a) * 2,
                this.body.GetPosition().y + Math.Sin(a) * 2,
                a
                );
            smoke.ShowPositionAndAngle();
        }
    }
}
