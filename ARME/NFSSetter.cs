using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ARME.MapFileRes;
using ARME.Struct;

namespace ARME
{
    public partial class NFSSetter : Form
    {
        private RappelzMapEditor main = null;
        private int top;
        private int bottom;
        private int left;
        private int right;
        private int id;
        private PointF[] points=new PointF[5];
        private int counter = 0;
        public NFSSetter(RappelzMapEditor res,string info, int id)
        {
            InitializeComponent();
            this.main = res;
            this.updateinfo(info);
            this.brn_qpfsave.Enabled = false;
            this.id = id;
            this.txt_id.Enabled = false;
            this.txt_id.Text = id.ToString();
            lbl_info.Text = "Click Top-Left Corner of area.";
        }

        private void brn_qpfsave_Click(object sender, EventArgs e)
        {
            try
            {
                NFS_MONSTER_LOCATION tmp = new NFS_MONSTER_LOCATION();
                tmp.RegionIndex = Convert.ToInt32(txt_id.Text);
                tmp.Top = this.top;
                tmp.Bottom = this.bottom;
                tmp.Left = this.left;
                tmp.Right = this.right;
                tmp.ScriptText = txt_script.Text;
                tmp.ScriptLength = txt_script.Text.Length;
                tmp.MonsterCount = Convert.ToInt32(txt_type.Text);
                tmp.Trigger = 0;
                main.updateNFS(tmp);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please check if all values are filled in in the correct format!");
            }
        }

        public void updateinfo(string txt)
        {
            this.lbl_info.Text = txt;
        }

        public void setsquare(int type, int value, int x, int y)
        {
            switch (type)
            {
                case 1:
                    this.top = value;
                    this.lbl_topval.Text = value.ToString();
                    points[0].X = x;
                    points[0].Y = y;
                    counter = 1;
                    main.drawPoint(points, 4, counter);
                    break;
                case 2:
                    this.bottom = value;
                    this.lbl_bottomval.Text = value.ToString();
                    points[1].X = x;
                    points[1].Y = y;
                    counter = 2;
                    main.drawPoint(points, 4, counter);
                    break;
                case 3:
                    this.left = value;
                    this.lbl_leftval.Text = value.ToString();
                    points[2].X = x;
                    points[2].Y = y;
                    counter = 3;
                    main.drawPoint(points, 4, counter);
                    break;
                case 4:
                    this.right = value;
                    this.lbl_rightval.Text = value.ToString();
                    this.brn_qpfsave.Enabled = true;
                    points[3].X = x;
                    points[3].Y = y;
                    counter = 5;
                    PointF[] tmp = new PointF[5];
                    tmp[0] = new PointF(points[2].X, points[0].Y);
                    tmp[1] = new PointF(points[3].X, points[0].Y);
                    tmp[2] = new PointF(points[3].X, points[1].Y);
                    tmp[3] = new PointF(points[2].X, points[1].Y);
                    tmp[4] = new PointF(points[2].X, points[0].Y);
                    main.drawCoords(tmp, 4);
                    this.Focus();
                    break;
            }

        }

        private void NFSSetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.releaseWorkblock();
        }

        private void txt_type_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

       
    }
}
