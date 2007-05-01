﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript
{
    using ScriptCoreLib.JavaScript;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.DOM.HTML;


    using ScriptCoreLib;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;


    [Script]
    public class AnimatedLabel
    {
        static AnimatedLabel()
        {
            Native.Window.onload +=
                delegate
                {
                    foreach (IHTMLElement v in Native.Document.getElementsByClassName("AnimatedLabel").ToArray())
                    {
                        IHTMLElement target = v;

                        target.style.color = Color.Red;
                        target.style.borderWidth = "1px";
                        target.style.borderStyle = "solid";
                        target.style.cursor = IStyle.CursorEnum.pointer;

                        var IsHot = false;

                        Action RandomizeColors =
                            () =>
                            {
                                Func<Color> RandomColor = () => (Color)Native.Math.floor(Native.Math.random() * 0xffffff);

                                if (!IsHot)
                                {
                                    target.style.color = RandomColor();
                                    target.style.backgroundColor = RandomColor();
                                }

                                target.style.borderColor = RandomColor();
                            };


                        Timer.Interval(
                            delegate(Timer t)
                            {
                                RandomizeColors();
                            },
                            300
                        );


                        target.onmouseover +=
                            delegate
                            {
                                IsHot = true;

                                target.style.color = Color.System.HighlightText;
                                target.style.backgroundColor = Color.System.Highlight;
                            };

                        target.onmouseout +=
                            delegate
                            {
                                IsHot = false;
             
                                RandomizeColors();
                            };
                    }


                };
        }
    }
}

public partial class AnimatedLabel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
