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
    public partial class NFCSetter : Form
    {
        private RappelzMapEditor main = null;
        private List<PointF> coords = new List<PointF>();
        private int count_coords;
        private NFCCoords[] coordsets;
        private int coordsetcnt;
        private int mapx;
        private int mapy;
        private bool loading = true;
        private int actindex = 0;
        private int editindex=0;
        private bool nfcediter = false;

        public NFCSetter(RappelzMapEditor res, string info, int id, int map_x, int map_y)
        {
            InitializeComponent();
            this.main = res;
            this.lbl_info.Text = info;
            this.count_coords = 0;
            this.txt_id.Text = id.ToString();
            this.cb_coordset.Items.Add("Coordset 1");
            this.cb_coordset.SelectedIndex = 0;
            this.cb_coordset.Enabled = false;
            this.coordsetcnt = 1;
            this.coordsets = new NFCCoords[this.coordsetcnt];
            this.loading = false;
            this.actindex = 0;
            this.mapx = map_x;
            this.mapy = map_y;
            this.btn_editcoord.Enabled = false;
            this.btn_delcoord.Enabled = false;
        }

        private void brn_qpfsave_Click(object sender, EventArgs e)
        {
            try
            {
                StructNFC tmp = new StructNFC();
                tmp.id = Convert.ToInt32(this.txt_id.Text);
                tmp.name = this.txt_name.Text;
                tmp.type = Convert.ToInt32(this.txt_type.Text);
                tmp.script = this.txt_script.Text;
                tmp.cnt_script = this.txt_script.Text.Length;
                tmp.cnt_name = this.txt_name.Text.Length;
                tmp.cnt_coords = this.coordsetcnt;
                coordsets[cb_coordset.SelectedIndex] = new NFCCoords();
                coordsets[cb_coordset.SelectedIndex].cnt_coords = coords.Count;
                coordsets[cb_coordset.SelectedIndex].coords = new PointF[coords.Count + 1];
                for (int i = 0; i < coords.Count; i++)
                {
                    if (i == 0)
                        coordsets[cb_coordset.SelectedIndex].coords[coords.Count] = this.coords[i];
                    coordsets[cb_coordset.SelectedIndex].coords[i] = this.coords[i];
                    tmp.coord = tmp.coord + (i + 1) + ". (" + this.coords[i].X + ", " + this.coords[i].Y + ")";
                }
                tmp.coords = this.coordsets;
                /*tmp.coords = new NFCCoords[1];
                tmp.coords[0] = new NFCCoords();
                tmp.coords[0].cnt_coords = coords.Count;
                tmp.coords[0].coords = new PointF[coords.Count + 1];
                for (int i = 0; i < coords.Count; i++)
                {
                    if (i == 0)
                        tmp.coords[0].coords[coords.Count] = this.coords[i];
                    tmp.coords[0].coords[i] = this.coords[i];
                    tmp.coord = tmp.coord + (i + 1) + ". (" + this.coords[i].X + ", " + this.coords[i].Y + ")";
                }*/
                tmp.unknown1 = 0;
                tmp.unknown2 = 0;
                tmp.unknown3 = 0;
                tmp.unknown4 = 0;
                main.updateNFC(tmp);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please check if all values and coordsets are filled in in the correct format!");
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
            main.drawCoords(tmplist, 2, this.count_coords + 1);
            if (this.coordlist.Items.Count > 2)
            {
                this.brn_qpfsave.Enabled = true;
                this.btn_addcoordset.Enabled = true;
                if (coordsetcnt > 1)
                    this.nfcediter = true;
                this.btn_delcoord.Enabled = true;
                this.btn_editcoord.Enabled = true;
                this.cb_coordset.Enabled = nfcediter;
                
            }
        }

        private void NFCSetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.releaseWorkblock();
        }

        private void txt_type_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) )
                e.Handled = true; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.loading = true;
            this.coordsetcnt = this.coordsetcnt + 1;
            NFCCoords[] tmpcoordsets = new NFCCoords[this.coordsetcnt];
            if (coordsetcnt > 2)
            {
                for (int i = 0; i < this.coordsets.Length; i++)
                {
                    tmpcoordsets[i] = coordsets[i];
                }
            }

            tmpcoordsets[cb_coordset.SelectedIndex] = new NFCCoords();
            tmpcoordsets[cb_coordset.SelectedIndex].cnt_coords = this.coords.Count;
            tmpcoordsets[cb_coordset.SelectedIndex].coords = new PointF[this.coords.Count + 1];
            this.coordlist.Items.Clear();             
            tmpcoordsets[cb_coordset.SelectedIndex].coords = new PointF[this.coords.Count + 1];
            for (int i = 0; i < this.coords.Count; i++)
            {
                if (i == 0)
                {
                    tmpcoordsets[cb_coordset.SelectedIndex].coords[this.coords.Count] = coords[i];                    
                }
                tmpcoordsets[cb_coordset.SelectedIndex].coords[i] = coords[i];
                //this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((this.coordsets[cb_coordset.SelectedIndex].coords[i].X) * 5.25) + (this.mapx * 16128)).ToString() + " y:" + (Convert.ToInt32((3072 - (this.coordsets[cb_coordset.SelectedIndex].coords[i].Y)) * 5.25) + (this.mapy * 16128)).ToString());
            }
            this.count_coords = 0;
            this.cb_coordset.Items.Add("Coordset " + coordsetcnt.ToString());
            this.coords.Clear();
            this.cb_coordset.SelectedIndex = coordsetcnt - 1;
            this.actindex = this.cb_coordset.SelectedIndex;
            this.coordsets = tmpcoordsets;
            this.loading = false;
            this.btn_addcoordset.Enabled = false;
            this.brn_qpfsave.Enabled = false;
            this.nfcediter = false;
            this.cb_coordset.Enabled = this.nfcediter;
            this.btn_editcoord.Enabled = false;
            this.btn_delcoord.Enabled = false;
            
        }

        private void cb_coordset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                this.loading = true;
                int index = this.cb_coordset.SelectedIndex;
                if (index > -1)
                {
                    this.coordsets[actindex] = new NFCCoords();
                    this.coordsets[actindex].cnt_coords = coords.Count;
                    this.coordsets[actindex].coords = new PointF[coords.Count + 1];
                    for (int i = 0; i < coords.Count; i++)
                    {
                        if (i == 0)
                            this.coordsets[actindex].coords[coords.Count] = coords[i];
                        this.coordsets[actindex].coords[i] = coords[i];
                    }
                    this.actindex = index;
                    this.coordlist.Items.Clear();
                    this.coords.Clear();
                    this.count_coords = 0;
                    this.count_coords = coordsets[index].coords.Length - 1;
                    this.cb_coordset.Enabled = this.nfcediter;
                    for (int i = 0; i < coordsets[index].coords.Length - 1; i++)
                    {
                        this.coords.Add(coordsets[index].coords[i]);
                        this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((coordsets[index].coords[i].X) * 5.25) + (this.mapx * 16128)).ToString() + " y:" + (Convert.ToInt32((3072 - (coordsets[index].coords[i].Y)) * 5.25) + (this.mapy * 16128)).ToString());
                    }
                    try
                    {
                        List<PointF> tmplist = new List<PointF>(coords);
                        tmplist.Add(tmplist[0]);
                        main.drawCoords(tmplist, 2, this.count_coords + 1);
                    }
                    catch
                    {
                    }
                    this.loading = false;
                }
            }
        }

        private void coordlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading && this.coordlist.SelectedIndex!=-1)
            {
                int index = this.coordlist.SelectedIndex;
                PointF[] tmppoints = new PointF[count_coords + 1];
                for (int i = 0; i < count_coords; i++)
                {
                    if (i == 0)
                        tmppoints[count_coords] = coords[i];
                    tmppoints[i] = coords[i];
                }
                main.selectCoord((int)coords[index].X, (int)coords[index].Y, tmppoints, 2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.editindex = this.coordlist.SelectedIndex;
            if (this.editindex > -1)
            {
                main.nfccoordediter();
                this.lbl_info.Text = "Click on the desired location on mainmap\n to edit coordinate id:" + this.editindex + 1;
                this.loading = true;
            }
            else
            {
                MessageBox.Show("Please select the coordinates from the listbox you want to modify!");
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
            main.drawCoords(tmplist, 2, this.count_coords + 1);
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
                main.drawCoords(tmplist, 2, this.count_coords + 1);
                if (coordlist.Items.Count < 3)
                {
                    this.brn_qpfsave.Enabled = false;
                    this.btn_addcoordset.Enabled = false;
                    this.btn_delcoord.Enabled = false;
                    this.btn_editcoord.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select the coordinates from the listbox you want to delete!");
            }
        }

        private void txt_script_Validating(object sender, CancelEventArgs e)
        {
            this.txt_name.Text = main.getstring(this.txt_script.Text);
        }

       
    }
}
