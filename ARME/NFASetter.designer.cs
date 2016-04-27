namespace ARME
{
    partial class NFASetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NFASetter));
            this.lblc_id = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lbl_info = new System.Windows.Forms.Label();
            this.lblc_coords = new System.Windows.Forms.Label();
            this.coordlist = new System.Windows.Forms.ListBox();
            this.brn_qpfsave = new System.Windows.Forms.Button();
            this.btn_editcoord = new System.Windows.Forms.Button();
            this.btn_delcoord = new System.Windows.Forms.Button();
            this.txt_maninsertx = new System.Windows.Forms.TextBox();
            this.txt_maninserty = new System.Windows.Forms.TextBox();
            this.btn_maninsert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblc_id
            // 
            this.lblc_id.AutoSize = true;
            this.lblc_id.Location = new System.Drawing.Point(12, 8);
            this.lblc_id.Name = "lblc_id";
            this.lblc_id.Size = new System.Drawing.Size(21, 13);
            this.lblc_id.TabIndex = 48;
            this.lblc_id.Text = "ID:";
            // 
            // txt_id
            // 
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(12, 25);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(233, 20);
            this.txt_id.TabIndex = 47;
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(15, 189);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(0, 13);
            this.lbl_info.TabIndex = 46;
            // 
            // lblc_coords
            // 
            this.lblc_coords.AutoSize = true;
            this.lblc_coords.Location = new System.Drawing.Point(15, 49);
            this.lblc_coords.Name = "lblc_coords";
            this.lblc_coords.Size = new System.Drawing.Size(43, 13);
            this.lblc_coords.TabIndex = 45;
            this.lblc_coords.Text = "Coords:";
            // 
            // coordlist
            // 
            this.coordlist.FormattingEnabled = true;
            this.coordlist.Location = new System.Drawing.Point(43, 65);
            this.coordlist.Name = "coordlist";
            this.coordlist.Size = new System.Drawing.Size(171, 121);
            this.coordlist.TabIndex = 44;
            this.coordlist.SelectedIndexChanged += new System.EventHandler(this.coordlist_SelectedIndexChanged);
            // 
            // brn_qpfsave
            // 
            this.brn_qpfsave.Enabled = false;
            this.brn_qpfsave.Location = new System.Drawing.Point(12, 267);
            this.brn_qpfsave.Name = "brn_qpfsave";
            this.brn_qpfsave.Size = new System.Drawing.Size(233, 25);
            this.brn_qpfsave.TabIndex = 49;
            this.brn_qpfsave.Text = "Save";
            this.brn_qpfsave.UseVisualStyleBackColor = true;
            this.brn_qpfsave.Click += new System.EventHandler(this.brn_qpfsave_Click);
            // 
            // btn_editcoord
            // 
            this.btn_editcoord.Enabled = false;
            this.btn_editcoord.Location = new System.Drawing.Point(220, 65);
            this.btn_editcoord.Name = "btn_editcoord";
            this.btn_editcoord.Size = new System.Drawing.Size(25, 121);
            this.btn_editcoord.TabIndex = 50;
            this.btn_editcoord.Text = "EDI T";
            this.btn_editcoord.UseVisualStyleBackColor = true;
            this.btn_editcoord.Click += new System.EventHandler(this.btn_editcoord_Click);
            // 
            // btn_delcoord
            // 
            this.btn_delcoord.Enabled = false;
            this.btn_delcoord.Location = new System.Drawing.Point(12, 65);
            this.btn_delcoord.Name = "btn_delcoord";
            this.btn_delcoord.Size = new System.Drawing.Size(25, 121);
            this.btn_delcoord.TabIndex = 51;
            this.btn_delcoord.Text = "DELETE";
            this.btn_delcoord.UseVisualStyleBackColor = true;
            this.btn_delcoord.Click += new System.EventHandler(this.btn_delcoord_Click);
            // 
            // txt_maninsertx
            // 
            this.txt_maninsertx.Location = new System.Drawing.Point(12, 205);
            this.txt_maninsertx.Name = "txt_maninsertx";
            this.txt_maninsertx.Size = new System.Drawing.Size(116, 20);
            this.txt_maninsertx.TabIndex = 52;
            this.txt_maninsertx.TextChanged += new System.EventHandler(this.txt_maninsertx_TextChanged);
            // 
            // txt_maninserty
            // 
            this.txt_maninserty.Location = new System.Drawing.Point(134, 205);
            this.txt_maninserty.Name = "txt_maninserty";
            this.txt_maninserty.Size = new System.Drawing.Size(111, 20);
            this.txt_maninserty.TabIndex = 53;
            this.txt_maninserty.TextChanged += new System.EventHandler(this.txt_maninserty_TextChanged);
            // 
            // btn_maninsert
            // 
            this.btn_maninsert.Enabled = false;
            this.btn_maninsert.Location = new System.Drawing.Point(12, 231);
            this.btn_maninsert.Name = "btn_maninsert";
            this.btn_maninsert.Size = new System.Drawing.Size(233, 25);
            this.btn_maninsert.TabIndex = 54;
            this.btn_maninsert.Text = "Insert";
            this.btn_maninsert.UseVisualStyleBackColor = true;
            this.btn_maninsert.Click += new System.EventHandler(this.btn_maninsert_Click);
            // 
            // NFASetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 304);
            this.Controls.Add(this.btn_maninsert);
            this.Controls.Add(this.txt_maninserty);
            this.Controls.Add(this.txt_maninsertx);
            this.Controls.Add(this.btn_delcoord);
            this.Controls.Add(this.btn_editcoord);
            this.Controls.Add(this.brn_qpfsave);
            this.Controls.Add(this.lblc_id);
            this.Controls.Add(this.txt_id);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lblc_coords);
            this.Controls.Add(this.coordlist);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NFASetter";
            this.Text = "Add new Enterable Region";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NFASetter_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblc_id;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Label lblc_coords;
        private System.Windows.Forms.ListBox coordlist;
        private System.Windows.Forms.Button brn_qpfsave;
        private System.Windows.Forms.Button btn_editcoord;
        private System.Windows.Forms.Button btn_delcoord;
        private System.Windows.Forms.TextBox txt_maninsertx;
        private System.Windows.Forms.TextBox txt_maninserty;
        private System.Windows.Forms.Button btn_maninsert;
    }
}