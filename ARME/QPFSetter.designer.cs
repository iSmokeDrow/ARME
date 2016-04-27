namespace ARME
{
    partial class QPFSetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QPFSetter));
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lbl_idcap = new System.Windows.Forms.Label();
            this.textOffsetZ = new System.Windows.Forms.TextBox();
            this.textP4 = new System.Windows.Forms.TextBox();
            this.textP6 = new System.Windows.Forms.TextBox();
            this.textP5 = new System.Windows.Forms.TextBox();
            this.brn_qpfsave = new System.Windows.Forms.Button();
            this.lbl_y = new System.Windows.Forms.Label();
            this.lbl_x = new System.Windows.Forms.Label();
            this.lbl_xval = new System.Windows.Forms.Label();
            this.lbl_yval = new System.Windows.Forms.Label();
            this.lblcScaleZ = new System.Windows.Forms.Label();
            this.lblcScaleY = new System.Windows.Forms.Label();
            this.lblcScaleX = new System.Windows.Forms.Label();
            this.lblcOffsetZ = new System.Windows.Forms.Label();
            this.txt_qpfid2 = new System.Windows.Forms.TextBox();
            this.lbl_qpfid2 = new System.Windows.Forms.Label();
            this.lbl_qpfhint = new System.Windows.Forms.Label();
            this.lbl_Offsetz = new System.Windows.Forms.Label();
            this.Offset_z = new KnobControl.KnobControl();
            this.SuspendLayout();
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(12, 55);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(115, 20);
            this.txt_id.TabIndex = 0;
            this.txt_id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_id_KeyPress);
            // 
            // lbl_idcap
            // 
            this.lbl_idcap.AutoSize = true;
            this.lbl_idcap.Location = new System.Drawing.Point(12, 39);
            this.lbl_idcap.Name = "lbl_idcap";
            this.lbl_idcap.Size = new System.Drawing.Size(21, 13);
            this.lbl_idcap.TabIndex = 2;
            this.lbl_idcap.Text = "ID:";
            // 
            // textOffsetZ
            // 
            this.textOffsetZ.Location = new System.Drawing.Point(12, 97);
            this.textOffsetZ.Name = "textOffsetZ";
            this.textOffsetZ.Size = new System.Drawing.Size(115, 20);
            this.textOffsetZ.TabIndex = 5;
            this.textOffsetZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textP2_KeyPress);
            // 
            // textP4
            // 
            this.textP4.Location = new System.Drawing.Point(130, 97);
            this.textP4.Name = "textP4";
            this.textP4.Size = new System.Drawing.Size(115, 20);
            this.textP4.TabIndex = 7;
            this.textP4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textP4_KeyPress);
            // 
            // textP6
            // 
            this.textP6.Location = new System.Drawing.Point(130, 145);
            this.textP6.Name = "textP6";
            this.textP6.Size = new System.Drawing.Size(115, 20);
            this.textP6.TabIndex = 9;
            this.textP6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textP6_KeyPress);
            // 
            // textP5
            // 
            this.textP5.Location = new System.Drawing.Point(12, 145);
            this.textP5.Name = "textP5";
            this.textP5.Size = new System.Drawing.Size(115, 20);
            this.textP5.TabIndex = 8;
            this.textP5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textP5_KeyPress);
            // 
            // brn_qpfsave
            // 
            this.brn_qpfsave.Location = new System.Drawing.Point(12, 240);
            this.brn_qpfsave.Name = "brn_qpfsave";
            this.brn_qpfsave.Size = new System.Drawing.Size(344, 25);
            this.brn_qpfsave.TabIndex = 11;
            this.brn_qpfsave.Text = "Save";
            this.brn_qpfsave.UseVisualStyleBackColor = true;
            this.brn_qpfsave.Click += new System.EventHandler(this.brn_qpfsave_Click);
            // 
            // lbl_y
            // 
            this.lbl_y.AutoSize = true;
            this.lbl_y.Location = new System.Drawing.Point(127, 9);
            this.lbl_y.Name = "lbl_y";
            this.lbl_y.Size = new System.Drawing.Size(17, 13);
            this.lbl_y.TabIndex = 13;
            this.lbl_y.Text = "Y:";
            // 
            // lbl_x
            // 
            this.lbl_x.AutoSize = true;
            this.lbl_x.Location = new System.Drawing.Point(12, 9);
            this.lbl_x.Name = "lbl_x";
            this.lbl_x.Size = new System.Drawing.Size(17, 13);
            this.lbl_x.TabIndex = 12;
            this.lbl_x.Text = "X:";
            // 
            // lbl_xval
            // 
            this.lbl_xval.AutoSize = true;
            this.lbl_xval.Location = new System.Drawing.Point(35, 9);
            this.lbl_xval.Name = "lbl_xval";
            this.lbl_xval.Size = new System.Drawing.Size(0, 13);
            this.lbl_xval.TabIndex = 14;
            // 
            // lbl_yval
            // 
            this.lbl_yval.AutoSize = true;
            this.lbl_yval.Location = new System.Drawing.Point(150, 9);
            this.lbl_yval.Name = "lbl_yval";
            this.lbl_yval.Size = new System.Drawing.Size(0, 13);
            this.lbl_yval.TabIndex = 15;
            // 
            // lblcScaleZ
            // 
            this.lblcScaleZ.AutoSize = true;
            this.lblcScaleZ.Location = new System.Drawing.Point(124, 129);
            this.lblcScaleZ.Name = "lblcScaleZ";
            this.lblcScaleZ.Size = new System.Drawing.Size(44, 13);
            this.lblcScaleZ.TabIndex = 16;
            this.lblcScaleZ.Text = "ScaleZ:";
            // 
            // lblcScaleY
            // 
            this.lblcScaleY.AutoSize = true;
            this.lblcScaleY.Location = new System.Drawing.Point(12, 129);
            this.lblcScaleY.Name = "lblcScaleY";
            this.lblcScaleY.Size = new System.Drawing.Size(44, 13);
            this.lblcScaleY.TabIndex = 17;
            this.lblcScaleY.Text = "ScaleY:";
            // 
            // lblcScaleX
            // 
            this.lblcScaleX.AutoSize = true;
            this.lblcScaleX.Location = new System.Drawing.Point(130, 81);
            this.lblcScaleX.Name = "lblcScaleX";
            this.lblcScaleX.Size = new System.Drawing.Size(44, 13);
            this.lblcScaleX.TabIndex = 19;
            this.lblcScaleX.Text = "ScaleX:";
            // 
            // lblcOffsetZ
            // 
            this.lblcOffsetZ.AutoSize = true;
            this.lblcOffsetZ.Location = new System.Drawing.Point(9, 81);
            this.lblcOffsetZ.Name = "lblcOffsetZ";
            this.lblcOffsetZ.Size = new System.Drawing.Size(45, 13);
            this.lblcOffsetZ.TabIndex = 20;
            this.lblcOffsetZ.Text = "OffsetZ:";
            // 
            // txt_qpfid2
            // 
            this.txt_qpfid2.Location = new System.Drawing.Point(130, 55);
            this.txt_qpfid2.Name = "txt_qpfid2";
            this.txt_qpfid2.Size = new System.Drawing.Size(115, 20);
            this.txt_qpfid2.TabIndex = 21;
            // 
            // lbl_qpfid2
            // 
            this.lbl_qpfid2.AutoSize = true;
            this.lbl_qpfid2.Location = new System.Drawing.Point(130, 39);
            this.lbl_qpfid2.Name = "lbl_qpfid2";
            this.lbl_qpfid2.Size = new System.Drawing.Size(30, 13);
            this.lbl_qpfid2.TabIndex = 22;
            this.lbl_qpfid2.Text = "ID 2:";
            // 
            // lbl_qpfhint
            // 
            this.lbl_qpfhint.AutoSize = true;
            this.lbl_qpfhint.Location = new System.Drawing.Point(12, 178);
            this.lbl_qpfhint.MaximumSize = new System.Drawing.Size(365, 0);
            this.lbl_qpfhint.Name = "lbl_qpfhint";
            this.lbl_qpfhint.Size = new System.Drawing.Size(0, 13);
            this.lbl_qpfhint.TabIndex = 23;
            // 
            // lbl_Offsetz
            // 
            this.lbl_Offsetz.AutoSize = true;
            this.lbl_Offsetz.Location = new System.Drawing.Point(277, 55);
            this.lbl_Offsetz.Name = "lbl_Offsetz";
            this.lbl_Offsetz.Size = new System.Drawing.Size(61, 13);
            this.lbl_Offsetz.TabIndex = 25;
            this.lbl_Offsetz.Text = "Orientation:";
            // 
            // Offset_z
            // 
            this.Offset_z.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Offset_z.LargeChange = 20;
            this.Offset_z.Location = new System.Drawing.Point(280, 81);
            this.Offset_z.Maximum = 620;
            this.Offset_z.Minimum = 0;
            this.Offset_z.Name = "Offset_z";
            this.Offset_z.ShowLargeScale = true;
            this.Offset_z.ShowSmallScale = false;
            this.Offset_z.Size = new System.Drawing.Size(76, 84);
            this.Offset_z.SmallChange = 5;
            this.Offset_z.TabIndex = 24;
            this.Offset_z.Value = 0;
            this.Offset_z.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Offset_z_MouseClick);
            this.Offset_z.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Offset_z_MouseDown);
            // 
            // QPFSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 277);
            this.Controls.Add(this.lbl_Offsetz);
            this.Controls.Add(this.Offset_z);
            this.Controls.Add(this.lbl_qpfhint);
            this.Controls.Add(this.lbl_qpfid2);
            this.Controls.Add(this.txt_qpfid2);
            this.Controls.Add(this.lblcOffsetZ);
            this.Controls.Add(this.lblcScaleX);
            this.Controls.Add(this.lblcScaleY);
            this.Controls.Add(this.lblcScaleZ);
            this.Controls.Add(this.lbl_yval);
            this.Controls.Add(this.lbl_xval);
            this.Controls.Add(this.lbl_y);
            this.Controls.Add(this.lbl_x);
            this.Controls.Add(this.brn_qpfsave);
            this.Controls.Add(this.textP6);
            this.Controls.Add(this.textP5);
            this.Controls.Add(this.textP4);
            this.Controls.Add(this.textOffsetZ);
            this.Controls.Add(this.lbl_idcap);
            this.Controls.Add(this.txt_id);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QPFSetter";
            this.Text = "Add new Fieldprop";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QPFSetter_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label lbl_idcap;
        private System.Windows.Forms.TextBox textOffsetZ;
        private System.Windows.Forms.TextBox textP4;
        private System.Windows.Forms.TextBox textP6;
        private System.Windows.Forms.TextBox textP5;
        private System.Windows.Forms.Button brn_qpfsave;
        private System.Windows.Forms.Label lbl_y;
        private System.Windows.Forms.Label lbl_x;
        private System.Windows.Forms.Label lbl_xval;
        private System.Windows.Forms.Label lbl_yval;
        private System.Windows.Forms.Label lblcScaleZ;
        private System.Windows.Forms.Label lblcScaleY;
        private System.Windows.Forms.Label lblcScaleX;
        private System.Windows.Forms.Label lblcOffsetZ;
        private System.Windows.Forms.TextBox txt_qpfid2;
        private System.Windows.Forms.Label lbl_qpfid2;
        private System.Windows.Forms.Label lbl_qpfhint;
        private KnobControl.KnobControl Offset_z;
        private System.Windows.Forms.Label lbl_Offsetz;
    }
}