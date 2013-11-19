using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsApplication1
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
            this.applicationWebService1 = new FormsApplication1.ApplicationWebService();
            this.SuspendLayout();
            // 
            // ApplicationControl
            // 
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(612, 506);
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
        private ApplicationWebService applicationWebService1;

    }
}
