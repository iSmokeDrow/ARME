using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ARME.MapFileRes;

namespace ARME
{
    public partial class NFMPropSetter : Form
    {
        private RappelzMapEditor res;
        private int mapx;
        private int mapy;

        public NFMPropSetter(RappelzMapEditor res, int mapx, int mapy)
        {
            this.res = res;
            this.mapx = mapx;
            this.mapy = mapy;
            InitializeComponent();
        }

        private void chk_Heighlocked_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_Heighlocked.Checked)
                this.txt_lckheight.Enabled = true;
            else
            {
                this.txt_lckheight.Enabled = false;
                this.txt_lckheight.Text = "0";
            }
        }

        public void addcoord(int x, int y)
        {
            this.txt_x.Text = x.ToString();
            this.txt_y.Text = y.ToString();

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                int x = (int)((Convert.ToInt32(this.txt_x.Text) - (mapx * 16128)) / (float)5.25);
                int y = 3072 - ((int)((Convert.ToInt32(this.txt_y.Text) - (mapy * 16128)) / (float)5.25));
                
                int[] segdat = res.getSegData(x, y);
                PROPS_TABLE_STRUCTURE tmp = new PROPS_TABLE_STRUCTURE(
                    segdat[0], 0, segdat[1], segdat[2], Convert.ToSingle(this.txt_z.Text),
                    Convert.ToSingle(this.txt_rotatex.Text), Convert.ToSingle(this.txt_rotatey.Text), Convert.ToSingle(this.txt_rotatez.Text),
                    Convert.ToSingle(this.txt_scalex.Text), Convert.ToSingle(this.txt_scaley.Text), Convert.ToSingle(this.txt_scalez.Text),
                    Convert.ToInt16(this.txt_propnum.Text),this.chk_Heighlocked.Checked,Convert.ToSingle(this.txt_lckheight.Text),Convert.ToInt16(this.txt_textgrpindex.Text));
                res.addNFMProp(tmp);                
                res.releaseWorkblock();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
