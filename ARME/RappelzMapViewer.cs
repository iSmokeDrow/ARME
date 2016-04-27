using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ARME.MapFileRes;
using ARME.Struct;

namespace ARME
{
    /// <summary>
    /// Rappelz Map Editor
    /// Original Source: c1ph3r
    /// Cleaned/Reorganized Source: iSmokeDrow
    /// DataBurner + Contributing Developer: xXExiledXx
    /// </summary>
    /// TODO: Rewrite Main Map Mouse click for async dragging 
    public partial class RappelzMapEditor : Form
    {
        private bool workcheck = false; 
        private MapManager Map;
        private FileIO file;
        private bool mousedown;
        private int dragsx;
        private int dragsy;
        private int dragex;
        private int dragey;
        private int partx;
        private int party;
        private int nfmhelperindex = -1;
        private string statustext;
        private bool error = false;
        private bool QPFsetter = false;
        private bool NFMPROPsetter = false;
        private bool NFSsetter = false;
        private bool NFEsetter = false;
        private bool NFCsetter = false;
        private bool NFAsetter = false;
        private bool NPCsetter = false;
        private bool TextureSetter = false;
        private bool TerrainSetter = false;
        private bool COORDgetter = false;
        private bool COORDget = false;
        private int COORDgettype = 0;
        private int COORDgetid = 0;
        private bool COORDediter = false;
        private bool nfcCOORDediter = false;
        private bool nfaCOORDediter = false;
        private bool nfeCOORDediter = false;
        private bool chworkblocker = false;
        private NFSSetter NFSwindow;
        private CoordGetter Coordwindow;
        private NFCSetter NFCwindow;
        private NFESetter NFEwindow;
        private NFASetter NFAwindow;
        private TerrainEditer TerrainWindow;
        private NFMPropSetter NFMWindow;
        private TextureEditer TextureWindow;
        private int nfstype;
        private bool drawpoint = false;
        private int drawpointx = 0;
        private int drawpointy = 0;
        public StructQPF qpftmp;
        public bool drawNFA = false;
        private NFMHelper nfm;

        /// <summary>
        /// Gets and Sets whether or not the user is currently holding down the mouse
        /// </summary>
        public bool IsMouseDown = false;

        public RappelzMapEditor()
        {
            InitializeComponent();
            this.tb_nfa.Parent = null;
            this.tb_nfe.Parent = null;
            this.tb_nfs.Parent = null;
            this.tb_qpf.Parent = null;
            this.tb_nfc.Parent = null;
            this.tb_npc.Parent = null;
            this.tb_nfm.Parent = null;
            this.btn_delete.Enabled = false;
            this.btn_delnfc.Enabled = false;
            this.btn_delnfe.Enabled = false;
            this.btn_delnfs.Enabled = false;
            this.btn_delnpc.Enabled = false;
            this.btn_delqpf.Enabled = false;
            this.btn_savenfa.Enabled = false;
            this.btn_savenfc.Enabled = false;
            this.btn_savenfe.Enabled = false;
            this.btn_savenfs.Enabled = false;
            this.btn_saveqpf.Enabled = false;
            this.btn_savenpc.Enabled = false;
            this.MouseWheel += RappelzMapEditor_MouseWheel;
            this.Map = new MapManager();          
        }

        /// <summary>
        /// Occurs when the mouse wheel is scrolled up 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RappelzMapEditor_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Map != null && !Map.error)
            {
                Control MapImage_control = GetChildAtPoint(MousePosition);
                if (MapImage_control != null)
                {
                    if (e.Delta > 0) { btn_zoomin.PerformClick(); }
                    if (e.Delta < 0) { btn_zoomout.PerformClick(); }
                }
            }
        }

        /// <summary>
        /// Occurs once the RappelzMapEditor form has been loaded
        /// </summary>
        private void RappelzMapEditor_Load(object sender, EventArgs e)
        {
            using (BackgroundWorker loadWorker = new BackgroundWorker())
            {
                loadWorker.DoWork += (o, x) =>
                {
                    resourcecheck();
                    this.Map.stringload(Properties.Settings.Default.stringDir);
                    this.file = new FileIO(Properties.Settings.Default.dataDir);
                };
                loadWorker.RunWorkerCompleted += (o, x) => 
                {
                    if (file != null) { this.mapList.DataSource = file.filenames; }
                    this.workcheck = Map.check;
                    if (mapList.Items.Count == 0)
                    {
                        DumpSelect();
                    }
                };
                loadWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Occurs if proper resources are not detected
        /// </summary>
        private void DumpSelect()
        {            
            if (MessageBox.Show("No Resources in: " + Properties.Settings.Default.dataDir + "\n Dump from Client?", "No Resources", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                DumpClientResources();
            }
            else
            {                
                if (MessageBox.Show("Cannot continue to load without Resources!\nWould you like to select another directory?", "No Resources", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    dataDirselect(); 
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btn_nfaselect_Click(object sender, EventArgs e)
        {
            dataDirselect();   
        }

        private void dataDirselect()
        {
            this.fbd_seldir.Description = "Please Select a resource folder that contains Rappelz Map Files";
            this.fbd_seldir.ShowDialog();
            Properties.Settings.Default.dataDir = fbd_seldir.SelectedPath;
            refreshresource(fbd_seldir.SelectedPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.savePic.ShowDialog();
        }

        private void savePic_FileOk(object sender, CancelEventArgs e)
        {
            Map.saveMapFile(this.savePic.FileName);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Map.zoomMap(2);
            refresh();
        }

        private void btn_zoomout_Click(object sender, EventArgs e)
        {
            Map.zoomMap(1);
            refresh();
        } 

        private Point calcMMCoords(Point coords)
        {
            return new Point(coords.X - this.pb_Minimap.Location.X, coords.Y - this.pb_Minimap.Location.X);
        }

        public void refresh()
        {
            if (Map.check)
            {
                this.lbl_dir.Text = Map.filename;
                this.lbl_fileparts.Text = Map.loaded_parts;
                this.pb_map.Image = Map.getMapFile();
                this.pb_Minimap.Image = Map.getMiniMap(this.chk_loadMiniMapProps.Checked);
                this.lbl_zoom.Text = Map.zoomfactor.ToString();
            }

            //If the mouse is being dragged we don't want to update the statics (who have already been filled out)
            if (!IsMouseDown) 
            {
                if (Map.NFM.check && Map.NFM.MapProps != null) 
                {
                    lbl_MPu1_1.Text = string.Format("U1 (3B): {0} {1} {2}", Map.NFM.MapProps.u1_1, Map.NFM.MapProps.u1_2, Map.NFM.MapProps.u1_3);
                    lbl_MPu1_2.Text = string.Format("U1 (3B): {0} {1} {2}", Map.NFM.MapProps.u1_4, Map.NFM.MapProps.u1_5, Map.NFM.MapProps.u1_6);
                    lbl_u2.Text = string.Concat("U2 (1F): ", Map.NFM.MapProps.u2);
                    lbl_MPu3.Text = string.Concat("U3 (1F): ", Map.NFM.MapProps.u3);
                    lbl_MPu4.Text = string.Concat("U4 (1F): ", Map.NFM.MapProps.u4);
                    lbl_MPu5_1.Text = string.Format("U5 (3B): {0} {1} {2}", Map.NFM.MapProps.u5_1, Map.NFM.MapProps.u5_2, Map.NFM.MapProps.u5_3);
                    lbl_MPu5_2.Text = string.Format("U5 (3B): {0} {1} {2}", Map.NFM.MapProps.u5_4, Map.NFM.MapProps.u5_5, Map.NFM.MapProps.u5_6);
                    lbl_MPu6.Text = string.Concat("U6 (1F): ", Map.NFM.MapProps.u6);
                    lbl_MPu7.Text = string.Concat("U7 (1F): ", Map.NFM.MapProps.u7);
                    lbl_MPu8.Text = string.Concat("U8 (1F): ", Map.NFM.MapProps.u8);
                    lbl_MPu9_1.Text = string.Format("U9 (3B): {0} {1} {2}", Map.NFM.MapProps.u9_4, Map.NFM.MapProps.u9_5, Map.NFM.MapProps.u9_6);
                    lbl_MPu9_2.Text = string.Format("U9 (3B): {0} {1} {2}", Map.NFM.MapProps.u9_1, Map.NFM.MapProps.u9_2, Map.NFM.MapProps.u9_3);
                    lbl_MPu10.Text = string.Concat("U10 (1F): ", Map.NFM.MapProps.u10);
                    lbl_MPu11.Text = string.Concat("U11 (1F): ", Map.NFM.MapProps.u11);
                    lbl_MPu12.Text = string.Concat("U12 (1I): ", Map.NFM.MapProps.u12);
                    lbl_MPsT.Text = string.Concat("showTerrain: ", Map.NFM.MapProps.showTerrain.ToString());
                    lbl_NFM1.Text = string.Concat("szSign: " , Map.NFM.header.szSign);
                    lbl_NFM2.Text = string.Concat("Version: ", Map.NFM.header.dwVersion);
                    lbl_NFMTC.Text = string.Concat("TileCount: ", Map.NFM.header.nTileCountPerSegment);
                    lbl_NFMSC.Text = string.Concat("SegmentCount: ", this.Map.NFM.header.nSegmentCountPerMap);
                    lbl_NFMTL.Text = string.Concat("TileLength: ", this.Map.NFM.header.fTileLength);
                    lbl_NFMGO.Text = string.Concat("Grass: ", this.Map.NFM.header.dwGrassColonyOffset);
                    lbl_NFMEAO.Text = string.Concat("EventArea: ", this.Map.NFM.header.dwEventAreaOffset);
                    lbl_NFMWO.Text = string.Concat("Water: ", this.Map.NFM.header.dwWaterOffset);
                    lbl_NFMTO.Text = string.Concat("Terrain: ", this.Map.NFM.header.dwTerrainSegmentOffset);
                    lbl_NFMMPO.Text = string.Concat("MapProps: ", this.Map.NFM.header.dwMapPropertiesOffset);
                    lbl_NFMVO.Text = string.Concat("Vector: ", this.Map.NFM.header.dwVectorAttrOffset);
                    lbl_NFMPO.Text = string.Concat("Props: ", this.Map.NFM.header.dwPropOffset);
                }

                chk_loadMiniMapProps.Enabled =
                    btn_savepic.Enabled =
                    btn_scrD.Enabled =
                    btn_scrU.Enabled =
                    btn_scrR.Enabled =
                    btn_scrL.Enabled =
                    btn_zoomin.Enabled =
                    btn_zoomout.Enabled =
                    btn_loadStrs.Enabled =
                    chk_nfa.Enabled =
                    chk_nfc.Enabled =
                    chk_nfe.Enabled =
                    chk_nfs.Enabled =
                    chk_qpf.Enabled =
                    chk_NFMV.Enabled =
                    chk_jpg.Enabled =
                    workcheck;

                if (this.error)
                    lbl_status.Text = string.Format("{0},{1}", statustext, Map.statustext);
                else
                    this.lbl_status.Text = Map.statustext;

                if (Map.check)
                {
                    this.dg_StructNFA.DataSource = Map.getStructNFA();
                    setRowNumbers(dg_StructNFA);
                    this.dg_StructNFA.Refresh();
                    this.dg_nfmprop.DataSource = Map.getNFMResProp();
                    setRowNumbers(dg_nfmprop);
                    this.dg_nfmprop.Refresh();
                    this.dg_nfmterrain.DataSource = Map.getNFMResTerrain();
                    setRowNumbers(dg_nfmterrain);
                    this.dg_nfmterrain.Refresh();
                    this.dg_StructNFC.DataSource = Map.getStructNFC();
                    setRowNumbers(dg_StructNFC);
                    this.dg_StructNFC.Refresh();
                    this.dg_StructNFE.DataSource = Map.getStructNFE();
                    setRowNumbers(dg_StructNFE);
                    this.dg_StructNFE.EndEdit();
                    this.dg_StructNFE.Refresh();
                    this.dg_nfsres.DataSource = Map.getNFSRes();
                    setRowNumbers(dg_nfsres);
                    this.dg_nfsres.Refresh();
                    this.dg_nfmvectors.DataSource = Map.getNFMResVector();
                    setRowNumbers(dg_nfmvectors);
                    this.dg_nfmvectors.Refresh();
                    this.dg_npcres.DataSource = Map.getNPCRes();
                    setRowNumbers(dg_npcres);
                    this.dg_npcres.Refresh();
                    this.dg_qpfres.DataSource = Map.getQPFRes();
                    setRowNumbers(dg_qpfres);
                    this.dg_qpfres.Refresh();
                    this.dg_nfmspeedgrass.DataSource = Map.getNFMGrassRes();
                    setRowNumbers(dg_nfmspeedgrass);
                    this.dg_nfmspeedgrass.Refresh();
                    this.dg_nfmevent.DataSource = Map.getNFMEventRes();
                    setRowNumbers(dg_nfmevent);
                    this.dg_nfmevent.Refresh();
                    this.chk_nfs.Checked = Map.loadnfs;
                    this.chk_nfa.Checked = Map.loadnfa;
                    this.chk_nfc.Checked = Map.loadnfc;
                    this.chk_nfe.Checked = Map.loadnfe;
                    this.chk_qpf.Checked = Map.loadqpf;
                    this.chk_jpg.Checked = Map.loadjpg;

                }
                this.partx = Map.ingamemappartx;
                this.party = Map.ingamemapparty;
                if (Map.check)
                {
                    if (Map.nfmfilled())
                    {
                        this.tb_nfm.Parent = tc_ctrlpanel;

                    }
                    if (Map.nfafilled())
                    {
                        this.tb_nfa.Parent = tc_ctrlpanel;
                        if (Map.NFA.edit)
                        {
                            this.tb_nfa.Text = "NFA*";
                            this.btn_savenfa.Enabled = true;
                        }
                        else
                            this.tb_nfa.Text = "NFA";
                    }
                    else
                        this.tb_nfa.Parent = tc_ctrlpanel;

                    if (Map.nfcfilled())
                    {
                        this.tb_nfc.Parent = tc_ctrlpanel;
                        try
                        {
                            this.dg_StructNFC.Columns[11].ReadOnly = true;
                            this.dg_StructNFC.Columns[0].ReadOnly = true;
                            this.dg_StructNFC.Columns[1].ReadOnly = true;
                            this.dg_StructNFC.Columns[2].ReadOnly = true;
                            this.dg_StructNFC.Columns[8].ReadOnly = true;
                            this.dg_StructNFC.Columns[10].ReadOnly = true;
                        }
                        catch
                        {
                        }
                        if (Map.NFC.edit)
                        {
                            this.tb_nfc.Text = "NFC*";
                            this.btn_savenfc.Enabled = true;
                        }
                        else
                            this.tb_nfc.Text = "NFC";
                    }
                    else
                        this.tb_nfc.Parent = tc_ctrlpanel;

                    if (Map.nfefilled())
                    {
                        this.tb_nfe.Parent = tc_ctrlpanel;
                        try
                        {
                            this.dg_StructNFE.Columns[2].ReadOnly = true;
                            this.dg_StructNFE.Columns[3].ReadOnly = true;
                        }
                        catch
                        {
                        }
                        if (Map.NFE.edit)
                        {
                            this.tb_nfe.Text = "NFE*";
                            this.btn_savenfe.Enabled = true;
                        }
                        else
                            this.tb_nfe.Text = "NFE";
                    }
                    else
                        this.tb_nfe.Parent = tc_ctrlpanel;

                    if (Map.nfsfilled())
                    {
                        this.tb_nfs.Parent = tc_ctrlpanel;
                        this.dg_nfsres.Columns[0].ReadOnly = true;
                        this.dg_nfsres.Columns[3].ReadOnly = true;
                        this.dg_nfsres.Columns[4].ReadOnly = true;
                        this.dg_nfsres.Columns[5].ReadOnly = true;
                        this.dg_nfsres.Columns[6].ReadOnly = true;

                        if (Map.NFS.editNFS)
                        {
                            this.tb_nfs.Text = "NFS*";
                            this.btn_savenfs.Enabled = true;
                        }
                        else
                            this.tb_nfs.Text = "NFS";
                    }
                    else
                        this.tb_nfs.Parent = tc_ctrlpanel;

                    if (Map.npcfilled())
                    {
                        this.tb_npc.Parent = tc_ctrlpanel;
                        try
                        {
                            this.dg_npcres.Columns[3].ReadOnly = true;
                            this.dg_npcres.Columns[0].ReadOnly = true;
                            this.dg_npcres.Columns[1].ReadOnly = true;
                            this.dg_npcres.Columns[4].ReadOnly = true;
                        }
                        catch
                        {
                        }
                        if (Map.NFS.editNPC)
                        {
                            this.tb_npc.Text = "NPC*";
                            this.btn_savenpc.Enabled = true;
                        }
                        else
                            this.tb_npc.Text = "NPC";
                    }
                    else
                        this.tb_npc.Parent = tc_ctrlpanel;


                    if (Map.qpffilled())
                    {
                        this.tb_qpf.Parent = tc_ctrlpanel;
                        try
                        {
                            this.dg_qpfres.Columns[3].ReadOnly = true;
                            this.dg_qpfres.Columns[2].ReadOnly = true;
                            this.dg_qpfres.Columns[1].ReadOnly = true;
                        }
                        catch
                        {
                        }
                        if (Map.QPF.edit)
                        {
                            this.tb_qpf.Text = "QPF*";
                            this.btn_saveqpf.Enabled = true;
                        }
                        else
                            this.tb_qpf.Text = "QPF";
                    }
                    else
                        this.tb_qpf.Parent = tc_ctrlpanel;
                }
            }
        }

        private void pb_Minimap_MouseDown(object sender, MouseEventArgs e)
        {
            Map.setMMPos(new Point(e.X, e.Y)); refresh();
            IsMouseDown = true;
        }

        private void pb_Minimap_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown) { Map.setMMPos(new Point(e.X, e.Y)); refresh(); }
        }   

        private void pb_Minimap_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void btn_scrU_Click(object sender, EventArgs e)
        {
            Map.scrY(2);
            refresh();
        }

        private void btn_scrD_Click(object sender, EventArgs e)
        {
            Map.scrY(1);
            refresh();
        }

        private void btn_scrR_Click(object sender, EventArgs e)
        {
            Map.scrX(1);
            refresh();
        }

        private void btn_scrL_Click(object sender, EventArgs e)
        {
            Map.scrX(2);
            refresh();
        }

        private void pb_map_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.workcheck)
            {
                Point ig = Map.getIngameCoords(new Point(e.X,e.Y));
                this.lbl_ingamex.Text = ig.X.ToString();
                this.lbl_ingamey.Text = ig.Y.ToString();
                Point mf = Map.getMapFileCoords(new Point(e.X, e.Y));
                this.lbl_mfcx.Text = mf.X.ToString();
                this.lbl_mfcy.Text = mf.Y.ToString();
                this.lbl_segment.Text = Map.getSegment(e.X, e.Y).ToString();
                this.lbl_segmentcoordsx.Text = Map.getSegmentCoords(e.X,e.Y,1).ToString();
                this.lbl_segmentcoordsy.Text = Map.getSegmentCoords(e.X, e.Y, 2).ToString();
                if (this.mousedown)
                {
                    this.dragex = e.X;
                    this.dragey = e.Y;
                    Map.dragMap(dragsx - dragex, dragsy - dragey);
                    refresh();
                }
            }
        }

        public void replaceNFMVector(VectorData[] data)
        {
            if (this.nfmhelperindex>0)
            {
                this.Map.NFM.replaceVector(this.nfmhelperindex,data);
                this.nfmhelperindex = -1;
            }
            
        }

        public void replaceNFMProp(PROPS_TABLE_STRUCTURE[] data)
        {
            if (this.nfmhelperindex > 0)
            {
                this.Map.NFM.replaceProp(this.nfmhelperindex, data);
                this.nfmhelperindex = -1;
            }
        }

        public void replaceNFMTerrain(NFM_VERTEXSTRUCT_V11[] data)
        {
            if (this.nfmhelperindex > 0)
            {
                this.Map.NFM.replaceTerrain (this.nfmhelperindex, data);
                this.nfmhelperindex = -1;
            }
        }

        public void replaceNFMGrass(PointF[] data)
        {
            if (this.nfmhelperindex > 0)
            {
                this.Map.NFM.replaceGrass(this.nfmhelperindex, data);
                this.nfmhelperindex = -1;
            }
        }

        private void chk_loadMiniMapProps_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_movemap_Click(object sender, EventArgs e)
        {
            try
            {
                int tmpx = Convert.ToInt32(this.txt_mapcalcx.Text);
                int tmpy = Convert.ToInt32(this.txt_mapcalcy.Text);
                /*int pointx = (int)((tmpx - (partx * 16128)) / (float)5.25);
                int pointy = 3072 - (int)((tmpy - (partx * 16128)) / (float)5.25);*/
                int pointx = (int)((Convert.ToInt32(this.txt_mapcalcx.Text) - (this.partx * 16128)) / (float)5.25);
                int pointy = 3072 - ((int)((Convert.ToInt32(this.txt_mapcalcy.Text) - (this.party * 16128)) / (float)5.25));
                
                string tmpname;
                tmpx = Convert.ToInt32((tmpx - (tmpx % (5.25 * 3072))) / (5.25 * 3072));
                tmpy = Convert.ToInt32((tmpy - (tmpy % (5.25 * 3072))) / (5.25 * 3072));
                if (tmpx>9) 
                {
                  tmpname = "m0"+tmpx;  
                }else{
                  tmpname = "m00"+tmpx;    
                }
                if (tmpy>9) 
                {
                  tmpname = tmpname+"_0"+tmpy;  
                }else{
                  tmpname = tmpname+"_00"+tmpy;    
                }
                this.lbl_mapcalcresult.Text = tmpname;
                if (workcheck)
                    loadnewMap(file.workingdir + tmpname + ".nfa");
                this.drawpoint = true;
                this.drawpointx = pointx;
                this.drawpointy = pointy;
                this.mapList.Text = tmpname;
            }
            catch
            {
                this.lbl_status.Text = "Error check the coordinates!";
            }
        }

        private void mapList_indexChanged(object sender, EventArgs e)
        {
            if (Map.IsEditing)
            {
                DialogResult result = MessageBox.Show("Do you want to save your changes?", "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) saveall();
            }

            loadnewMap(file.getfilepath(this.mapList.SelectedIndex));
            this.mapList.Enabled = false;
            this.btn_MapMove.Enabled = false;
            this.mapList.Focus();
        }

        private void saveall()
        {
            Map.NFA.saveNFA(this.chk_hashexp.Checked);
            Map.NFC.saveNFC(this.chk_hashexp.Checked);
            Map.NFE.saveNFE(this.chk_hashexp.Checked);
            Map.NFS.saveNFS(this.chk_hashexp.Checked);
            Map.QPF.saveQPF(this.chk_hashexp.Checked);
            Map.NFS.saveNPC(this.chk_hashexp.Checked);
            refresh();
        }

        private void loadnewMap(string file)
        {
            if (File.Exists(file))
            {
                this.lbl_status.Text = "Loading File: " + file;
                bw_mapload.RunWorkerAsync(file);
            }
            else
            {
                this.lbl_status.Text = file + " not found";
            }
        }

        private void bw_mapload_DoWork(object sender, DoWorkEventArgs e)
        {
            string tmpfile = e.Argument.ToString();
            this.Map.LoadNewMap(tmpfile);
        }

        private void bw_mapload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.Map.error == false)
                workcheck = true;
            if (this.drawpoint)
            {
                Map.drawPoint(this.drawpointx, this.drawpointy, 6);
                Map.setCalcSelection(this.drawpointx, this.drawpointy);
                this.drawpoint = false;
                this.drawpointx = 0;
                this.drawpointy = 0;
                
            }
            refresh();

            this.mapList.Enabled = true;
            this.btn_MapMove.Enabled = true;
            
            
        }

        private void chk_nfa_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadnfa(chk_nfa.Checked);
            refresh();
        }

        private void chk_nfc_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadnfc(chk_nfc.Checked);
            refresh();
        }

        private void chk_NFE_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadnfe(chk_nfe.Checked);
            refresh();
        }

        private void chk_nfs_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadnfs(chk_nfs.Checked);
            refresh();
        }

        private void chk_QPF_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadqpf(chk_qpf.Checked);
            refresh();
        }

        private void chk_jpg_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadjpg(chk_jpg.Checked);
            refresh();
        }

        private void pb_map_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.workcheck)
            {
                this.mousedown = true;
                this.dragsx = e.X;
                this.dragsy = e.Y;
            }            
        }

        private void pb_map_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.workcheck)
            {
                this.mousedown = false;
                this.dragex = e.X;
                this.dragey = e.Y;
                Map.dragMap(dragsx - dragex, dragsy - dragey);
                refresh();
            }
        }

        private void pb_map_MouseLeave(object sender, EventArgs e)
        {
            this.mousedown = false;
        }

        private void btn_loadStrs_Click(object sender, EventArgs e)
        {
            this.openStrings.ShowDialog();
        }

        private void openStrings_FileOk(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.stringDir = this.openStrings.FileName;
            if (workcheck)
                this.Map.stringload(Properties.Settings.Default.stringDir);
        }

        private void dg_qpfres_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delqpf.Enabled = true;
            if (!this.chworkblocker)
            {
                int intid = e.RowIndex;
                Map.setQPFSelection(intid);
                refresh();
            }
        }

        private void dg_nfsres_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delnfs.Enabled = true;
            if (!this.chworkblocker)
            {
                int intid = e.RowIndex;
                Map.setNFSSelection(intid);
                refresh();
            }
        }

        private void dg_npcres_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delnpc.Enabled = true;
            if (!this.chworkblocker)
            {
                int intid = e.RowIndex;
                Map.setNPCSelection(intid);
                refresh();
            }
        }

        private void dg_StructNFE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.chworkblocker)
            {
                this.btn_delnfe.Enabled = true;
                int intid = e.RowIndex;
                Map.setNFESelection(intid);
                refresh();
            }
        }

        private void dg_StructNFC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delnfc.Enabled = true;
            if (!this.chworkblocker)
            {
                int intid = e.RowIndex;
                Map.setNFCSelection(intid);
                refresh();
            }

        }

        private void btn_sQPF_Click(object sender, EventArgs e)
        {
            this.QPFsetter = true;
            MessageBox.Show("Please click on the desired position on the Mainmap.", "Add a Fieldprop");
            workblocker(true);
        }

        private void pb_map_Click(object sender, EventArgs e)
        {
            if (this.workcheck)
            {
                int tmp_x = (int)((Convert.ToInt32(this.lbl_ingamex.Text) - (partx * 16128)) / (float)5.25);
                int tmp_y = 3072 - ((int)((Convert.ToInt32(this.lbl_ingamey.Text) - (party * 16128)) / (float)5.25));
                int x = Convert.ToInt32(this.lbl_ingamex.Text);
                int y = Convert.ToInt32(this.lbl_ingamey.Text);
                if (this.QPFsetter)
                {
                    this.QPFsetter = false;
                    QPFSetter setter = new QPFSetter(this, x, y);
                    setter.Show();
                    Map.drawPoint(tmp_x, tmp_y, 5);
                    refresh();
                }
                if (this.NPCsetter)
                {
                    this.NPCsetter = false;
                    NPCSetter setter = new NPCSetter(this,x, y, Map.NFS.cnt_npcinit + 1);
                    setter.Show();
                    Map.drawPoint(tmp_x, tmp_y, 6);
                    refresh();
                }
                if (this.NFSsetter)
                {
                    nfstype++;
                    if (nfstype == 1)
                    {
                        NFSwindow.setsquare(nfstype, y, tmp_x, tmp_y);
                        NFSwindow.updateinfo("Click Bottom-Right Corner of area.");
                        NFSwindow.Focus();
                    }
                    if (nfstype == 2)
                    {
                        NFSwindow.setsquare(nfstype, y, tmp_x, tmp_y);
                        NFSwindow.updateinfo("Click Bottom-Left Corner of area.");
                        NFSwindow.Focus();

                    }
                    if (nfstype == 3)
                    {
                        NFSwindow.setsquare(nfstype, x, tmp_x, tmp_y);
                        NFSwindow.updateinfo("Click Top-Right Corner of area.");
                        NFSwindow.Focus();

                    }
                    if (nfstype == 4)
                    {
                        NFSwindow.setsquare(nfstype, x, tmp_x, tmp_y);
                        NFSwindow.updateinfo("Region generated. Please\n fill in Count and Script!");
                        NFSwindow.Focus();
                        this.NFSsetter = false;
                    }


                }
                if (this.NFEsetter)
                {
                    this.NFEwindow.addcoord(tmp_x, tmp_y, x, y);
                    this.NFEwindow.Focus();
                }
                if (this.NFCsetter)
                {
                    this.NFCwindow.addcoord(tmp_x, tmp_y, x, y);
                    this.NFCwindow.Focus();
                }
                if (this.NFAsetter)
                {
                    this.NFAwindow.addcoord(tmp_x, tmp_y, x, y);
                    this.NFAwindow.Focus();
                }
                if (this.NFMPROPsetter)
                {
                    this.NFMWindow.addcoord(x, y);
                    this.NFMWindow.Focus();
                }
                if (this.TerrainSetter)
                {
                    this.TerrainWindow.addSeg(Convert.ToInt32(this.lbl_segment.Text));
                    this.TerrainWindow.Focus();
                }
                if (this.TextureSetter)
                {
                    this.TextureWindow.addSeg(Convert.ToInt32(this.lbl_segment.Text));
                    this.TextureWindow.Focus();
                }
                if (this.COORDgetter)
                {
                    this.Coordwindow.addcoord(tmp_x, tmp_y, x, y);
                    this.Coordwindow.Focus();
                }
                if (this.COORDget)
                {
                    this.COORDget = false;
                    if (this.COORDgettype < 5)
                        Map.NFS.updateNFSCoord(COORDgettype, x, y, COORDgetid);
                    if (this.COORDgettype == 6)
                        Map.NFS.updateNPCCoord(x, y, COORDgetid);
                    if (this.COORDgettype == 5)
                        Map.QPF.updateQPFCoord(x, y, COORDgetid);
                    this.COORDgetid = 0;
                    this.COORDgettype = 0;
                    workblocker(false);
                    Map.RefreshMap_image();
                    refresh();
                }

                if (this.COORDediter)
                {
                    this.Coordwindow.editcoord(tmp_x, tmp_y, Convert.ToInt32(this.lbl_ingamex.Text), Convert.ToInt32(this.lbl_ingamey.Text));
                    this.Coordwindow.Focus();
                    this.COORDediter = false;
                }
                if (this.nfcCOORDediter)
                {
                    this.NFCwindow.editcoord(tmp_x, tmp_y, Convert.ToInt32(this.lbl_ingamex.Text), Convert.ToInt32(this.lbl_ingamey.Text));
                    this.NFCwindow.Focus();
                    this.nfcCOORDediter = false;
                    this.NFCsetter = true;
                }
                if (this.nfaCOORDediter)
                {
                    this.NFAwindow.editcoord(tmp_x, tmp_y, Convert.ToInt32(this.lbl_ingamex.Text), Convert.ToInt32(this.lbl_ingamey.Text));
                    this.NFAwindow.Focus();
                    this.nfaCOORDediter = false;
                    this.NFAsetter = true;
                }
                if (this.nfeCOORDediter)
                {
                    this.NFEwindow.editcoord(tmp_x, tmp_y, Convert.ToInt32(this.lbl_ingamex.Text), Convert.ToInt32(this.lbl_ingamey.Text));
                    this.NFEwindow.Focus();
                    this.nfeCOORDediter = false;
                    this.NFEsetter = true;
                }
            }
        }

        public void updateQPF(StructQPF res)
        {
            /*res.x = (float)(res.x - (partx * 16128))/(float)5.25;
            res.y = 3072-((float)(res.y - (party * 16128))/(float)5.25);*/
            this.Map.QPF.updateQPFRes(res);
            Map.RefreshMap_image();
            this.refresh();
            workblocker(false);
        }

        public void updateNFS(NFS_MONSTER_LOCATION res)
        {
            /*res.top = Convert.ToInt32(3072-((float)(res.top - (party * 16128)) / (float)5.25));
            res.bottom = Convert.ToInt32(3072 - ((float)(res.bottom - (party * 16128)) / (float)5.25));
            res.left = Convert.ToInt32((float)(res.left - (partx * 16128)) / (float)5.25);
            res.right = Convert.ToInt32((float)(res.right - (partx * 16128)) / (float)5.25);*/
            this.Map.NFS.updateNFSRes(res);
            Map.RefreshMap_image();
            this.refresh();
            workblocker(false);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.NFSsetter = true;
            this.NFSwindow = new NFSSetter(this, "Please click on the desired top\n position on the main map.",Map.NFS.cnt);
            this.NFSwindow.Show();
            this.nfstype = 0;
            workblocker(true);
        }

        private void btn_setnpc_Click(object sender, EventArgs e)
        {
            this.NPCsetter = true;
            MessageBox.Show("Please click on the desired position on the Mainmap.","Add a NPC");
            workblocker(true);
        }

        private void btn_setnfe_Click(object sender, EventArgs e)
        {
            this.NFEsetter = true;
            this.NFEwindow = new NFESetter(this, "Select the Coordinates from the mainmap.", this.partx, this.party);
            this.NFEwindow.Show();
            workblocker(true);
        }

        public void updateNFE(StructNFE res)
        {
            this.Map.NFE.updateStructNFE(res);
            Map.RefreshMap_image();
            this.refresh();
            this.NFEsetter = false;
            workblocker(false);
        }

        private void btn_setnfc_Click(object sender, EventArgs e)
        {
            this.NFCsetter = true;
            this.NFCwindow = new NFCSetter(this, "Select the Coordinates from the mainmap.",Map.NFC.cnt+1, this.partx, this.party);
            this.NFCwindow.Show();
            workblocker(true);
        }

        public void setcoordediter()
        {
            this.COORDediter = true;
            this.COORDgetter = false;
        }

        public void nfccoordediter()
        {
            this.nfcCOORDediter = true;
            this.NFCsetter = false;
        }

        public void nfacoordediter()
        {
            this.nfaCOORDediter = true;
            this.NFAsetter = false;
        }

        public void nfecoordediter()
        {
            this.nfeCOORDediter = true;
            this.NFEsetter = false;
        }

        public void setcoordsetter()
        {
            this.COORDediter = false;
            this.COORDgetter = true;
        }

        public void releasecoordsetter()
        {
            this.COORDediter = false;
            this.COORDgetter = false;
        }

        public void updateNFC(StructNFC res)
        {
            this.Map.NFC.updateStructNFC(res);
            Map.RefreshMap_image();
            this.refresh();
            this.NFCsetter = false;
            workblocker(false);
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            this.NFAsetter = true;
            this.NFAwindow = new NFASetter(this, "Select the Coordinates from the mainmap.", Map.NFA.cnt + 1, this.partx, this.party);
            this.NFAwindow.Show();
            workblocker(true);
        }

        public void updateNFA(StructNFA res)
        {
            this.Map.NFA.updateStructNFA(res);
            Map.RefreshMap_image();
            this.refresh();
            this.NFAsetter = false;
            workblocker(false);
        }

        public void updateNPC(StructNFSNPC res)
        {
            /*res.x = Convert.ToInt32((res.x - (partx * 16128)) / (float)5.25);
            res.y = 3072 - (Convert.ToInt32((res.y - (party * 16128)) / (float)5.25));*/
            this.Map.NFS.updateNPCRes(res);
            Map.RefreshMap_image();
            this.refresh();
            workblocker(false);
        }

        private void workblocker(bool check)
        {
            if (check)
            {
                this.btn_loadStrs.Enabled = false;
                this.btn_setnfa.Enabled = false;
                this.btn_setnfc.Enabled = false;
                this.btn_setnfs.Enabled = false;
                this.btn_setnfe.Enabled = false;
                this.btn_setnpc.Enabled = false;
                this.btn_sQPF.Enabled = false;

                this.ctx_map.Enabled = false;
                this.btn_safenfm.Enabled = false;
                this.mapList.Enabled = false;
                this.btn_MapMove.Enabled = false;
                this.chk_jpg.Enabled = false;
                this.chk_nfa.Enabled = false;
                this.chk_nfc.Enabled = false;
                this.chk_nfs.Enabled = false;
                this.chk_nfe.Enabled = false;
                this.chk_qpf.Enabled = false;
                this.btn_nfaselect.Enabled = false;
                this.btn_delete.Enabled = false;
                this.btn_delnfc.Enabled = false;
                this.btn_delnfe.Enabled = false;
                this.btn_delnfs.Enabled = false;
                this.btn_delnpc.Enabled = false;
                this.btn_delqpf.Enabled = false;
                this.btn_savenfa.Enabled = false;
                this.btn_savenfc.Enabled = false;
                this.btn_savenfe.Enabled = false;
                this.btn_savenfs.Enabled = false;
                this.btn_saveqpf.Enabled = false;
                this.btn_savenpc.Enabled = false;
                chworkblocker = true;
            }
            else
            {

                this.ctx_map.Enabled = true;
                this.btn_safenfm.Enabled = true;
                this.btn_loadStrs.Enabled = true;
                this.btn_setnfa.Enabled = true;
                this.btn_setnfc.Enabled = true;
                this.btn_setnfs.Enabled = true;
                this.btn_setnfe.Enabled = true;
                this.btn_setnpc.Enabled = true;
                this.btn_sQPF.Enabled = true;
                this.mapList.Enabled = true;
                this.btn_MapMove.Enabled = true;
                this.chk_jpg.Enabled = true;
                this.chk_nfa.Enabled = true;
                this.chk_nfc.Enabled = true;
                this.chk_nfs.Enabled = true;
                this.chk_nfe.Enabled = true;
                this.chk_qpf.Enabled = true;
                /*this.btn_delete.Enabled = true;
                this.btn_delnfc.Enabled = true;
                this.btn_delnfe.Enabled = true;
                this.btn_delnfs.Enabled = true;
                this.btn_delnpc.Enabled = true;
                this.btn_delqpf.Enabled = true;
                this.btn_savenfa.Enabled = true;
                this.btn_savenfc.Enabled = true;
                this.btn_savenfe.Enabled = true;
                this.btn_savenfs.Enabled = true;
                this.btn_saveqpf.Enabled = true;
                this.btn_savenpc.Enabled = true;*/
                this.btn_nfaselect.Enabled = true;
                chworkblocker = false;
            }

            

        }

        public void releaseWorkblock()
        {
            workblocker(false);
            COORDgetter = false;
            COORDediter = false;
            NFAsetter = false;
            NFEsetter = false;
            NFCsetter = false;
            NFSsetter = false;
            NPCsetter = false;
            QPFsetter = false;
            TextureSetter = false;
            nfcCOORDediter = false;
            NFMPROPsetter = false;
            TerrainSetter = false;
            Map.refreshMapBMP();
            refresh();
        }

        private void pb_map_DoubleClick(object sender, EventArgs e)
        {
            if (!chworkblocker)
            {
                string gmcmd = "/run warp(" + this.lbl_ingamex.Text + "," + this.lbl_ingamey.Text + ")";
                try
                {
                    Clipboard.SetDataObject(gmcmd, true);
                    this.statustext = "";
                    this.error = false;
                }
                catch
                {
                    this.statustext = "Unable to copy GM-Command to clipboard";
                    this.error = true;
                }
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dg_StructNFA.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show("Do you want to delete Region id:" + id + "?", "Delete Region", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Map.NFA.deleteNFA(id);
                Map.RefreshMap_image();
                refresh();
            }
        }

        private void btn_delnfc_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dg_StructNFC.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show("Do you want to delete Worldlocation id:" + id + "?", "Delete Worldlocation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Map.NFC.deleteNFC(id);
                Map.RefreshMap_image();
                refresh();
            }
        }

        private void btn_delnfs_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dg_nfsres.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show("Do you want to delete Mobspawn id:" + id + "?", "Delete Mobspawn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Map.NFS.deleteNFS(id);
                Map.RefreshMap_image();
                refresh();
            }
        }

        private void btn_delnpc_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dg_npcres.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show("Do you want to delete NPC id:" + id + "?", "Delete NPC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Map.NFS.deleteNPC(id);
                Map.RefreshMap_image();
                refresh();
            }
        }

        private void btn_delnfe_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dg_StructNFE.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show("Do you want to delete Event Area id:" + id + "?", "Delete NPC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Map.NFE.deleteNFE(id);
                Map.RefreshMap_image();
                refresh();
            }
        }

        private void btn_delqpf_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dg_qpfres.CurrentRow.Cells[0].Value);
            int index = this.dg_qpfres.CurrentRow.Index;
            DialogResult result = MessageBox.Show("Do you want to delete Fieldprop id:" + id + "?", "Delete NPC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Map.QPF.deleteQPF(id, index);
                Map.RefreshMap_image();
                refresh();
            }
        }

        private void btn_saveqpf_Click(object sender, EventArgs e)
        {
            Map.QPF.saveQPF(this.chk_hashexp.Checked);
            refresh();
        }

        private void btn_savenfa_Click(object sender, EventArgs e)
        {
            Map.NFA.saveNFA(this.chk_hashexp.Checked);
            refresh();
        }

        private void btn_savenfc_Click(object sender, EventArgs e)
        {
            Map.NFC.saveNFC(this.chk_hashexp.Checked);
            refresh();
        }

        private void btn_savenfe_Click(object sender, EventArgs e)
        {
            Map.NFE.saveNFE(this.chk_hashexp.Checked);
            Map.RefreshMap_image();
            refresh();
        }

        private void btn_savenfs_Click(object sender, EventArgs e)
        {
            Map.NFS.saveNFS(this.chk_hashexp.Checked);
            refresh();
        }

        private void btn_savenpc_Click(object sender, EventArgs e)
        {
            Map.NFS.saveNPC(this.chk_hashexp.Checked);
            refresh();
        }

        private void dg_StructNFC_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Map.NFC.edit = true;
            refresh();
        }

        private void dg_StructNFE_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Map.NFE.edit = true;
            refresh();
        }

        private void dg_nfsres_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Map.NFS.editNFS = true;
            refresh();
        }

        private void dg_qpfres_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Map.QPF.edit = true;
            refresh();
        }

        private void dg_npcres_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Map.NFS.editNPC = true;
            refresh();
        }

        private void dg_StructNFA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (this.dg_StructNFA.CurrentCell.ColumnIndex == 2 && !this.chworkblocker)
            {
                int id = Convert.ToInt32(this.dg_StructNFA.CurrentRow.Cells[0].Value);
                Map.NFA.edit = true;
                PointF[] coords = Map.NFA.data[e.RowIndex].points;
                this.Coordwindow = new CoordGetter(this, "Select the Coordinates from the mainmap \nto \"Add\" coordinates or \"Edit\"\nthe values of nfa ID:"+id+".",id,1,coords,partx,party);
                this.Coordwindow.Show();
                workblocker(true);
                refresh();
            }
        }

        public void modifyCoords(int id, string coord, PointF[] coords,int type)
        {
            if (type == 1)
                Map.NFA.modifycoords(id, coord, coords);
            if (type == 3)
                Map.NFE.modifycoords(id, coord, coords);
            this.COORDgetter = false;
            Map.RefreshMap_image();
            refresh();
        }

        public void modifyCoords(int id, string coord, NFCCoords[] coords, int type)
        {
            if (type == 2)
                Map.NFC.modifycoords(id, coord, coords);
            this.COORDgetter = false;
            Map.RefreshMap_image();
            refresh();
        }

        private void dg_StructNFC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dg_StructNFC.CurrentCell.ColumnIndex == 11 && !this.chworkblocker)
            {
                int id = Convert.ToInt32(this.dg_StructNFC.CurrentRow.Cells[0].Value);
                Map.NFC.edit = true;
                NFCCoords[] coords = Map.NFC.data[e.RowIndex].coords;
                this.Coordwindow = new CoordGetter(this, "Select the Coordinates from the mainmap \nto \"Add\" coordinates or \"Edit\"\nthe values of nfc ID:" + id + ".", id, 2, coords, partx, party);
                this.Coordwindow.Show();
                workblocker(true);
                refresh();
            }

        }

        private void dg_StructNFE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dg_StructNFE.CurrentCell.ColumnIndex == 3 && !this.chworkblocker)
            {
                int id = Convert.ToInt32(this.dg_StructNFE.CurrentRow.Cells[0].Value);
                Map.NFE.edit = true;
                PointF[] coords = Map.NFE.data[e.RowIndex].coords;
                this.Coordwindow = new CoordGetter(this, "Select the Coordinates from the mainmap \nto \"Add\" coordinates or \"Edit\"\nthe values of nfe ID:" + id + ".", id, 3, coords, partx, party);
                this.Coordwindow.Show();
                workblocker(true);
                refresh();
            }
        }

        private void dg_StructNFA_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (!this.chworkblocker)
            {
                int intid = e.RowIndex;
                Map.setNFASelection(intid);
                refresh();
            }
        }

        public void drawCoords(List<PointF> coords, int type,int count_coords)
        {
            PointF[] tmpcoords = new PointF[count_coords];
            for (int i = 0; i < coords.Count; i++)
            {
                tmpcoords[i] = coords[i];
            }
            
            Map.drawLines(tmpcoords, type);
            Map.setCalcSelection(Convert.ToInt32(tmpcoords[0].X), Convert.ToInt32(tmpcoords[0].Y));
            refresh();
        }

        public void drawCoords(PointF[] coords, int type)
        {
            Map.drawLines(coords, type);
            refresh();
        }

        public void drawPoint(PointF[] coords, int type, int count)
        {
            Map.drawPoint(coords, type, count);
            refresh();
        }

        public void refreshMain()
        {
            Map.refreshMapBMP();
            refresh();
        }

        public void selectCoord(int x, int y,PointF[] coords, int type)
        {
            Map.setCoordSelection(x, y, coords,type);
            refresh();
        }

        private void dg_nfsres_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delnfs.Enabled = true;
            if (this.dg_nfsres.CurrentCell.ColumnIndex == 3 && !this.chworkblocker)
            {
                this.COORDgetid = e.RowIndex;
                DialogResult result = MessageBox.Show("Do you want to change Mob Spawn Position? Then select the Position on the Mainmap after clicking Yes.", "Modify a Mob Spawn Position?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Map.NFS.editNFS = true;
                    this.COORDget = true;
                    this.COORDgettype = 1;
                    workblocker(true);
                    refresh();
                }   
            }
            if (this.dg_nfsres.CurrentCell.ColumnIndex == 4 && !this.chworkblocker)
            {
                this.COORDgetid = e.RowIndex;
                DialogResult result = MessageBox.Show("Do you want to change Mob Spawn Position? Then select the Position on the Mainmap after clicking Yes.", "Modify a Mob Spawn Position?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Map.NFS.editNFS = true;
                    this.COORDget = true;
                    this.COORDgettype = 3;
                    workblocker(true);
                    refresh();
                }   
            }
            if (this.dg_nfsres.CurrentCell.ColumnIndex == 5 && !this.chworkblocker)
            {
                this.COORDgetid = e.RowIndex;
                DialogResult result = MessageBox.Show("Do you want to change Mob Spawn Position? Then select the Position on the Mainmap after clicking Yes.", "Modify a Mob Spawn Position?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Map.NFS.editNFS = true;
                    this.COORDget = true;
                    this.COORDgettype = 2;
                    workblocker(true);
                    refresh();
                }   
            }
            if (this.dg_nfsres.CurrentCell.ColumnIndex == 6 && !this.chworkblocker)
            {

                this.COORDgetid = e.RowIndex;
                DialogResult result = MessageBox.Show("Do you want to change Mob Spawn Position? Then select the Position on the Mainmap after clicking Yes.", "Modify a Mob Spawn Position?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Map.NFS.editNFS = true;
                    this.COORDget = true;
                    this.COORDgettype = 4;
                    workblocker(true);
                    refresh();
                }   
            }
        }

        private void dg_npcres_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delnpc.Enabled = true;
            if (this.dg_npcres.CurrentCell.ColumnIndex == 3 && !this.chworkblocker || this.dg_npcres.CurrentCell.ColumnIndex == 4 && !this.chworkblocker)
            {
                this.COORDgetid = e.RowIndex;
                DialogResult result = MessageBox.Show("Do you want to change the NPC Position? Then select the Position on the Mainmap after clicking Yes.", "Modify a NPC Position?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.COORDget = true;
                    this.COORDgettype = 6;
                    workblocker(true);
                    Map.NFS.editNPC = true;
                    refresh();
                }   
            }

        }

        private void dg_qpfres_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delqpf.Enabled = true;
            if (this.dg_qpfres.CurrentCell.ColumnIndex == 2 && !this.chworkblocker || this.dg_qpfres.CurrentCell.ColumnIndex == 3 && !this.chworkblocker)
            {
                this.COORDgetid = e.RowIndex;
                DialogResult result = MessageBox.Show("Do you want to change Fieldprop Position? Then select the Position on the Mainmap after clicking Yes.", "Modify a FieldProp Position?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Map.NFC.edit = true;
                    this.COORDget = true;
                    this.COORDgettype = 5;
                    workblocker(true);
                    Map.QPF.edit = true;
                    refresh();
                }                
            }
        }
      
        private void dg_StructNFC_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dg_StructNFC.CurrentCell.ColumnIndex == 3 || dg_StructNFC.CurrentCell.ColumnIndex == 4
                || dg_StructNFC.CurrentCell.ColumnIndex == 5 || dg_StructNFC.CurrentCell.ColumnIndex == 6
                || dg_StructNFC.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    int id = Convert.ToInt32(e.FormattedValue);
                }
                catch
                {
                    e.Cancel = true;
                }
            }
            if (dg_StructNFC.CurrentCell.ColumnIndex == 9)
            {
                Map.NFC.data[e.RowIndex].cnt_script = e.FormattedValue.ToString().Length;
                dg_StructNFC.Refresh();
            }
        }

        private void dg_StructNFE_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dg_StructNFE.CurrentCell.ColumnIndex == 0 || dg_StructNFE.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    int id = Convert.ToInt32(e.FormattedValue);
                }
                catch
                {
                    e.Cancel = true;
                }
            }
        }

        private void tc_ctrlpanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dg_nfsres_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dg_nfsres.CurrentCell.ColumnIndex == 7 || dg_nfsres.CurrentCell.ColumnIndex == 8
                || dg_nfsres.CurrentCell.ColumnIndex == 10 || dg_nfsres.CurrentCell.ColumnIndex == 11)
            {
                try
                {
                    int id = Convert.ToInt32(e.FormattedValue);
                }
                catch
                {
                    e.Cancel = true;
                }
            }
            if (dg_nfsres.CurrentCell.ColumnIndex == 1)
            {
                Map.NFS.data[e.RowIndex].ScriptLength = e.FormattedValue.ToString().Length;
                dg_nfsres.Refresh();
            }
            
        }

        private void dg_qpfres_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dg_qpfres.CurrentCell.ColumnIndex == 4 || dg_qpfres.CurrentCell.ColumnIndex == 5
                || dg_qpfres.CurrentCell.ColumnIndex == 6 || dg_qpfres.CurrentCell.ColumnIndex == 7
                || dg_qpfres.CurrentCell.ColumnIndex == 8 || dg_qpfres.CurrentCell.ColumnIndex == 9
                || dg_qpfres.CurrentCell.ColumnIndex == 10)
            {
                try
                {
                    decimal id = Convert.ToDecimal(e.FormattedValue);
                }
                catch
                {
                    e.Cancel = true;
                }
            }
            if (dg_qpfres.CurrentCell.ColumnIndex == 0 || dg_qpfres.CurrentCell.ColumnIndex == 11)
            {
                try
                {
                    int id = Convert.ToInt32(e.FormattedValue);
                }
                catch
                {
                    e.Cancel = true;
                }
            }
        }

        private void dg_npcres_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dg_npcres.CurrentCell.ColumnIndex == 2 || dg_npcres.CurrentCell.ColumnIndex == 5
                || dg_npcres.CurrentCell.ColumnIndex == 6 || dg_npcres.CurrentCell.ColumnIndex == 7
                || dg_npcres.CurrentCell.ColumnIndex == 10)
            {
                try
                {
                    int id = Convert.ToInt32(e.FormattedValue);
                }
                catch
                {
                    e.Cancel = true;
                }
            }
            if (dg_npcres.CurrentCell.ColumnIndex == 9)
            {
                Map.NFS.npcdata[e.RowIndex].cnt_initscript = e.FormattedValue.ToString().Length;
                dg_npcres.Refresh();
            }
            if (dg_npcres.CurrentCell.ColumnIndex == 12)
            {
                Map.NFS.npcdata[e.RowIndex].cnt_contact = e.FormattedValue.ToString().Length;
                dg_npcres.Refresh();
            }
        }
        
        public string getstring(string script)
        {
            return this.Map.NFC.getstring(script);
        }

        private void dg_StructNFA_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_delete.Enabled = true;
            if (!this.chworkblocker)
            {
                
                int intid = e.RowIndex;
                if(intid>-1)
                {
                    Map.setNFASelection(intid);
                    refresh();
                }
            }
        }

        private void btn_dump_Click(object sender, EventArgs e)
        {
            DumpClientResources();
        }

        private void bgw_filedump_DoWork(object sender, DoWorkEventArgs e)
        {
            /*Logic*/
        }

        private void bgw_filedump_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshresource(Properties.Settings.Default.dataDir);
        }

        private bool dmp_namecheck(string name)
        {
            if (name.Contains("m000") || name.Contains("m001") || name.Contains("m002") || name.Contains("m003") || name.Contains("m004") ||
                name.Contains("m005") || name.Contains("m006") || name.Contains("m007") || name.Contains("m008") || name.Contains("m009") ||
                name.Contains("m010") || name.Contains("m011") || name.Contains("m012") || name.Contains("m014") || name.Contains("m013") ||
                name.Contains("m015") || name.Contains("m016") || name.Contains("m017") || name.Contains("m018") || name.Contains("m019") ||
                name.Contains("db_string") || name.Contains("sframe") || name.Contains("terrain"))
            {
                if (name.Contains(".nfa") || name.Contains(".nfc") || name.Contains(".nfe") || name.Contains(".qpf") || name.Contains(".nfs") || name.Contains(".rdb") || name.Contains(".pdb") || name.Contains(".nfm") || name.Contains(".cfg"))
                {
                    return true;
                }
                else
                {
                    if (name.Contains(".jpg") && name.Contains("v256"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            else
            {
                return false;
            }

        }

        private void refreshresource(string path)
        {
            this.Map.stringload(path+"\\db_string.rdb");
            this.file = new FileIO(path);
            this.mapList.DataSource = file.filenames;
            this.workcheck = Map.check;
        }

        private void resourcecheck()
        {
            if (Properties.Settings.Default.dataDir == string.Empty)
            {
                Properties.Settings.Default.dataDir = Environment.CurrentDirectory + "\\res\\";
                Properties.Settings.Default.exportDir = Environment.CurrentDirectory + "\\hash\\";
                Properties.Settings.Default.saveDir = Environment.CurrentDirectory + "\\save\\";
                if (!Directory.Exists(Properties.Settings.Default.dataDir))
                    Directory.CreateDirectory(Properties.Settings.Default.dataDir);
                if (!Directory.Exists(Properties.Settings.Default.exportDir))
                    Directory.CreateDirectory(Properties.Settings.Default.exportDir);
                if (!Directory.Exists(Properties.Settings.Default.saveDir))
                    Directory.CreateDirectory(Properties.Settings.Default.saveDir);
            }
            if (Properties.Settings.Default.stringDir == string.Empty)
            {
                Properties.Settings.Default.stringDir = Environment.CurrentDirectory + "\\res\\db_string.rdb";
            }
        }

        /// <summary>
        /// TO-DO: Implement Data Burner Logic
        /// </summary>
        private void DumpClientResources()
        {
            /*Logic*/
        }

        private void RappelzMapEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void dg_nfm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int intid = e.RowIndex;
            //MessageBox.Show(intid.ToString());
            if (Map.NFM.propsegments[intid].props != null)
            {
                NFMHelper nfm = new NFMHelper(this,Map.NFM.propsegments[intid].props);
            }
        }

        private void dg_nfm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int intid = e.RowIndex;

            if (intid > -1)
            {
                try
                {
                    nfm.Close();
                }
                catch { }
                if (Map.NFM.propsegments[intid].props != null)
                {
                    nfm = new NFMHelper(this,Map.NFM.propsegments[intid].props);
                    this.nfmhelperindex = intid;
                    nfm.Show();
                }
            }
        }

        private DataGridView setRowNumbers(DataGridView data)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataGridViewRowHeaderCell cell = data.Rows[i].HeaderCell;
                cell.Value = (i + 1).ToString();
                data.Rows[i].HeaderCell = cell;                
            }
            return data;
        }

        private void dg_nfmterrain_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int intid = e.RowIndex;
            
            if (intid > -1)
            {
                try
                {
                    nfm.Close();
                }
                catch { }
                if (Map.NFM.terrain.segments[intid] != null)
                {
                nfm = new NFMHelper(this,Map.NFM.terrain.segments[intid].vertices);
                this.nfmhelperindex = intid;
                nfm.Show();
                }
            }

        }

        private void dg_nfmvectors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int intid = e.RowIndex;
   
            if (intid > -1)
            {
                try
                {
                    nfm.Close();
                }
                catch { }
                if (Map.NFM.vectordata[intid] != null)
                {
                    nfm = new NFMHelper(this,Map.NFM.vectordata[intid].data);
                    this.nfmhelperindex = intid;
                    nfm.Show();
                }
            }

        }

        private void chk_NFMV_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadnfmvect(this.chk_NFMV.Checked);
            refresh();
        }

        private void btn_safenfm_Click(object sender, EventArgs e)
        {
            if (this.Map.NFE.data != null&&this.chk_nfesync.Checked)
                this.Map.NFM.nfeSync(this.Map.NFE.data);
            if (this.Map.NFA.data != null && this.chk_nfasync.Checked)
                this.Map.NFM.nfaSync(this.Map.NFA.data);
            this.Map.NFM.saveNFMData();
        }

        private void tsm_segterrain_Click(object sender, EventArgs e)
        {
            try
            {

                this.tc_ctrlpanel.SelectedTab = tb_nfm;
                this.tc_nfm.SelectedTab = tb_terraindata;

                int intid = Convert.ToInt32(this.lbl_segment.Text);
                this.dg_nfmterrain.FirstDisplayedScrollingRowIndex = intid - 1;
                this.dg_nfmterrain.ClearSelection();
                this.dg_nfmterrain.Rows[intid].Selected = true;
                if (intid > -1)
                {
                    try
                    {
                        nfm.Close();
                    }
                    catch { }
                    if (Map.NFM.terrain != null)
                    {
                        nfm = new NFMHelper(this, Map.NFM.terrain.segments[intid].vertices);
                        this.nfmhelperindex = intid;
                        nfm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: "+ex.Message);
            }
        }

        private void tsm_segprop_Click(object sender, EventArgs e)
        {
            try
            {

                this.tc_ctrlpanel.SelectedTab = tb_nfm;
                this.tc_nfm.SelectedTab = tb_propdata;

                int intid = Convert.ToInt32(this.lbl_segment.Text);
                this.dg_nfmprop.FirstDisplayedScrollingRowIndex = intid - 1;
                this.dg_nfmprop.ClearSelection();
                this.dg_nfmprop.Rows[intid].Selected = true;
                if (intid > -1)
                {
                    try
                    {
                        nfm.Close();
                    }
                    catch { }
                    if (Map.NFM.propsegments != null)
                    {
                        if (Map.NFM.propsegments[intid]._prop_count > 0)
                        {
                            nfm = new NFMHelper(this, Map.NFM.propsegments[intid].props);
                            this.nfmhelperindex = intid;
                            nfm.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: " + ex.Message);
            }
        }

        private void dg_nfmspeedgrass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int intid = e.RowIndex;

            if (intid > -1)
            {
                try
                {
                    nfm.Close();
                }
                catch { }
                if (Map.NFM.grass != null)
                {
                    if (Map.NFM.grass[intid].coords != null)
                    {
                        nfm = new NFMHelper(this, Map.NFM.grass[intid].coords);
                        this.nfmhelperindex = intid;
                        nfm.Show();
                    }
                }
            }

        }

        private void chk_Grass_CheckedChanged(object sender, EventArgs e)
        {
            Map.setloadnfmgrass(this.chk_Grass.Checked);
            refresh();
        }

        private void btn_addProp_Click(object sender, EventArgs e)
        {
            this.NFMPROPsetter = true;
            this.NFMWindow = new ARME.NFMPropSetter(this, this.partx, this.party);
            this.NFMWindow.Show();
            this.workblocker(true);

        }

        public int[] getSegData(int x, int y)
        {
            int[] segdata = new int[3];
            segdata[0]=this.Map.getSegment(x,y);
            segdata[1]=this.Map.getSegmentCoords(x,y,1);
            segdata[2] = this.Map.getSegmentCoords(x, y, 2);
            return segdata;
        }

        public void addNFMProp(PROPS_TABLE_STRUCTURE tmp)
        {
            this.Map.NFM.addProp(tmp);
        }

        private void tsm_segeditheight_Click(object sender, EventArgs e)
        {
            this.TerrainSetter = true;
            this.TerrainWindow = new ARME.TerrainEditer(this);
            this.TerrainWindow.Show();
            this.workblocker(true);
        }

        public void editNFMTile(int seg, int tid, NFM_VERTEXSTRUCT_V11 tile, bool[] changeVals)
        {
            this.Map.NFM.editTile(seg, tid, tile,changeVals);
        }

        public void editNFMTexture(int seg, uint nVersion, uint t1, uint t2, uint t3, bool[] changeVals)
        {
            this.Map.NFM.updateTerrainTextureData(seg,nVersion,t1,t2,t3, changeVals);
        }

        private void tsm_segtextures_Click(object sender, EventArgs e)
        {
            this.TextureSetter = true;
            this.TextureWindow = new ARME.TextureEditer(this);
            this.TextureWindow.Show();
            this.workblocker(true);
        }

        public StructNFA[] getNFAData()
        {
            return this.Map.NFA.data;
        }
    }
}
