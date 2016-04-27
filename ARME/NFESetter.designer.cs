namespace ARME
{
    partial class NFESetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NFESetter));
            this.lblcType = new System.Windows.Forms.Label();
            this.txt_type = new System.Windows.Forms.TextBox();
            this.lbl_idcap = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.coordlist = new System.Windows.Forms.ListBox();
            this.lblc_coords = new System.Windows.Forms.Label();
            this.brn_qpfsave = new System.Windows.Forms.Button();
            this.lbl_info = new System.Windows.Forms.Label();
            this.btn_editcoord = new System.Windows.Forms.Button();
            this.btn_delcoord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblcType
            // 
            this.lblcType.AutoSize = true;
            this.lblcType.Location = new System.Drawing.Point(130, 12);
            this.lblcType.Name = "lblcType";
            this.lblcType.Size = new System.Drawing.Size(34, 13);
            this.lblcType.TabIndex = 23;
            this.lblcType.Text = "Type:";
            // 
            // txt_type
            // 
            this.txt_type.Location = new System.Drawing.Point(133, 28);
            this.txt_type.Name = "txt_type";
            this.txt_type.Size = new System.Drawing.Size(112, 20);
            this.txt_type.TabIndex = 22;
            this.txt_type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_type_KeyPress);
            // 
            // lbl_idcap
            // 
            this.lbl_idcap.AutoSize = true;
            this.lbl_idcap.Location = new System.Drawing.Point(12, 12);
            this.lbl_idcap.Name = "lbl_idcap";
            this.lbl_idcap.Size = new System.Drawing.Size(21, 13);
            this.lbl_idcap.TabIndex = 21;
            this.lbl_idcap.Text = "ID:";
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(12, 28);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(115, 20);
            this.txt_id.TabIndex = 20;
            this.txt_id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_id_KeyPress);
            // 
            // coordlist
            // 
            this.coordlist.FormattingEnabled = true;
            this.coordlist.Location = new System.Drawing.Point(43, 70);
            this.coordlist.Name = "coordlist";
            this.coordlist.Size = new System.Drawing.Size(171, 121);
            this.coordlist.TabIndex = 24;
            this.coordlist.SelectedIndexChanged += new System.EventHandler(this.coordlist_SelectedIndexChanged);
            // 
            // lblc_coords
            // 
            this.lblc_coords.AutoSize = true;
            this.lblc_coords.Location = new System.Drawing.Point(12, 54);
            this.lblc_coords.Name = "lblc_coords";
            this.lblc_coords.Size = new System.Drawing.Size(43, 13);
            this.lblc_coords.TabIndex = 25;
            this.lblc_coords.Text = "Coords:";
            // 
            // brn_qpfsave
            // 
            this.brn_qpfsave.Enabled = false;
            this.brn_qpfsave.Location = new System.Drawing.Point(12, 225);
            this.brn_qpfsave.Name = "brn_qpfsave";
            this.brn_qpfsave.Size = new System.Drawing.Size(233, 25);
            this.brn_qpfsave.TabIndex = 31;
            this.brn_qpfsave.Text = "Save";
            this.brn_qpfsave.UseVisualStyleBackColor = true;
            this.brn_qpfsave.Click += new System.EventHandler(this.brn_qpfsave_Click);
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(9, 195);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(0, 13);
            this.lbl_info.TabIndex = 32;
            // 
            // btn_editcoord
            // 
            this.btn_editcoord.Enabled = false;
            this.btn_editcoord.Location = new System.Drawing.Point(220, 70);
            this.btn_editcoord.Name = "btn_editcoord";
            this.btn_editcoord.Size = new System.Drawing.Size(25, 121);
            this.btn_editcoord.TabIndex = 33;
            this.btn_editcoord.Text = "EDI T";
            this.btn_editcoord.UseVisualStyleBackColor = true;
            this.btn_editcoord.Click += new System.EventHandler(this.btn_editcoord_Click);
            // 
            // btn_delcoord
            // 
            this.btn_delcoord.Enabled = false;
            this.btn_delcoord.Location = new System.Drawing.Point(12, 70);
            this.btn_delcoord.Name = "btn_delcoord";
            this.btn_delcoord.Size = new System.Drawing.Size(25, 121);
            this.btn_delcoord.TabIndex = 34;
            this.btn_delcoord.Text = "DELETE";
            this.btn_delcoord.UseVisualStyleBackColor = true;
            this.btn_delcoord.Click += new System.EventHandler(this.btn_delcoord_Click);
            // 
            // NFESetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 262);
            this.Controls.Add(this.btn_delcoord);
            this.Controls.Add(this.btn_editcoord);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.brn_qpfsave);
            this.Controls.Add(this.lblc_coords);
            this.Controls.Add(this.coordlist);
            this.Controls.Add(this.lblcType);
            this.Controls.Add(this.txt_type);
            this.Controls.Add(this.lbl_idcap);
            this.Controls.Add(this.txt_id);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NFESetter";
            this.Text = "Add new Event Area";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NFESetter_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblcType;
        private System.Windows.Forms.TextBox txt_type;
        private System.Windows.Forms.Label lbl_idcap;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.ListBox coordlist;
        private System.Windows.Forms.Label lblc_coords;
        private System.Windows.Forms.Button brn_qpfsave;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Button btn_editcoord;
        private System.Windows.Forms.Button btn_delcoord;
    }
}