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
    public partial class TerrainEditer : Form
    {
        private RappelzMapEditor res;
        public TerrainEditer(RappelzMapEditor res)
        {
            this.res = res;
            InitializeComponent();
            this.checkBox3.Checked = true;
            this.checkBox4.Checked = true;
            this.chk_Color.Checked = true;
            this.chk_Height.Checked = true;
            this.checkValues();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.chk_t2.Checked = this.chk_t2.Checked?false:true;
            this.chk_t8.Checked = this.chk_t8.Checked?false:true;
            this.chk_t14.Checked = this.chk_t14.Checked?false:true;
            this.chk_t20.Checked = this.chk_t20.Checked?false:true;
            this.chk_t26.Checked = this.chk_t26.Checked?false:true;
            this.chk_t32.Checked = this.chk_t32.Checked ? false : true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.chk_t3.Checked= this.chk_t3.Checked?false:true;
            this.chk_t9.Checked = this.chk_t9.Checked?false:true;
            this.chk_t15.Checked= this.chk_t15.Checked?false:true;
            this.chk_t21.Checked= this.chk_t21.Checked?false:true;
            this.chk_t27.Checked= this.chk_t27.Checked?false:true;
            this.chk_t33.Checked = this.chk_t33.Checked ? false : true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.chk_t4.Checked= this.chk_t4.Checked?false:true;
            this.chk_t10.Checked= this.chk_t10.Checked?false:true;
            this.chk_t16.Checked= this.chk_t16.Checked?false:true;
            this.chk_t22.Checked= this.chk_t22.Checked?false:true;
            this.chk_t28.Checked= this.chk_t28.Checked?false:true;
            this.chk_t34.Checked = this.chk_t34.Checked ? false : true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.chk_t5.Checked= this.chk_t5.Checked?false:true;
            this.chk_t11.Checked= this.chk_t11.Checked?false:true;
            this.chk_t17.Checked= this.chk_t17.Checked?false:true;
            this.chk_t23.Checked= this.chk_t23.Checked?false:true;
            this.chk_t29.Checked= this.chk_t29.Checked?false:true;
            this.chk_t35.Checked = this.chk_t35.Checked ? false : true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.chk_t6.Checked= this.chk_t6.Checked?false:true;
            this.chk_t12.Checked= this.chk_t12.Checked?false:true;
            this.chk_t18.Checked= this.chk_t18.Checked?false:true;
            this.chk_t24.Checked= this.chk_t24.Checked?false:true;
            this.chk_t30.Checked= this.chk_t30.Checked?false:true;
            this.chk_t36.Checked = this.chk_t36.Checked ? false : true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.chk_t7.Checked= this.chk_t7.Checked?false:true;
            this.chk_t13.Checked= this.chk_t13.Checked?false:true;
            this.chk_t19.Checked= this.chk_t19.Checked?false:true;
            this.chk_t25.Checked= this.chk_t25.Checked?false:true;
            this.chk_t31.Checked= this.chk_t31.Checked?false:true;
            this.chk_t1.Checked = this.chk_t1.Checked ? false : true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.chk_t2.Checked= this.chk_t2.Checked?false:true;
            this.chk_t3.Checked= this.chk_t3.Checked?false:true;
            this.chk_t4.Checked= this.chk_t4.Checked?false:true;
            this.chk_t5.Checked= this.chk_t5.Checked?false:true;
            this.chk_t6.Checked= this.chk_t6.Checked?false:true;
            this.chk_t1.Checked = this.chk_t1.Checked ? false : true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.chk_t8.Checked= this.chk_t8.Checked?false:true;
            this.chk_t9.Checked= this.chk_t9.Checked?false:true;
            this.chk_t10.Checked= this.chk_t10.Checked?false:true;
            this.chk_t11.Checked= this.chk_t11.Checked?false:true;
            this.chk_t12.Checked= this.chk_t12.Checked?false:true;
            this.chk_t7.Checked = this.chk_t7.Checked ? false : true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.chk_t14.Checked= this.chk_t14.Checked?false:true;
            this.chk_t15.Checked= this.chk_t15.Checked?false:true;
            this.chk_t16.Checked= this.chk_t16.Checked?false:true;
            this.chk_t17.Checked= this.chk_t17.Checked?false:true;
            this.chk_t18.Checked= this.chk_t18.Checked?false:true;
            this.chk_t13.Checked = this.chk_t13.Checked ? false : true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.chk_t20.Checked= this.chk_t20.Checked?false:true;
            this.chk_t21.Checked= this.chk_t21.Checked?false:true;
            this.chk_t22.Checked=  this.chk_t22.Checked?false:true;
            this.chk_t23.Checked= this.chk_t23.Checked?false:true;
            this.chk_t24.Checked=  this.chk_t24.Checked?false:true;
            this.chk_t19.Checked = this.chk_t19.Checked ? false : true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.chk_t26.Checked= this.chk_t26.Checked?false:true;
            this.chk_t27.Checked= this.chk_t27.Checked?false:true;
            this.chk_t28.Checked= this.chk_t28.Checked?false:true;
            this.chk_t29.Checked= this.chk_t29.Checked?false:true;
            this.chk_t30.Checked= this.chk_t30.Checked?false:true;
            this.chk_t25.Checked = this.chk_t25.Checked ? false : true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.chk_t32.Checked= this.chk_t32.Checked?false:true;
            this.chk_t33.Checked= this.chk_t33.Checked?false:true;
            this.chk_t34.Checked= this.chk_t34.Checked?false:true;
            this.chk_t35.Checked= this.chk_t35.Checked?false:true;
            this.chk_t36.Checked= this.chk_t36.Checked?false:true;
            this.chk_t31.Checked = this.chk_t31.Checked ? false : true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string segments = this.txt_segments.Text.Trim();
                string[] segs = segments.Split(',');
                int segcnt = segs.Length;
                List<int> terrainsegments = new List<int>();
                List<int> tiles = new List<int>();
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
                if (this.chk_t1.Checked)
                {
                    tiles.Add(1);
                }
                if (this.chk_t2.Checked)
                {
                    tiles.Add(2);
                }
                if (this.chk_t3.Checked)
                {
                    tiles.Add(3);
                }
                if (this.chk_t4.Checked)
                {
                    tiles.Add(4);
                }
                if (this.chk_t5.Checked)
                {
                    tiles.Add(5);
                }
                if (this.chk_t6.Checked)
                {
                    tiles.Add(6);
                }
                if (this.chk_t7.Checked)
                {
                    tiles.Add(7);
                }
                if (this.chk_t8.Checked)
                {
                    tiles.Add(8);
                }
                if (this.chk_t9.Checked)
                {
                    tiles.Add(9);
                }
                if (this.chk_t10.Checked)
                {
                    tiles.Add(10);
                }
                if (this.chk_t11.Checked)
                {
                    tiles.Add(11);
                }
                if (this.chk_t12.Checked)
                {
                    tiles.Add(12);
                }
                if (this.chk_t13.Checked)
                {
                    tiles.Add(13);
                }
                if (this.chk_t14.Checked)
                {
                    tiles.Add(14);
                }
                if (this.chk_t15.Checked)
                {
                    tiles.Add(15);
                }
                if (this.chk_t16.Checked)
                {
                    tiles.Add(16);
                }
                if (this.chk_t17.Checked)
                {
                    tiles.Add(17);
                }
                if (this.chk_t18.Checked)
                {
                    tiles.Add(18);
                }
                if (this.chk_t19.Checked)
                {
                    tiles.Add(19);
                }
                if (this.chk_t20.Checked)
                {
                    tiles.Add(20);
                }
                if (this.chk_t21.Checked)
                {
                    tiles.Add(21);
                }
                if (this.chk_t22.Checked)
                {
                    tiles.Add(22);
                }
                if (this.chk_t23.Checked)
                {
                    tiles.Add(23);
                }
                if (this.chk_t24.Checked)
                {
                    tiles.Add(24);
                }
                if (this.chk_t25.Checked)
                {
                    tiles.Add(25);
                }
                if (this.chk_t26.Checked)
                {
                    tiles.Add(26);
                }
                if (this.chk_t27.Checked)
                {
                    tiles.Add(27);
                }
                if (this.chk_t28.Checked)
                {
                    tiles.Add(28);
                }
                if (this.chk_t29.Checked)
                {
                    tiles.Add(29);
                }
                if (this.chk_t30.Checked)
                {
                    tiles.Add(30);
                }
                if (this.chk_t31.Checked)
                {
                    tiles.Add(31);
                }
                if (this.chk_t32.Checked)
                {
                    tiles.Add(32);
                }
                if (this.chk_t34.Checked)
                {
                    tiles.Add(34);
                }
                if (this.chk_t36.Checked)
                {
                    tiles.Add(36);
                }
                if (this.chk_t35.Checked)
                {
                    tiles.Add(35);
                }
                if (this.chk_t33.Checked)
                {
                    tiles.Add(33);
                }

                NFM_VERTEXSTRUCT_V11 tmp = new NFM_VERTEXSTRUCT_V11();
                tmp.fHeight = Convert.ToSingle(txt_fHeight.Text);
                tmp.wAttribute = Convert.ToUInt64(txt_wAttribute.Text);
                tmp.color1 = Convert.ToByte(txt_color1.Text);
                tmp.color2 = Convert.ToByte(txt_color2.Text);
                tmp.color3 = Convert.ToByte(txt_color3.Text);
                tmp.wFillBits1 = Convert.ToUInt16(txt_wFillBits1.Text);
                tmp.wFillBits2 = Convert.ToUInt16(txt_wFillBits2.Text);
                bool[] changeVals = { this.chk_Height.Checked, this.checkBox3.Checked, this.chk_Color.Checked, this.checkBox4.Checked };
                for (int i = 0; i < terrainsegments.Count; i++)
                {
                    for (int j = 0; j < tiles.Count; j++)
                    {
                        res.editNFMTile(terrainsegments[i], tiles[j] - 1, tmp, changeVals);
                    }
                }
                res.releaseWorkblock();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please fill in valid Values!");
            }
        }

        private void chk_Height_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_fHeight.Enabled = chk_Height.Checked;
            this.txt_fHeight.Text = "0";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_wFillBits1.Enabled = this.checkBox3.Checked;
            this.txt_wFillBits2.Enabled = this.checkBox3.Checked;
            this.txt_wFillBits2.Text = "0";
            this.txt_wFillBits1.Text = "0";

        }

        private void chk_Color_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_color1.Enabled = this.chk_Color.Checked;
            this.txt_color2.Enabled = this.chk_Color.Checked;
            this.txt_color3.Enabled = this.chk_Color.Checked;
            this.txt_color1.Text = "0";
            this.txt_color2.Text = "0";
            this.txt_color3.Text = "0";
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_wAttribute.Enabled = this.checkBox4.Checked;
            this.txt_wAttribute.Text = "0";
        }

        private void checkValues()
        {
            try
            {
                Convert.ToSingle(this.txt_fHeight.Text);
                Convert.ToByte(this.txt_color1.Text);
                Convert.ToByte(this.txt_color2.Text);
                Convert.ToByte(this.txt_color3.Text);
                Convert.ToInt16(this.txt_wFillBits1.Text);
                Convert.ToInt16(this.txt_wFillBits2.Text);
                Convert.ToInt64(this.txt_wAttribute.Text);
                this.btn_save.Enabled = true;
            }
            catch
            {
                this.btn_save.Enabled = false;
            }
        }

        private void txt_fHeight_TextChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void txt_wFillBits1_TextChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void txt_wFillBits2_TextChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void txt_color1_TextChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void txt_color2_TextChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void txt_color3_TextChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void txt_wAttribute_TextChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.chk_t2.Checked = this.chk_t2.Checked ? false : true;
            this.chk_t3.Checked = this.chk_t3.Checked ? false : true;
            this.chk_t4.Checked = this.chk_t4.Checked ? false : true;
            this.chk_t5.Checked = this.chk_t5.Checked ? false : true;
            this.chk_t6.Checked = this.chk_t6.Checked ? false : true;
            this.chk_t1.Checked = this.chk_t1.Checked ? false : true;
            this.chk_t8.Checked = this.chk_t8.Checked ? false : true;
            this.chk_t9.Checked = this.chk_t9.Checked ? false : true;
            this.chk_t10.Checked = this.chk_t10.Checked ? false : true;
            this.chk_t11.Checked = this.chk_t11.Checked ? false : true;
            this.chk_t12.Checked = this.chk_t12.Checked ? false : true;
            this.chk_t7.Checked = this.chk_t7.Checked ? false : true;
            this.chk_t14.Checked = this.chk_t14.Checked ? false : true;
            this.chk_t15.Checked = this.chk_t15.Checked ? false : true;
            this.chk_t16.Checked = this.chk_t16.Checked ? false : true;
            this.chk_t17.Checked = this.chk_t17.Checked ? false : true;
            this.chk_t18.Checked = this.chk_t18.Checked ? false : true;
            this.chk_t13.Checked = this.chk_t13.Checked ? false : true;
            this.chk_t20.Checked = this.chk_t20.Checked ? false : true;
            this.chk_t21.Checked = this.chk_t21.Checked ? false : true;
            this.chk_t22.Checked = this.chk_t22.Checked ? false : true;
            this.chk_t23.Checked = this.chk_t23.Checked ? false : true;
            this.chk_t24.Checked = this.chk_t24.Checked ? false : true;
            this.chk_t19.Checked = this.chk_t19.Checked ? false : true;
            this.chk_t26.Checked = this.chk_t26.Checked ? false : true;
            this.chk_t27.Checked = this.chk_t27.Checked ? false : true;
            this.chk_t28.Checked = this.chk_t28.Checked ? false : true;
            this.chk_t29.Checked = this.chk_t29.Checked ? false : true;
            this.chk_t30.Checked = this.chk_t30.Checked ? false : true;
            this.chk_t25.Checked = this.chk_t25.Checked ? false : true;
            this.chk_t32.Checked = this.chk_t32.Checked ? false : true;
            this.chk_t33.Checked = this.chk_t33.Checked ? false : true;
            this.chk_t34.Checked = this.chk_t34.Checked ? false : true;
            this.chk_t35.Checked = this.chk_t35.Checked ? false : true;
            this.chk_t36.Checked = this.chk_t36.Checked ? false : true;
            this.chk_t31.Checked = this.chk_t31.Checked ? false : true;
        }

        public void addSeg(int seg)
        {
            if (this.txt_segments.Text.Trim().Equals(""))
                this.txt_segments.Text = seg.ToString();
            else
                this.txt_segments.Text = this.txt_segments.Text.Trim() + "," + seg.ToString();

                
        }
           
            
            

        

        
    }
}
