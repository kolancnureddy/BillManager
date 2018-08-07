using BillManager.Entities;
using BillManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.BusinessLogic
{
    public interface IBillHandler
    {
        Bill AddBill(Post_Put_BillDTO billDTO);
        List<BillDTO> GetAllBills();
        BillDTO GetBillById(int id);

    }
}
