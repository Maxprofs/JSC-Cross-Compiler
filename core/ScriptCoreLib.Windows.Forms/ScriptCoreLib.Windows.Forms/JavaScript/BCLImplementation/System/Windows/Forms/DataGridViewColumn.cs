﻿using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewColumn))]
    internal class __DataGridViewColumn
    {
        public IHTMLTableColumn InternalTableColumn;
        public DragHelper InternalHorizontalDrag;


        public string InternalHeaderText;
        public event Action InternalHeaderTextChanged;

        public string HeaderText
        {
            get
            {
                return InternalHeaderText;
            }
            set
            {
                InternalHeaderText = value;

                if (InternalHeaderTextChanged != null)
                    InternalHeaderTextChanged();
            }
        }

        public string Name { get; set; }

        public int InternalWidth;
        public event Action InternalWidthChanged;
        public int Width
        {
            get
            {
                return InternalWidth;
            }
            set
            {
                InternalWidth = value;
                if (InternalWidthChanged != null)
                    InternalWidthChanged();
            }
        }

        public __DataGridViewColumn()
        {
            this.HeaderText = "Column";
            this.Width = 100;
        }
    }
}
