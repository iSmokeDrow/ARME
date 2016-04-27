using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;

namespace ARME.MapFileRes
{
    /// <summary>
    /// nfa | Nflavor Collision Manager 
    /// Provides Interface for .NFA management
    /// </summary>
    class nfa
    {
        public nfa(string path)
        {
            this.directory = Path.GetDirectoryName(path) + "/";
            this.filename = Path.GetFileNameWithoutExtension(path) + ".nfa";
            this.fullpath = this.directory + this.filename;
            this.data = new StructNFA[0];
            this.check = true;
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

        public StructNFA[] data
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
                FileStream fileStream = File.Open(this.fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.ASCII);
                this.cnt = binaryReader.ReadInt32();
                data = new StructNFA[this.cnt];
                for (int i = 1; i <= this.cnt; i++)
                {
                    data[i - 1] = new StructNFA();
                    data[i - 1].id = i; 
                    data[i - 1].coordcount = binaryReader.ReadInt32();
                    data[i - 1].points = new PointF[data[i - 1].coordcount + 1];
                    int stringcnt = 0;
                    for (int j = 1; j <= data[i - 1].coordcount; j++)                
                    {
                        data[i - 1].points[j - 1] = new Point();
                        int x = binaryReader.ReadInt32();
                        int y = mirrory(binaryReader.ReadInt32());
                        data[i - 1].points[j - 1].X = x;
                        data[i - 1].points[j - 1].Y = y;
                        data[i - 1].coord = data[i - 1].coord + j + ". (" + ((x * 5.25) + Hexcnv.GetCoords(this.filename, 1)).ToString() + ", " 
                            + (((3072 - y) * 5.25) + Hexcnv.GetCoords(this.filename, 2)).ToString() + ")";
                        if (stringcnt == 7)
                        {
                            data[i - 1].coord = data[i - 1].coord + "\n";
                            stringcnt = 0;
                        }
                        else
                            stringcnt++;
                    }
                    data[i - 1].points[data[i - 1].coordcount] = new Point();
                    data[i - 1].points[data[i - 1].coordcount].X = data[i - 1].points[0].X;
                    data[i - 1].points[data[i - 1].coordcount].Y = data[i - 1].points[0].Y;
                }
                binaryReader.Close();
                fileStream.Close();  
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

        private void updateCoordstxt(int id)
        {
            int stringcnt = 0;
            this.data[id].coord = "";
            for (int i = 0; i < this.data[id].coordcount; i++)
            {
                data[id].coord = data[id].coord + (i + 1).ToString() + ". (" + ((data[id].points[i].X * 5.25) + Hexcnv.GetCoords(this.filename, 1)).ToString() 
                    + ", " + (((3072 - data[id].points[i].Y) * 5.25) + Hexcnv.GetCoords(this.filename, 1)).ToString() + ")";
                if (stringcnt == 7)
                {
                    data[id].coord = data[id].coord + "\n";
                    stringcnt = 0;
                }
                else
                    stringcnt++;
            }

        }

        private int mirrory(int y)
        {
            return (3072 - y);
        }

        private void drawMapImg()
        {
            try
            {
                for (int i = 1; i <= this.cnt; i++)
                    g.DrawLines(new Pen(Color.Lime, 1), data[i - 1].points);
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

        public Bitmap drawSelectedNFA(int nfaid, Bitmap CurMap)
        {

            Bitmap tmp = new Bitmap(CurMap);
            g = Graphics.FromImage(tmp);
            try
            {
                g.DrawLines(new Pen(Color.Aquamarine, 2),
                    data[nfaid].points);

            }
            catch
            {
                this.error = true;
                this.MapImg = new Bitmap(3072, 3072);
                this.check = false;
            }
            return tmp;
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

        public void saveNFA(bool hashexp)
        {
            updateFile(hashexp);
        }

        public void deleteNFA(int id)
        {
            StructNFA[] tmpdata = new StructNFA[this.data.Length - 1];
            int j = 0;
            for (int i = 0; i < this.data.Length; i++)
            {
                if (data[i].id != id)
                {
                    tmpdata[j] = new StructNFA();
                    tmpdata[j] = data[i];
                    tmpdata[j].id = j + 1;
                    j++;
                }
                
            }
            this.data = tmpdata;
            this.cnt = cnt - 1;
            this.edit = true;
        }

        public void updateStructNFA(StructNFA tmp)
        {
            StructNFA[] tmpdata = new StructNFA[this.data.Length + 1];
            for (int i = 0; i < this.data.Length; i++)
            {
                tmpdata[i] = new StructNFA();
                tmpdata[i] = data[i];
            }
            tmpdata[this.data.Length] = new StructNFA();
            tmpdata[this.data.Length] = tmp;
            this.data = tmpdata;
            updateCoordstxt(this.data.Length - 1);
            this.cnt = cnt + 1;
            this.edit = true;
        }

        public void modifycoords(int id, string newcoord, PointF[] newpoints)
        {
            StructNFA[] tmpdata = new StructNFA[this.data.Length];
            int index=0;
            for (int i = 0; i < this.data.Length; i++)
            {
                if (data[i].id == id)
                {
                    index = i;
                    tmpdata[i] = new StructNFA();
                    tmpdata[i] = data[i];
                    tmpdata[i].points = newpoints;
                    tmpdata[i].coord = newcoord;
                    tmpdata[i].coordcount = newpoints.Length-1;
                }
                else
                {
                    tmpdata[i] = new StructNFA();
                    tmpdata[i] = data[i];
                }
            }
            this.edit = true;
            updateCoordstxt(index);
        }

        public void updateFile(bool hashexport)
        {
            try
            {
                FileStream stream;
                if (File.Exists(this.fullpath))
                {
                    File.Copy(this.fullpath, Path.GetDirectoryName(this.fullpath) + "\\" + Path.GetFileNameWithoutExtension(this.fullpath) + ".nfa" + DateTime.Now.ToString("ddMMMyyyyHHmmss"));
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
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].points.Length-1)));
                    for (int i = 0; i < data[j].points.Length - 1; i++)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].points[i].X)));
                        bw.Write(BitConverter.GetBytes(mirrory(Convert.ToInt32(data[j].points[i].Y))));
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
