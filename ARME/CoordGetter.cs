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
    public partial class CoordGetter : Form
    {
        private RappelzMapEditor main = null;
        NFCCoords[] nfccoords;
        private List<PointF> coords = new List<PointF>();
        private int count_coords;
        private int id;
        private int type;
        private int editindex;
        private bool editer = false;
        private int x;
        private int y;
        private int actindex;
        private bool loading = true;

        public CoordGetter(RappelzMapEditor res, string info, int id, int type, PointF[] points,int x,int y)
        {
            InitializeComponent();
            this.main = res;
            this.lbl_info.Text = info;
            this.count_coords = 0;
            this.id = id;
            this.type = type;
            this.x = x;
            this.y = y;
            this.count_coords = points.Length-1;
            this.cb_coordset.Items.Add("Coordset 1");
            this.cb_coordset.SelectedIndex = 0;
            this.actindex = 0;
            this.btn_delcoordset.Enabled = false;
            this.button1.Enabled = false;
            this.brn_qpfsave.Enabled = false;
            this.cb_coordset.Enabled = false;
            this.btn_delcoordset.Enabled = false;
            this.cb_coordset.Enabled = false;
            for (int i = 0; i < points.Length-1; i++)
            {
                this.coords.Add(points[i]);
                this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((points[i].X) * 5.25) + (x * 16128)).ToString() + " y:" + (Convert.ToInt32((3072-(points[i].Y)) * 5.25) + (y * 16128)).ToString());
            }
        }

        public CoordGetter(RappelzMapEditor res, string info, int id, int type, NFCCoords[] points, int x, int y)
        {
            InitializeComponent();
            this.main = res;
            this.nfccoords = points;
            this.lbl_info.Text = info;
            this.id = id;
            this.type = type;
            for (int i = 0; i < nfccoords.Length; i++)
            {
                this.cb_coordset.Items.Add("Coordset "+(i+1));
            }


            this.count_coords = nfccoords[0].coords.Length - 1;
            this.cb_coordset.SelectedIndex = 0;
            this.cb_coordset.Enabled = true;
            for (int i = 0; i < nfccoords[0].coords.Length - 1; i++)
            {
                this.coords.Add(nfccoords[0].coords[i]);
                this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((nfccoords[0].coords[i].X) * 5.25) + (x * 16128)).ToString() + " y:" + (Convert.ToInt32((3072 - (nfccoords[0].coords[i].Y)) * 5.25) + (y * 16128)).ToString());
            }
            if (nfccoords.Length <= 1)
            {
                this.btn_delcoordset.Enabled = false;
                this.cb_coordset.Enabled = false;
            }
            this.loading = false;
        }

        private void brn_qpfsave_Click(object sender, EventArgs e)
        {
            PointF[] tmpcoords = new PointF[this.count_coords+1];
            string tmpcoord = "";
            for (int i = 0; i < coords.Count; i++)
            {
                if (i == 0)
                    tmpcoords[count_coords] = coords[i];
                tmpcoords[i] = this.coords[i];
                tmpcoord = tmpcoord + (i + 1) + ". (" + this.coords[i].X + ", " + this.coords[i].Y + ")";
            }
            if (type == 2)
            {
                this.nfccoords[actindex] = new NFCCoords();
                this.nfccoords[actindex].coords = new PointF[tmpcoords.Length];
                this.nfccoords[actindex].coords = tmpcoords;
                this.nfccoords[actindex].cnt_coords = tmpcoords.Length;
                main.modifyCoords(this.id, tmpcoord, nfccoords, type);
            }
            else
            {
                main.modifyCoords(this.id, tmpcoord, tmpcoords, type);
            }
            this.Close();
        }

        public void editcoord(int x, int y, int displayx, int displayy)
        {
            PointF tmp = new PointF();
            this.coordlist.Items[editindex] = (editindex+1).ToString() + ". x:" + displayx + " y:" + displayy;
            tmp.X = x;
            tmp.Y = y;
            this.coords[editindex] = tmp;
            List<PointF> tmplist = new List<PointF>(coords);
            tmplist.Add(tmplist[0]);
            main.drawCoords(tmplist, this.type, this.count_coords+1);
            this.editer = false;
            main.releasecoordsetter();
            this.brn_qpfsave.Enabled = true;
        }

        public void addcoord(int x, int y, int displayx, int displayy)
        {
            PointF tmp = new PointF();
            this.count_coords = count_coords + 1;
            this.coordlist.Items.Add(count_coords.ToString() + ". x:" + displayx + " y:" + displayy);
            tmp.X = x;
            tmp.Y = y;
            if (this.coordlist.SelectedIndex>-1)
                this.coords.Insert(this.coordlist.SelectedIndex+1,tmp);
            else
                this.coords.Add(tmp);
            List<PointF> tmplist = new List<PointF>(coords);
            tmplist.Add(tmplist[0]);
            main.drawCoords(tmplist, this.type, this.count_coords+1);
            this.coordlist.SelectedIndex = this.coordlist.SelectedIndex + 1;
            if (this.coords.Count > 2)
            {
                this.button1.Enabled = true;
                this.brn_qpfsave.Enabled = true;
                this.cb_coordset.Enabled = true;
                this.btn_delcoordset.Enabled = true;
            }

        }

        private void CoordGetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.releaseWorkblock();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.coordlist.Items.Clear();
            this.coords.Clear();
            this.count_coords = 0;
            main.releasecoordsetter();
            main.refreshMain();
        }

        private void coordlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!editer)
            {
                int index = this.coordlist.SelectedIndex;
                PointF[] tmppoints = new PointF[count_coords+1];
                for (int i = 0; i < count_coords; i++)
                {
                    if (i == 0)
                        tmppoints[count_coords] = coords[i];
                    tmppoints[i] = coords[i];
                }
                main.selectCoord((int)coords[index].X, (int)coords[index].Y,tmppoints,type);
            }
        }

        private void btn_coordedit_Click(object sender, EventArgs e)
        {
            this.editindex = this.coordlist.SelectedIndex;
            if (editindex > -1)
            {
                main.setcoordediter();
                this.lbl_info.Text = "Click on the desired location on mainmap\n to edit coordinate id:" + (this.editindex + 1);
                this.btn_maninsert.Text = "Edit";
                if (editindex < 9)
                {
                    this.txt_maninsertx.Text = this.coordlist.Items[editindex].ToString().Substring(5, this.coordlist.Items[editindex].ToString().IndexOf('y') - 5);
                }
                else
                {
                    this.txt_maninsertx.Text = this.coordlist.Items[editindex].ToString().Substring(6, this.coordlist.Items[editindex].ToString().IndexOf('y') - 6);
                }
                this.txt_maninserty.Text = this.coordlist.Items[editindex].ToString().Substring(this.coordlist.Items[editindex].ToString().IndexOf('y') +2 );
                this.editer = true;
            }
        }

        private void btn_addcoords_Click(object sender, EventArgs e)
        {
            main.setcoordsetter();

        }

        private void cb_coordset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type == 2 && !loading)
            {
                this.loading = true;
                int index = this.cb_coordset.SelectedIndex;
                if (index > -1)
                {
                    this.nfccoords[actindex] = new NFCCoords();
                    this.nfccoords[actindex].cnt_coords = coords.Count+1;
                    this.nfccoords[actindex].coords = new PointF[coords.Count + 1];
                    for (int i = 0; i < coords.Count; i++)
                    {
                        if (i == 0)
                            this.nfccoords[actindex].coords[coords.Count] = coords[i];
                        this.nfccoords[actindex].coords[i] = coords[i];
                    }
                    this.actindex = index;
                    this.coordlist.Items.Clear();
                    this.coords.Clear();
                    this.count_coords = 0;
                    this.count_coords = nfccoords[index].coords.Length - 1;
                    this.cb_coordset.Enabled = true;
                    for (int i = 0; i < nfccoords[index].coords.Length - 1; i++)
                    {
                        this.coords.Add(nfccoords[index].coords[i]);
                        this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((nfccoords[index].coords[i].X) * 5.25) + (this.x * 16128)).ToString() + " y:" + (Convert.ToInt32((3072 - (nfccoords[index].coords[i].Y)) * 5.25) + (this.y * 16128)).ToString());
                    }
                    List<PointF> tmplist = new List<PointF>(coords);
                    tmplist.Add(tmplist[0]);
                    main.drawCoords(tmplist, 2, this.count_coords + 1);
                    this.loading = false;
                }
            }
        }

        private void btn_coorddelete_Click(object sender, EventArgs e)
        {
            if (this.coordlist.SelectedIndex > -1)
            {
                int id = this.coordlist.SelectedIndex;
                coords.RemoveAt(id);
                this.count_coords = this.count_coords - 1;
                this.coordlist.Items.Clear();
                for (int i = 0; i < coords.Count; i++)
                    this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((coords[i].X) * 5.25) + (x * 16128)).ToString() + " y:" + (Convert.ToInt32((3072 - (coords[i].Y)) * 5.25) + (y * 16128)).ToString());

                List<PointF> tmplist = new List<PointF>(coords);
                tmplist.Add(tmplist[0]);
                main.drawCoords(tmplist, this.type, this.count_coords + 1);
                if (id < coordlist.Items.Count)
                    this.coordlist.SelectedIndex = id;
                else
                    this.coordlist.SelectedIndex = id - 1;
                this.brn_qpfsave.Enabled = true;
            }
        }

        private void btn_delcoordset_Click(object sender, EventArgs e)
        {
            if(nfccoords.Length>1)
            {
                this.loading=true;

                NFCCoords[] tmp = new NFCCoords[nfccoords.Length - 1];
                int j = 0;
                for(int i=0;i<nfccoords.Length;i++)
                {
                    if(i==cb_coordset.SelectedIndex)
                    {
                    }
                    else
                    {
                        tmp[j]=this.nfccoords[i];
                        j++;
                    }


                }
                this.nfccoords=tmp;
                this.coordlist.Items.Clear();
                this.coords.Clear();
                this.cb_coordset.Items.Clear();
                for (int i = 0; i < nfccoords.Length; i++)
                {
                    this.cb_coordset.Items.Add("Coordset "+(i+1));
                }


                this.count_coords = nfccoords[0].coords.Length - 1;
               
                this.cb_coordset.Enabled = true;
                for (int i = 0; i < nfccoords[0].coords.Length - 1; i++)
                {
                    this.coords.Add(nfccoords[0].coords[i]);
                    this.coordlist.Items.Add((i + 1).ToString() + ". x:" + (Convert.ToInt32((nfccoords[0].coords[i].X) * 5.25) + (x * 16128)).ToString() + " y:" + (Convert.ToInt32((3072 - (nfccoords[0].coords[i].Y)) * 5.25) + (y * 16128)).ToString());
                }
                this.cb_coordset.SelectedIndex = 0;
                if (nfccoords.Length <= 1)
                {
                    this.btn_delcoordset.Enabled = false;
                    this.cb_coordset.Enabled = false;
                }
                this.actindex = cb_coordset.SelectedIndex;
                this.loading = false;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.loading = true;
            NFCCoords[] tmpcoordsets = new NFCCoords[nfccoords.Length+1];
            if (nfccoords.Length > 1)
            {
                for (int i = 0; i < this.nfccoords.Length-1; i++)
                {
                    tmpcoordsets[i] = nfccoords[i];
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
            this.cb_coordset.Items.Add("Coordset " + tmpcoordsets.Length);
            this.coords.Clear();
            this.cb_coordset.SelectedIndex = tmpcoordsets.Length-1;
            this.actindex = this.cb_coordset.SelectedIndex;
            this.nfccoords = tmpcoordsets;
            this.loading = false;
            this.button1.Enabled = false;
            this.brn_qpfsave.Enabled = false;
            this.cb_coordset.Enabled = false;
            this.btn_delcoordset.Enabled = false;
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

        private void btn_maninsert_Click(object sender, EventArgs e)
        {
            try
            {
                int tmp_x = (int)((Convert.ToInt32(this.txt_maninsertx.Text) - (this.x * 16128)) / (float)5.25);
                int tmp_y = 3072 - ((int)((Convert.ToInt32(this.txt_maninserty.Text) - (this.y * 16128)) / (float)5.25));
                int lx = Convert.ToInt32(this.txt_maninsertx.Text);
                int ly = Convert.ToInt32(this.txt_maninserty.Text);
                if (this.btn_maninsert.Text.Equals("Edit"))
                {
                    editcoord(tmp_x, tmp_y, lx, ly);
                    this.btn_maninsert.Text = "Insert";
                }
                else
                    addcoord(tmp_x, tmp_y, lx, ly);
            }
            catch
            {
                MessageBox.Show("Please fill in valid Coordinates");
            }
        }

        
        
    }
}
