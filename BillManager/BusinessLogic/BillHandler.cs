using BillManager.Entities;
using BillManager.Models;
using BillManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.BusinessLogic
{
    public class BillHandler: IBillHandler
    {
        IBillManagerRepository _billManagerRepository;
        public BillHandler(IBillManagerRepository billManagerRepository)
        {
            _billManagerRepository = billManagerRepository;
        }

        public Bill AddBill(Post_Put_BillDTO billDTO)
        {
            if (billDTO == null || billDTO.FriendIds == null || !billDTO.FriendIds.Any())
                return null;

            IEnumerable<Friend> friendsFromDb =_billManagerRepository.GetFriends(billDTO.FriendIds, false);
            if (billDTO.FriendIds.Count() != friendsFromDb.Count())
                return null;

            Bill bill = new Bill()
            {                
                Amount = billDTO.Amount,
                Description = billDTO.Description
            };
            
            decimal splitAmount = billDTO.Amount / friendsFromDb.Count();

            List<SplitBill> splitBills = new List<SplitBill>();

            foreach (Friend friend in friendsFromDb)
            {
                SplitBill splitBill = new SplitBill()
                {                    
                    FriendId = friend.Id,
                    Amount = splitAmount
                };

                bill.FriendsSplit.Add(splitBill);                
            }
            
            _billManagerRepository.AddBill(bill);

            return bill;
        }

        public List<BillDTO> GetAllBills()
        {
            IEnumerable<Bill> billsFromDb = _billManagerRepository.GetAllBills(true);

            List<BillDTO> bills = new List<BillDTO>();
            if (billsFromDb != null && billsFromDb.Any())
            {
                bills = billsFromDb.Select(x => new BillDTO()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Description = x.Description,
                    FriendsSplit = x.FriendsSplit.Select(s => new SplitBillDTO() { Id = s.Id, BillId = s.BillId, FriendId = s.FriendId, Amount = s.Amount, Description = s.Bill.Description, FriendName = s.Friend.FirstName + " " + s.Friend.LastName }).ToList()
                }).ToList();
            }

            return bills;
        }

        public BillDTO GetBillById(int id)
        {
            Bill billFromDb = _billManagerRepository.GetBillById(id, true);

            BillDTO bill = new BillDTO();
            if (billFromDb != null)
            {
                bill = new BillDTO()
                {
                    Id = billFromDb.Id,
                    Amount = billFromDb.Amount,
                    Description = billFromDb.Description,
                    FriendsSplit = billFromDb.FriendsSplit.Select(s => new SplitBillDTO() { Id = s.Id, BillId = s.BillId, FriendId = s.FriendId, Amount = s.Amount, Description = s.Bill.Description, FriendName = s.Friend.FirstName + " " + s.Friend.LastName }).ToList()
                };
            }

            return bill;
        }

        //public BillDTO UpdateBill(Post_Put_BillDTO updateBillDTO)
        //{

        //}
    }
}
