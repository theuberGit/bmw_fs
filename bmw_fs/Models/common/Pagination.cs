using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.common
{
    public class Pagination
    {
        public Pagination()
        {
            this.page = 1;
            this.nextPage = 10;
            this.itemPerPage = 10;
        }
        public int itemPerPage { get; set; }
        public int nextPage { get; set; }
        public int page { get; set; }
        public int totalItemCount { get; set; }
        public int currentItem { get { return (page - 1) * itemPerPage + 1; } }
        public int currentItemEnd { get { return page * itemPerPage; } }
        public int jumpNextPage { get { return Math.Min(pageCount, page + nextPage); } }
        public int jumpPrevPage { get { return Math.Max(1, page - nextPage); } }
        public int lastPage { get { return (int)Math.Ceiling(1.0 * totalItemCount / itemPerPage); } }
        public int currentPage
        {
            get
            {
                int page = this.page;
                if (page < 1)
                {
                    page = 1;
                }
                int pageCount = this.pageCount;
                if (page > pageCount)
                {
                    page = pageCount;
                }
                return page;
            }
        }
        public int pageCount { get { return (totalItemCount - 1) / itemPerPage + 1; } }
        public int pageBegin { get { return ((currentPage - 1) / nextPage) * nextPage + 1; } }
        public int pageEnd
        {
            get
            {
                int pageCount = this.pageCount;
                int num = pageBegin + nextPage - 1;
                return Math.Min(pageCount, num);
            }
        }
    }
}