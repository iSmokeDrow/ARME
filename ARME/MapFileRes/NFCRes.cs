using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace ARME.MapFileRes
{
    public class StructNFC
    {

        public int id
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public int cnt_name
        {
            get;
            set;
        }


        public int type
        {
            get;
            set;
        }

        public int unknown1
        {
            get;
            set;
        }

        public int unknown2
        {
            get;
            set;
        }

        public int unknown3
        {
            get;
            set;
        }

        public int unknown4
        {
            get;
            set;
        }

                

        public int cnt_script
        {
            get;
            set;
        }

        public string script
        {
            get;
            set;
        }


        public int cnt_coords
        {
            get;
            set;
        }

        public NFCCoords[] coords
        {
            get;
            set;
        }

        public string coord
        {
            get;
            set;
        }

    }

    public class NFCCoords
    {
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
}
