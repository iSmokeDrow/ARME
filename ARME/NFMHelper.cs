using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ARME.MapFileRes;

namespace ARME
{
    public partial class NFMHelper : Form
    {
        private RappelzMapEditor main = null;
        private bool props = false;
        private bool vertex = false;
        private bool vector = false;
        private bool grass = false;
        private PointF[] gdata;
        private PROPS_TABLE_STRUCTURE[] pdata;
        private NFM_VERTEXSTRUCT_V11[] verdata;
        private VectorData[] vecdata;


        public NFMHelper(RappelzMapEditor res, PROPS_TABLE_STRUCTURE[] props)
        {
            InitializeComponent();
            pdata = props;
            this.dg_nfaprops.DataSource = pdata;
            this.dg_nfaprops.Refresh();
            this.main = res;
            this.props = true;
            
        }

        public NFMHelper(RappelzMapEditor res, NFM_VERTEXSTRUCT_V11[] props)
        {
            InitializeComponent();
            this.verdata = props;
            this.dg_nfaprops.DataSource = verdata;
            this.dg_nfaprops.Refresh();
            this.main = res;
            this.vertex = true;
        }

        public NFMHelper(RappelzMapEditor res, VectorData[] props)
        {
            InitializeComponent();
            this.vecdata = props;
            this.dg_nfaprops.DataSource = vecdata ;
            this.dg_nfaprops.Refresh();
            this.main = res;
            this.vector = true;
        }

        public NFMHelper(RappelzMapEditor res, PointF[] props)
        {
            InitializeComponent();
            this.grass = true;
            this.gdata = props;
            this.dg_nfaprops.DataSource = gdata;
            this.dg_nfaprops.Refresh();
            this.main = res;
        }

        private void NFMHelper_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.vector)
            {
                main.replaceNFMVector(this.vecdata);
                this.vector = false;
            }
            if (this.props)
            {
                main.replaceNFMProp(this.pdata);
                this.props = false;
            }
            if (this.vertex)
            {
                main.replaceNFMTerrain(this.verdata);
                this.vertex = false;
            }
            if (this.grass)
            {
                main.replaceNFMGrass(this.gdata);
                this.grass = false;
            }
            
        }
    }
}
