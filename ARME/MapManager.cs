using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;
using ARME.MapFileRes;
using ARME.Struct;

namespace ARME
{
    /// <summary>
    /// Provides interface for managing maps
    /// </summary>
    class MapManager
    {
        public bool check = false;
        private Graphics g;
        public string statustext;
        public float zoomfactor;

        #region Bitmaps

        private Bitmap MapImg = new Bitmap(3072, 3072);
        private Bitmap TmpMapImg = new Bitmap(3072, 3072);
        private Bitmap Preview = new Bitmap(256, 256);
        private Bitmap PreviewProp = new Bitmap(256, 256);

        #endregion 

        #region Map/Minomap X/Y

        private int minimx;
        private int minimy;
        private int mapx;
        private int mapy;
        public int ingamemappartx;
        public int ingamemapparty;

        #endregion 

        public bool error;
        public string loaded_parts;
        protected string nfapath;
        public string filename;
        protected string directory;

        #region .NFx Managers

        public nfa NFA;
        public nfc NFC;
        public nfe NFE;
        public nfs NFS;
        public qpf QPF;
        public nfm NFM;

        #endregion 
      
        #region .NFx Flags

        public bool loadnfa = true;
        public bool loadnfc = true;
        public bool loadnfe = true;
        public bool loadnfs = true;
        public bool loadqpf = true;
        public bool loadjpg = true;
        public bool loadnfmvect = false;
        public bool loadnfmgrass = true;

        #endregion

        protected MAPJPG JPG;
        private StringResource strings = null;
        private bool loadstrings = false;

        public MapManager()
        {
            this.check = false;       
        }

        /// <summary>
        /// Loads a new map from a given path
        /// </summary>
        /// <param name="path">Path to the desired map</param>
        public void LoadNewMap(string path)
        {
            this.filename = Path.GetFileNameWithoutExtension(path);
            this.directory = Path.GetDirectoryName(path) + "/";
            this.nfapath = path;
            this.ingamemappartx = Convert.ToInt32(this.filename.Substring(this.filename.IndexOf("m") + 1, 3));
            this.ingamemapparty = Convert.ToInt32(this.filename.Substring(this.filename.IndexOf("_") + 1, 3));
            AssembleMap();
        }

        /// <summary>
        /// Gets wether or not the user is currently editing a map
        /// </summary>
        public bool IsEditing
        {
            get
            {
                if (check)
                {
                    return this.NFA.edit == true || this.NFC.edit == true || this.NFS.editNFS == true || this.NFS.editNPC == true || this.QPF.edit == true;
                }

                return false;
            }
        }

        /// <summary>
        /// Assembles the map parts
        /// </summary>x
        private void AssembleMap()
        {
            this.zoomfactor = 6;
            this.minimx = 0;
            this.minimy = 0;
            this.mapx = 0;
            this.mapy = 0;
            this.error = false;
            this.statustext = "";
            this.loaded_parts = "";

            this.JPG = new MAPJPG(nfapath);
            this.NFA = new nfa(nfapath);
            this.NFC = new nfc(nfapath, this.strings);
            this.NFE = new nfe(nfapath);
            this.NFS = new nfs(nfapath, this.strings);
            this.QPF = new qpf(nfapath, this.strings);
            this.NFM = new nfm(nfapath);

            AssembleMap_image();
            CheckMapForErrors();
        }

        /// <summary>
        /// Assembles the map image
        /// </summary>
        private void AssembleMap_image()
        {
            if (loadjpg)
            {
                this.MapImg = JPG.MapImg;
                this.MapImg = maprotator(this.MapImg);
                this.Preview = new System.Drawing.Bitmap(this.MapImg, 256, 256);                
            }
            else
            {
                this.MapImg = AssembleMap_noImage(3072,3072);
                this.Preview = AssembleMap_noImage(256, 256);
            }

            if (loadnfa) this.MapImg = NFA.mergeMapImg(this.MapImg);
            if (loadnfc) this.MapImg = NFC.mergeMapImg(this.MapImg);
            if (loadnfe) this.MapImg = NFE.mergeMapImg(this.MapImg);
            if (loadnfs) this.MapImg = NFS.mergeMapImg(this.MapImg);
            if (loadqpf) this.MapImg = QPF.mergeMapImg(this.MapImg);
            if (loadnfmvect) this.MapImg = NFM.mergeMapImgVect(this.MapImg);
            if (loadnfmgrass) this.MapImg = NFM.mergeMapImgGrass(this.MapImg);
            this.TmpMapImg = new Bitmap(this.MapImg);
            this.PreviewProp = new System.Drawing.Bitmap(this.MapImg, 256, 256);
            this.check = true;
        }

        /// <summary>
        /// Assembles the map without an image (solid color background)
        /// </summary>
        /// <param name="width">Width of map</param>
        /// <param name="height">Height of map</param>
        /// <returns></returns>
        private Bitmap AssembleMap_noImage(int width, int height)
        {
            using (Bitmap emptypic = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(emptypic))
                {
                    g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);
                }
                return emptypic;
            }
        }

        /// <summary>
        /// Issues a call to AssembleMap_image to refresh the map image
        /// </summary>
        public void RefreshMap_image()
        {
            AssembleMap_image();
        }

        /// <summary>
        /// Checks the assembled map for errors
        /// </summary>
        private void CheckMapForErrors()
        {
            string tmperrortxt = "";

            if (NFA.check) this.loaded_parts += "nfa ";
            if (NFS.check == true) this.loaded_parts += "nfs ";
            if (NFE.check) this.loaded_parts += "nfe ";
            if (NFC.check) this.loaded_parts += "nfc ";
            if (QPF.check) this.loaded_parts += "qpf ";
            if (JPG.check) this.loaded_parts += "jpg ";

            if (NFA.error || NFC.error || NFE.error || NFM.error || NFS.error || QPF.error)
            {
                this.error = true;

                if (NFA.error) tmperrortxt += "NFA ";
                if (NFA.error) tmperrortxt += "NFC ";
                if (NFA.error) tmperrortxt += "NFE ";
                if (NFA.error) tmperrortxt += "NFM "; 
                if (NFA.error) tmperrortxt += "NFS ";
                if (NFA.error) tmperrortxt += "QPF "; 
            }

            if (this.error) this.statustext = string.Concat("Error loading:", tmperrortxt); else this.statustext = "No Errors";
        }

        public void setQPFSelection(int intid)
        {
            if (intid >= 0)
            {
                this.mapx = ((int)((this.QPF.data[intid].x-Hexcnv.GetCoords(this.QPF.filename,1))/5.25)) - (int)((3072 / this.zoomfactor) / 2);
                this.mapy = ((int)(3072 - ((this.QPF.data[intid].y - Hexcnv.GetCoords(this.QPF.filename, 2))) / 5.25)) - (int)((3072 / this.zoomfactor) / 2);
                this.minimx = (int)(this.mapx / 12);
                this.minimy = (int)(this.mapy / 12);
                this.MapImg = this.QPF.drawSelectedQPF(intid, this.TmpMapImg);
            }
        }

        public void setNFSSelection(int intid)
        {
            if (intid >= 0)
            {
                this.mapx = ((int)((this.NFS.data[intid].Left - Hexcnv.GetCoords(this.NFS.filename, 1)) / 5.25)) - (int)((3072 / this.zoomfactor) / 2);
                this.mapy = ((int)(3072 - ((this.NFS.data[intid].Bottom - Hexcnv.GetCoords(this.NFS.filename, 2))) / 5.25)) - (int)((3072 / this.zoomfactor) / 2);
                this.minimx = (int)(this.mapx / 12);
                this.minimy = (int)(this.mapy / 12);
                this.MapImg = this.NFS.drawSelectedNFS(intid, this.TmpMapImg);
            }
        }

        public void setNPCSelection(int intid)
        {
            if (intid >= 0)
            {
                this.mapx = ((int)((this.NFS.npcdata[intid].x - Hexcnv.GetCoords(this.NFS.filename, 1)) / 5.25)) - (int)((3072 / this.zoomfactor) / 2);
                this.mapy = ((int)(3072 - ((this.NFS.npcdata[intid].y - Hexcnv.GetCoords(this.NFS.filename, 2))) / 5.25)) - (int)((3072 / this.zoomfactor) / 2);
                this.minimx = (int)(this.mapx / 12);
                this.minimy = (int)(this.mapy / 12);
                this.MapImg = this.NFS.drawSelectedNPC(intid, this.TmpMapImg);
            }
        }

        public void setNFESelection(int intid)
        {
            if (intid >= 0)
            {
                this.mapx = (int)this.NFE.data[intid].coords[0].X - (int)((3072 / this.zoomfactor) / 2);
                this.mapy = (int)this.NFE.data[intid].coords[0].Y - (int)((3072 / this.zoomfactor) / 2);
                this.minimx = (int)(this.mapx / 12);
                this.minimy = (int)(this.mapy / 12);
                this.MapImg = this.NFE.drawSelectedNFE(intid, this.TmpMapImg);
            }
        }

        public void setNFASelection(int intid)
        {
            if (intid >= 0)
            {
                this.mapx = (int)this.NFA.data[intid].points[(int)(this.NFA.data[intid].points.Length / 2)].X - (int)((3072 / this.zoomfactor) / 2);
                this.mapy = (int)this.NFA.data[intid].points[(int)(this.NFA.data[intid].points.Length / 2)].Y - (int)((3072 / this.zoomfactor) / 2);
                this.minimx = (int)(this.mapx / 12);
                this.minimy = (int)(this.mapy / 12);
                this.MapImg = this.NFA.drawSelectedNFA(intid, this.TmpMapImg);
            }
        }

        public void setNFCSelection(int intid)
        {
            if (intid >= 0)
            {
                this.mapx = (int)this.NFC.data[intid].coords[0].coords[0].X - (int)((3072 / this.zoomfactor) / 2);
                this.mapy = (int)this.NFC.data[intid].coords[0].coords[0].Y - (int)((3072 / this.zoomfactor) / 2);
                this.minimx = (int)(this.mapx / 12);
                this.minimy = (int)(this.mapy / 12);
                this.MapImg = this.NFC.drawSelectedNFC(intid, this.TmpMapImg);
            }
        }

        public void setCalcSelection(int x,int y)
        {           
            this.mapx = x - (int)((3072 / this.zoomfactor) / 2);
            this.mapy = y - (int)((3072 / this.zoomfactor) / 2);
            this.minimx = (int)(this.mapx / 12);
            this.minimy = (int)(this.mapy / 12);      
        }

        public void setCoordSelection(int x,int y,PointF[] coords,int type)
        {
            this.mapx = (int)(x - (int)((3072 / this.zoomfactor) / 2));
            this.mapy = (int)(y - (int)((3072 / this.zoomfactor) / 2));
            this.minimx = (int)(this.mapx / 12);
            this.minimy = (int)(this.mapy / 12);
            this.MapImg = new Bitmap(TmpMapImg);
            Pen pen = new Pen(Color.Aquamarine, 3);
            Brush brush = new SolidBrush(Color.Red);
            if (type == 2)
            {
                pen = new Pen(Color.Red, 3);
                brush = new SolidBrush(Color.AntiqueWhite);
            }
            if (type == 3)
                pen = new Pen(Color.Green, 3);
            g = Graphics.FromImage(this.MapImg);
            try
            {
                g.DrawLines(pen, coords);
                g.FillRectangle(brush, x, y, 5, 5);          
            }
            catch
            {
            }
        }

        private Bitmap maprotator(Bitmap part)
        {
            
            Bitmap temp = new Bitmap(part,part.Width, part.Height);
            temp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return temp;
            
            
        }        

        public Bitmap getMapFile()
        {
            mappadjust();
            Bitmap tmp = this.MapImg.Clone(new Rectangle(mapx, mapy, Convert.ToInt32(3072 / this.zoomfactor), Convert.ToInt32(3072 / this.zoomfactor)), System.Drawing.Imaging.PixelFormat.DontCare);
            return new System.Drawing.Bitmap(tmp, 512, 512); 
        }

        private void mappadjust()
        {
            if (this.mapx < 0)
            {
                this.mapx = 0;
                this.minimx = 0;
            }

            if (this.mapy < 0)
            {
                this.mapy = 0;
                this.minimy = 0;
            }

            if (this.mapx + Convert.ToInt32(3072 / this.zoomfactor) > 3072)
            {
                this.mapx = 3072 - Convert.ToInt32(3072 / this.zoomfactor);
                this.minimx = 256 - Convert.ToInt32(256 / this.zoomfactor);
            }

            if (this.mapy + Convert.ToInt32(3072 / this.zoomfactor) > 3072)
            {
                this.mapy = 3072 - Convert.ToInt32(3072 / this.zoomfactor);
                this.minimy = 256 - Convert.ToInt32(256 / this.zoomfactor);
            }
        }

        public Bitmap setMiniMapFocus(Bitmap MiniMap)
        {
            mappadjust();
            Bitmap MiniMapFocus = new System.Drawing.Bitmap(MiniMap, 256, 256);
            int factor = Convert.ToInt32(256/this.zoomfactor);
            g = Graphics.FromImage(MiniMapFocus);
            g.DrawRectangle(new Pen(Brushes.OrangeRed, 2), minimx, minimy, factor, factor);
            return MiniMapFocus;
        }

        public Bitmap getMiniMap(bool withProps)
        {
            if (withProps == true)
                return setMiniMapFocus(this.PreviewProp);
            else
                return setMiniMapFocus(this.Preview);
        }

        public void saveMapFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                else
                {
                    FileStream fileStream2 = File.Open(path, FileMode.Create, FileAccess.ReadWrite);
                    fileStream2.Close();
                }
                Graphics g = Graphics.FromImage(this.MapImg);
                g.DrawString("Rappelz Map Viewer:  " + this.filename + "\n© c1ph3r", new Font("Times New Roman", 10, FontStyle.Regular), new SolidBrush(Color.Black), new PointF(5, 5));
                this.MapImg.Save(@path);
            }
            catch
            {
                this.error = true;
            }
        }

        public void zoomMap(int type)
        {
            if (type == 1 && this.zoomfactor > 1)
            {
                this.zoomfactor = this.zoomfactor - (float)0.5;                
            }
            if (type == 2 && this.zoomfactor < 12)
            {
                this.zoomfactor = this.zoomfactor + (float)0.5;                
            }         
      
        }

        public void setMMPos(Point coords)
        {
            this.minimx = Convert.ToInt32(coords.X - ((256 / zoomfactor) / 2));
            this.minimy = Convert.ToInt32(coords.Y - ((256 / zoomfactor) / 2));
            this.mapx = Convert.ToInt32((coords.X * 12) - ((3072 / this.zoomfactor) / 2));
            this.mapy = Convert.ToInt32((coords.Y * 12) - ((3072 / this.zoomfactor) / 2));
        }

        public void dragMap(int x, int y)
        {
            this.mapx = Convert.ToInt32(this.mapx + (x / this.zoomfactor));
            this.mapy = Convert.ToInt32(this.mapy + (y / this.zoomfactor));
            this.minimx = Convert.ToInt32(this.minimx + (x / (this.zoomfactor*12)));
            this.minimy = Convert.ToInt32(this.minimy + (y / (this.zoomfactor*12)));
        }

        public void scrY(int type)
        {
            if (this.mapy < 2500 && type == 1)
            {
                this.mapy = this.mapy + 60;
                this.minimy = this.minimy + 5;
            }
            if (this.mapy > 60 && type == 2)
            {
                this.mapy = this.mapy - 60;
                this.minimy = this.minimy - 5;
            }
        }

        public void scrX(int type)
        {
            if (this.mapx < 2500 && type == 1)
            {
                this.mapx = this.mapx + 60;
                this.minimx = this.minimx + 5;
            }
            if (this.mapx > 60 && type == 2)
            {
                this.mapx = this.mapx - 60;
                this.minimx = this.minimx - 5;
            }
        }

        public Point getIngameCoords(Point cursor)
        {
            return new Point(igconv(cursor.X,1) , igconv(cursor.Y,2));
        }

        public Point getMapFileCoords(Point cursor)
        {
            return new Point(mfconv(cursor.X, 1), mfconv(cursor.Y, 2));
        }

        public int igconv(int coord,int type)
        {
            if (type == 1)
                return Convert.ToInt32(((coord * (6 / this.zoomfactor)) + mapx  + (this.ingamemappartx * 3072)) * 5.25);
            else
                return Convert.ToInt32((mirrory((int)(coord * (6 / this.zoomfactor)) + mapy) +  (this.ingamemapparty * 3072)) * 5.25);

        }

        public int mfconv(int coord, int type)
        {
            if (type == 1)
                return Convert.ToInt32(((coord * (6 / this.zoomfactor)) + mapx ));
            else
                return Convert.ToInt32((mirrory((int)(coord * (6 / this.zoomfactor)) + mapy)));

        }

        public int getSegment(int x, int y)
        {
            int pointx = Convert.ToInt32((((x * (6 / this.zoomfactor))+mapx)  * 5.25));
            int pointy = Convert.ToInt32((mirrory((int)((y * (6 / this.zoomfactor))+mapy)) * 5.25));
            int xseg = (pointx / 252);
            int seg = xseg + (int)(pointy / 252)*64;
            return seg;
        }

        public int getSegmentCoords(int x, int y, int type)
        {
            int pointx = Convert.ToInt32((((x * (6 / this.zoomfactor)) + mapx) * 5.25));
            int pointy = Convert.ToInt32((mirrory((int)((y * (6 / this.zoomfactor)) + mapy)) * 5.25));
            int xseg = (pointx / 252);
            int yseg = (pointy / 252);
            if (type == 1)
            {
                return (pointx - ((xseg) * 252));
            }
            else
            {
                return (pointy - ((yseg) * 252));
            }
        }

        private int mirrory(int y)
        {
            return (3072 - y);
        }

        public StructNFA[] getStructNFA()
        {
            if (this.NFA.check)
                return NFA.data;
            else
                return null;

        }

        public SEGMENTS_TABLE_STRUCTURE[] getNFMResProp()
        {
            if (this.NFM.check)
                return NFM.propsegments;
           else
                return null;

        }

        public NFM_SEGMENTHEADER_V11[] getNFMResTerrain()
        {
            if (this.NFM.check && NFM.terrain!=null)
                return NFM.terrain.segments;
            else
                return null;

        }

        public VectorAttrib[] getNFMResVector()
        {
            if (this.NFM.check && NFM.vectordata != null)
                return NFM.vectordata;
            else
                return null;

        }

        public StructNFE[] getStructNFE()
        {
            if (this.NFE.check)
                return NFE.data;
            else
                return null;

        }

        public StructNFC[] getStructNFC()
        {
            if (this.NFC.check)
                return NFC.data;
            else
                return null;

        }

        public NFS_MONSTER_LOCATION[] getNFSRes()
        {
            if (this.NFS.check)
                return NFS.data;
            else
                return null;
        }

        public StructNFSNPC[] getNPCRes()
        {
            if (this.NFS.npcdata!=null)
                return NFS.npcdata;
            else
                return null;

        }

        public StructQPF[] getQPFRes()
        {
            if (this.QPF.check)
                return QPF.data;
            else
                return null;
        }

        public SpeedGrass[] getNFMGrassRes()
        {
            if (this.NFM.grass!=null)
                return NFM.grass;
            else
                return null;
        }

        public StructNFE[] getNFMEventRes()
        {
            if (this.NFM.events != null)
                return NFM.events;
            else
                return null;
        }

        public bool nfafilled()
        {
            if (this.NFA.data != null)
                return true;
            else
                return false;
        }

        public bool nfmfilled()
        {
            if (this.NFM.terrain != null)
                return true;
            else
                return false;
        }

        public bool nfcfilled()
        {
            if (this.NFC.data != null)
                return true;
            else
                return false;
        }

        public bool nfefilled()
        {
            if (this.NFE.cnt>0)
                return true;
            else
                return false;
        }

        public bool nfsfilled()
        {
            if (this.NFS.cnt>0)
                return true;
            else
                return false;
        }

        public bool qpffilled()
        {
            if (this.QPF.cnt>0)
                return true;
            else
                return false;
        }

        public bool npcfilled()
        {
            if (this.NFS.cnt_npcinit > 0)
                return true;
            else
                return false;
        }

        public void setloadnfa(bool switcher)
        {
            this.loadnfa = switcher;
            this.RefreshMap_image();
        }

        public void setloadnfc(bool switcher)
        {
            this.loadnfc = switcher;
            this.RefreshMap_image();
        }

        public void setloadnfe(bool switcher)
        {
            this.loadnfe = switcher;
            this.RefreshMap_image();
        }

        public void setloadnfs(bool switcher)
        {
            this.loadnfs = switcher;
            this.RefreshMap_image();
        }

        public void setloadnfmvect(bool switcher)
        {
            this.loadnfmvect = switcher;
            this.RefreshMap_image();
        }

        public void setloadnfmgrass(bool switcher)
        {
            this.loadnfmgrass = switcher;
            this.RefreshMap_image();
        }

        public void setloadqpf(bool switcher)
        {
            this.loadqpf = switcher;
            this.RefreshMap_image();
        }

        public void setloadjpg(bool switcher)
        {
            this.loadjpg = switcher;
            this.RefreshMap_image();
        }

        public void stringload(string stringresource)
        {
            this.strings = new StringResource(stringresource);
            if (!this.strings.check)
                this.strings = new StringResource(Properties.Settings.Default.stringDir);
            else
                Properties.Settings.Default.stringDir=stringresource;
            this.loadstrings = this.strings.check;
        }

        public void drawLines(PointF[] Points, int type)
        {
            Pen pen = new Pen(Color.Green, 3);
            if (type == 1)
                pen = new Pen(Color.Aquamarine, 3);
            if (type == 2)
                pen = new Pen(Color.Red, 3);
            if (type == 4)
                pen = new Pen(Color.Blue, 3);

            this.MapImg = new Bitmap(this.TmpMapImg);
            g = Graphics.FromImage(this.MapImg);
            try
            {
                g.DrawLines(pen,Points);
            }
            catch
            {
            }
        }

        public void drawPoint(PointF[] points, int type, int count)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            this.MapImg = new Bitmap(this.TmpMapImg);
            g = Graphics.FromImage(this.MapImg);
            for (int i = 0; i < count; i++)
            {
                g.FillRectangle(brush, points[i].X, points[i].Y, 3, 3);
            }
        }

        public void drawPoint(int x,int y, int type)
        {
            SolidBrush brush = new SolidBrush(Color.Orange);
            if (type==6)
                brush = new SolidBrush(Color.Pink);
            this.MapImg = new Bitmap(this.TmpMapImg);
            g = Graphics.FromImage(this.MapImg);
            g.FillRectangle(brush, x, y, 10, 10);
        }

        public void refreshMapBMP()
        {
            this.MapImg = this.TmpMapImg;
        }   
    }
}
