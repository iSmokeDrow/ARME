using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ARME
{
    class FileIO
    {
        public FileIO(string path)
        {
            this.workingdir = path+"\\";
            loadexistingfiles();
        }

        public string workingdir
        {
            get;
            set;
        }

        public string[] files
        {
            get;
            set;
        }

        public string[] filenames
        {
            get;
            set;
        }

        private void loadexistingfiles()
        {
            this.files = Directory.GetFiles(this.workingdir, "*.nfa");
            loadfilenames();
        }

        private void loadfilenames()
        {
            this.filenames = new string[this.files.Length];
            for(int i=0;i<this.files.Length;i++)
                this.filenames[i]=Path.GetFileNameWithoutExtension(this.files[i]);
        }

        public string getfilepath(int id)
        {
            if (id < this.files.Length)
                return files[id];
            else
                return files[files.Length - 1];

        }

        public static void ExportHashed(string fullpath)
        {
            if (!Directory.Exists(Properties.Settings.Default.exportDir))
            {
                Directory.CreateDirectory(Properties.Settings.Default.exportDir);
            }

            /*Dumper Logic (DUSTIN)*/

            //File.Copy(fullpath, Properties.Settings.Default.exportDir + Dumper.RappelzNameCipher.Encrypt(Path.GetFileName(fullpath)), true);
        }

        public static void CompressDir(string fullpath)
        {


        }

    }
}
