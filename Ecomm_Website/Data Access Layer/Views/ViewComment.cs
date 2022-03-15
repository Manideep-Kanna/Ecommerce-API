using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Views
{
    public class ViewComment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public string Comment_Text { get; set; }
       public  List<ViewComment>? Replies { get; set; }
    }
}
