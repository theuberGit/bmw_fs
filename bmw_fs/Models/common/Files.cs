using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.common
{
    public class Files
    {
        public int fileIdx { get; set; }
        public int masterIdx { get; set; }
        public String originalFilename { get; set; }
        public String savedFilename { get; set; }
        public String type { get; set; }
        public IList<int> fileIdxs { get; set; }
        public DateTime regDate { get; set; }

        override
        public string ToString()
        {
            return
                (fileIdx > 0 ? "fileIdx : " + fileIdx + ", " : "") +
                (masterIdx > 0 ? "masterIdx : " + masterIdx + "," : "");
        }
    }
}