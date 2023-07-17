using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.ViewModel.ServerSearch
{
    public class ServerSearchable
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public string search_data { get; set; }
        public string  orderBy_Column { get; set; }
        public string order_By { get; set; }
        public int page_Size { get; set; }
        public int page_No { get; set; }
    }
}
