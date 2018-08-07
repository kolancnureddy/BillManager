using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class SplitBillDTO
    {
        public int Id { get; set; }
        public int BillId { get; set; }

        public int FriendId { get; set; }

        public String FriendName { get; set; }
        public decimal Amount { get; set; }
        public String Description { get; set; }
    }
}
