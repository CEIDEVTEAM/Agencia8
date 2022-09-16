using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLProcess.Configuration
{
    public class Documents
    {
        public const string Position = "Documents";
        public List<Doc> ColDocuments { get; set; }

        public class Doc
        {
            public string FileName { get; set; }
            public string FolderRoute { get; set; }
        }
    }
}
