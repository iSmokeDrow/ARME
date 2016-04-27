using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;


namespace ARME
{
    class MAPJPG
    {
        public MAPJPG(string path)
        {
            this.directory = Path.GetDirectoryName(path) + "/";
            this.filepartname = Path.GetFileNameWithoutExtension(path);
            this.filename = new string[64];
            this.filename[0] = directory + "v256_" + filepartname + "_0_0.jpg";
            this.filenameasc = directory + "v256_" + filepartname + "_0_0(ascii).jpg";
            this.loadMapImg();
        }

        public string directory
        {
            get;
            set;
        }

        public string filepartname
        {
            get;
            set;
        }

        public string[] filename
        {
            get;
            set;
        }

        public string filenameasc
        {
            get;
            set;
        }

        public bool check
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

        private void loadMapImg()
        {
            this.MapImg = new Bitmap(3072,3072);
            g = Graphics.FromImage(this.MapImg);
            string path = "";
            int num = 0;
            int curwidth = 0;
            int curheight = 0;
            int[,] xpoints = new int[8, 8];
            int[,] ypoints = new int[8, 8];
            if (File.Exists(this.filename[0])||File.Exists(this.filenameasc))
            {
                try
                {
                    string ascii = "";
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            this.filenameasc = "v256_" + filepartname + "_" + i + "_" + j + "(ascii).jpg";
                            if (File.Exists(this.directory + filenameasc))
                            {
                                ascii = "(ascii)";
                            }
                            else
                            {
                                ascii = "";
                            }
                            this.filename[num] = "v256_" + filepartname + "_" + i + "_" + j + ascii + ".jpg";
                            path = this.directory + this.filename[num];
                            Image curimg = Image.FromFile(path);
                            if (j > 0)
                            {
                                curwidth = xpoints[i, j - 1];
                                xpoints[i, j] = xpoints[i, j - 1] + (curimg.Width + 128);
                            }
                            else
                            {
                                curwidth = 0;
                                xpoints[i, j] = (curimg.Width + 128);
                            }
                            if (i > 0)
                            {
                                curheight = ypoints[i - 1, j];
                                ypoints[i, j] = ypoints[i - 1, j] + (curimg.Height + 128);
                            }
                            else
                            {
                                curheight = 0;
                                ypoints[i, j] = (curimg.Height + 128);
                            }

                            g.DrawImage(curimg, new Rectangle(curwidth, curheight, curimg.Width + 128, curimg.Height + 128));
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
        }




    }
}
