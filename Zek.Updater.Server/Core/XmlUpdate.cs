using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Zek.Updater
{
    [Serializable]
    //[XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    //[XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class XmlUpdate
    {
        public XmlUpdate()
        {
            Extension = string.Empty;
            Files = new List<XmlFile>();
        }

        private string _appName = string.Empty;
        [XmlElement]
        public string AppName
        {
            get { return _appName; }
            set { _appName = value ?? string.Empty; }
        }


        //private string _appFolderName = string.Empty;
        //[XmlElement]
        //public string AppFolderName
        //{
        //    get { return _appFolderName; }
        //    set { _appFolderName = value ?? string.Empty; }
        //}


        private string _appExeName = string.Empty;
        [XmlElement]
        public string AppExeName
        {
            get { return _appExeName; }
            set { _appExeName = value ?? string.Empty; }
        }


        private string _version = string.Empty;
        [XmlElement]
        public string Version
        {
            get { return _version; }
            set { _version = value ?? string.Empty; ; }
        }


        private string _compressFolderName = string.Empty;
        [XmlElement]
        public string CompressFolderName
        {
            get { return _compressFolderName; }
            set { _compressFolderName = value ?? string.Empty; }
        }


        private string _compress = string.Empty;
        [XmlElement]
        public string Compress
        {
            get { return _compress; }
            set
            {
                _compress = value ?? string.Empty;

                Extension = ".zip";
                //switch (_compress.ToLowerInvariant())
                //{
                //    case "zip":
                //        Extension = ".zip";
                //        break;

                //    case "gzip":
                //        Extension = ".zip";
                //        break;

                //    case "7zip":
                //        Extension = ".zip";
                //        break;
                //}
            }
        }

        [XmlElement]
        public bool DeleteBin { get; set; }


        [XmlIgnore]
        public string Extension
        {
            get;
            private set;
        }


        [XmlElement("DeployDate")]
        public string XmlDeployDate
        {
            get
            {
                if (DeployDate == null) return null;
                return XmlConvert.ToString(DeployDate.Value, XmlDateTimeSerializationMode.Local);
            }
            set
            {
                DeployDate = !string.IsNullOrWhiteSpace(value) ? (DateTime?)XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Local) : null;
            }
        }
        [XmlIgnore]
        public DateTime? DeployDate { get; set; }

        [XmlElement]
        public List<XmlFile> Files { get; set; }



        [Serializable]
        public class XmlFile
        {
            public XmlFile()
            {
            }
            public XmlFile(string path, string file, string hash)
            {
                Path = path;
                File = file;
                Hash = hash;
            }

            [XmlIgnore]
            public string Path { get; set; }
            [XmlElement]
            public string File { get; set; }
            [XmlElement]
            public string Hash { get; set; }
        }
    }



}
