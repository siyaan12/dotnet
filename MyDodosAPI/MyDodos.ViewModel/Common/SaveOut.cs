using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.ViewModel.Common
{
    public class SaveOut
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public bool? IsRequired { get; set; }
    }
}