using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;

namespace ARME.MapFileRes
{
    /// <summary>
    /// nfm | nFlavor Terrain Manager
    /// Provides interface for .NFM management
    /// </summary>
    class nfm
    {
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

        public StructNFM header
        {
            get;
            set;
        }

        public StructNFA nfasync
        {
            get;
            set;
        }
 
        public SEGMENTS_TABLE_STRUCTURE[] propsegments //4096*4 (Offsets)+4096(cnt*49)
        {
            get;
            set;
        }

        public SpeedGrass[] grass //cnt*((16*4)+stringcnt+(coordcnt*8))
        {
            get;
            set;
        }

        public MapProps MapProps //55 Bytes
        {
            get;
            set;
        }

        public TerrainSegments terrain //4096(10+(36*23))
        {
            get;
            set;
        }

        public StructNFE[] events //4+(cnt*44)
        {
            get;
            set;
        }

        public VectorAttrib[] vectordata // 8+(cnt*(cnt*8)
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

        public void nfaSync(StructNFA[] data)
        {
            this.vectordata = new VectorAttrib[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                this.vectordata[i] = new VectorAttrib();
                this.vectordata[i].cnt = data[i].coordcount;
                this.vectordata[i].data = new VectorData[data[i].coordcount];
                for (int j=0;j<data[i].coordcount;j++)
                {
                    this.vectordata[i].data[j] = new VectorData();
                    this.vectordata[i].data[j].x = Convert.ToInt32(data[i].points[j].X);
                    this.vectordata[i].data[j].y = Convert.ToInt32(data[i].points[j].Y);
                }
            }
        }

        public void nfeSync(StructNFE[] data)
        {
            this.events = data;
        }

        public void calculateOffsets()
        {
            this.header._dwMapPropertiesOffset = 60;
            this.header._dwTerrainSegmentOffset = 115;
            this.header._dwPropOffset = 115 + (4096 * (10 + (36 * 23)));
            this.header._dwVectorAttrOffset = this.header._dwPropOffset+(4096*4);
            if (propsegments != null)
            {
                for (int i = 0; i < this.propsegments.Length; i++)
                {
                    this.header._dwVectorAttrOffset = this.header._dwVectorAttrOffset + 8;
                    if (this.propsegments[i].props!=null)
                        this.header._dwVectorAttrOffset = this.header._dwVectorAttrOffset + (this.propsegments[i].props.Length * 49);
                }
            }
            this.header._dwWaterOffset = this.header._dwVectorAttrOffset + 4;
            if (vectordata != null)
            {
                for (int i = 0; i < this.vectordata.Length; i++)
                {
                    this.header._dwWaterOffset = this.header._dwWaterOffset + 4;
                    if (this.vectordata[i].data.Length > 0)
                    {
                        this.header._dwWaterOffset = this.header._dwWaterOffset + (this.vectordata[i].data.Length * 8);
                    }
                }
            }
            //WaterOffset = WaterOffset + 4;
            this.header._dwGrassColonyOffset = this.header._dwWaterOffset + 4;
            this.header._dwEventAreaOffset = this.header._dwGrassColonyOffset + 4;
            //cnt*((16*4)+stringcnt+(coordcnt*8))
            if (grass != null)
            {
                for (int i = 0; i < this.grass.Length; i++)
                {
                    this.header._dwEventAreaOffset = this.header._dwEventAreaOffset + (16 * 4);
                    this.header._dwEventAreaOffset = this.header._dwEventAreaOffset + this.grass[i].cnt_filename;
                    this.header._dwEventAreaOffset = this.header._dwEventAreaOffset + (this.grass[i].cnt_coords * 8);
                }
            }
        }

        public nfm(string path)
        {
            this.directory = Path.GetDirectoryName(path) + "/";
            this.filename = Path.GetFileNameWithoutExtension(path) + ".nfm";
            this.fullpath = this.directory + this.filename;
            this.check = true;
            this.header = new StructNFM();
            this.loadData();
   
        }

        public void replaceProp(int seg,PROPS_TABLE_STRUCTURE[] props)
        {
            this.propsegments[seg].props = props;
            this.propsegments[seg]._prop_count = props.Length;
        }

        public void replaceTerrain(int seg,NFM_VERTEXSTRUCT_V11[] props)
        {
            this.terrain.segments[seg].vertices = props;
        }

        public void replaceGrass(int seg, PointF[] props)
        {
            this.grass[seg].coords = props;
        }

        public void replaceVector(int seg, VectorData[] props)
        {
            this.vectordata[seg].data = props;
            this.vectordata[seg].cnt = props.Length;
        }

        private void loadData()
        {
            try
            {
                if (File.Exists(this.fullpath))
                {
                    FileStream fileStream = File.Open(this.fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader br = new BinaryReader(fileStream, Encoding.ASCII);
                    try
                    {
                        this.header.szSign = System.Text.Encoding.GetEncoding(949).GetString(br.ReadBytes(16));
                        this.header.dwVersion = br.ReadUInt32();
                        this.header.dwMapPropertiesOffset = br.ReadUInt32();
                        this.header.dwTerrainSegmentOffset = br.ReadUInt32();
                        this.header.dwPropOffset = br.ReadUInt32();
                        this.header.dwVectorAttrOffset = br.ReadUInt32();
                        this.header.dwWaterOffset = br.ReadUInt32();
                        this.header.dwGrassColonyOffset = br.ReadUInt32();
                        this.header.dwEventAreaOffset = br.ReadUInt32();
                        this.header.nTileCountPerSegment = br.ReadInt32();
                        this.header.nSegmentCountPerMap = br.ReadInt32();
                        this.header.fTileLength = br.ReadSingle();
                        br.BaseStream.Position = this.header.dwMapPropertiesOffset;
                        this.MapProps = new MapProps();
                        this.MapProps.u1_1 = br.ReadByte();
                        this.MapProps.u1_2 = br.ReadByte();
                        this.MapProps.u1_3 = br.ReadByte();
                        this.MapProps.u1_4 = br.ReadByte();
                        this.MapProps.u1_5 = br.ReadByte();
                        this.MapProps.u1_6 = br.ReadByte();
                        this.MapProps.u2 = br.ReadSingle();
                        this.MapProps.u3 = br.ReadSingle();
                        this.MapProps.u4 = br.ReadSingle();

                        this.MapProps.u5_1 = br.ReadByte();
                        this.MapProps.u5_2 = br.ReadByte();
                        this.MapProps.u5_3 = br.ReadByte();
                        this.MapProps.u5_4 = br.ReadByte();
                        this.MapProps.u5_5 = br.ReadByte();
                        this.MapProps.u5_6 = br.ReadByte();
                        this.MapProps.u6 = br.ReadSingle();
                        this.MapProps.u7 = br.ReadSingle();
                        this.MapProps.u8 = br.ReadSingle();

                        this.MapProps.u9_1 = br.ReadByte();
                        this.MapProps.u9_2 = br.ReadByte();
                        this.MapProps.u9_3 = br.ReadByte();
                        this.MapProps.u9_4 = br.ReadByte();
                        this.MapProps.u9_5 = br.ReadByte();
                        this.MapProps.u9_6 = br.ReadByte();
                        this.MapProps.u10 = br.ReadSingle();
                        this.MapProps.u11 = br.ReadSingle();
                        this.MapProps.u12 = br.ReadInt32();
                        this.MapProps.showTerrain = br.ReadBoolean();

                        br.BaseStream.Position = this.header.dwPropOffset;
                        this.propsegments = new SEGMENTS_TABLE_STRUCTURE[4096];
                        int dataoffset = br.ReadInt32();
                        int segment = 0;
                        int cnt = 0;
                        while (segment < 4096)
                        {
                            br.BaseStream.Position = dataoffset;
                            cnt = br.ReadInt32();
                            propsegments[segment]=new SEGMENTS_TABLE_STRUCTURE(segment,dataoffset,cnt);
                            if (cnt > 0)
                            {
                                propsegments[segment].props = new PROPS_TABLE_STRUCTURE[cnt];
                                for (int i = 0; i < cnt; i++)
                                {
                                    propsegments[segment].props[i] = new PROPS_TABLE_STRUCTURE(segment, br.ReadInt32(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadSingle(),
                                    br.ReadInt16(),
                                    br.ReadBoolean(),
                                    br.ReadSingle(),
                                    br.ReadInt16());
                                    
                                }
                            }
                            segment++;
                            br.BaseStream.Position = this.header.dwPropOffset+(segment*4);
                            dataoffset = br.ReadInt32();
                        }
                        this.terrain = new TerrainSegments();
                        this.terrain.segments = new NFM_SEGMENTHEADER_V11[4096];
                        br.BaseStream.Position = this.header.dwTerrainSegmentOffset;
                        segment = 0;
                        while (segment < 4096)
                        {
                            this.terrain.segments[segment] = new NFM_SEGMENTHEADER_V11();
                            this.terrain.segments[segment].segment = segment;
                            this.terrain.segments[segment].dwVersion = br.ReadUInt32();
                            this.terrain.segments[segment].tile1 = br.ReadUInt16();
                            
                            this.terrain.segments[segment].tile2 = br.ReadUInt16();
                            this.terrain.segments[segment].tile3 = br.ReadUInt16();
                            this.terrain.segments[segment].vertices = new NFM_VERTEXSTRUCT_V11[36];
                            for (int i = 0; i < 36; i++)
                            {
                                this.terrain.segments[segment].vertices[i] = new NFM_VERTEXSTRUCT_V11();
                                this.terrain.segments[segment].vertices[i].fHeight = br.ReadSingle();
                       
                           
                                this.terrain.segments[segment].vertices[i].wFillBits1 = br.ReadUInt32();
                                this.terrain.segments[segment].vertices[i].wFillBits2 = br.ReadUInt32();
                                this.terrain.segments[segment].vertices[i].wAttribute = br.ReadUInt64();
                                this.terrain.segments[segment].vertices[i].color1 = br.ReadByte();
                                this.terrain.segments[segment].vertices[i].color2 = br.ReadByte();
                                this.terrain.segments[segment].vertices[i].color3 = br.ReadByte();
                            }
                            segment++;

                        }
                        br.BaseStream.Position = this.header.dwGrassColonyOffset;
                        int grasscnt = br.ReadInt32();
                        if (grasscnt > 0)
                        {
                            this.grass = new SpeedGrass[grasscnt];
                            for (int i = 0; i < grasscnt; i++)
                            {
                                this.grass[i] = new SpeedGrass();
                                this.grass[i].nPolygonID = br.ReadInt32();
                                this.grass[i].fDensity = br.ReadSingle();
                                this.grass[i].fDisTribution = br.ReadSingle();
                                this.grass[i].fSize = br.ReadSingle();
                                this.grass[i].fHeightP = br.ReadSingle();
                                this.grass[i].fHeightM = br.ReadSingle();
                                this.grass[i].Color1 = br.ReadByte();
                                this.grass[i].Color2 = br.ReadByte();
                                this.grass[i].Color3 = br.ReadByte();
                                this.grass[i].Color4 = br.ReadByte();
                                this.grass[i].fColorRatio = br.ReadSingle();
                                this.grass[i].fColorTone = br.ReadSingle();
                                this.grass[i].fChroma = br.ReadSingle();
                                this.grass[i].fBrightness = br.ReadSingle();
                                this.grass[i].fCombinationRatio = br.ReadSingle();
                                this.grass[i].fWindReaction = br.ReadSingle();
                                this.grass[i].cnt_filename = br.ReadInt32();
                                this.grass[i].file_name = System.Text.Encoding.GetEncoding(949).GetString(br.ReadBytes(this.grass[i].cnt_filename));
                                this.grass[i].utype = br.ReadInt32();
                                this.grass[i].cnt_coords = br.ReadInt32();
                                this.grass[i].coords = new PointF[this.grass[i].cnt_coords];
                                for (int j = 0; j < this.grass[i].cnt_coords; j++)
                                {
                                    this.grass[i].coords[j] = new PointF();
                                    this.grass[i].coords[j].X = (br.ReadUInt32())/(float)5.25;
                                    this.grass[i].coords[j].Y = (br.ReadUInt32())/(float)5.25;

                                }
                            }
                        }
                        br.BaseStream.Position = this.header.dwEventAreaOffset;
                        int eventcnt = br.ReadInt32();
                        if (eventcnt > 0)
                        {
                            this.events = new StructNFE[eventcnt];
                            for (int i = 0; i < eventcnt; i++)
                            {
                                this.events[i] = new StructNFE();
                                this.events[i] = new StructNFE();
                                this.events[i].id = br.ReadInt32();
                                this.events[i].type = br.ReadInt32();
                                this.events[i].count_coords = br.ReadInt32();
                                this.events[i].coords = new PointF[this.events[i].count_coords + 1];
                                for (int j = 0; j < this.events[i].count_coords; j++)
                                {
                                    int x = br.ReadInt32();
                                    int y = mirrory(br.ReadInt32());
                                    this.events[i].coords[j].X = x;
                                    this.events[i].coords[j].Y = y;
                                    this.events[i].coord = this.events[i].coord + j + ". (" + ((x * 5.25) + Hexcnv.GetCoords(this.filename, 1)) + ", " + (((3072 - y) * 5.25) + Hexcnv.GetCoords(this.filename, 2)) + ")";
                                    
                                }
                               
                            }
                        }

                        br.BaseStream.Position = this.header.dwVectorAttrOffset;
                        int vectorcnt = br.ReadInt32();
                        if (vectorcnt > 0)
                        {
                            this.vectordata = new VectorAttrib[vectorcnt];
                            for (int i = 0; i < vectorcnt; i++)
                            {
                                this.vectordata[i] = new VectorAttrib();
                                this.vectordata[i].cnt = br.ReadInt32();
                                if (this.vectordata[i].cnt > 0)
                                {
                                    this.vectordata[i].data = new VectorData[this.vectordata[i].cnt];
                                    for (int j=0;j<this.vectordata[i].cnt;j++)
                                    {
                                        this.vectordata[i].data[j]=new VectorData();
                                        this.vectordata[i].data[j].x=br.ReadInt32();
                                        this.vectordata[i].data[j].y=br.ReadInt32();
                                    }
                                }
                            }
                        }
                        this.calculateOffsets();
                        
                    }catch{
                        this.error = true;
                        this.MapImg = new Bitmap(3072, 3072);
                        this.check = false;
                    }

                }

            }
            catch
            {
                this.error = true;
                this.MapImg = new Bitmap(3072, 3072);
                this.check = false;
            }
        }

        private void drawMapImgVect()
        {
            try{
            
                
                for (int i = 0; i < this.vectordata.Length; i++)
                {
                    
                    if (this.vectordata[i].data.Length > 0)
                    {
                        Point[] points = new Point[this.vectordata[i].data.Length+1];
                        for (int j = 0; j < this.vectordata[i].data.Length;j++ )
                        {
                            points[j] = new Point();
                            points[j].X = this.vectordata[i].data[j].x;
                            points[j].Y = mirrory(this.vectordata[i].data[j].y);
                        }
                        points[this.vectordata[i].data.Length] = new Point();
                        points[this.vectordata[i].data.Length].X = this.vectordata[i].data[0].x;
                        points[this.vectordata[i].data.Length].Y =  mirrory(this.vectordata[i].data[0].y);
                        g.DrawLines(new Pen(Color.Gray, 2), points);
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

        private void drawMapImgGrass()
        {
            try
            {

                if (this.grass != null)
                {
                    for (int i = 0; i < this.grass.Length; i++)
                    {

                        if (this.grass[i].coords.Length > 0)
                        {
                            Point[] points = new Point[this.grass[i].coords.Length];
                            for (int j = 0; j < this.grass[i].coords.Length; j++)
                            {
                                points[j] = new Point();
                                points[j].X = (int)this.grass[i].coords[j].X;
                                points[j].Y = mirrory((int)this.grass[i].coords[j].Y);
                            }
                            g.DrawLines(new Pen(Color.LawnGreen, 2), points);
                        }
                    }

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

        public Bitmap mergeMapImgVect(Bitmap Map)
        {
            if (this.check == true)
            {
                this.MapImg = Map;
                g = Graphics.FromImage(Map);
                drawMapImgVect();
                return this.MapImg;
            }
            else
            {
                return Map;
            }
        }

        public Bitmap mergeMapImgGrass(Bitmap Map)
        {
            if (this.check == true)
            {
                this.MapImg = Map;
                g = Graphics.FromImage(Map);
                drawMapImgGrass();
                return this.MapImg;
            }
            else
            {
                return Map;
            }
        }

        public void saveNFMData()
        {
            this.calculateOffsets();
            this.saveData();
        }

        private void saveData() 
        {
            try
            {
                FileStream stream;
                if (File.Exists(this.fullpath))
                {
                    File.Copy(this.fullpath, Path.GetDirectoryName(this.fullpath) + "\\" + Path.GetFileNameWithoutExtension(this.fullpath) + "_nfm.bak" + DateTime.Now.ToString("ddMMMyyyyHHmmss"));
                    stream = new FileStream(this.fullpath, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    stream = new FileStream(this.fullpath, FileMode.Create, FileAccess.ReadWrite);
                }
                BinaryWriter bw = new BinaryWriter(stream, Encoding.Default);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                //Header
                
                bw.Write(enc.GetBytes(this.header.szSign));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header.dwVersion)));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header._dwMapPropertiesOffset)));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header._dwTerrainSegmentOffset)));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header._dwPropOffset)));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header._dwVectorAttrOffset)));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header._dwWaterOffset)));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header._dwGrassColonyOffset)));
                bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.header._dwEventAreaOffset)));
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.header.nTileCountPerSegment)));
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.header.nSegmentCountPerMap)));
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.header.fTileLength)));

                //MapProperties
                bw.Write(this.MapProps.u1_1);
                bw.Write(this.MapProps.u1_2);
                bw.Write(this.MapProps.u1_3);
                bw.Write(this.MapProps.u1_4);
                bw.Write(this.MapProps.u1_5);
                bw.Write(this.MapProps.u1_6);
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u2)));
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u3)));
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u4)));
                bw.Write(this.MapProps.u5_1);
                bw.Write(this.MapProps.u5_2);
                bw.Write(this.MapProps.u5_3);
                bw.Write(this.MapProps.u5_4);
                bw.Write(this.MapProps.u5_5);
                bw.Write(this.MapProps.u5_6);
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u6)));
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u7)));
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u8)));
                bw.Write(this.MapProps.u9_1);
                bw.Write(this.MapProps.u9_2);
                bw.Write(this.MapProps.u9_3);
                bw.Write(this.MapProps.u9_4);
                bw.Write(this.MapProps.u9_5);
                bw.Write(this.MapProps.u9_6);
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u10)));
                bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.MapProps.u11)));
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.MapProps.u12)));
                bw.Write(BitConverter.GetBytes(Convert.ToBoolean(this.MapProps.showTerrain)));
                
                //Terrain

                for (int i = 0; i < 4096; i++)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.terrain.segments[i].dwVersion)));
                    bw.Write(BitConverter.GetBytes(Convert.ToUInt16(this.terrain.segments[i].tile1)));
                    bw.Write(BitConverter.GetBytes(Convert.ToUInt16(this.terrain.segments[i].tile2)));
                    bw.Write(BitConverter.GetBytes(Convert.ToUInt16(this.terrain.segments[i].tile3)));
                    for (int j = 0; j < 36; j++)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.terrain.segments[i].vertices[j].fHeight)));
                        bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.terrain.segments[i].vertices[j].wFillBits1)));
                        bw.Write(BitConverter.GetBytes(Convert.ToUInt32(this.terrain.segments[i].vertices[j].wFillBits2)));
                        bw.Write(BitConverter.GetBytes(Convert.ToUInt64(this.terrain.segments[i].vertices[j].wAttribute)));
                        bw.Write(this.terrain.segments[i].vertices[j].color1);
                        bw.Write(this.terrain.segments[i].vertices[j].color2);
                        bw.Write(this.terrain.segments[i].vertices[j].color3);
                    }
                }

                //Props

                int offset = this.header._dwPropOffset + (4096 * 4);
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(offset)));

                //Offsets
                for (int i = 1; i < 4096; i++)
                {
                    if (this.propsegments[i - 1].props != null)
                    {
                        offset = offset + (this.propsegments[i - 1].props.Length * 49);
                    }
                    offset = offset + 8;                    
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(offset)));
                }

                //Data

                for (int i = 0; i < 4096; i++)
                {
                    if (this.propsegments[i].props != null)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.propsegments[i].props.Length)));
                        for (int j = 0; j < this.propsegments[i].props.Length; j++)
                        {
                            bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.propsegments[i].props[j]._prop_index)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._x)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._y)));

                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._z)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._rotate_x)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._rotate_y)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._rotate_z)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._scale_x)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._scale_y)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._scale_z)));
                            bw.Write(BitConverter.GetBytes(Convert.ToInt16(this.propsegments[i].props[j]._prop_num)));
                            bw.Write(BitConverter.GetBytes(Convert.ToBoolean(this.propsegments[i].props[j]._height_locked)));
                            bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.propsegments[i].props[j]._lock_height)));
                            bw.Write(BitConverter.GetBytes(Convert.ToInt16(this.propsegments[i].props[j]._texture_group_index)));
                        }
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(0)));
                    }
                    else
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(0)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(0)));
                    }
                    
                }

                //Vector
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.vectordata.Length)));
                for (int i = 0; i < this.vectordata.Length; i++)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.vectordata[i].cnt)));
                    for (int j = 0; j < this.vectordata[i].cnt; j++)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.vectordata[i].data[j].x)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.vectordata[i].data[j].y)));
                    }
                }
                

                //Water (empty)
                bw.Write(BitConverter.GetBytes(Convert.ToInt32(0)));

                //Grass
                if (this.grass != null)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.grass.Length)));
                    for (int i = 0; i < this.grass.Length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.grass[i].nPolygonID)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fDensity)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fDisTribution)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fSize)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fHeightP)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fHeightP)));
                        bw.Write(this.grass[i].Color1);
                        bw.Write(this.grass[i].Color2);
                        bw.Write(this.grass[i].Color3);
                        bw.Write(this.grass[i].Color4);
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fColorRatio)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fColorTone)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fChroma)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fBrightness)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fCombinationRatio)));
                        bw.Write(BitConverter.GetBytes(Convert.ToSingle(this.grass[i].fWindReaction)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.grass[i].cnt_filename)));
                        bw.Write(enc.GetBytes(this.grass[i].file_name));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.grass[i].utype)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.grass[i].cnt_coords)));
                        for (int j = 0; j < this.grass[i].cnt_coords; j++)
                        {
                            bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.grass[i].coords[j].X*5.25)));
                            bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.grass[i].coords[j].Y*5.25)));

                        }

                    }
                }
                else
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(0)));
                }

                //EventAreas

                if (this.events != null)
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events.Length)));
                    for (int i = 0; i < this.events.Length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].id)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].type)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].count_coords)));
                        for(int j=0;j<this.events[i].count_coords;j++)
                        {
                            bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].coords[j].X)));
                            bw.Write(BitConverter.GetBytes(mirrory(Convert.ToInt32(this.events[i].coords[j].Y))));
                        }
                        /*bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i])));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u4)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u5)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u6)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u7)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u8)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u9)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u10)));
                        bw.Write(BitConverter.GetBytes(Convert.ToInt32(this.events[i].u11)));*/
                    }
                }
                else
                {
                    bw.Write(BitConverter.GetBytes(Convert.ToInt32(0)));
                }
                stream.Close();
                bw.Close();

            }
            catch
            {

            }
        }

        private int getMaxPropIndex()
        {
            int maxindex = 0;
            for (int i = 0; i < 4096; i++)
            {
                if (this.propsegments[i]._prop_count > 0)
                {
                    for (int j = 0; j < this.propsegments[i]._prop_count; j++)
                    {
                        if (maxindex < this.propsegments[i].props[j]._prop_index)
                            maxindex = this.propsegments[i].props[j]._prop_index;
                    }
                }
            }
            return maxindex;
        }

        public void addProp(PROPS_TABLE_STRUCTURE tmp)
        {
            tmp._prop_index = getMaxPropIndex();
            if (this.propsegments[tmp._segment_number]._prop_count > 0)
            {
                this.propsegments[tmp._segment_number]._prop_count++;
                PROPS_TABLE_STRUCTURE[] proptmp = new PROPS_TABLE_STRUCTURE[this.propsegments[tmp._segment_number]._prop_count];
                for (int i = 0; i < this.propsegments[tmp._segment_number]._prop_count - 1; i++)
                {
                    proptmp[i] = new PROPS_TABLE_STRUCTURE(this.propsegments[tmp._segment_number].props[i]);
                }
                proptmp[proptmp.Length - 1] = tmp;
                this.propsegments[tmp._segment_number].props = proptmp;
            }
            else
            {
                this.propsegments[tmp._segment_number]._prop_count++;
                this.propsegments[tmp._segment_number].props = new PROPS_TABLE_STRUCTURE[1];
                this.propsegments[tmp._segment_number].props[0] = new PROPS_TABLE_STRUCTURE(tmp);
            }
        }

        public void editTile(int seg, int tilenumber, NFM_VERTEXSTRUCT_V11 tile,bool[] changeVals)
        {
            if (!changeVals[0])
                tile.fHeight = this.terrain.segments[seg].vertices[tilenumber].fHeight;
            if (!changeVals[3])
                tile.wAttribute = this.terrain.segments[seg].vertices[tilenumber].wAttribute;
            if (!changeVals[1])
            {
                tile.wFillBits1 = this.terrain.segments[seg].vertices[tilenumber].wFillBits1;
                tile.wFillBits2 = this.terrain.segments[seg].vertices[tilenumber].wFillBits2;
            }
            if (!changeVals[2])
            {
                tile.color1 = this.terrain.segments[seg].vertices[tilenumber].color1;
                tile.color2 = this.terrain.segments[seg].vertices[tilenumber].color2;
                tile.color3 = this.terrain.segments[seg].vertices[tilenumber].color3;
            }
                
            this.terrain.segments[seg].vertices[tilenumber] = tile;
        }

        public void updateTerrainTextureData(int seg, uint dwVersion, uint tile1,uint tile2,uint tile3,bool[] changeVal )
        {
            if (changeVal[0])
                this.terrain.segments[seg].dwVersion = dwVersion;
            if (changeVal[1])
                this.terrain.segments[seg].tile1 = tile1;
            if (changeVal[2])
                this.terrain.segments[seg].tile2 = tile2;
            if (changeVal[3])
                this.terrain.segments[seg].tile3 = tile3;
        }
    }
}
