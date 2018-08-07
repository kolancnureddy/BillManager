using BillManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager
{
    public class BillManagerDataStore
    {
        public static BillManagerDataStore Current { get; } = new BillManagerDataStore();
        public List<FriendDTO> Friends { get; set; }
        public List<BillDTO> Bills { get; set; }
        public List<SplitBillDTO> SplitBills { get; set; }

        public int LastFriendId = 5;
        public int LastBillId = 2;
        public int LastSplitBillId = 7;
        public BillManagerDataStore()
        {
            Friends = new List<FriendDTO>()
            {
                new FriendDTO(){Id = 1, FirstName = "Test", LastName = "Friend1"},
                new FriendDTO(){Id = 2, FirstName = "Test", LastName = "Friend2"},
                new FriendDTO(){Id = 3, FirstName = "Test", LastName = "Friend3"},
                new FriendDTO(){Id = 4, FirstName = "Test", LastName = "Friend4"},
                new FriendDTO(){Id = 5, FirstName = "Test", LastName = "Friend5"},                
            };

            Bills = new List<BillDTO>()
            {
                new BillDTO(){ Id = 1, Amount = 100, Description = "Spent for bowling night"},
                new BillDTO(){ Id = 2, Amount = 150, Description = "Spent at Fish n Chip Bar n Grill"}
            };

            SplitBills = new List<SplitBillDTO>()
            {
                new SplitBillDTO(){ Id = 1, BillId = 1, FriendId = 1, Amount = 50, FriendName = "Test Friend1", Description = "Spent for bowling night" },
                new SplitBillDTO(){ Id = 2, BillId = 1, FriendId = 2, Amount = 50, FriendName = "Test Friend2", Description = "Spent for bowling night" },

                new SplitBillDTO(){ Id = 3, BillId = 2, FriendId = 1, Amount = 30, FriendName = "Test Friend1", Description = "Spent at Fish n Chip Bar n Grill" },
                new SplitBillDTO(){ Id = 4, BillId = 2, FriendId = 2, Amount = 30, FriendName = "Test Friend2", Description = "Spent at Fish n Chip Bar n Grill" },
                new SplitBillDTO(){ Id = 5, BillId = 2, FriendId = 3, Amount = 30, FriendName = "Test Friend3", Description = "Spent at Fish n Chip Bar n Grill" },
                new SplitBillDTO(){ Id = 6, BillId = 2, FriendId = 4, Amount = 30, FriendName = "Test Friend4", Description = "Spent at Fish n Chip Bar n Grill" },
                new SplitBillDTO(){ Id = 7, BillId = 2, FriendId = 5, Amount = 30, FriendName = "Test Friend5", Description = "Spent at Fish n Chip Bar n Grill" }
            };
        }
    }
}
