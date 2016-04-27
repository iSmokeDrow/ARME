namespace ARME
{
    partial class TextureEditer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureEditer));
            this.txt_segments = new System.Windows.Forms.TextBox();
            this.txt_dwVersion = new System.Windows.Forms.TextBox();
            this.txt_tile1 = new System.Windows.Forms.TextBox();
            this.txt_tile3 = new System.Windows.Forms.TextBox();
            this.txt_tile2 = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.chk_V = new System.Windows.Forms.CheckBox();
            this.chk_t2 = new System.Windows.Forms.CheckBox();
            this.chk_t1 = new System.Windows.Forms.CheckBox();
            this.chk_t3 = new System.Windows.Forms.CheckBox();
            this.dwVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_segments
            // 
            this.txt_segments.Location = new System.Drawing.Point(8, 171);
            this.txt_segments.Name = "txt_segments";
            this.txt_segments.Size = new System.Drawing.Size(240, 20);
            this.txt_segments.TabIndex = 0;
            // 
            // txt_dwVersion
            // 
            this.txt_dwVersion.Location = new System.Drawing.Point(8, 25);
            this.txt_dwVersion.Name = "txt_dwVersion";
            this.txt_dwVersion.Size = new System.Drawing.Size(117, 20);
            this.txt_dwVersion.TabIndex = 1;
            this.txt_dwVersion.TextChanged += new System.EventHandler(this.txt_dwVersion_TextChanged);
            // 
            // txt_tile1
            // 
            this.txt_tile1.Location = new System.Drawing.Point(131, 25);
            this.txt_tile1.Name = "txt_tile1";
            this.txt_tile1.Size = new System.Drawing.Size(117, 20);
            this.txt_tile1.TabIndex = 5;
            this.txt_tile1.TextChanged += new System.EventHandler(this.txt_tile1_TextChanged);
            // 
            // txt_tile3
            // 
            this.txt_tile3.Location = new System.Drawing.Point(131, 67);
            this.txt_tile3.Name = "txt_tile3";
            this.txt_tile3.Size = new System.Drawing.Size(117, 20);
            this.txt_tile3.TabIndex = 7;
            this.txt_tile3.TextChanged += new System.EventHandler(this.txt_tile3_TextChanged);
            // 
            // txt_tile2
            // 
            this.txt_tile2.Location = new System.Drawing.Point(8, 67);
            this.txt_tile2.Name = "txt_tile2";
            this.txt_tile2.Size = new System.Drawing.Size(117, 20);
            this.txt_tile2.TabIndex = 6;
            this.txt_tile2.TextChanged += new System.EventHandler(this.txt_tile2_TextChanged);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(8, 197);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(240, 23);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // chk_V
            // 
            this.chk_V.AutoSize = true;
            this.chk_V.Location = new System.Drawing.Point(8, 114);
            this.chk_V.Name = "chk_V";
            this.chk_V.Size = new System.Drawing.Size(75, 17);
            this.chk_V.TabIndex = 9;
            this.chk_V.Text = "dwVersion";
            this.chk_V.UseVisualStyleBackColor = true;
            this.chk_V.CheckedChanged += new System.EventHandler(this.chk_V_CheckedChanged);
            // 
            // chk_t2
            // 
            this.chk_t2.AutoSize = true;
            this.chk_t2.Location = new System.Drawing.Point(8, 132);
            this.chk_t2.Name = "chk_t2";
            this.chk_t2.Size = new System.Drawing.Size(49, 17);
            this.chk_t2.TabIndex = 10;
            this.chk_t2.Text = "Tile2";
            this.chk_t2.UseVisualStyleBackColor = true;
            this.chk_t2.CheckedChanged += new System.EventHandler(this.chk_t2_CheckedChanged);
            // 
            // chk_t1
            // 
            this.chk_t1.AutoSize = true;
            this.chk_t1.Location = new System.Drawing.Point(131, 114);
            this.chk_t1.Name = "chk_t1";
            this.chk_t1.Size = new System.Drawing.Size(49, 17);
            this.chk_t1.TabIndex = 11;
            this.chk_t1.Text = "Tile1";
            this.chk_t1.UseVisualStyleBackColor = true;
            this.chk_t1.CheckedChanged += new System.EventHandler(this.chk_t1_CheckedChanged);
            // 
            // chk_t3
            // 
            this.chk_t3.AutoSize = true;
            this.chk_t3.Location = new System.Drawing.Point(131, 132);
            this.chk_t3.Name = "chk_t3";
            this.chk_t3.Size = new System.Drawing.Size(49, 17);
            this.chk_t3.TabIndex = 12;
            this.chk_t3.Text = "Tile3";
            this.chk_t3.UseVisualStyleBackColor = true;
            this.chk_t3.CheckedChanged += new System.EventHandler(this.chk_t3_CheckedChanged);
            // 
            // dwVersion
            // 
            this.dwVersion.AutoSize = true;
            this.dwVersion.Location = new System.Drawing.Point(5, 9);
            this.dwVersion.Name = "dwVersion";
            this.dwVersion.Size = new System.Drawing.Size(56, 13);
            this.dwVersion.TabIndex = 13;
            this.dwVersion.Text = "dwVersion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Tile1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Tile2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tile3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Values to be changed:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Segments (Example 1,2,3-10,11):";
            // 
            // TextureEditer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 223);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dwVersion);
            this.Controls.Add(this.chk_t3);
            this.Controls.Add(this.chk_t1);
            this.Controls.Add(this.chk_t2);
            this.Controls.Add(this.chk_V);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_tile3);
            this.Controls.Add(this.txt_tile2);
            this.Controls.Add(this.txt_tile1);
            this.Controls.Add(this.txt_dwVersion);
            this.Controls.Add(this.txt_segments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextureEditer";
            this.Text = "TextureEditer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_segments;
        private System.Windows.Forms.TextBox txt_dwVersion;
        private System.Windows.Forms.TextBox txt_tile1;
        private System.Windows.Forms.TextBox txt_tile3;
        private System.Windows.Forms.TextBox txt_tile2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.CheckBox chk_V;
        private System.Windows.Forms.CheckBox chk_t2;
        private System.Windows.Forms.CheckBox chk_t1;
        private System.Windows.Forms.CheckBox chk_t3;
        private System.Windows.Forms.Label dwVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}