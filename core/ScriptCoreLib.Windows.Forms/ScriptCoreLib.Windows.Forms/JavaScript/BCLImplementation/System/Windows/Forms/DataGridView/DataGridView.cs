﻿using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridView))]
    public partial class __DataGridView : __Control, __ISupportInitialize
    {
        // X:\jsc.svn\examples\javascript\forms\FormsHistoricBindingSourcePosition\FormsHistoricBindingSourcePosition\ApplicationControl.cs
        public bool ColumnHeadersVisible { get; set; }

        public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode { get; set; }




        #region AllowUserToDeleteRows
        public event EventHandler AllowUserToDeleteRowsChanged;
        bool InternalAllowUserToDeleteRows = true;
        public bool AllowUserToDeleteRows
        {
            get
            {
                return InternalAllowUserToDeleteRows;
            }
            set
            {

                InternalAllowUserToDeleteRows = value;
                if (AllowUserToDeleteRowsChanged != null)
                    AllowUserToDeleteRowsChanged(this, new EventArgs());

            }
        }
        #endregion


        #region AllowUserToAddRows
        public event EventHandler AllowUserToAddRowsChanged;
        bool InternalAllowUserToAddRows;
        public bool AllowUserToAddRows
        {
            get
            {
                return InternalAllowUserToAddRows;
            }
            set
            {

                InternalAllowUserToAddRows = value;
                if (AllowUserToAddRowsChanged != null)
                    AllowUserToAddRowsChanged(this, new EventArgs());

            }
        }
        #endregion


        public IHTMLDiv __Corner;



        // naming convention
        public IHTMLDiv InternalElement;
        public IHTMLDiv InternalScrollContainerElement;

        [Obsolete("rename to InternalVirtual ?")]
        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        public override IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return this.InternalScrollContainerElement;
            }
        }


        // we do not yet use it.. needs to be tested
        // used for
        // SelectionBackColor
        // tested by
        // X:\jsc.svn\examples\javascript\Forms\FormsGridCellStyle\FormsGridCellStyle\Application.cs


        #region DefaultCellStyle
        __DataGridViewCellStyle InternlDefaultCellStyle;
        public DataGridViewCellStyle DefaultCellStyle
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\FormsDataGridRowSelect\FormsDataGridRowSelect\ApplicationControl.cs


            get
            {
                return InternlDefaultCellStyle;
            }
            set
            {
                this.InternlDefaultCellStyle = value;

                Action y = delegate
                {

                    if ((object)this.InternlDefaultCellStyle != value)
                        return;

                    //Console.WriteLine("AlternatingRowsDefaultCellStyle " + new { this.__ContentTable_css_alt_td });


                    var BackColor = this.InternlDefaultCellStyle.BackColor;
                    this.__ContentTable_css_td.style.backgroundColor = BackColor.ToString();

                };

                this.InternlDefaultCellStyle.InternalBackColorChanged += delegate { y(); };

                y();
            }
        }
        #endregion


        //script: error JSC1000: No implementation found for this native method, please implement 
        // [System.Windows.Forms.DataGridView.set_AlternatingRowsDefaultCellStyle(System.Windows.Forms.DataGridViewCellStyle)]

        #region AlternatingRowsDefaultCellStyle
        __DataGridViewCellStyle InternalAlternatingRowsDefaultCellStyle;
        public DataGridViewCellStyle AlternatingRowsDefaultCellStyle
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\FormsDataGridRowSelect\FormsDataGridRowSelect\ApplicationControl.cs


            get
            {
                return InternalAlternatingRowsDefaultCellStyle;
            }
            set
            {
                this.InternalAlternatingRowsDefaultCellStyle = value;

                Action y = delegate
                {

                    if ((object)this.InternalAlternatingRowsDefaultCellStyle != value)
                        return;

                    //Console.WriteLine("AlternatingRowsDefaultCellStyle " + new { this.__ContentTable_css_alt_td });


                    var BackColor = this.InternalAlternatingRowsDefaultCellStyle.BackColor;
                    this.__ContentTable_css_alt_td.style.backgroundColor = BackColor.ToString();

                };

                this.InternalAlternatingRowsDefaultCellStyle.InternalBackColorChanged += delegate { y(); };

                y();
            }
        }
        #endregion

        #region ColumnHeadersDefaultCellStyle
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131213-forms-css

        public __DataGridViewCellStyle InternalColumnHeadersDefaultCellStyle;
        public DataGridViewCellStyle ColumnHeadersDefaultCellStyle
        {
            get
            {
                return this.InternalColumnHeadersDefaultCellStyle;
            }
            set
            {
                this.InternalColumnHeadersDefaultCellStyle = value;

                Action y = delegate
                {
                    if ((object)this.InternalColumnHeadersDefaultCellStyle != value)
                        return;

                    var BackColor = this.ColumnHeadersDefaultCellStyle.BackColor;
                    this.__ColumnsTable_css_td.style.backgroundColor = BackColor.ToString();

                    this.__Corner.style.backgroundColor = BackColor.ToString();
                };

                this.InternalColumnHeadersDefaultCellStyle.InternalBackColorChanged += delegate { y(); };

                y();
            }
        }
        #endregion


        #region RowHeadersDefaultCellStyle
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131213-forms-css

        public __DataGridViewCellStyle InternalRowHeadersDefaultCellStyle;
        public DataGridViewCellStyle RowHeadersDefaultCellStyle
        {
            get
            {
                return this.InternalRowHeadersDefaultCellStyle;
            }
            set
            {
                this.InternalRowHeadersDefaultCellStyle = value;

                Action y = delegate
                {
                    if ((object)this.InternalRowHeadersDefaultCellStyle != value)
                        return;

                    var BackColor = this.InternalRowHeadersDefaultCellStyle.BackColor;
                    this.__RowsTable_css_td.style.backgroundColor = BackColor.ToString();
                };

                this.InternalRowHeadersDefaultCellStyle.InternalBackColorChanged += delegate { y(); };

                y();
            }
        }
        #endregion


        #region AutoResizeColumn
        public Action<int, bool> InternalAutoResizeColumn;

        public void AutoResizeColumn(int columnIndex)
        {
            AutoResizeColumn(columnIndex, true);
        }

        public void AutoResizeColumn(int columnIndex, bool ObeyAutoSizeMode)
        {
            var AutoResizeColumnStopwatch = Stopwatch.StartNew();

            if (InternalAutoResizeColumn != null)
                InternalAutoResizeColumn(columnIndex, ObeyAutoSizeMode);

            // 19238ms { Name = dataGridView1, ElapsedMilliseconds = 370 } exit AutoResizeColumn  
            // 18436ms { Name = dataGridView2 } exit AutoResizeColumn { ElapsedMilliseconds = 391, columnIndex = 6 } 
            // 590530ms { Name = dataGridView1 } exit AutoResizeColumn { ElapsedMilliseconds = 127, columnIndex = 4 } 
            // close the console!

            // 1628ms { Name = dataGridView1 } exit AutoResizeColumn { ElapsedMilliseconds = 1106, columnIndex = 0 } 

            //Console.WriteLine(
            //    new { Name }
            //    + " exit AutoResizeColumn "
            //    + new { AutoResizeColumnStopwatch.ElapsedMilliseconds, columnIndex }
            //    );
        }
        #endregion


        #region GridColor
        public CSSStyleRuleMonkier InternalGridColor_css;

        global::System.Drawing.Color InternalGridColor;
        public global::System.Drawing.Color GridColor
        {
            get
            {
                return InternalGridColor;
            }
            set
            {
                this.InternalGridColor = value;

                //l.style.backgroundColor

                // we could use a group style instead?

                //InternalGridColor_css.style.backgroundColor = value.ToString();

                ////SourceCell.InternalTableColumn.style.borderBottom = "1px solid rgba(0,0,0, 0.4)";
                //SourceCell.InternalTableColumn.style.borderBottom = "2px solid red";

                this.__ContentTable_css_td.style.borderBottom = "1px solid " + value.ToString();
                this.__ContentTable_css_td.style.borderRight = "1px solid " + value.ToString();

                this.__RowsTable_css_td.style.borderBottom = "1px solid " + value.ToString();
                this.__RowsTable_css_td.style.borderRight = "1px solid " + value.ToString();
                this.__RowsTable_css_td.style.borderLeft = "1px solid " + value.ToString();

                this.__ColumnsTable_css_td.style.borderRight = "1px solid " + value.ToString();
                this.__ColumnsTable_css_td.style.borderBottom = "1px solid " + value.ToString();
                this.__ColumnsTable_css_td.style.borderTop = "1px solid " + value.ToString();


                this.__Corner.style.border = "1px solid " + value.ToString();
            }
        }
        #endregion

        #region BackgroundColor
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BackgroundColor(System.Drawing.Color)]
        public global::System.Drawing.Color InternalBackgroundColor;
        public event Action InternalBackgroundColorChanged;
        public global::System.Drawing.Color BackgroundColor
        {
            get
            {
                return InternalBackgroundColor;
            }
            set
            {
                this.InternalBackgroundColor = value;
                this.InternalScrollContainerElement.style.backgroundColor = InternalBackgroundColor.ToString();
                if (InternalBackgroundColorChanged != null)
                    InternalBackgroundColorChanged();
            }
        }
        #endregion

        public bool InternalSkipAutoSize;


        private void InternalBindCellMouseEnter(__DataGridViewCell SourceCell)
        {
            SourceCell.InternalTableColumn_div.onmouseover +=
                delegate
                {
                    if (this.CellMouseEnter != null)
                        this.CellMouseEnter(
                            this,
                            new DataGridViewCellEventArgs(
                                SourceCell.ColumnIndex,
                                SourceCell.OwningRow.Index
                            )
                        );

                };

            SourceCell.InternalTableColumn_div.onmouseout +=
                delegate
                {
                    if (this.CellMouseLeave != null)
                        this.CellMouseLeave(
                            this,
                            new DataGridViewCellEventArgs(
                                SourceCell.ColumnIndex,
                                SourceCell.OwningRow.Index
                            )
                        );

                };
        }




        public bool AllowUserToOrderColumns { get; set; }
        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }



        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.get_SelectedRows()]

        public __DataGridViewColumnCollection InternalColumns;
        public DataGridViewColumnCollection Columns { get; set; }



        public __DataGridViewRowCollection InternalRows;
        public DataGridViewRowCollection Rows { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            // resume updates?
        }

        public Action<__DataGridViewCell> InternalRaiseCellBeginEdit;
        public Action<__DataGridViewCell> InternalRaiseCellEndEdit;
        public Action<__DataGridViewCell> InternalRaiseCellValueChanged;


        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.add_ColumnWidthChanged(System.Windows.Forms.DataGridViewColumnEventHandler)]



        public event DataGridViewColumnEventHandler ColumnWidthChanged;
        public event DataGridViewColumnEventHandler ColumnAdded;

        public event DataGridViewCellEventHandler CellDoubleClick;
        public event DataGridViewCellEventHandler CellContentClick;
        public event DataGridViewCellEventHandler CellClick;
        public event DataGridViewCellEventHandler CellEndEdit;
        public event DataGridViewCellCancelEventHandler CellBeginEdit;
        public event DataGridViewCellEventHandler CellValueChanged;

        public event DataGridViewRowEventHandler UserAddedRow;

        public event DataGridViewRowEventHandler InternalBeforeUserDeletedRow;
        public event DataGridViewRowEventHandler UserDeletedRow;

        public event EventHandler SelectionChanged;
        public event DataGridViewCellEventHandler CellEnter;
        public event DataGridViewCellEventHandler CellLeave;


        public DataGridViewCell this[int columnIndex, int rowIndex]
        {
            get
            {
                return (DataGridViewCell)(object)this
                    .InternalRows.InternalItems.Source[rowIndex]
                    .InternalCells.InternalItems[columnIndex];
            }
            set
            {
            }
        }





        public DataGridViewSelectionMode SelectionMode { get; set; }


        // tested by
        // X:\jsc.svn\examples\javascript\forms\FormsDataGridRowSelect\FormsDataGridRowSelect\ApplicationControl.Designer.cs
        public bool ReadOnly { get; set; }


        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_ReadOnly(System.Boolean)]

        //        arg[0] is typeof System.Windows.Forms.BorderStyle
        ////script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BorderStyle(System.Windows.Forms.BorderStyle)]
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BorderStyle(System.Windows.Forms.BorderStyle)]

        #region BorderStyle
        public BorderStyle InternalBorderStyle;
        public BorderStyle BorderStyle
        {
            get
            {
                return InternalBorderStyle;
            }
            set
            {
                InternalBorderStyle = value;

                if (value == global::System.Windows.Forms.BorderStyle.FixedSingle)
                {
                    // a border here will make us bigger if we are dock fill
                    // add css boxSizing
                    this.InternalScrollContainerElement.style.border = "1px solid gray";
                }
                else
                {
                    this.InternalScrollContainerElement.style.border = "none";

                }

            }
        }
        #endregion


        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_ColumnHeadersBorderStyle(System.Windows.Forms.DataGridViewHeaderBorderStyle)]

        public DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle { get; set; }







        public event DataGridViewCellValidatingEventHandler CellValidating;


        #region CellFormatting
        // tested by
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataGridViewCellFormattingEven\TestDataGridViewCellFormattingEven\ApplicationControl.cs
        public event DataGridViewCellFormattingEventHandler CellFormatting;


        private void InternalRaiseCellFormatting(__DataGridViewCell SourceCell)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestDataGridViewCellFormattingEven\TestDataGridViewCellFormattingEven\ApplicationControl.cs
            // how costly is this? should we call this
            // only for cells in view?

            var a = new DataGridViewCellFormattingEventArgs(
                SourceCell.ColumnIndex,
                SourceCell.OwningRow.Index,
                SourceCell.Value,

                // what type do we desire?
                typeof(string),

                SourceCell.InternalStyle
            );

            if (this.CellFormatting != null)
                this.CellFormatting(
                    this,
                   a
                );


            SourceCell.FormattedValue = a.Value;

            //Console.WriteLine("InternalRaiseCellFormatting " + new { SourceCell.FormattedValue });
        }
        #endregion


        public event DataGridViewCellEventHandler CellMouseEnter;
        public event DataGridViewCellEventHandler CellMouseLeave;


        public static implicit operator __DataGridView(DataGridView g)
        {
            return (__DataGridView)(object)g;
        }


        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412
        public DataGridViewRow RowTemplate { get; set; }
    }
}
