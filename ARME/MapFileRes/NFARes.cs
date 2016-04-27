using System;
using System.Drawing;

namespace ARME.MapFileRes
{
    public class StructNFA
    {
        public int id
        {
            get;
            set;
        }

        public int coordcount
        {
            get;
            set;
        }

        public PointF[] points
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
