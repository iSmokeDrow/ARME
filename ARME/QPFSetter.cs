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
    public partial class QPFSetter : Form
    {
        private RappelzMapEditor main = null;
        private int x;
        private int y;
        public QPFSetter(RappelzMapEditor res, int x, int y)
        {
            InitializeComponent();
            this.lbl_qpfhint.Text = @"If your Fieldprop won't show up, make sure that there are no identical ID2s in the whole qpf file! ID and ID2 can be different! Use the round button to setup the orientation of the Fieldprop. Directions on the button and on the map are the same.";
            this.main = res;
            this.x = x;
            this.y = y;
            this.lbl_xval.Text = x.ToString();
            this.lbl_yval.Text = y.ToString();
        }


        private void brn_qpfsave_Click(object sender, EventArgs e)
        {
            try
            {
                StructQPF tmp = new StructQPF();
                tmp.id = Convert.ToInt32(this.txt_id.Text);
                tmp.rotation_z = Convert.ToSingle(620-this.Offset_z.Value) / (float)100;
                tmp.x = x;
                tmp.y = y;
                tmp.offset_z = Convert.ToSingle(this.textOffsetZ.Text);
                tmp.rotation_x = 0;
                tmp.rotation_y = 0;
                tmp.scale_x = Convert.ToSingle(this.textP4.Text);
                tmp.scale_y = Convert.ToSingle(this.textP5.Text);
                tmp.scale_z = Convert.ToSingle(this.textP6.Text);
                tmp.id2 = Convert.ToInt32(this.txt_qpfid2.Text);
                tmp.name = "";
                this.main.updateQPF(tmp);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please check your values. Only decimals are allowed!");
                //textP1.Text = "";
                textOffsetZ.Text = "";
                textP4.Text = "";
                textP5.Text = "";
                textP6.Text = "";
   

            }
        }

        private void QPFSetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.releaseWorkblock();
        }

        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void textP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }

        private void textP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }

        private void textP3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }

        private void txt_orientation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }

        private void textP4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }

        private void textP5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }

        private void textP6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)&&!char.IsPunctuation(e.KeyChar))
                e.Handled = true; 
        }

       

        

        private void Offset_z_MouseMove(object sender, MouseEventArgs e)
        {
            this.lbl_Offsetz.Text = "Orientation: " + ((float)(620-this.Offset_z.Value) / (float)100).ToString();
        }

        private void Offset_z_MouseDown(object sender, MouseEventArgs e)
        {
            this.lbl_Offsetz.Text = "Orientation: " + ((float)(620 - this.Offset_z.Value) / (float)100).ToString();
        }

        private void Offset_z_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbl_Offsetz.Text = "Orientation: " + ((float)(620 - this.Offset_z.Value) / (float)100).ToString();
        }

     
 



  


      
    }
}
