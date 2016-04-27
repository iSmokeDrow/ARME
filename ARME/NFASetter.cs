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
    public partial class NFASetter : Form
    {
        private RappelzMapEditor main = null;
        private List<PointF> coords = new List<PointF>();
        private int count_coords;
        private bool loading = true;
        private int editindex = 0;
        private int mapx;
        private int mapy;

        public NFASetter(RappelzMapEditor res, string info, int id, int mapx, int mapy)
        {
            InitializeComponent();
            this.main = res;
            this.lbl_info.Text = info;
            this.count_coords = 0;
            this.txt_id.Text = id.ToString();
            this.loading = false;
            this.mapx = mapx;
            this.mapy = mapy;
        }

        private void brn_qpfsave_Click(object sender, EventArgs e)
        {
            StructNFA tmp = new StructNFA();
            tmp.id = Convert.ToInt32(this.txt_id.Text);
            tmp.coordcount = this.count_coords;
            tmp.points = new PointF[coords.Count + 1];
            for (int i = 0; i < coords.Count; i++)
            {
                if (i == 0)
                    tmp.points[coords.Count] = this.coords[i];
                tmp.points[i] = this.coords[i];
                tmp.coord = tmp.coord + (i + 1) + ". (" + this.coords[i].X + ", " + this.coords[i].Y + ")";
            }
            main.updateNFA(tmp);
            this.Close();

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
            main.drawCoords(tmplist, 1, this.count_coords + 1);
            
            if (this.coordlist.Items.Count > 2)
            {
                this.brn_qpfsave.Enabled = true;
                this.btn_editcoord.Enabled = true;
                this.btn_delcoord.Enabled = true;
            }
        }

       

        private void NFASetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.releaseWorkblock();
        }

        private void coordlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                int index = this.coordlist.SelectedIndex;
                if (index>-1)
                {
                    PointF[] tmppoints = new PointF[count_coords + 1];
                    for (int i = 0; i < count_coords; i++)
                    {
                        if (i == 0)
                            tmppoints[count_coords] = coords[i];
                        tmppoints[i] = coords[i];
                    }
                    main.selectCoord((int)coords[index].X, (int)coords[index].Y, tmppoints, 1);
                }
            }
        }

        private void btn_editcoord_Click(object sender, EventArgs e)
        {
            this.editindex = this.coordlist.SelectedIndex;
            if (this.editindex > -1)
            {
                main.nfacoordediter();
                this.lbl_info.Text = "Click on the desired location on mainmap\n to edit coordinate id:" + this.editindex + 1;
                this.txt_maninsertx.Text = this.coordlist.Items[editindex].ToString().Substring(5, this.coordlist.Items[editindex].ToString().IndexOf('y') - 5);
                this.txt_maninserty.Text = this.coordlist.Items[editindex].ToString().Substring(this.coordlist.Items[editindex].ToString().IndexOf('y') + 2);
                this.btn_maninsert.Text = "Edit";
                this.loading = true;
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
            main.drawCoords(tmplist, 1, this.count_coords + 1);
            this.loading = false;
            main.releasecoordsetter();
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
                main.drawCoords(tmplist, 1, this.count_coords + 1);
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

        private void btn_maninsert_Click(object sender, EventArgs e)
        {
            try
            {
                int tmp_x = (int)((Convert.ToInt32(this.txt_maninsertx.Text) - (mapx * 16128)) / (float)5.25);
                int tmp_y = 3072 - ((int)((Convert.ToInt32(this.txt_maninserty.Text) - (mapy * 16128)) / (float)5.25));
                int x = Convert.ToInt32(this.txt_maninsertx.Text);
                int y = Convert.ToInt32(this.txt_maninserty.Text);
                if (this.btn_maninsert.Text.Equals("Edit"))
                {
                    editcoord(tmp_x, tmp_y, x, y);
                    this.btn_maninsert.Text = "Insert";
                }
                else
                    addcoord(tmp_x, tmp_y, x, y);
            }
            catch
            {
                MessageBox.Show("Please fill in valid Coordinates");
            }
        }

        private void txt_maninsertx_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (Int32.TryParse(txt_maninsertx.Text, out result) && Int32.TryParse(txt_maninserty.Text, out result))
            {
                this.btn_maninsert.Enabled = true;
            }
            else
            {
                this.btn_maninsert.Enabled = false;
            }
        }

        private void txt_maninserty_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (Int32.TryParse(txt_maninsertx.Text, out result) && Int32.TryParse(txt_maninserty.Text, out result))
            {
                this.btn_maninsert.Enabled = true;
            }
            else
            {
                this.btn_maninsert.Enabled = false;
            }
        }
       

        
    }
}
