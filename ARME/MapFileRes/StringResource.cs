using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ARME
{
    class StringResource
    {
        public StringResource(string path)
        {
            this.fullpath = path;
            load_data();
        }

        private string fullpath;
        private int cnt;
        public bool check = false;
        public string errortxt = "";
        public bool error = false;
        private List<StringResourceRes> Fieldprops = new List<StringResourceRes>();
        private List<StringResourceRes> Worldlocations = new List<StringResourceRes>();
        private List<StringResourceRes> NPCnames = new List<StringResourceRes>();

        private void load_data()
        {
            try
            {


                FileStream fileStream = File.Open(this.fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.Default);
                binaryReader.ReadChars(128);
                this.cnt=binaryReader.ReadInt32();
                for(int i=1; i<=this.cnt; i++)
                {
                    int name_length = binaryReader.ReadInt32();
                    int value_length = binaryReader.ReadInt32();
                    string name = new string(binaryReader.ReadChars(name_length)).Replace("\x00", "");
                    string value = new string(binaryReader.ReadChars(value_length)).Replace("\x00", "");
                    int code = binaryReader.ReadInt32();
                    int grp = binaryReader.ReadInt32();
                    if (name.Contains("name_prop"))
                    {
                        StringResourceRes tmp = new StringResourceRes();
                        tmp.value = value;
                        tmp.code = code;
                        this.Fieldprops.Add(tmp);
                    }

                    if (name.Contains("name_worldlocation"))
                    {
                        StringResourceRes tmp = new StringResourceRes();
                        tmp.value = value;
                        tmp.code = code;
                        this.Worldlocations.Add(tmp);
                    }

                    if (name.Contains("npc_title"))
                    {
                        StringResourceRes tmp = new StringResourceRes();
                        tmp.value = value;
                        tmp.code = code;
                        this.NPCnames.Add(tmp);
                    }

                    binaryReader.ReadBytes(16);
                }
                binaryReader.Close();
                fileStream.Close();
                this.check = true;
                this.error = false;
            }
            catch
            {
                this.error = true;
                this.errortxt = "Fehler beim lesen von " + fullpath;
            }


        }


        public string get_Fname(int id)
        {
            try
            {
                if (this.error == false && check == true)
                {
                    StringResourceRes result = (from f in Fieldprops
                                                where f.code == id
                                                select f).FirstOrDefault();
                    return result.value;
                }
            }
            catch
            {
                return "empty string: " + id.ToString();
            }
            return "";
            
        }

        public string get_Wname(int id)
        {
            try
            {
                if (this.error == false && check == true)
                {
                    StringResourceRes result = (from f in Worldlocations
                                                where f.code == id
                                                select f).FirstOrDefault();
                    return result.value;
                }
            }
            catch
            {
                return "empty string: " + id.ToString();
            }
            return "";

        }

        public string get_NPCname(int id)
        {
            try
            {
                if (this.error == false && check == true)
                {
                    StringResourceRes result = (from f in NPCnames
                                                where f.code == id
                                                select f).FirstOrDefault();
                    return result.value;
                }
            }
            catch
            {
                return "empty string: " + id.ToString();
            }
            return "";

        }

    }
}
