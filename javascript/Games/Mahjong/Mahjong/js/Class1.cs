﻿//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace Mahjong.js
{
    [Script]
    class TileSettings
    {
        public int OuterWidth = 32;
        public int OuterHeight = 46;

        public int InnerWidth = 24;
        public int InnerHeight = 38;

        public IHTMLImage BackgroundImage;

    }

    [Script]
    class TileInfo
    {
        public IHTMLImage Image;



    }

    [Script]
    class Tile
    {
        public readonly TileInfo Info;
        public readonly TileSettings Settings;

        public readonly IHTMLDiv Background = new IHTMLDiv();
        public readonly IHTMLDiv Display = new IHTMLDiv();




        public Tile(TileInfo Info, TileSettings Settings)
        {
            this.Settings = Settings;
            this.Info = Info;

            this.Display.style.SetLocation(
                Settings.OuterWidth - Settings.InnerWidth - 1, 1, Settings.InnerWidth, Settings.InnerHeight);

            Info.Image.ToBackground(this.Display.style);

            this.Background.style.SetSize(Settings.OuterWidth, Settings.OuterHeight);
            this.Background.appendChild(this.Display);

            Settings.BackgroundImage.ToBackground(Background);
        }
    }

    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            var s = new TileSettings
                    {
                        BackgroundImage = "assets/tile0.png"
                    };


            var i1 = new TileInfo { Image = "assets/1.png" };
            var i3 = new TileInfo { Image = "assets/3.png" };
            var i4 = new TileInfo { Image = "assets/4.png" };
            var i5 = new TileInfo { Image = "assets/5.png" };
            var i6 = new TileInfo { Image = "assets/6.png" };
            var i7 = new TileInfo { Image = "assets/7.png" };

            #region CreateTile
            Func<int, int, TileInfo, Tile> CreateTile =
                (x, y, i) =>
                {
                    var a = new Tile(i, s);

                    a.Background.attachToDocument();
                    a.Background.SetCenteredLocation(x, y);

                    a.Background.onmouseover +=
                        delegate { a.Background.style.Opacity = 0.5; };


                    a.Background.onmouseout +=
                        delegate { a.Background.style.Opacity = 1; };

                    return a;
                };
            #endregion


            CreateTile(40 * 1, 40, i1);
            CreateTile(40 * 2, 40, i3);
            CreateTile(40 * 3, 40, i4);
            CreateTile(40 * 4, 40, i5);
            CreateTile(40 * 5, 40, i6);
            CreateTile(40 * 7, 40, i7);

        }




        static Class1()
        {
            Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
