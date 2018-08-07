using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BillManager.Models;
using BillManager.Services;
using BillManager.BusinessLogic;
using BillManager.Entities;

namespace BillManager.Controllers
{
    //[Produces("application/json")]
    [Route("api/Bills")]
    public class BillsController : Controller
    {
        IBillManagerRepository _billManagerRepository;
        IBillHandler _billHandler;

        public BillsController(IBillHandler billHandler, IBillManagerRepository billManagerRepository)
        {
            _billHandler = billHandler;
            _billManagerRepository = billManagerRepository;
        }

        [HttpGet]
        public IActionResult GetBills()
        {
            List<BillDTO> bills = _billHandler.GetAllBills();
            return Ok(bills);
        }

        [HttpGet("{id}", Name = "GetBill")]
        public IActionResult GetBill(int id)
        {
            BillDTO bill = _billHandler.GetBillById(id);
                        
            if (bill == null)
            {
                return NotFound();
            }
            
            return Ok(bill);
        }

        [HttpPost]
        public IActionResult CreateBill([FromBody] Post_Put_BillDTO createBillDTO)
        {
            if (createBillDTO == null || !createBillDTO.FriendIds.Any())
            {
                return BadRequest();
            }

            Bill bill = _billHandler.AddBill(createBillDTO);
            createBillDTO.Id = bill.Id;
                        
            return CreatedAtRoute("GetBill", new { id = createBillDTO.Id }, createBillDTO);
        }
                
        [HttpPut]
        public IActionResult UpdateBill([FromBody] Post_Put_BillDTO updateBillDTO)
        {
            if (updateBillDTO == null || updateBillDTO.Id == 0 || updateBillDTO.FriendIds.Any())
            {
                return BadRequest();
            }


            List<FriendDTO> friends = BillManagerDataStore.Current.Friends.Where(x => updateBillDTO.FriendIds.Contains(x.Id)).ToList();
            if (friends.Count() != updateBillDTO.FriendIds.Count())
            {
                return BadRequest();
            }

            BillDTO bill = new BillDTO()
            {
                Id = ++BillManagerDataStore.Current.LastBillId,
                Amount = updateBillDTO.Amount,
                Description = updateBillDTO.Description
            };

            BillManagerDataStore.Current.Bills.Add(bill);

            decimal splitAmount = updateBillDTO.Amount / friends.Count();

            List<SplitBillDTO> splitBills = new List<SplitBillDTO>();

            foreach (FriendDTO friend in friends)
            {
                SplitBillDTO splitBill = new SplitBillDTO()
                {
                    Id = ++BillManagerDataStore.Current.LastSplitBillId,
                    BillId = bill.Id,
                    FriendId = friend.Id,
                    Amount = splitAmount,
                    Description = bill.Description,
                    FriendName = friend.FirstName + " " + friend.LastName
                };
                BillManagerDataStore.Current.SplitBills.Add(splitBill);
                splitBills.Add(splitBill);
            }

            bill.FriendsSplit = splitBills;
            return CreatedAtRoute("GetBill", new { id = bill.Id }, bill);
        }
    }
}