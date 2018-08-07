using BillManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.BusinessLogic
{
    public interface IFriendHandler
    {
        List<FriendDTO> GetFriends();
        FriendDTO GetFriendById(int friendId);
    }
}
