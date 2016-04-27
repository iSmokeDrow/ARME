using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;

namespace ARME.MapFileRes
{
    /// <summary>
    /// qpf | nFlavor Field Prop Manager
    /// Provides interface for .QPF management
    /// </summary>
    class qpf
    {
        public qpf(string path, StringResource names)
        {
            this.directory = Path.GetDirectoryName(path) + "/";
            this.filename = Path.GetFileNameWithoutExtension(path) + ".qpf";
            this.fullpath = this.directory + this.filename;

            if (names != null)
            {
                this.loadstrings = true;
                this.strings = names;
            }
            else { this.loadstrings = false; }
        
            this.data = new StructQPF[0];
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

        public StructQPF[] data
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
        private StringResource strings;
        private bool loadstrings = false;
        private byte[] header;
        public bool edit = false;
        private void loadData()
        {
            try
            {
                if (File.Exists(this.fullpath))
                {
                    FileStream fileStream = File.Open(this.fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.ASCII);
                    this.header = new byte[22];
                    for (int i = 0; i < 22; i++)
                    {
                        this.header[i] = binaryReader.ReadByte();
                    }
                    //binaryReader.ReadBytes(22);
                    this.cnt = binaryReader.ReadInt32();
                    data = new StructQPF[this.cnt];
                    for (int i = 1; i <= cnt; i++)
                    {
                        data[i - 1] = new StructQPF();
                        data[i - 1].id = binaryReader.ReadInt32();
                        if (this.loadstrings)
                            data[i - 1].name = this.strings.get_Fname(data[i - 1].id + 170000000);
                        else
                            data[i - 1].name = "No StringResource found!";
                        /*data[i - 1].x = binaryReader.ReadSingle() / (float)5.25;
                        data[i - 1].y = mirrory((int)(binaryReader.ReadSingle() / (float)5.25));*/
                        data[i - 1].x = binaryReader.ReadSingle() + Hexcnv.GetCoords(this.filename, 1);
                        data[i - 1].y = binaryReader.ReadSingle() + Hexcnv.GetCoords(this.filename, 2);
                        data[i - 1].offset_z = binaryReader.ReadSingle();
                        data[i - 1].rotation_x = binaryReader.ReadSingle();
                        data[i - 1].rotation_y = binaryReader.ReadSingle();
                        data[i - 1].rotation_z = binaryReader.ReadSingle();
                        data[i - 1].scale_x = binaryReader.ReadSingle();
                        data[i - 1].scale_y = binaryReader.ReadSingle();
                        data[i - 1].scale_z = binaryReader.ReadSingle();
                        data[i - 1].id2 = binaryReader.ReadInt32();
                        binaryReader.ReadByte();
                        binaryReader.ReadInt32();
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

        private int mirrory(int y)
        {
            return (3072 - y);
        }

        private float mirrory(float y)
        {
            return (3072 - y);
        }

        private void drawMapImg()
        {
            try
            {
                for (int i = 1; i <= this.cnt; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Orange), ((data[i - 1].x - Hexcnv.GetCoords(this.filename, 1)) / (float)5.25), mirrory((int)((data[i - 1].y - Hexcnv.GetCoords(this.filename, 2)) / (float)5.25)), 3, 3);
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

        public Bitmap drawSelectedQPF(int qpfid,Bitmap CurMap)
        {

            Bitmap tmp = new Bitmap(CurMap);
            g = Graphics.FromImage(tmp);
            try
            {
                g.FillRectangle(new SolidBrush(Color.OrangeRed), ((data[qpfid].x - Hexcnv.GetCoords(this.filename, 1)) / (float)5.25), mirrory((int)((data[qpfid].y - Hexcnv.GetCoords(this.filename, 2)) / (float)5.25)), 10, 10);
                g.DrawString(data[qpfid].name, new Font("Times New Roman", 10, FontStyle.Regular), new SolidBrush(Color.OrangeRed), new PointF(((data[qpfid].x - Hexcnv.GetCoords(this.filename, 1)) / (float)5.25) + 6, mirrory((int)(data[qpfid].y - Hexcnv.GetCoords(this.filename, 2)) / (float)5.25) + 6));
                this.error = false;
                this.check = true;            
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

        public void saveQPF(bool hashexport)
        {
            updateFile(hashexport);
        }

        public void deleteQPF(int id, int index)
        {
            StructQPF[] tmpdata = new StructQPF[this.data.Length - 1];
            int j = 0;
            for (int i = 0; i < this.data.Length; i++)
            {
                if (i!=index)
                {
                    tmpdata[j] = new StructQPF();
                    tmpdata[j] = data[i];
                    j++;
                }

            }
            this.data = tmpdata;
            this.cnt = cnt - 1;
            this.edit = true;
        }

        public void updateQPFRes(StructQPF tmp)
        {
            StructQPF[] tmpdata;
            if (this.data.Length==0)
            {
                tmpdata = new StructQPF[1];
                tmpdata[0]=new StructQPF();
                tmpdata[0]=tmp;
                this.header = Hexcnv.StringToByteArrayFastest("6E466C61766F7220517565737450726F700003000000");
            }else{
                tmpdata = new StructQPF[this.data.Length + 1];
                for (int i = 0;i<this.data.Length;i++)
                {
                    tmpdata[i]=new StructQPF();
                    tmpdata[i]=data[i];
                }
                tmpdata[this.data.Length]=new StructQPF();
                tmpdata[this.data.Length]=tmp;
            }
            this.data = tmpdata;
            this.cnt = cnt + 1;
            this.edit = true;
        }

        public void updateQPFCoord(int x, int y, int id)
        {
            this.data[id].x = x;
            this.data[id].y = y;
        }

        public void updateFile(bool hashexport)
        {
            try
            {
                FileStream stream;
                if (File.Exists(this.fullpath))
                {
                    File.Copy(this.fullpath, Path.GetDirectoryName(this.fullpath) + "\\" + Path.GetFileNameWithoutExtension(this.fullpath) + ".qpf" + DateTime.Now.ToString("ddMMMyyyyHHmmss"));
                    stream = new FileStream(this.fullpath, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    stream = new FileStream(this.fullpath, FileMode.Create, FileAccess.ReadWrite);
                } 
                BinaryWriter bw = new BinaryWriter(stream, Encoding.Default);
                //stream.Position = 22;
                bw.Write(header);
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.cnt)));
                for (int i = 0; i < this.cnt; i++)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[i].id)));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].x- Hexcnv.GetCoords(this.filename, 1))));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].y- Hexcnv.GetCoords(this.filename, 2))));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].offset_z)));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].rotation_x)));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].rotation_y)));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].rotation_z)));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].scale_x)));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].scale_y)));
                    bw.Write(BitConverter.GetBytes(Convert.ToSingle(data[i].scale_z)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[i].id2)));
                    stream.WriteByte(0x00);
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(65535)));
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
