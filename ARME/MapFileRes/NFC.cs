using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace ARME.MapFileRes
{
    /// <summary>
    /// nfc | nFlavor Location Manager
    /// Provides interface for .NFC management
    /// </summary>
    class nfc
    {
        public nfc(string path, StringResource names)
        {
            this.directory = Path.GetDirectoryName(path) + "/";
            this.filename = Path.GetFileNameWithoutExtension(path) + ".nfc";
            this.fullpath = this.directory + this.filename;

            if (names != null)
            {
                this.loadstrings = true;
                this.strings = names;
            }
            else { this.loadstrings = false; }
        
            this.data = new StructNFC[0];
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

        public StructNFC[] data
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
        public bool loadstrings = false;
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
                    data = new StructNFC[this.cnt];
                    for (int i = 1; i <= this.cnt; i++)
                    {
                        data[i - 1] = new StructNFC();
                        data[i - 1].id = i;
                        data[i - 1].type = binaryReader.ReadInt32();
                        data[i - 1].unknown1 = binaryReader.ReadInt32();
                        data[i - 1].unknown2 = binaryReader.ReadInt32();
                        data[i - 1].unknown3 = binaryReader.ReadInt32();
                        data[i - 1].unknown4 = binaryReader.ReadInt32();
                        data[i - 1].cnt_name = binaryReader.ReadInt32();
                        data[i - 1].name = new string(binaryReader.ReadChars(data[i - 1].cnt_name));
                        data[i - 1].cnt_script = binaryReader.ReadInt32();
                        data[i - 1].script = new string(binaryReader.ReadChars(data[i - 1].cnt_script));
                        if (this.loadstrings&&data[i-1].script.Contains("call_lc"))
                        {
                            Match match = Regex.Match(data[i - 1].script, @".*\((?<inner>.*)\).*");
                            int name_id = Convert.ToInt32(match.Groups["inner"].Value);
                            data[i - 1].name = strings.get_Wname(name_id + 70000000);
                        }
                        else
                        {
                            data[i - 1].name = "No StringResource found or unable to parse script!";
                        }
                        data[i - 1].cnt_name = data[i - 1].name.Length;
                        data[i - 1].cnt_coords = binaryReader.ReadInt32();
                        data[i - 1].coords = new NFCCoords[data[i - 1].cnt_coords];
                        for (int k = 1; k <= data[i - 1].cnt_coords; k++)
                        {
                            data[i - 1].coords[k - 1] = new NFCCoords();
                            data[i - 1].coords[k-1].cnt_coords = binaryReader.ReadInt32();
                            data[i - 1].coords[k - 1].coords = new PointF[data[i - 1].coords[k - 1].cnt_coords + 1];
                            data[i - 1].coord = data[i - 1].coord + k + ": {";
                            int stringcnt = 0;
                            for (int j = 1; j <= data[i - 1].coords[k-1].cnt_coords; j++)
                            {
                                int x = binaryReader.ReadInt32();
                                int y = mirrory(binaryReader.ReadInt32());
                                data[i - 1].coords[k - 1].coords[j - 1].X = x;
                                data[i - 1].coords[k - 1].coords[j - 1].Y = y;
                                data[i - 1].coord = data[i - 1].coord + j + ". (" + ((x*5.25)+Hexcnv.GetCoords(this.filename,1)) + ", " + (((3072-y)*5.25)+Hexcnv.GetCoords(this.filename,2)) + ") ";
                                if (stringcnt == 7)
                                {
                                    data[i - 1].coord = data[i - 1].coord + "\n";
                                    stringcnt = 0;
                                }
                                else
                                    stringcnt++;
                                if (j - 1 == 0)
                                {
                                    data[i - 1].coords[k - 1].coords[data[i - 1].coords[k - 1].cnt_coords] = new Point(x, y);
                                }

                            }
                            data[i - 1].coord = data[i - 1].coord + "} ";
                            if (this.data[i - 1].cnt_coords > 1)
                                this.data[i - 1].coord = data[i - 1].coord + "\n";
                        }
                    }
                    binaryReader.Close();
                    fileStream.Close();  
                }
                this.check = true;
                this.error = false;
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

        private void drawMapImg()
        {
            try
            {
                for (int i = 1; i <= this.cnt; i++)
                {
                    for (int j = 1; j <= data[i - 1].cnt_coords; j++)
                    {
                        g.DrawLines(new Pen(Color.Red, 1), this.data[i - 1].coords[j-1].coords);
                    }
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

        public Bitmap drawSelectedNFC(int nfcid, Bitmap CurMap)
        {

            Bitmap tmp = new Bitmap(CurMap);
            g = Graphics.FromImage(tmp);
            try
            {
                for (int i = 1; i <= data[nfcid].cnt_coords; i++)
                {
                    g.DrawLines(new Pen(Color.Red, 2),
                        this.data[nfcid].coords[i-1].coords);

                    g.DrawString(this.data[nfcid].name,
                        new Font("Times New Roman", 10, FontStyle.Regular),
                        new SolidBrush(Color.Red),
                        new PointF(this.data[nfcid].coords[i - 1].coords[0].X, this.data[nfcid].coords[i - 1].coords[0].Y));  
                }

                  
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

        public void saveNFC(bool hashexport)
        {
            updateFile(hashexport);
        }

        public void deleteNFC(int id)
        {
            StructNFC[] tmpdata = new StructNFC[this.data.Length - 1];
            int j = 0;
            for (int i = 0; i < this.data.Length; i++)
            {
                if (data[i].id != id)
                {
                    tmpdata[j] = new StructNFC();
                    tmpdata[j] = data[i];
                    tmpdata[j].id = j+1;
                    j++;                    
                }

            }
            this.data = tmpdata;
            this.cnt = cnt - 1;
            this.edit = true;
        }


        public void updateStructNFC(StructNFC tmp)
        {
            StructNFC[] tmpdata;
            if(this.cnt>0)
                tmpdata = new StructNFC[this.data.Length + 1];
            else
                tmpdata = new StructNFC[1];    
            for (int i = 0; i < tmpdata.Length-1; i++)
            {
                tmpdata[i] = new StructNFC();
                tmpdata[i] = data[i];
            }
            tmpdata[tmpdata.Length - 1] = new StructNFC();
            tmpdata[tmpdata.Length - 1] = tmp;
            if (this.loadstrings && tmpdata[tmpdata.Length - 1].script.Contains("call_lc"))
            {
                Match match = Regex.Match(tmpdata[tmpdata.Length - 1].script, @".*\((?<inner>.*)\).*");
                int name_id = Convert.ToInt32(match.Groups["inner"].Value);
                tmpdata[tmpdata.Length - 1].name = strings.get_Wname(name_id + 70000000);
            }
            else
            {
                tmpdata[tmpdata.Length - 1].name = "No StringResource found or unable to parse script!";
            }
            this.data = tmpdata;
            updatecoordstring(this.data.Length-1);
            this.cnt = cnt + 1;
            this.edit = true;
        }

        public string getstring(string script)
        {
            try
            {
                Match match = Regex.Match(script, @".*\((?<inner>.*)\).*");
                int name_id = Convert.ToInt32(match.Groups["inner"].Value);
                return strings.get_Wname(name_id + 70000000);
            }
            catch
            {
                return "No StringResource found or unable to parse script!";
            }

        }

        public void modifycoords(int id, string newcoord, NFCCoords[] points)
        {
            StructNFC[] tmpdata = new StructNFC[this.data.Length];
            for (int i = 0; i < this.data.Length; i++)
            {
                if (data[i].id == id)
                {
                    tmpdata[i] = new StructNFC();
                    tmpdata[i] = data[i];
                    tmpdata[i].cnt_coords = points.Length;
                    tmpdata[i].coords = points;
                    tmpdata[i].coord = newcoord;
                    for (int j = 0; j < tmpdata[i].coords.Length; j++)
                    {
                        tmpdata[i].coords[j].cnt_coords = tmpdata[i].coords[j].cnt_coords - 1;
                    }
                    updatecoordstring(i);
                }
                else
                {
                    tmpdata[i] = new StructNFC();
                    tmpdata[i] = data[i];
                }
            }
            this.edit = true;
        }

        public void updateFile(bool hashexport)
        {
            try
            {
                FileStream stream;
                if (File.Exists(this.fullpath))
                {
                    File.Copy(this.fullpath, Path.GetDirectoryName(this.fullpath) + "\\" + Path.GetFileNameWithoutExtension(this.fullpath) + ".nfc" + DateTime.Now.ToString("ddMMMyyyyHHmmss"));
                    stream = new FileStream(this.fullpath, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    stream = new FileStream(this.fullpath, FileMode.Create, FileAccess.ReadWrite);
                }      
                BinaryWriter bw = new BinaryWriter(stream, Encoding.Default);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.cnt)));        
                for (int j = 0; j < this.cnt; j++)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].type)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].unknown1)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].unknown2)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].unknown3)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].unknown4)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].name.Length)));
                    bw.Write(enc.GetBytes(data[j].name));
                    if (data[j].script.Contains("\x00"))
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].script.Length)));
                        bw.Write(enc.GetBytes(data[j].script));
                    }
                    else
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].script.Length + 1)));
                        bw.Write(enc.GetBytes(data[j].script + "\x00"));
                    }
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].coords.Length)));
                    for (int i = 0; i < data[j].cnt_coords; i++)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].coords[i].cnt_coords)));
                        for (int k = 0; k < data[j].coords[i].cnt_coords; k++)
                        {
                            bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[j].coords[i].coords[k].X)));
                            bw.Write(BitConverter.GetBytes(mirrory(Convert.ToInt32(data[j].coords[i].coords[k].Y))));
                        }
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

         private void updatecoordstring(int id)
        {
            this.data[id].coord = "";
            for(int i=0;i<this.data[id].cnt_coords;i++)
            {
                this.data[id].coord = data[id].coord +  (i+1) + ": {";
                int stringcnt = 0;
                for (int j = 0; j < this.data[id].coords[i].coords.Length-1; j++)
                {
                    data[id].coord = data[id].coord + (j+1) + ". (" + ((data[id].coords[i].coords[j].X*5.25)+Hexcnv.GetCoords(this.filename,1)).ToString() + 
                        ", " + (((3072-data[id].coords[i].coords[j].Y)*5.25)+Hexcnv.GetCoords(this.filename,2)).ToString() + ") ";
                    if (stringcnt == 7)
                    {
                        data[id].coord = data[id].coord + "\n";
                        stringcnt = 0;
                    }
                    else
                        stringcnt++;
                }
                this.data[id].coord = data[id].coord + "}";
                if (this.data[id].cnt_coords>1)
                    this.data[id].coord = data[id].coord + "\n";
            }

        }
    }
    
}
