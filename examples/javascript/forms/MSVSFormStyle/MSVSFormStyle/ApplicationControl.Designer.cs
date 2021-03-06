using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSVSFormStyle
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "new default Form()";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(45, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 27);
            this.button2.TabIndex = 2;
            this.button2.Text = "new metro Form()";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(45, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(211, 27);
            this.button3.TabIndex = 2;
            this.button3.Text = "new windows 3 Form()";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(45, 190);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(211, 27);
            this.button4.TabIndex = 3;
            this.button4.Text = "new custom Form()";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(45, 42);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(211, 27);
            this.button5.TabIndex = 4;
            this.button5.Text = "new Form()";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(45, 313);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(211, 27);
            this.button6.TabIndex = 5;
            this.button6.Text = "new windows 98 Form()";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(45, 433);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(211, 27);
            this.button7.TabIndex = 6;
            this.button7.Text = "new aero Form()";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(45, 223);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(211, 27);
            this.button8.TabIndex = 7;
            this.button8.Text = "new tycoon Form()";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(45, 256);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(211, 27);
            this.button9.TabIndex = 8;
            this.button9.Text = "new Heat Zeeker Form()";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(45, 346);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(211, 27);
            this.button10.TabIndex = 9;
            this.button10.Text = "new studio 2013 Form()";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(45, 379);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(211, 27);
            this.button11.TabIndex = 10;
            this.button11.Text = "new studio host 2013 Form()";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(45, 466);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(211, 27);
            this.button12.TabIndex = 6;
            this.button12.Text = "new chrome Form()";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button7_Click);
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 504);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }
        public Button button1;
        public Button button2;
        public Button button3;
        public Button button4;
        public Button button5;
        public Button button6;
        public Button button7;
        public Button button8;
        public Button button9;
        public Button button10;
        public Button button11;
        public Button button12;

    }
}
