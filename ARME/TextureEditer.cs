using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ARME
{
    public partial class TextureEditer : Form
    {
        RappelzMapEditor main;
        public TextureEditer(RappelzMapEditor res)
        {
            this.main = res;

            InitializeComponent();
            this.chkValues();
            this.chk_t1.Checked = true;
            this.chk_t2.Checked = true;
            this.chk_t3.Checked = true;
            this.chk_V.Checked = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string segments = this.txt_segments.Text.Trim();
            string[] segs = segments.Split(',');
            int segcnt = segs.Length;
            List<int> terrainsegments = new List<int>();
            int r1 = 0;
            int r2 = 0;
            for (int i = 0; i < segs.Length; i++)
            {
                string[] seg = segs[i].Trim().Split('-');
                if (seg.Length > 1)
                {
                    try
                    {
                        r1 = Convert.ToInt32(seg[0]);
                        r2 = Convert.ToInt32(seg[1]);
                        for (int x = r1; x < r2 + 1; x++)
                        {
                            terrainsegments.Add(x);
                        }
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        terrainsegments.Add(Convert.ToInt32(seg[0]));
                    }
                    catch
                    {

                    }
                }
            }

            bool[] changeVals = { this.chk_V.Checked, this.chk_t1.Checked, this.chk_t2.Checked, this.chk_t3.Checked };
            for (int i = 0; i < terrainsegments.Count; i++)
            {
                main.editNFMTexture(terrainsegments[i],Convert.ToUInt32(this.txt_dwVersion.Text),
                    Convert.ToUInt16(this.txt_tile1.Text), Convert.ToUInt16(this.txt_tile2.Text),
                     Convert.ToUInt16(this.txt_tile3.Text),changeVals);                
            }
            this.main.releaseWorkblock();
            this.Close();
        }

        public void addSeg(int seg)
        {
            if (this.txt_segments.Text.Trim().Equals(""))
                this.txt_segments.Text = seg.ToString();
            else
                this.txt_segments.Text = this.txt_segments.Text.Trim() + "," + seg.ToString();
        }

        private void chkValues()
        {
            try
            {
                Convert.ToUInt32(this.txt_dwVersion.Text);
                Convert.ToUInt16(this.txt_tile1.Text); 
                Convert.ToUInt16(this.txt_tile2.Text);
                Convert.ToUInt16(this.txt_tile3.Text);
                this.btn_save.Enabled = true;
            }
            catch
            {
                this.btn_save.Enabled = false;
            }
        }

        private void chk_V_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_dwVersion.Enabled = this.chk_V.Checked;
        }

        private void chk_t1_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_tile1.Enabled = this.chk_t1.Checked;      
        }

        private void chk_t2_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_tile2.Enabled = this.chk_t2.Checked;
        }

        private void chk_t3_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_tile3.Enabled = this.chk_t3.Checked;
        }

        private void txt_dwVersion_TextChanged(object sender, EventArgs e)
        {
            this.chkValues();
        }

        private void txt_tile1_TextChanged(object sender, EventArgs e)
        {
            this.chkValues();
        }

        private void txt_tile2_TextChanged(object sender, EventArgs e)
        {
            this.chkValues();
        }

        private void txt_tile3_TextChanged(object sender, EventArgs e)
        {
            this.chkValues();
        }

        
    }
}
