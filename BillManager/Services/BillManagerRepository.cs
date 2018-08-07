using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillManager.Services
{
    public class BillManagerRepository : IBillManagerRepository
    {
        private BillManagerDbContext _dbContext;
        public BillManagerRepository(BillManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Friend> GetAllFriends(bool includeSplitBills)
        {
            IQueryable<Friend> query = _dbContext.Friends;

            if (includeSplitBills)
            {
                query = query.Include(r => r.SplitBills);
            }
            var friends = query.ToList();

            if (includeSplitBills)
            {
                var friendIds = friends.Select(x => x.Id);
                var splitBills = _dbContext.SplitBills.Where(x => friendIds.Contains(x.FriendId)).ToList();
                var billIds = splitBills.Select(x => x.BillId);
                _dbContext.Bills.Where(x => billIds.Contains(x.Id)).ToList();
            }
            return friends;
        }

        public IEnumerable<Friend> GetFriends(List<int> friendsIds, bool includeSplitBills)
        {
            IQueryable<Friend> query = _dbContext.Friends;
            query.Where(x => friendsIds.Contains(x.Id));

            if (includeSplitBills)
            {
                query = query.Include(r => r.SplitBills);
            }

            List<Friend> friends = query.ToList();

            if (includeSplitBills)
            {
                var friendIds = friends.Select(x => x.Id);
                var splitBills = _dbContext.SplitBills.Where(x => friendIds.Contains(x.FriendId)).ToList();
                var billIds = splitBills.Select(x => x.BillId);
                _dbContext.Bills.Where(x => billIds.Contains(x.Id)).ToList();
            }
            return query.ToList();
        }

        public Friend GetFriendById(int id, bool includeSplitBills)
        {
            IQueryable<Friend> query = _dbContext.Friends;           

            if (includeSplitBills)
            {
                query = query.Include(r => r.SplitBills);
            }

            Friend friend = query.FirstOrDefault(x => x.Id == id);

            if(friend != null && includeSplitBills)
            {
                //_dbContext.SplitBills.Where(x => friend.Id == x.FriendId).ToList();
                var billIds = _dbContext.SplitBills.Where(x=> x.FriendId == friend.Id).Select(x => x.BillId);
                _dbContext.Bills.Where(x => billIds.Contains(x.Id)).ToList();
            }
            return friend;
        }

        public Friend AddFriend(Friend friend)
        {
            _dbContext.Add(friend);
            _dbContext.SaveChanges();
            return friend;
        }

        public void UpdateFriend(Friend friend)
        {
            Friend friendFromDb = _dbContext.Friends.FirstOrDefault(x => x.Id == friend.Id);
            if(friendFromDb != null)
            {
                friendFromDb.FirstName = friend.FirstName;
                friendFromDb.LastName = friend.LastName;
            }

            _dbContext.SaveChanges();
        }

        public Friend DeleteFriend(int friendId)
        {
            Friend friend = _dbContext.Friends.FirstOrDefault(x => x.Id == friendId);
            if (friend != null)
            {
                _dbContext.Friends.Remove(friend);
                _dbContext.SaveChanges();
            }
            return friend;
        }

        public IEnumerable<Bill> GetAllBills(bool includeSplitFriends)
        {
            IQueryable<Bill> query = _dbContext.Bills;

            if (includeSplitFriends)
            {                
                query = query.Include(r => r.FriendsSplit);                
            }
            var bills = query.ToList();

            if (includeSplitFriends)
            {
                var billIds = bills.Select(x => x.Id);
                var splitBills = _dbContext.SplitBills.Where(x => billIds.Contains(x.BillId)).ToList();
                var friendIds = splitBills.Select(x => x.FriendId);
                _dbContext.Friends.Where(x => friendIds.Contains(x.Id)).ToList();
            }

            return query.ToList();
        }

        public Bill GetBillById(int id, bool includeSplitFriends)
        {
            IQueryable<Bill> query = _dbContext.Bills;

            if (includeSplitFriends)
            {
                query = query.Include(r => r.FriendsSplit);
            }

            Bill bill = query.FirstOrDefault(x => x.Id == id);

            if (bill != null && includeSplitFriends)
            {
                var splitBills = _dbContext.SplitBills.Where(x => bill.Id == x.BillId).ToList();
                var friendIds = splitBills.Select(x => x.FriendId);
                _dbContext.Friends.Where(x => friendIds.Contains(x.Id)).ToList();
            }
            return bill;
        }

        public void AddBill(Bill bill)
        {
            if (bill == null)
                return;

            _dbContext.Bills.Add(bill);
            _dbContext.SaveChanges();
        }

        public void UpdateBill(Bill bill)
        {
            Bill billFromDb = _dbContext.Bills.FirstOrDefault(x => x.Id == bill.Id);
            if (billFromDb != null)
            {
                billFromDb.Amount = bill.Amount;
                billFromDb.Description = bill.Description;
                bill.FriendsSplit = bill.FriendsSplit;
            }

            _dbContext.SaveChanges();
        }

        public Bill DeleteBill(int billId)
        {
            Bill bill = _dbContext.Bills.FirstOrDefault(x => x.Id == billId);
            if (bill != null)
            {
                _dbContext.Bills.Remove(bill);
                _dbContext.SaveChanges();
            }
            return bill;
        }
    }
}
