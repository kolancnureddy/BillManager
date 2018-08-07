using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BillManager.Models;
using BillManager.Services;
using BillManager.Entities;
using BillManager.BusinessLogic;

namespace BillManager.Controllers
{
    //[Produces("application/json")]
    [Route("api/Friends")]
    public class FriendsController : Controller
    {
        IBillManagerRepository _billManagerRepository;
        IFriendHandler _friendHandler;

        public FriendsController(IFriendHandler friendHandler, IBillManagerRepository billManagerRepository)
        {
            _friendHandler = friendHandler;
            _billManagerRepository = billManagerRepository;
        }

        [HttpGet(Name = "GetFrieds")]
        public IActionResult GetFriends()
        {
            List<FriendDTO> friends = _friendHandler.GetFriends();
            return Ok(friends);
        }

        [HttpGet("{id}", Name ="GetFriend")]
        public IActionResult GetFriend(int id)
        {
            FriendDTO friend = _friendHandler.GetFriendById(id);
            if(friend == null)
            {
                return NotFound();
            }                
            return Ok(friend);            
        }

        [HttpPost]
        public IActionResult CreateFriend([FromBody] FriendDTO friend)
        {
            if(friend == null || friend.Id !=0)
            {
                return BadRequest();
            }

            Friend friendFromDb = _billManagerRepository.AddFriend(new Friend()
            {
                FirstName = friend.FirstName,
                LastName = friend.LastName
            });

            friend.Id = friendFromDb.Id;
            return CreatedAtRoute("GetFriend", new { id = friend.Id }, friend);
        }

        [HttpPut]
        public IActionResult UpdateFriend([FromBody] FriendDTO friend)
        {
            if (friend == null || friend.Id == 0)
            {
                return BadRequest();
            }

            _billManagerRepository.UpdateFriend(new Friend()
            {
                Id = friend.Id,
                FirstName = friend.FirstName,
                LastName = friend.LastName
            });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFriend(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Friend friendFromDB = _billManagerRepository.DeleteFriend(id);
                        
            if (friendFromDB == null)
            {
                return NotFound();
            }
                        
            return NoContent();
        }
    }
}