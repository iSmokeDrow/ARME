namespace ARME
{
    partial class NFMHelper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NFMHelper));
            this.dg_nfaprops = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dg_nfaprops)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_nfaprops
            // 
            this.dg_nfaprops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_nfaprops.Location = new System.Drawing.Point(2, 2);
            this.dg_nfaprops.Name = "dg_nfaprops";
            this.dg_nfaprops.Size = new System.Drawing.Size(699, 175);
            this.dg_nfaprops.TabIndex = 0;
            // 
            // NFMHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 180);
            this.Controls.Add(this.dg_nfaprops);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NFMHelper";
            this.Text = "NFMHelper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NFMHelper_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dg_nfaprops)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_nfaprops;


    }
}