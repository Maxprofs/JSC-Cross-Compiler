using Abstractatech.ConsoleFormPackage.Library;
using FlashHeatZeeker.UnitPedSync.Design;
using FlashHeatZeeker.UnitPedSync.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
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
using Abstractatech.JavaScript.FormAsPopup;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;

namespace FlashHeatZeeker.UnitPedSync
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        ApplicationSprite leftsprite = new ApplicationSprite();
        ApplicationSprite uppersprite = new ApplicationSprite();
        ApplicationSprite lowersprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            InitializeSprites();

            InitializeConsole();

            InitializeTransport();


        }

        private void InitializeTransport()
        {
            Console.WriteLine("leftsprite.__transport_out");
            leftsprite.__transport_out +=
                xml =>
                {
                    uppersprite.__transport_in(xml);
                    lowersprite.__transport_in_fakelag(xml);
                };

            uppersprite.__transport_out +=
                xml =>
                {
                    leftsprite.__transport_in(xml);
                    lowersprite.__transport_in_fakelag(xml);

                };

            lowersprite.__transport_out +=
           xml =>
           {
               leftsprite.__transport_in_fakelag(xml);
               uppersprite.__transport_in_fakelag(xml);

           };


            //Console.WriteLine("before WhenReady");
            //leftsprite.WhenReady(
            //    delegate
            //    {


            //        var __xml = new XElement("check", new XAttribute("bugfix", "bugfix"));
            //        var __xmlstring = __xml.ToString();

            //        Console.WriteLine(new { __xmlstring });

            //        leftsprite.__raise_transport_out(__xmlstring);
            //        Console.WriteLine("after __raise_transport_out");
            //    }
            //);
        }

        private void InitializeConsole()
        {
            #region con
            var con = new ConsoleForm();

            con.InitializeConsoleFormWriter();

            con.Show();

            con.Height = 150;
            con.Left = Native.window.Width - con.Width;
            con.Top = 0;

            Native.window.onresize +=
                  delegate
                  {
                      con.Left = Native.window.Width - con.Width;
                      con.Top = 0;
                  };


            con.Opacity = 0.6;




            // !! not compatible yet
            //FormAsPopupExtensions
            con.HandleFormClosing = false;
            con.PopupInsteadOfClosing();
            #endregion

            Action<string> Console_Write =
               x =>
               {
                   Console.Write(x);
               };


            Action<string> Console_WriteLine =
               x =>
               {
                   Console.WriteLine(x);
               };

            leftsprite.InitializeConsoleFormWriter(
              Console_Write, Console_WriteLine
           );
        }

        private void InitializeSprites()
        {
            {
                //leftsprite.wmode();

                leftsprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(0, 0);
                           embed.style.SetSize(Native.window.Width / 2 - 1, Native.window.Height);

                           Native.window.onresize +=
                               delegate
                               {
                                   embed.style.SetSize(Native.window.Width / 2 - 1, Native.window.Height);
                               };
                       }
                   );
            }

            {

                //uppersprite.wmode();

                uppersprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(Native.window.Width / 2, 0);
                           embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2 - 1);

                           Native.window.onresize +=
                               delegate
                               {
                                   embed.style.SetLocation(Native.window.Width / 2, 0);
                                   embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2 - 1);
                               };
                       }
                   );
            }

            {
                //lowersprite.wmode();

                lowersprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(Native.window.Width / 2, Native.window.Height / 2);
                           embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2);

                           Native.window.onresize +=
                               delegate
                               {
                                   embed.style.SetLocation(Native.window.Width / 2, Native.window.Height / 2);
                                   embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2);
                               };
                       }
                   );
            }
        }

    }



}
