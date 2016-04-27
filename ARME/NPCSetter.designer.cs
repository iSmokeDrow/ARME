namespace ARME
{
    partial class NPCSetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NPCSetter));
            this.lbl_yval = new System.Windows.Forms.Label();
            this.lbl_xval = new System.Windows.Forms.Label();
            this.lbl_y = new System.Windows.Forms.Label();
            this.lbl_x = new System.Windows.Forms.Label();
            this.brn_qpfsave = new System.Windows.Forms.Button();
            this.lblc_initscript = new System.Windows.Forms.Label();
            this.txt_initscript = new System.Windows.Forms.TextBox();
            this.lblcType = new System.Windows.Forms.Label();
            this.txt_type = new System.Windows.Forms.TextBox();
            this.lbl_idcap = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lblc_contactscript = new System.Windows.Forms.Label();
            this.txt_contactscript = new System.Windows.Forms.TextBox();
            this.txt_propID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_yval
            // 
            this.lbl_yval.AutoSize = true;
            this.lbl_yval.Location = new System.Drawing.Point(150, 14);
            this.lbl_yval.Name = "lbl_yval";
            this.lbl_yval.Size = new System.Drawing.Size(0, 13);
            this.lbl_yval.TabIndex = 36;
            // 
            // lbl_xval
            // 
            this.lbl_xval.AutoSize = true;
            this.lbl_xval.Location = new System.Drawing.Point(35, 14);
            this.lbl_xval.Name = "lbl_xval";
            this.lbl_xval.Size = new System.Drawing.Size(0, 13);
            this.lbl_xval.TabIndex = 35;
            // 
            // lbl_y
            // 
            this.lbl_y.AutoSize = true;
            this.lbl_y.Location = new System.Drawing.Point(127, 14);
            this.lbl_y.Name = "lbl_y";
            this.lbl_y.Size = new System.Drawing.Size(17, 13);
            this.lbl_y.TabIndex = 34;
            this.lbl_y.Text = "Y:";
            // 
            // lbl_x
            // 
            this.lbl_x.AutoSize = true;
            this.lbl_x.Location = new System.Drawing.Point(12, 14);
            this.lbl_x.Name = "lbl_x";
            this.lbl_x.Size = new System.Drawing.Size(17, 13);
            this.lbl_x.TabIndex = 33;
            this.lbl_x.Text = "X:";
            // 
            // brn_qpfsave
            // 
            this.brn_qpfsave.Location = new System.Drawing.Point(9, 188);
            this.brn_qpfsave.Name = "brn_qpfsave";
            this.brn_qpfsave.Size = new System.Drawing.Size(233, 25);
            this.brn_qpfsave.TabIndex = 32;
            this.brn_qpfsave.Text = "Save";
            this.brn_qpfsave.UseVisualStyleBackColor = true;
            this.brn_qpfsave.Click += new System.EventHandler(this.brn_qpfsave_Click);
            // 
            // lblc_initscript
            // 
            this.lblc_initscript.AutoSize = true;
            this.lblc_initscript.Location = new System.Drawing.Point(12, 87);
            this.lblc_initscript.Name = "lblc_initscript";
            this.lblc_initscript.Size = new System.Drawing.Size(54, 13);
            this.lblc_initscript.TabIndex = 50;
            this.lblc_initscript.Text = "Init Script:";
            // 
            // txt_initscript
            // 
            this.txt_initscript.Location = new System.Drawing.Point(9, 103);
            this.txt_initscript.Name = "txt_initscript";
            this.txt_initscript.Size = new System.Drawing.Size(233, 20);
            this.txt_initscript.TabIndex = 49;
            // 
            // lblcType
            // 
            this.lblcType.AutoSize = true;
            this.lblcType.Location = new System.Drawing.Point(90, 41);
            this.lblcType.Name = "lblcType";
            this.lblcType.Size = new System.Drawing.Size(34, 13);
            this.lblcType.TabIndex = 40;
            this.lblcType.Text = "Type:";
            // 
            // txt_type
            // 
            this.txt_type.Location = new System.Drawing.Point(93, 57);
            this.txt_type.Name = "txt_type";
            this.txt_type.Size = new System.Drawing.Size(68, 20);
            this.txt_type.TabIndex = 39;
            this.txt_type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_type_KeyPress);
            // 
            // lbl_idcap
            // 
            this.lbl_idcap.AutoSize = true;
            this.lbl_idcap.Location = new System.Drawing.Point(12, 41);
            this.lbl_idcap.Name = "lbl_idcap";
            this.lbl_idcap.Size = new System.Drawing.Size(21, 13);
            this.lbl_idcap.TabIndex = 38;
            this.lbl_idcap.Text = "ID:";
            // 
            // txt_id
            // 
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(9, 57);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(68, 20);
            this.txt_id.TabIndex = 37;
            // 
            // lblc_contactscript
            // 
            this.lblc_contactscript.AutoSize = true;
            this.lblc_contactscript.Location = new System.Drawing.Point(12, 137);
            this.lblc_contactscript.Name = "lblc_contactscript";
            this.lblc_contactscript.Size = new System.Drawing.Size(77, 13);
            this.lblc_contactscript.TabIndex = 52;
            this.lblc_contactscript.Text = "Contact Script:";
            // 
            // txt_contactscript
            // 
            this.txt_contactscript.Location = new System.Drawing.Point(9, 153);
            this.txt_contactscript.Name = "txt_contactscript";
            this.txt_contactscript.Size = new System.Drawing.Size(233, 20);
            this.txt_contactscript.TabIndex = 51;
            // 
            // txt_propID
            // 
            this.txt_propID.Location = new System.Drawing.Point(174, 57);
            this.txt_propID.Name = "txt_propID";
            this.txt_propID.Size = new System.Drawing.Size(68, 20);
            this.txt_propID.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "PropID (Int16):";
            // 
            // NPCSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 222);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_propID);
            this.Controls.Add(this.lblc_contactscript);
            this.Controls.Add(this.txt_contactscript);
            this.Controls.Add(this.lblc_initscript);
            this.Controls.Add(this.txt_initscript);
            this.Controls.Add(this.lblcType);
            this.Controls.Add(this.txt_type);
            this.Controls.Add(this.lbl_idcap);
            this.Controls.Add(this.txt_id);
            this.Controls.Add(this.lbl_yval);
            this.Controls.Add(this.lbl_xval);
            this.Controls.Add(this.lbl_y);
            this.Controls.Add(this.lbl_x);
            this.Controls.Add(this.brn_qpfsave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NPCSetter";
            this.Text = "Add a NPC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NPCSetter_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_yval;
        private System.Windows.Forms.Label lbl_xval;
        private System.Windows.Forms.Label lbl_y;
        private System.Windows.Forms.Label lbl_x;
        private System.Windows.Forms.Button brn_qpfsave;
        private System.Windows.Forms.Label lblc_initscript;
        private System.Windows.Forms.TextBox txt_initscript;
        private System.Windows.Forms.Label lblcType;
        private System.Windows.Forms.TextBox txt_type;
        private System.Windows.Forms.Label lbl_idcap;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label lblc_contactscript;
        private System.Windows.Forms.TextBox txt_contactscript;
        private System.Windows.Forms.TextBox txt_propID;
        private System.Windows.Forms.Label label1;
    }
}