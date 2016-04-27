using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ARME.MapFileRes
{
    public class StructNFE
    {
        public int id
        {
            get;
            set;
        }

        public int type
        {
            get;
            set;
        }

        public int count_coords
        {
            get;
            set;
        }

        public PointF[] coords
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
}
