using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using ARME.Struct;

namespace ARME.MapFileRes
{
    /// <summary>
    /// nfs | nFlavor Monster/NPC Spawn Manager
    /// Provides interface for .NFS management
    /// NPC Location/Script depreciated!!!
    /// </summary>
    class nfs
    {
        public nfs(string path, StringResource names)
        {
            this.directory = Path.GetDirectoryName(path) + "/";
            this.filename = Path.GetFileNameWithoutExtension(path) + ".nfs";
            this.fullpath = this.directory + this.filename;

            if (names != null)
            {
                this.loadstrings = true;
                this.strings = names;
            }
            else { this.loadstrings = false; }

            this.check = true;
            this.loadData();            
        }

        private int EVENT_LOCATION_OFFSET 
        {
            get;
            set;
        }

        private int EVENT_SCRIPT_OFFSET 
        {
            get;
            set;
        }

        private int EVENT_PROP_OFFSET 
        {
            get;
            set;
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

        public int cnt2
        {
            get;
            set;
        }

        public int cnt_npcinit
        {
            get;
            set;
        }

        NFS_HEADER Header = new NFS_HEADER();

        public NFS_MONSTER_LOCATION[] data;

        public StructNFSNPC[] npcdata
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
        public bool editNFS = false;
        public bool editNPC = false;

        private void loadData()
        {
            try
            {
                if (File.Exists(this.fullpath))
                {
                    using (FileStream fileStream = File.Open(this.fullpath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.ASCII))
                        {
                            this.Header.Signature = Encoding.Default.GetString(binaryReader.ReadBytes(16));
                            this.Header.Version = binaryReader.ReadInt32();
                            this.Header.LocationOffset = binaryReader.ReadInt32();
                            this.Header.MonsterOffset = binaryReader.ReadInt32();
                            this.Header.NPCOffset = binaryReader.ReadInt32();
                            this.Header.RowCount = binaryReader.ReadInt32();
                            this.cnt = Header.RowCount;
                            data = new NFS_MONSTER_LOCATION[cnt];

                            for (int i = 1; i <= cnt; i++)
                            {
                                data[i - 1] = new NFS_MONSTER_LOCATION();
                                data[i - 1].Left = ((Convert.ToInt32((binaryReader.ReadInt32() * 8) * 5.25)) + Hexcnv.GetCoords(this.filename, 1));
                                data[i - 1].Top = ((Convert.ToInt32((binaryReader.ReadInt32() * 8) * 5.25)) + Hexcnv.GetCoords(this.filename, 2));
                                data[i - 1].Right = ((Convert.ToInt32((binaryReader.ReadInt32() * 8) * 5.25)) + Hexcnv.GetCoords(this.filename, 1));
                                data[i - 1].Bottom = ((Convert.ToInt32((binaryReader.ReadInt32() * 8) * 5.25)) + Hexcnv.GetCoords(this.filename, 2));
                                binaryReader.ReadInt32();
                            }

                            cnt2 = binaryReader.ReadInt32();
                            for (int i = 1; i <= cnt2; i++)
                            {
                                data[i - 1].RegionIndex = binaryReader.ReadInt32();
                                data[i - 1].MonsterCount = binaryReader.ReadInt32();
                                data[i - 1].Trigger = binaryReader.ReadInt32();
                                data[i - 1].ScriptLength = binaryReader.ReadInt32();
                                data[i - 1].ScriptText = new string(binaryReader.ReadChars(data[i - 1].ScriptLength));
                            }

                            cnt_npcinit = binaryReader.ReadInt32();
                            npcdata = new StructNFSNPC[cnt_npcinit];
                            for (int i = 1; i <= cnt_npcinit; i++)
                            {
                                npcdata[i - 1] = new StructNFSNPC();
                                npcdata[i - 1].id = binaryReader.ReadInt16();
                                npcdata[i - 1].unknown1 = binaryReader.ReadInt16();
                                //npcdata[i - 1].x = (int)(binaryReader.ReadSingle() / (float)5.25);
                                //npcdata[i - 1].y = mirrory((int)(binaryReader.ReadSingle() / (float)5.25));
                                npcdata[i - 1].x = (int)(binaryReader.ReadSingle() + Hexcnv.GetCoords(this.filename, 1));
                                npcdata[i - 1].y = (int)(binaryReader.ReadSingle() + Hexcnv.GetCoords(this.filename, 2));
                                npcdata[i - 1].propID = binaryReader.ReadInt16(); //14
                                //binaryReader.ReadBytes(2);
                                npcdata[i - 1].unknown2 = binaryReader.ReadInt32();  //18
                                npcdata[i - 1].unknown4 = binaryReader.ReadInt32();      //22                  
                                //binaryReader.ReadBytes(4);
                                npcdata[i - 1].cnt_initscript = binaryReader.ReadInt32(); //26

                                npcdata[i - 1].initscript = new string(binaryReader.ReadChars(npcdata[i - 1].cnt_initscript));

                                if (this.loadstrings && npcdata[i - 1].initscript.Contains("init_npc"))
                                {
                                    Match match = Regex.Match(npcdata[i - 1].initscript, @".*\((?<inner>.*)\,.*");
                                    Match replace = Regex.Match(npcdata[i - 1].initscript, "\"([^\"]*)\"");
                                    int name_id = Convert.ToInt32(match.Groups["inner"].Value);
                                    string replacer = replace.ToString();
                                    string name = strings.get_NPCname(name_id + 100000000);
                                    npcdata[i - 1].name = name;
                                    name = "\"" + name + "\"";
                                    npcdata[i - 1].initscript = npcdata[i - 1].initscript.Replace(replacer, name);
                                }
                                else
                                {
                                    npcdata[i - 1].name = "No StringResource found!";
                                }
                                npcdata[i - 1].cnt_initscript = npcdata[i - 1].initscript.Length;
                                if (npcdata[i - 1].unknown2 == 2)
                                {
                                    npcdata[i - 1].type = binaryReader.ReadInt32();
                                    npcdata[i - 1].cnt_contact = binaryReader.ReadInt32();
                                    npcdata[i - 1].contact = new string(binaryReader.ReadChars(npcdata[i - 1].cnt_contact));
                                }
                            }
                        }
                    }
                }
                this.error = false;
                this.check = true;
            }
            catch //(Exception ex)
            {
                //throw new Exception(ex.ToString());
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
                    PointF[] points = {   new Point((int)((int)(data[i - 1].Left - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[i - 1].Top- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))), 
                                          new Point((int)((int)(data[i - 1].Right - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[i - 1].Top- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))), 
                                          new Point((int)((int)(data[i - 1].Right - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[i - 1].Bottom- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))), 
                                          new Point((int)((int)(data[i - 1].Left - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[i - 1].Bottom- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))), 
                                          new Point((int)((int)(data[i - 1].Left - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[i - 1].Top- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))) };

                    g.DrawLines(new Pen(Color.Blue, 1), points);
                }
                for (int i = 1; i <= this.cnt_npcinit; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Pink),
                        ((int)npcdata[i - 1].x - Hexcnv.GetCoords(this.filename, 1)) / (float)5.25, 3072-((int)(npcdata[i - 1].y - Hexcnv.GetCoords(this.filename, 2))/(float)5.25), 3, 3);
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

        public Bitmap drawSelectedNFS(int nfsid, Bitmap CurMap)
        {
            Bitmap tmp = new Bitmap(CurMap);
            g = Graphics.FromImage(tmp);
            try
            {
                PointF[] points = {   new Point((int)((int)(data[nfsid].Left - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[nfsid].Top- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))),
                                      new Point((int)((int)(data[nfsid].Right - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[nfsid].Top- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))),
                                      new Point((int)((int)(data[nfsid].Right - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[nfsid].Bottom- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))),
                                      new Point((int)((int)(data[nfsid].Left - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[nfsid].Bottom- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))), 
                                      new Point((int)((int)(data[nfsid].Left - Hexcnv.GetCoords(this.filename, 1))/(float)5.25),
                                                  (int)(mirrory((int)((data[nfsid].Top- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))) };

                if (data[nfsid].ScriptText != null)
                {
                    g.DrawLines(new Pen(Color.Blue, 2), points);
                    g.DrawString(data[nfsid].ScriptText.ToString(),
                        new Font("Times New Roman", 10, FontStyle.Regular), 
                        new SolidBrush(Color.Blue), 
                        new PointF(Convert.ToInt32(((int)(data[nfsid].Left - Hexcnv.GetCoords(this.filename, 1))/(float)5.25)) + 5, Convert.ToInt32((mirrory((int)((data[nfsid].Bottom- Hexcnv.GetCoords(this.filename, 2))/(float)5.25)))) + 5));
                }
                else
                {
                    g.DrawLines(new Pen(Color.Aquamarine, 2), points);
                    g.DrawString("empty: " + (nfsid).ToString(), 
                        new Font("Times New Roman", 10, FontStyle.Regular), 
                        new SolidBrush(Color.Aquamarine),
                        new PointF(Convert.ToInt32(((int)(data[nfsid].Left - Hexcnv.GetCoords(this.filename, 1)) / (float)5.25)) + 5, Convert.ToInt32((mirrory((int)((data[nfsid].Bottom - Hexcnv.GetCoords(this.filename, 2)) / (float)5.25)))) + 5));

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
            return tmp;
        }

        public Bitmap drawSelectedNPC(int npcid, Bitmap CurMap)
        {

            Bitmap tmp = new Bitmap(CurMap);
            g = Graphics.FromImage(tmp);
            try
            {
                g.FillRectangle(new SolidBrush(Color.Pink),
                    (int)(npcdata[npcid].x - Hexcnv.GetCoords(this.filename, 1))/(float)5.25,
                    mirrory((int)((npcdata[npcid].y - Hexcnv.GetCoords(this.filename, 2))/5.25)), 8, 8);

                g.DrawString(npcdata[npcid].name.ToString(), 
                    new Font("Times New Roman", 10, FontStyle.Regular), 
                    new SolidBrush(Color.Pink),
                    new PointF(Convert.ToInt32((npcdata[npcid].x - Hexcnv.GetCoords(this.filename, 1)) / 5.25) + 5, Convert.ToInt32(3072-((npcdata[npcid].y - Hexcnv.GetCoords(this.filename, 2))/5.25)) + 2));

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
            this.MapImg = Map;
            if (this.check)
            {
                g = Graphics.FromImage(this.MapImg);
                drawMapImg();
                return this.MapImg;
            }
            else
            {
                return Map;
            }
        }

        public void deleteNFS(int id)
        {
            NFS_MONSTER_LOCATION[] tmpdata = new NFS_MONSTER_LOCATION[this.data.Length - 1];
            int j = 0;
            for (int i = 0; i < this.data.Length; i++)
            {
                if (i != id)
                {
                    tmpdata[j] = new NFS_MONSTER_LOCATION();
                    tmpdata[j] = data[i];
                    tmpdata[j].RegionIndex = j;
                    j++;                    
                }
                else
                {
                    if (string.IsNullOrEmpty(data[i].ScriptText))
                        this.cnt2 -= 1;
                }

            }
            this.data = tmpdata;
            this.cnt = cnt - 1;
            this.editNFS = true;
        }

        public void updateNFSRes(NFS_MONSTER_LOCATION tmp)
        {
            NFS_MONSTER_LOCATION[] tmpdata;
            if (this.data.Length == 0)
            {
                tmpdata = new NFS_MONSTER_LOCATION[1];
                tmpdata[0] = tmp;
                
            }
            else
            {
                tmpdata = new NFS_MONSTER_LOCATION[this.data.Length + 1];
                for (int i = 0; i < this.data.Length; i++)
                {
                    tmpdata[i] = new NFS_MONSTER_LOCATION();
                    tmpdata[i] = data[i];
                }
                tmpdata[this.data.Length] = new NFS_MONSTER_LOCATION();
                tmpdata[this.data.Length] = tmp;
            }
            this.data = tmpdata;
            this.cnt = cnt + 1;
            this.cnt2 = cnt2 + 1;
            this.editNFS = true;
        }

        public void updateNFSCoord(int type, int x,int y, int id)
        {
            if (type == 1)
                this.data[id].Top = y;
            if (type == 2)
                this.data[id].Bottom = y;
            if (type == 3)
                this.data[id].Left = x;
            if (type == 4)
                this.data[id].Right = x;
   
            this.editNFS = true;
        }

        public void updateNPCCoord(int x, int y, int id)
        {
            this.npcdata[id].x = x;
            this.npcdata[id].y = y;
        }

        public void deleteNPC(int id)
        {
            StructNFSNPC[] tmpdata = new StructNFSNPC[this.npcdata.Length - 1];
            int j = 0;
            for (int i = 0; i < this.npcdata.Length; i++)
            {
                if (npcdata[i].id != id)
                {
                    tmpdata[j] = new StructNFSNPC();
                    tmpdata[j] = npcdata[i];
                    tmpdata[j].id = j+1;
                    j++;
                    
                }

            }
            this.npcdata = tmpdata;
            this.cnt_npcinit = cnt_npcinit - 1;
            this.editNPC = true;
        }
        
        public void updateNPCRes(StructNFSNPC tmp)
        {
            StructNFSNPC[] tmpdata;
            if (this.npcdata.Length == 0)
            {
                tmpdata = new StructNFSNPC[1];
                tmpdata[0] = new StructNFSNPC();
                tmpdata[0] = tmp;
                
            }
            else
            {
                tmpdata = new StructNFSNPC[this.npcdata.Length + 1];
                for (int i = 0; i < this.npcdata.Length; i++)
                {
                    tmpdata[i] = new StructNFSNPC();
                    tmpdata[i] = npcdata[i];
                }
                tmpdata[this.npcdata.Length] = new StructNFSNPC();
                tmpdata[this.npcdata.Length] = tmp;
            }
            this.npcdata = tmpdata;
            this.cnt_npcinit = cnt_npcinit + 1;
            this.editNPC = true;
        }

        public void saveNFS(bool hashexport)
        {
            writenewNFSRes(hashexport);
        }

        public void saveNPC(bool hashexport)
        {
            writenewNFSRes(hashexport);
        }

        private void writenewNFSRes(bool hashexport)
        {
            try
            {
                checkHeader();
                FileStream stream;
                if (File.Exists(this.fullpath))
                {
                    File.Copy(this.fullpath, Path.GetDirectoryName(this.fullpath) + "\\" + Path.GetFileNameWithoutExtension(this.fullpath) + ".nfs" + DateTime.Now.ToString("ddMMMyyyyHHmmss"));
                    stream = new FileStream(this.fullpath, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    stream = new FileStream(this.fullpath, FileMode.Create, FileAccess.ReadWrite);
                }      
                BinaryWriter bw = new BinaryWriter(stream, Encoding.Default);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                bw.Write(enc.GetBytes(Header.Signature));
                bw.Write(BitConverter.GetBytes(Header.Version));
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(EVENT_LOCATION_OFFSET)));
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(getOffset(1))));
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(getOffset(2))));
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.cnt)));
                for (int i = 0; i < cnt; i++)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(((data[i].Left - Hexcnv.GetCoords(this.filename, 1))/5.25) / 8)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(((data[i].Top - Hexcnv.GetCoords(this.filename, 2)) / 5.25) / 8)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(((data[i].Right - Hexcnv.GetCoords(this.filename, 1)) / 5.25) / 8)));
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(((data[i].Bottom - Hexcnv.GetCoords(this.filename, 2)) / 5.25) / 8)));
                    bw.Write(new byte[4]);
                }
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.cnt2)));
                for (int i = 0; i < cnt; i++)
                {
                    if (!string.IsNullOrEmpty(data[i].ScriptText))
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[i].RegionIndex)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[i].MonsterCount)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[i].Trigger)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(data[i].ScriptLength)));
                        bw.Write(enc.GetBytes(data[i].ScriptText));
                    }
                }
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.cnt_npcinit)));
                if (this.cnt_npcinit > 0)
                {
                    for (int i = 0; i < this.cnt_npcinit; i++)
                    {
                        
                        bw.Write(BitConverter.GetBytes(Convert.ToInt16(npcdata[i].id)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt16(npcdata[i].unknown1)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(npcdata[i].x - Hexcnv.GetCoords(this.filename, 1))));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(npcdata[i].y - Hexcnv.GetCoords(this.filename, 2))));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt16(npcdata[i].propID)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(npcdata[i].unknown2)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(npcdata[i].unknown4)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(npcdata[i].initscript.Length)));
                        bw.Write(enc.GetBytes(npcdata[i].initscript));
                        if (npcdata[i].unknown2 == 2)
                        {
                            bw.Write(BitConverter.GetBytes(Convert.ToInt32(npcdata[i].type)));
                            bw.Write(BitConverter.GetBytes(Convert.ToInt32(npcdata[i].contact.Length)));
                            bw.Write(enc.GetBytes(npcdata[i].contact));
                        }

                    }
                }
                bw.Close();
                stream.Close();
                if (hashexport)
                {
                    FileIO.ExportHashed(this.fullpath);    
                }
                this.editNFS = false;
                this.editNPC = false;
            }
            catch
            {
                this.error = true;
                System.Windows.Forms.MessageBox.Show("Error while saving!");
            }

            

            
        }
        
        private int getOffset(int type)
        {          
            if (type == 1)
                return 36 + (this.cnt * 20);
            else 
            {
                int tmpoff = 40 + (this.cnt * 20);
                for (int i = 0; i < this.data.Length; i++)
                {
                    if (!string.IsNullOrEmpty(data[i].ScriptText))
                        tmpoff = tmpoff + 16 + this.data[i].ScriptLength;
                }
                return tmpoff;
            }           
                
        }

        private void checkHeader()
        {
            if (this.cnt == 0)
                this.cnt = 0;
            if (Header.Version == 0)
                Header.Version = 2;
            if (Header.Signature != "nFlavor Script\0\0")
                Header.Signature = "nFlavor Script\0\0";
        }
    }
}
