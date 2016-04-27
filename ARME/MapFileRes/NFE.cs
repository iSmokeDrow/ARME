using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;

namespace ARME.MapFileRes
{
    /// <summary>
    /// nfe | nFlavor Event Location Manager
    /// Provides interface for .NFE management
    /// </summary>
    class nfe
    {
        public nfe(string path)
        {
            this.directory = Path.GetDirectoryName(path) + "/";
            this.filename = Path.GetFileNameWithoutExtension(path) + ".nfe";
            this.fullpath = this.directory + this.filename;
            this.check = true;
            this.data = new StructNFE[0];
            this.loadData();         
        }

        public string fullpath
        {
            get;
            set;
        }

        public string directory
        {
            get;
            set;
        }

        public string filename
        {
            get;
            set;
        }

        public bool check
        {
            get;
            set;
        }

        public int cnt
        {
            get;
            set;
        }

        public StructNFE[] data
        {
            get;
            set;
        }

        public Bitmap MapImg
        {
            get;
            set;
        }

        public bool error
        {
            get;
            set;
        }

        private Graphics g;
        public bool edit = false;
        private void loadData()
        {
            try
            {
                if (File.Exists(this.fullpath))
                {
                    FileStream fileStream = File.Open(this.fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.ASCII);
                    this.cnt = binaryReader.ReadInt32();
                    data = new StructNFE[this.cnt];
                    for (int i = 1; i <= this.cnt; i++)
                    {
                        data[i - 1] = new StructNFE();
                        data[i - 1].id = binaryReader.ReadInt32();
                        data[i - 1].type = binaryReader.ReadInt32();
                        data[i - 1].count_coords = binaryReader.ReadInt32();
                        data[i - 1].coords = new PointF[data[i - 1].count_coords + 1];
                        for (int j = 1; j <= data[i - 1].count_coords; j++)
                        {
                            int x = binaryReader.ReadInt32();
                            int y = mirrory(binaryReader.ReadInt32());
                            data[i - 1].coords[j - 1].X = x;
                            data[i - 1].coords[j - 1].Y = y;
                            data[i - 1].coord = data[i - 1].coord + j + ". (" + ((x*5.25)+Hexcnv.GetCoords(this.filename,1)) + ", " + (((3072-y)*5.25)+Hexcnv.GetCoords(this.filename,2)) + ")";
                            if (j - 1 == 0)
                            {
                                data[i - 1].coords[data[i - 1].count_coords] = new Point(x, y);
                            }
                        }
                    }
                    binaryReader.Close();
                    fileStream.Close(); 
                    this.error = false;
                    this.check = true;
                }
            }
            catch
            {
                this.error = true;
                this.MapImg = new Bitmap(3072, 3072);
                this.check = false;
            }
        }

        private void updateCoordString(int id)
        {
            this.data[id].coord = "";
            for (int i = 0; i < this.data[id].count_coords;i++ )
            {
                this.data[id].coord = this.data[id].coord + (i+1) + ".(" + ((this.data[id].coords[i].X*5.25)+Hexcnv.GetCoords(this.filename,1))
                    + ", " + (((3072 - this.data[id].coords[i].Y) * 5.25) + Hexcnv.GetCoords(this.filename, 2)) + ")";
            }


        }

        private void drawMapImg()
        {
            try
            {
                for (int i = 1; i <= this.cnt; i++)
                {
                    g.DrawLines(new Pen(Color.Green, 1), data[i - 1].coords);
                }
                this.error = false;
                this.check = true;
            }
            catch
            {
                this.error = true;
                this.MapImg = new Bitmap(3072, 3072);
                this.check = false;
            }
        }

        public Bitmap drawSelectedNFE(int nfeid, Bitmap CurMap)
        {

            Bitmap tmp = new Bitmap(CurMap);
            g = Graphics.FromImage(tmp);
            try
            {
                g.DrawLines(new Pen(Color.Green, 2), 
                    data[nfeid].coords);

                g.DrawString(data[nfeid].id.ToString(), 
                    new Font("Times New Roman", 10, FontStyle.Regular), 
                    new SolidBrush(Color.Green),
                    new PointF(data[nfeid].coords[0].X, data[nfeid].coords[0].Y));
            }
            catch
            {
                this.error = true;
                this.MapImg = new Bitmap(3072, 3072);
                this.check = false;
            }
            return tmp;
        }

        private int mirrory(int y)
        {
            return (3072 - y);
        }

        public Bitmap mergeMapImg(Bitmap Map)
        {
            if (this.check == true)
            {
                this.MapImg = Map;
                g = Graphics.FromImage(Map);
                drawMapImg();
                return this.MapImg;
            }
            else
            {
                return Map;
            }
        }

        public void updateStructNFE(StructNFE tmp)
        {
            StructNFE[] tmpdata;
            if (this.data.Length==0)
            {
                tmpdata = new StructNFE[1];
                tmpdata[0] = new StructNFE();
                tmpdata[0] = tmp;
                this.data = new StructNFE[tmpdata.Length];
                this.data = tmpdata;
                this.cnt = 0;
            }
            else
            {
                tmpdata = new StructNFE[this.data.Length + 1]; 
                for (int i = 0; i < this.data.Length; i++)
                {
                    tmpdata[i] = new StructNFE();
                    tmpdata[i] = data[i];
                }
                tmpdata[this.data.Length] = new StructNFE();
                tmpdata[this.data.Length] = tmp;
                this.data = tmpdata;
            }
            updateCoordString(this.data.Length-1);
            this.cnt = cnt + 1;
            this.edit = true;
        }

        public void saveNFE(bool hashexport)
        {
            updateFile(hashexport);
        }

        public void modifycoords(int id, string newcoord, PointF[] newpoints)
        {
            int index = 0;
            StructNFE[] tmpdata = new StructNFE[this.data.Length];
            for (int i = 0; i < this.data.Length; i++)
            {                
                if (data[i].id == id)
                {
                    index = i;
                    tmpdata[i] = new StructNFE();
                    tmpdata[i] = data[i];
                    tmpdata[i].coords = newpoints;
                    tmpdata[i].coord = newcoord;
                    tmpdata[i].count_coords = newpoints.Length-1;
                }
                else
                {
                    tmpdata[i] = new StructNFE();
                    tmpdata[i] = data[i];
                }
            }
            updateCoordString(index);
            this.edit = true;
        }

        public void deleteNFE(int id)
        {
            StructNFE[] tmpdata = new StructNFE[this.data.Length - 1];
            int j = 0;
            for (int i = 0; i < this.data.Length; i++)
            {
                if (data[i].id != id)
                {
                    tmpdata[j] = new StructNFE();
                    tmpdata[j] = data[i];
                    j++;
                }

            }
            this.data = tmpdata;
            this.cnt = cnt - 1;
            this.edit = true;
        }

        public void updateFile(bool hashexport)
        {
            try
            {
                FileStream stream;
                if (File.Exists(this.fullpath))
                {
                    File.Copy(this.fullpath, Path.GetDirectoryName(this.fullpath) + "\\" + Path.GetFileNameWithoutExtension(this.fullpath) + ".nfe" + DateTime.Now.ToString("ddMMMyyyyHHmmss"));
                    stream = new FileStream(this.fullpath, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    stream = new FileStream(this.fullpath, FileMode.Create, FileAccess.ReadWrite);
                }          
                BinaryWriter bw = new BinaryWriter(stream, Encoding.Default);
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.cnt)));
                for (int j = 0; j < this.cnt; j++)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].id)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].type)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].coords.Length-1)));
                    for (int i = 0; i < data[j].coords.Length - 1; i++)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].coords[i].X)));
                        bw.Write(BitConverter.GetBytes(mirrory(Convert.ToInt32(data[j].coords[i].Y))));
                    }

                }
                bw.Close();
                stream.Close();
                if (hashexport)
                {
                    FileIO.ExportHashed(this.fullpath);    
                }
                this.edit = false;
            }
            catch
            {
                this.error = true;
                System.Windows.Forms.MessageBox.Show("Error while saving!");
            }




        }

    }
}
