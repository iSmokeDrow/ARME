namespace ARME
{
    partial class NFCSetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NFCSetter));
            this.lbl_info = new System.Windows.Forms.Label();
            this.brn_qpfsave = new System.Windows.Forms.Button();
            this.lblc_coords = new System.Windows.Forms.Label();
            this.coordlist = new System.Windows.Forms.ListBox();
            this.lblcType = new System.Windows.Forms.Label();
            this.txt_type = new System.Windows.Forms.TextBox();
            this.lbl_idcap = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.txt_script = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lblc_script = new System.Windows.Forms.Label();
            this.lblc_name = new System.Windows.Forms.Label();
            this.cb_coordset = new System.Windows.Forms.ComboBox();
            this.btn_addcoordset = new System.Windows.Forms.Button();
            this.btn_editcoord = new System.Windows.Forms.Button();
            this.btn_delcoord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(9, 194);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(0, 13);
            this.lbl_info.TabIndex = 40;
            // 
            // brn_qpfsave
            // 
            this.brn_qpfsave.Enabled = false;
            this.brn_qpfsave.Location = new System.Drawing.Point(132, 335);
            this.brn_qpfsave.Name = "brn_qpfsave";
            this.brn_qpfsave.Size = new System.Drawing.Size(113, 25);
            this.brn_qpfsave.TabIndex = 39;
            this.brn_qpfsave.Text = "Save";
            this.brn_qpfsave.UseVisualStyleBackColor = true;
            this.brn_qpfsave.Click += new System.EventHandler(this.brn_qpfsave_Click);
            // 
            // lblc_coords
            // 
            this.lblc_coords.AutoSize = true;
            this.lblc_coords.Location = new System.Drawing.Point(12, 53);
            this.lblc_coords.Name = "lblc_coords";
            this.lblc_coords.Size = new System.Drawing.Size(43, 13);
            this.lblc_coords.TabIndex = 38;
            this.lblc_coords.Text = "Coords:";
            // 
            // coordlist
            // 
            this.coordlist.FormattingEnabled = true;
            this.coordlist.Location = new System.Drawing.Point(43, 69);
            this.coordlist.Name = "coordlist";
            this.coordlist.Size = new System.Drawing.Size(171, 121);
            this.coordlist.TabIndex = 37;
            this.coordlist.SelectedIndexChanged += new System.EventHandler(this.coordlist_SelectedIndexChanged);
            // 
            // lblcType
            // 
            this.lblcType.AutoSize = true;
            this.lblcType.Location = new System.Drawing.Point(130, 11);
            this.lblcType.Name = "lblcType";
            this.lblcType.Size = new System.Drawing.Size(34, 13);
            this.lblcType.TabIndex = 36;
            this.lblcType.Text = "Type:";
            // 
            // txt_type
            // 
            this.txt_type.Location = new System.Drawing.Point(133, 27);
            this.txt_type.Name = "txt_type";
            this.txt_type.Size = new System.Drawing.Size(112, 20);
            this.txt_type.TabIndex = 35;
            this.txt_type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_type_KeyPress);
            // 
            // lbl_idcap
            // 
            this.lbl_idcap.AutoSize = true;
            this.lbl_idcap.Location = new System.Drawing.Point(12, 11);
            this.lbl_idcap.Name = "lbl_idcap";
            this.lbl_idcap.Size = new System.Drawing.Size(21, 13);
            this.lbl_idcap.TabIndex = 34;
            this.lbl_idcap.Text = "ID:";
            // 
            // txt_id
            // 
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(12, 27);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(115, 20);
            this.txt_id.TabIndex = 33;
            // 
            // txt_script
            // 
            this.txt_script.Location = new System.Drawing.Point(12, 238);
            this.txt_script.Name = "txt_script";
            this.txt_script.Size = new System.Drawing.Size(233, 20);
            this.txt_script.TabIndex = 41;
            this.txt_script.Validating += new System.ComponentModel.CancelEventHandler(this.txt_script_Validating);
            // 
            // txt_name
            // 
            this.txt_name.Enabled = false;
            this.txt_name.Location = new System.Drawing.Point(12, 277);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(233, 20);
            this.txt_name.TabIndex = 42;
            // 
            // lblc_script
            // 
            this.lblc_script.AutoSize = true;
            this.lblc_script.Location = new System.Drawing.Point(12, 222);
            this.lblc_script.Name = "lblc_script";
            this.lblc_script.Size = new System.Drawing.Size(37, 13);
            this.lblc_script.TabIndex = 43;
            this.lblc_script.Text = "Script:";
            // 
            // lblc_name
            // 
            this.lblc_name.AutoSize = true;
            this.lblc_name.Location = new System.Drawing.Point(12, 261);
            this.lblc_name.Name = "lblc_name";
            this.lblc_name.Size = new System.Drawing.Size(38, 13);
            this.lblc_name.TabIndex = 44;
            this.lblc_name.Text = "Name:";
            // 
            // cb_coordset
            // 
            this.cb_coordset.FormattingEnabled = true;
            this.cb_coordset.Location = new System.Drawing.Point(12, 308);
            this.cb_coordset.Name = "cb_coordset";
            this.cb_coordset.Size = new System.Drawing.Size(234, 21);
            this.cb_coordset.TabIndex = 45;
            this.cb_coordset.SelectedIndexChanged += new System.EventHandler(this.cb_coordset_SelectedIndexChanged);
            // 
            // btn_addcoordset
            // 
            this.btn_addcoordset.Enabled = false;
            this.btn_addcoordset.Location = new System.Drawing.Point(12, 335);
            this.btn_addcoordset.Name = "btn_addcoordset";
            this.btn_addcoordset.Size = new System.Drawing.Size(113, 25);
            this.btn_addcoordset.TabIndex = 46;
            this.btn_addcoordset.Text = "Add Coordset";
            this.btn_addcoordset.UseVisualStyleBackColor = true;
            this.btn_addcoordset.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_editcoord
            // 
            this.btn_editcoord.Enabled = false;
            this.btn_editcoord.Location = new System.Drawing.Point(220, 69);
            this.btn_editcoord.Name = "btn_editcoord";
            this.btn_editcoord.Size = new System.Drawing.Size(25, 121);
            this.btn_editcoord.TabIndex = 47;
            this.btn_editcoord.Text = "EDI T";
            this.btn_editcoord.UseVisualStyleBackColor = true;
            this.btn_editcoord.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_delcoord
            // 
            this.btn_delcoord.Enabled = false;
            this.btn_delcoord.Location = new System.Drawing.Point(12, 69);
            this.btn_delcoord.Name = "btn_delcoord";
            this.btn_delcoord.Size = new System.Drawing.Size(25, 121);
            this.btn_delcoord.TabIndex = 48;
            this.btn_delcoord.Text = "DELETE";
            this.btn_delcoord.UseVisualStyleBackColor = true;
            this.btn_delcoord.Click += new System.EventHandler(this.btn_delcoord_Click);
            // 
            // NFCSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 372);
            this.Controls.Add(this.btn_delcoord);
            this.Controls.Add(this.btn_editcoord);
            this.Controls.Add(this.btn_addcoordset);
            this.Controls.Add(this.cb_coordset);
            this.Controls.Add(this.lblc_name);
            this.Controls.Add(this.lblc_script);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_script);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.brn_qpfsave);
            this.Controls.Add(this.lblc_coords);
            this.Controls.Add(this.coordlist);
            this.Controls.Add(this.lblcType);
            this.Controls.Add(this.txt_type);
            this.Controls.Add(this.lbl_idcap);
            this.Controls.Add(this.txt_id);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NFCSetter";
            this.Text = "Add new Worldlocation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NFCSetter_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Button brn_qpfsave;
        private System.Windows.Forms.Label lblc_coords;
        private System.Windows.Forms.ListBox coordlist;
        private System.Windows.Forms.Label lblcType;
        private System.Windows.Forms.TextBox txt_type;
        private System.Windows.Forms.Label lbl_idcap;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.TextBox txt_script;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lblc_script;
        private System.Windows.Forms.Label lblc_name;
        private System.Windows.Forms.ComboBox cb_coordset;
        private System.Windows.Forms.Button btn_addcoordset;
        private System.Windows.Forms.Button btn_editcoord;
        private System.Windows.Forms.Button btn_delcoord;
    }
}