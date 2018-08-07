using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class BillDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public String Description { get; set; }

        public List<SplitBillDTO> FriendsSplit { get; set; }
    }
}
