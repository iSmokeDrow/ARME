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
    public partial class NPCSetter : Form
    {
        private RappelzMapEditor main = null;
        private int x;
        private int y;

        public NPCSetter(RappelzMapEditor res, int x, int y, int id)
        {
            InitializeComponent();
            this.main = res;
            this.x = x;
            this.y = y;
            this.lbl_xval.Text = x.ToString();
            this.lbl_yval.Text = y.ToString();
            this.txt_id.Text = id.ToString();
        }

        private void brn_qpfsave_Click(object sender, EventArgs e)
        {
            try
            {
                StructNFSNPC tmp = new StructNFSNPC();
                tmp.id = Convert.ToInt32(txt_id.Text);
                tmp.unknown1 = 0;
                tmp.unknown2 = 2;
                tmp.propID = Convert.ToInt16(txt_propID.Text);
                tmp.unknown4 = 0;
                tmp.x = this.x;
                tmp.y = this.y;
                tmp.type = Convert.ToInt32(txt_type.Text);
                tmp.cnt_contact = txt_contactscript.Text.Length;
                tmp.cnt_initscript = txt_initscript.Text.Length;
                tmp.initscript = txt_initscript.Text;
                tmp.contact = txt_contactscript.Text;
                tmp.name = "";
                main.updateNPC(tmp);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please check if all values are filled in in the correct format!");
            }
        }

        private void NPCSetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.releaseWorkblock();
        }

        private void txt_type_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }
    }
}
