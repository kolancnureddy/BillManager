using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class Post_Put_BillDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public String Description { get; set; }

        public List<int> FriendIds { get; set; }
    }
}
