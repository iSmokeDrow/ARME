namespace ARME
{
    partial class CoordGetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoordGetter));
            this.brn_qpfsave = new System.Windows.Forms.Button();
            this.lbl_info = new System.Windows.Forms.Label();
            this.lblc_coords = new System.Windows.Forms.Label();
            this.coordlist = new System.Windows.Forms.ListBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_coordedit = new System.Windows.Forms.Button();
            this.btn_addcoords = new System.Windows.Forms.Button();
            this.cb_coordset = new System.Windows.Forms.ComboBox();
            this.btn_coorddelete = new System.Windows.Forms.Button();
            this.btn_delcoordset = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_maninsert = new System.Windows.Forms.Button();
            this.txt_maninserty = new System.Windows.Forms.TextBox();
            this.txt_maninsertx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // brn_qpfsave
            // 
            this.brn_qpfsave.Location = new System.Drawing.Point(132, 259);
            this.brn_qpfsave.Name = "brn_qpfsave";
            this.brn_qpfsave.Size = new System.Drawing.Size(113, 25);
            this.brn_qpfsave.TabIndex = 53;
            this.brn_qpfsave.Text = "Save";
            this.brn_qpfsave.UseVisualStyleBackColor = true;
            this.brn_qpfsave.Click += new System.EventHandler(this.brn_qpfsave_Click);
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(15, 152);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(0, 13);
            this.lbl_info.TabIndex = 52;
            // 
            // lblc_coords
            // 
            this.lblc_coords.AutoSize = true;
            this.lblc_coords.Location = new System.Drawing.Point(15, 12);
            this.lblc_coords.Name = "lblc_coords";
            this.lblc_coords.Size = new System.Drawing.Size(43, 13);
            this.lblc_coords.TabIndex = 51;
            this.lblc_coords.Text = "Coords:";
            // 
            // coordlist
            // 
            this.coordlist.FormattingEnabled = true;
            this.coordlist.Location = new System.Drawing.Point(12, 28);
            this.coordlist.Name = "coordlist";
            this.coordlist.Size = new System.Drawing.Size(233, 121);
            this.coordlist.TabIndex = 50;
            this.coordlist.SelectedIndexChanged += new System.EventHandler(this.coordlist_SelectedIndexChanged);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(12, 259);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(113, 25);
            this.btn_clear.TabIndex = 54;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_coordedit
            // 
            this.btn_coordedit.Location = new System.Drawing.Point(12, 228);
            this.btn_coordedit.Name = "btn_coordedit";
            this.btn_coordedit.Size = new System.Drawing.Size(73, 25);
            this.btn_coordedit.TabIndex = 55;
            this.btn_coordedit.Text = "Edit";
            this.btn_coordedit.UseVisualStyleBackColor = true;
            this.btn_coordedit.Click += new System.EventHandler(this.btn_coordedit_Click);
            // 
            // btn_addcoords
            // 
            this.btn_addcoords.Location = new System.Drawing.Point(93, 228);
            this.btn_addcoords.Name = "btn_addcoords";
            this.btn_addcoords.Size = new System.Drawing.Size(73, 25);
            this.btn_addcoords.TabIndex = 56;
            this.btn_addcoords.Text = "Add";
            this.btn_addcoords.UseVisualStyleBackColor = true;
            this.btn_addcoords.Click += new System.EventHandler(this.btn_addcoords_Click);
            // 
            // cb_coordset
            // 
            this.cb_coordset.FormattingEnabled = true;
            this.cb_coordset.Location = new System.Drawing.Point(39, 201);
            this.cb_coordset.Name = "cb_coordset";
            this.cb_coordset.Size = new System.Drawing.Size(179, 21);
            this.cb_coordset.TabIndex = 57;
            this.cb_coordset.SelectedIndexChanged += new System.EventHandler(this.cb_coordset_SelectedIndexChanged);
            // 
            // btn_coorddelete
            // 
            this.btn_coorddelete.Location = new System.Drawing.Point(172, 228);
            this.btn_coorddelete.Name = "btn_coorddelete";
            this.btn_coorddelete.Size = new System.Drawing.Size(73, 25);
            this.btn_coorddelete.TabIndex = 58;
            this.btn_coorddelete.Text = "Delete";
            this.btn_coorddelete.UseVisualStyleBackColor = true;
            this.btn_coorddelete.Click += new System.EventHandler(this.btn_coorddelete_Click);
            // 
            // btn_delcoordset
            // 
            this.btn_delcoordset.Location = new System.Drawing.Point(224, 201);
            this.btn_delcoordset.Name = "btn_delcoordset";
            this.btn_delcoordset.Size = new System.Drawing.Size(21, 21);
            this.btn_delcoordset.TabIndex = 59;
            this.btn_delcoordset.Text = "X";
            this.btn_delcoordset.UseVisualStyleBackColor = true;
            this.btn_delcoordset.Click += new System.EventHandler(this.btn_delcoordset_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 21);
            this.button1.TabIndex = 60;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_maninsert
            // 
            this.btn_maninsert.Enabled = false;
            this.btn_maninsert.Location = new System.Drawing.Point(12, 316);
            this.btn_maninsert.Name = "btn_maninsert";
            this.btn_maninsert.Size = new System.Drawing.Size(233, 25);
            this.btn_maninsert.TabIndex = 63;
            this.btn_maninsert.Text = "Insert";
            this.btn_maninsert.UseVisualStyleBackColor = true;
            this.btn_maninsert.Click += new System.EventHandler(this.btn_maninsert_Click);
            // 
            // txt_maninserty
            // 
            this.txt_maninserty.Location = new System.Drawing.Point(134, 290);
            this.txt_maninserty.Name = "txt_maninserty";
            this.txt_maninserty.Size = new System.Drawing.Size(111, 20);
            this.txt_maninserty.TabIndex = 62;
            this.txt_maninserty.TextChanged += new System.EventHandler(this.txt_maninserty_TextChanged);
            // 
            // txt_maninsertx
            // 
            this.txt_maninsertx.Location = new System.Drawing.Point(12, 290);
            this.txt_maninsertx.Name = "txt_maninsertx";
            this.txt_maninsertx.Size = new System.Drawing.Size(116, 20);
            this.txt_maninsertx.TabIndex = 61;
            this.txt_maninsertx.TextChanged += new System.EventHandler(this.txt_maninsertx_TextChanged);
            // 
            // CoordGetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 352);
            this.Controls.Add(this.btn_maninsert);
            this.Controls.Add(this.txt_maninserty);
            this.Controls.Add(this.txt_maninsertx);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_delcoordset);
            this.Controls.Add(this.btn_coorddelete);
            this.Controls.Add(this.cb_coordset);
            this.Controls.Add(this.btn_addcoords);
            this.Controls.Add(this.btn_coordedit);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.brn_qpfsave);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lblc_coords);
            this.Controls.Add(this.coordlist);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CoordGetter";
            this.Text = "Modify Coordinates";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CoordGetter_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button brn_qpfsave;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Label lblc_coords;
        private System.Windows.Forms.ListBox coordlist;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_coordedit;
        private System.Windows.Forms.Button btn_addcoords;
        private System.Windows.Forms.ComboBox cb_coordset;
        private System.Windows.Forms.Button btn_coorddelete;
        private System.Windows.Forms.Button btn_delcoordset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_maninsert;
        private System.Windows.Forms.TextBox txt_maninserty;
        private System.Windows.Forms.TextBox txt_maninsertx;
    }
}