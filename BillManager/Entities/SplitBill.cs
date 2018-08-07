using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Entities
{
    public class SplitBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }

        public int FriendId { get; set; }
        public Friend Friend { get; set; }
        
        public decimal Amount { get; set; }
    }
}
