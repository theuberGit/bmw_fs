using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bmw_fs.Models.board
{
    public class Board : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        [AllowHtml]
        public String contents { get; set; }
        public DateTime regDate { get; set; }
        public String regId { get; set; }
        public DateTime updateDate { get; set; }
        public String updateId { get; set; }
        public IList<int> fileIdxs { get; set; }
        public IList<int> fileIdxs2 { get; set; }
    }
}