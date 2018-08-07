using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class FriendDTO
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public List<SplitBillDTO> SplitBills { get; set; } = new List<SplitBillDTO>();
    }
}
