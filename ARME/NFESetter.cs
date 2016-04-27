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
    public partial class NFESetter : Form
    {
        private RappelzMapEditor main = null;
        private List<PointF> coords = new List<PointF>();
        private int count_coords;
        private bool loading = true;
        private int editindex = 0;
        private int mapx;
        private int mapy;
        public NFESetter(RappelzMapEditor res, string info, int mapx, int mapy)
        {
            InitializeComponent();
            this.main = res;
            this.lbl_info.Text = info;
            this.count_coords = 0;
            this.loading = false;
            this.mapx = mapx;
            this.mapy = mapy;
        }

        private void brn_qpfsave_Click(object sender, EventArgs e)
        {
            try
            {
                StructNFE tmp = new StructNFE();
                tmp.id = Convert.ToInt32(this.txt_id.Text);
                tmp.count_coords = this.count_coords;
                tmp.type = Convert.ToInt32(this.txt_type.Text);
                tmp.coords = new PointF[coordlist.Items.Count+1];
                for (int i = 0; i < coordlist.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        tmp.coords[coordlist.Items.Count] = new PointF();
                        tmp.coords[coordlist.Items.Count] = this.coords[i];

                    }
                    tmp.coords[i] = new PointF();
                    tmp.coords[i] = this.coords[i];
                    tmp.coord = tmp.coord + (i + 1) + ". (" + this.coords[i].X + ", " + this.coords[i].Y + ")";                
                }
                this.main.updateNFE(tmp);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check if all values are filled in in the correct format!\n"+ex.Message);
            }
        }

        public void addcoord(int x, int y, int displayx, int displayy)
        {
            PointF tmp = new PointF();
            this.count_coords = count_coords + 1;
            this.coordlist.Items.Add(count_coords.ToString() + ". x:" + displayx + " y:" + displayy);
            tmp.X = x;
            tmp.Y = y;
            this.coords.Add(tmp);
            List<PointF> tmplist = new List<PointF>(coords);
            tmplist.Add(tmplist[0]);
            main.drawCoords(tmplist, 3, this.count_coords + 1);
            if (this.coordlist.Items.Count > 2)
            {
                this.brn_qpfsave.Enabled = true;
                this.btn_editcoord.Enabled = true;
                this.btn_delcoord.Enabled = true;
            }
        }

        private void NFESetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.releaseWorkblock();
        }

        private void txt_type_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void coordlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                int index = this.coordlist.SelectedIndex;
                PointF[] tmppoints = new PointF[count_coords + 1];
                for (int i = 0; i < count_coords; i++)
                {
                    if (i == 0)
                        tmppoints[count_coords] = coords[i];
                    tmppoints[i] = coords[i];
                }
                main.selectCoord((int)coords[index].X, (int)coords[index].Y, tmppoints, 3);
            }
        }
        public void editcoord(int x, int y, int displayx, int displayy)
        {
            PointF tmp = new PointF();
            this.coordlist.Items[editindex] = (editindex + 1).ToString() + ". x:" + displayx + " y:" + displayy;
            tmp.X = x;
            tmp.Y = y;
            this.coords[editindex] = tmp;
            List<PointF> tmplist = new List<PointF>(coords);
            tmplist.Add(tmplist[0]);
            main.drawCoords(tmplist, 3, this.count_coords + 1);
            this.loading = false;
            main.releasecoordsetter();
        }

        private void btn_editcoord_Click(object sender, EventArgs e)
        {
            this.editindex = this.coordlist.SelectedIndex;
            if (this.editindex > -1)
            {
                main.nfecoordediter();
                this.lbl_info.Text = "Click on the desired location on mainmap\n to edit coordinate id:" + this.editindex + 1;
                this.loading = true;
            }
        }

        private void btn_delcoord_Click(object sender, EventArgs e)
        {
            int id = this.coordlist.SelectedIndex;
            if (id > -1)
            {
                coords.RemoveAt(id);
                this.count_coords = this.count_coords - 1;
                this.coordlist.Items.Clear();
                for (int i = 0; i < coords.Count; i++)
                    this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((coords[i].X) * 5.25) + (mapx * 16128)).ToString() + " y:" + (Convert.ToInt32((3072 - (coords[i].Y)) * 5.25) + (mapy * 16128)).ToString());

                List<PointF> tmplist = new List<PointF>(coords);
                tmplist.Add(tmplist[0]);
                main.drawCoords(tmplist, 3, this.count_coords + 1);
                if (coordlist.Items.Count < 3)
                {
                    this.brn_qpfsave.Enabled = false;
                    this.btn_delcoord.Enabled = false;
                    this.btn_editcoord.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select the coordinates from the listbox you want to delete!");
            }
        }
    }
}
