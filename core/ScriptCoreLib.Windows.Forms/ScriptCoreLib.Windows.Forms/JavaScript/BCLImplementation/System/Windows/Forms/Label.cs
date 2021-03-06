﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{


    [Script(Implements = typeof(global::System.Windows.Forms.Label))]
    internal class __Label : __Control
    {
        public IHTMLDiv HTMLTargetContainer;

        public IHTMLElement HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTargetContainer;
            }
        }

        public IHTMLLabel InternalLabel;

        public __Label()
        {
            this.HTMLTargetContainer = new IHTMLDiv();

            this.InternalLabel = new IHTMLLabel().AttachTo(this.HTMLTargetContainer);

            this.InternalLabel.style.position = IStyle.PositionEnum.absolute;
            this.InternalLabel.style.left = "0";
            this.InternalLabel.style.right = "0";
            this.InternalLabel.style.bottom = "0";
            this.InternalLabel.style.top = "0";

            this.InternalLabel.style.whiteSpace = DOM.IStyle.WhiteSpaceEnum.pre;
            this.InternalLabel.style.display = IStyle.DisplayEnum.inline_block;

            this.HTMLTarget = this.InternalLabel;

            this.Size = new global::System.Drawing.Size(100, 18);
            this.InternalSetDefaultFont();
        }

        public override string Text
        {
            get
            {
                return HTMLTarget.innerText;
            }
            set
            {
                HTMLTarget.innerText = value;

                if (this.AutoSize)
                {
                    this.HTMLTarget.requestAnimationFrame +=
                        delegate
                        {
                            if (this.HTMLTarget.scrollWidth == 0)
                            {
                                // not attached yet?
                                //Console.WriteLine(new { this.Name } + " set_Text unable to set Size");
                                return;
                            }

                            this.Size = new global::System.Drawing.Size(

                            //this.HTMLTargetContainer.style.SetSize(
                                this.HTMLTarget.scrollWidth,
                                this.HTMLTarget.scrollHeight
                            );
                        };
                }
            }
        }

        public override global::System.Drawing.Size GetPreferredSize(global::System.Drawing.Size proposedSize)
        {
            return
                new global::System.Drawing.Size(
                    this.HTMLTarget.scrollWidth,
                    this.HTMLTarget.scrollHeight
                );
        }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.Label.set_TextAlign(System.Drawing.ContentAlignment)]
        public global::System.Drawing.ContentAlignment InternalTextAlign;

        // public virtual ContentAlignment TextAlign { get; set; }
        public virtual global::System.Drawing.ContentAlignment TextAlign
        {
            get
            {
                return InternalTextAlign;
            }
            set
            {
                InternalTextAlign = value;

                if (value == global::System.Drawing.ContentAlignment.MiddleRight)
                    this.HTMLTarget.style.textAlign = IStyle.TextAlignEnum.right;

            }
        }

        #region
        static public implicit operator Label(__Label e)
        {
            return (Label)(object)e;
        }

        static public implicit operator __Label(Label e)
        {
            return (__Label)(object)e;
        }
        #endregion

        public bool TabStop { get; set; }

        // is there a style we can use?
        public bool AutoEllipsis { get; set; }

        // how flat should we look? :)
        public FlatStyle FlatStyle { get; set; }

        BorderStyle _BorderStyle;
        public BorderStyle BorderStyle
        {
            get
            {
                return this._BorderStyle;
            }
            set
            {
                this._BorderStyle = value;

                this.HTMLTargetRef.ApplyBorderStyle(value);
            }
        }
    }
}
