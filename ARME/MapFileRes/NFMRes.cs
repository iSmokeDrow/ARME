using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ARME.MapFileRes
{
    class StructNFM
    {
        public String szSign;
        public UInt32 dwVersion;
        public UInt32 dwMapPropertiesOffset;
        public UInt32 dwTerrainSegmentOffset;
        public UInt32 dwPropOffset;
        public UInt32 dwVectorAttrOffset;
        public UInt32 dwWaterOffset;
        public UInt32 dwGrassColonyOffset;
        public UInt32 dwEventAreaOffset;
        public int _dwMapPropertiesOffset;
        public int _dwTerrainSegmentOffset;
        public int _dwPropOffset;
        public int _dwVectorAttrOffset;
        public int _dwWaterOffset;
        public int _dwGrassColonyOffset;
        public int _dwEventAreaOffset;
        public Int32 nTileCountPerSegment;
        public Int32 nSegmentCountPerMap;
        public Single fTileLength;

    }

    public class SEGMENTS_TABLE_STRUCTURE{
        public int _prop_count
        {
            get;
            set;
        }
        public int _segment
        {
            get;
            set;
        }
        public int _offset
        {
            get;
            set;
        }
        public PROPS_TABLE_STRUCTURE[] props
        {
            get;
            set;
        }
        public SEGMENTS_TABLE_STRUCTURE(Int32 SEGMENT, Int32 OFFSET, Int32 PROP_COUNT){

            _segment = SEGMENT;
            _offset = OFFSET;
            _prop_count = PROP_COUNT;

        }
    }

        public class PROPS_TABLE_STRUCTURE{
        
        public int _segment_number
            {
                get;
                set;
            }
        public int _prop_index
        {
            get;
            set;
        }
        public float _x
        {
            get;
            set;
        }
        public float _y
        {
            get;
            set;
        }
        public float _z
        {
            get;
            set;
        }
        public float _rotate_x
        {
            get;
            set;
        }
        public float _rotate_y
        {
            get;
            set;
        }
        public float _rotate_z
        {
            get;
            set;
        }
        public float _scale_x
        {
            get;
            set;
        }
        public float _scale_y
        {
            get;
            set;
        }
        public float _scale_z
        {
            get;
            set;
        }
        public int _prop_num
        {
            get;
            set;
        }
        public bool _height_locked
        {
            get;
            set;
        }
        public float _lock_height
        {
            get;
            set;
        }
        public int _texture_group_index
        {
            get;
            set;
        }
        public PROPS_TABLE_STRUCTURE(PROPS_TABLE_STRUCTURE tmp)
        {
            _segment_number=tmp._segment_number;
            _prop_index=tmp._prop_index;
            _x=tmp._x;
            _y=tmp._y;
            _z=tmp._z;
            _rotate_x=tmp._rotate_x;
            _rotate_y=tmp._rotate_y;
            _rotate_z=tmp._rotate_z;
            _scale_x=tmp._scale_x;
            _scale_y=tmp._scale_y;
            _scale_z=tmp._scale_z;
            _prop_num=tmp._prop_num;
            _height_locked=tmp._height_locked;
            _lock_height=tmp._lock_height;
            _texture_group_index = tmp._texture_group_index;
        }
        public PROPS_TABLE_STRUCTURE( 
         Int32 SEGMENT_NUMBER, 
         Int32 PROP_INDEX, 
         Single X, 
         Single Y, 
         Single Z, 
         Single ROTATE_X, 
         Single ROTATE_Y, 
         Single ROTATE_Z, 
         Single SCALE_X, 
         Single SCALE_Y, 
         Single SCALE_Z, 
         Int16 PROP_NUM, 
         Boolean HEIGHT_LOCKED, 
         Single LOCK_HEIGHT,    
         Int16 TEXTURE_GROUP_INDEX){

            _segment_number = SEGMENT_NUMBER;
            _prop_index = PROP_INDEX;
            _x = X;
            _y = Y;
            _z = Z;
            _rotate_x = ROTATE_X;
            _rotate_y = ROTATE_Y;
            _rotate_z = ROTATE_Z;
            _scale_x = SCALE_X;
            _scale_y = SCALE_Y;
            _scale_z = SCALE_Z;
            _prop_num = PROP_NUM;
            _height_locked = HEIGHT_LOCKED;
            _lock_height = LOCK_HEIGHT;
            _texture_group_index = TEXTURE_GROUP_INDEX;

        }     
  }
  public class TerrainSegments
  {
      public NFM_SEGMENTHEADER_V11[] segments //matrix of 64x64 of NFM_SEGMENTHEADER_V11 structure 4096
      {
                get;
                set;
      }
  }

public class NFM_SEGMENTHEADER_V11  //information about one segment
{
    public int segment
    {
        get;
        set;
    }
  public uint dwVersion
  {
      get;
      set;
  }
  public uint tile1 //3 Int16
  {
      get;
      set;      
  }
  public uint tile2 //3 Int16
  {
      get;
      set;
  }
  public uint tile3 //3 Int16
  {
      get;
      set;
  }
  public NFM_VERTEXSTRUCT_V11[] vertices //36
  {
      get;
      set;
  }
}

public class NFM_VERTEXSTRUCT_V11
{
  public float fHeight  //the height of the vertex
  {
      get;
      set;
  }
  public uint wFillBits1 //2
  {
      get;
      set;
  }
  public uint wFillBits2 //2
  {
      get;
      set;
  }
  public ulong wAttribute //int64
  {
      get;
      set;
  }
  public byte color1
  {
      get;
      set;
  }
  public byte color2
  {
      get;
      set;
  }
  public byte color3
  {
      get;
      set;
  }
}

public class SpeedGrass
{
    public int nPolygonID
    {
        get;
        set;
    }
    public float fDensity
    {
        get;
        set;
    }
    public float fDisTribution
    {
        get;
        set;
    }
    public float fSize
    {
        get;
        set;
    }
    public float fHeightP
    {
        get;
        set;
    }
    public float fHeightM
    {
        get;
        set;
    }
    public byte Color1
    {
        get;
        set;
    }
    public byte Color2
    {
        get;
        set;
    }
    public byte Color3
    {
        get;
        set;
    }
    public byte Color4
    {
        get;
        set;
    }
    public float fColorRatio
    {
        get;
        set;
    }
    public float fColorTone
    {
        get;
        set;
    }
    public float fChroma
    {
        get;
        set;
    }
    public float fBrightness
    {
        get;
        set;
    }
    public float fCombinationRatio
    {
        get;
        set;
    }
    public float fWindReaction
    {
        get;
        set;
    }
    public int cnt_filename
    {
        get;
        set;
    }
    public string file_name
    {
        get;
        set;
    }
    public int utype
    {
        get;
        set;
    }
    public int cnt_coords
    {
        get;
        set;
    }
    public PointF[] coords
    {
        get;
        set;
    }
}

public class gcoords
{
    public int top
    {
        get;
        set;
    }
    public int bottom
    {
        get;
        set;
    }
    public int left
    {
        get;
        set;
    }
    public int right
    {
        get;
        set;
    }
}

public class EventData
{
    public int u1
    {
        get;
        set;
    }
    public int u2
    {
        get;
        set;
    }
    public int u3
    {
        get;
        set;
    }
    public int u4
    {
        get;
        set;
    }
    public int u5
    {
        get;
        set;
    }
    public int u6
    {
        get;
        set;
    }
    public int u7
    {
        get;
        set;
    }
    public int u8
    {
        get;
        set;
    }
    public int u9
    {
        get;
        set;
    }
    public int u10
    {
        get;
        set;
    }
    public int u11
    {
        get;
        set;
    }
}

public class VectorData
{
    public int x
    {
        get;
        set;
    }
    public int y
    {
        get;
        set;
    }
}

public class VectorAttrib
{
    public int cnt{
        get;
        set;
    }
    public VectorData[] data
    {
        get;
        set;
    }
}

public class KColor
{
    public byte b
    {
        get;
        set;
    }
    public byte g
    {
        get;
        set;
    }
    public byte r
    {
        get;
        set;
    }
    public byte a
    {
        get;
        set;
    }
}
public class MapProps
{
    
    public byte u1_1
    {
        get;
        set;
    }
    public byte u1_2
    {
        get;
        set;
    }
    public byte u1_3
    {
        get;
        set;
    }
    public byte u1_4
    {
        get;
        set;
    }
    public byte u1_5
    {
        get;
        set;
    }
    public byte u1_6
    {
        get;
        set;
    }
    public float u2
    {
        get;
        set;
    }
    public float u3
    {
        get;
        set;
    }
    public float u4
    {
        get;
        set;
    }
    public byte u5_1
    {
        get;
        set;
    }
    public byte u5_2
    {
        get;
        set;
    }
    public byte u5_3
    {
        get;
        set;
    }
    public byte u5_4
    {
        get;
        set;
    }
    public byte u5_5
    {
        get;
        set;
    }
    public byte u5_6
    {
        get;
        set;
    }
    public float u6
    {
        get;
        set;
    }
    public float u7
    {
        get;
        set;
    }
    public float u8
    {
        get;
        set;
    }
    public byte u9_1
    {
        get;
        set;
    }
    public byte u9_2
    {
        get;
        set;
    }
    public byte u9_3
    {
        get;
        set;
    }
    public byte u9_4
    {
        get;
        set;
    }
    public byte u9_5
    {
        get;
        set;
    }
    public byte u9_6
    {
        get;
        set;
    }
    public float u10
    {
        get;
        set;
    }
    public float u11
    {
        get;
        set;
    }
    public int u12
    {
        get;
        set;
    }
    public bool showTerrain
    {
        get;
        set;
    }

}

}
