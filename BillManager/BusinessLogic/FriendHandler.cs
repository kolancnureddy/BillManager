using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillManager.Models;
using BillManager.Services;
using BillManager.Entities;

namespace BillManager.BusinessLogic
{
    public class FriendHandler : IFriendHandler
    {
        IBillManagerRepository _billManagerRepository;
        public FriendHandler(IBillManagerRepository billManagerRepository)
        {
            _billManagerRepository = billManagerRepository;
        }
        public List<FriendDTO> GetFriends()
        {
            IEnumerable<Friend> friendsFromDb = _billManagerRepository.GetAllFriends(true);

            List<FriendDTO> friends = new List<FriendDTO>();
            if (friendsFromDb != null && friendsFromDb.Any())
            {
                friends = friendsFromDb.Select(x => new FriendDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SplitBills = x.SplitBills.Select(s => new SplitBillDTO() { Id = s.Id, BillId = s.BillId, FriendId = s.FriendId, Amount = s.Amount, Description = s.Bill.Description, FriendName = s.Friend.FirstName + " " + s.Friend.LastName }).ToList()
                }).ToList();
            }

            return friends;
        }

        public FriendDTO GetFriendById(int friendId)
        {            
            Friend friendFromDb = _billManagerRepository.GetFriendById(friendId, true);

            if (friendFromDb == null)
            {
                return null;
            }
            else
            {
                FriendDTO friend = new FriendDTO()
                {
                    Id = friendFromDb.Id,
                    FirstName = friendFromDb.FirstName,
                    LastName = friendFromDb.LastName
                };
                if (friendFromDb.SplitBills.Any())
                {
                    foreach (var splitBill in friendFromDb.SplitBills)
                    {
                        friend.SplitBills.Add(new SplitBillDTO()
                        {
                            Amount = splitBill.Amount,
                            Description = splitBill.Bill.Description,
                            FriendName = friendFromDb.FirstName + " " + friendFromDb.LastName,
                            FriendId = friendFromDb.Id,
                            BillId = splitBill.BillId
                        });
                    }
                }
                return friend;
            }
        }
    }
}
