using BillManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services
{
    public interface IBillManagerRepository
    {
        Friend AddFriend(Friend friend);
        void UpdateFriend(Friend friend);
        Friend DeleteFriend(int friendId);
        IEnumerable<Friend> GetAllFriends(bool includeSplitBills);
        IEnumerable<Friend> GetFriends(List<int> friendsIds, bool includeSplitBills);

        Friend GetFriendById(int id, bool includeSplitBills);

        IEnumerable<Bill> GetAllBills(bool includeSplitFriends);

        Bill GetBillById(int id, bool includeSplitFriends);

        void AddBill(Bill bill);
        void UpdateBill(Bill bill);
        Bill DeleteBill(int billId);
    }
}
