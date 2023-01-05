using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXSM3935_C_Sharp_Final_Project.Data;
using EXSM3935_C_Sharp_Final_Project.Models;
using EXSM3935_C_Sharp_Final_Project.Models.Exceptions;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace EXSM3935_C_Sharp_Final_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AddNew()
        {
            if (TempData["Exceptions"] != null)
                ViewData["Exceptions"] = JsonConvert.DeserializeObject(TempData["Exceptions"].ToString(), typeof(Validation));

            ViewData["Accounttypes"] = new SelectList(_context.Accounttypes, "Id", "AcctNameAndId");
            return View();
        }

        public IActionResult DoRegistration(string firstName, string lastName, string birthDate, string homeAddress,
                                            string accountType, string openBalance, string theDate)
        {      // Let's do some validation!
            Validation validationState = new Validation();

            // Do validation, if something fails, add it as a sub exception.

            // TEST EACH ITEM INDIVIDUALLY FIRST.

            // First validation item is typically "does this even exist", before we test it.
            if (string.IsNullOrWhiteSpace(firstName))
                validationState.SubExceptions.Add(new Exception("FIRST NAME MUST BE PROVIDED!"));

                // Do any other validation with this item - does it exist in the database, does it have necessary properties/permissions, etc.

            if (string.IsNullOrWhiteSpace(lastName))
                validationState.SubExceptions.Add(new Exception("LAST NAME MUST BE PROVIDED!"));

            if (string.IsNullOrWhiteSpace(homeAddress))
                validationState.SubExceptions.Add(new Exception("HOME ADDRESS MUST BE PROVIDED!"));

            if (string.IsNullOrWhiteSpace(openBalance))
            {
                validationState.SubExceptions.Add(new Exception("A STARTING BALANCE MUST BE PROVIDED!"));
            }
            else
            {
                if (!int.TryParse(openBalance, out int temp))
                {
                    validationState.SubExceptions.Add(new Exception("BALANCE MUST BE A NUMBER!"));
                }
                else if(temp <= 0)
                {
                    validationState.SubExceptions.Add(new Exception("YOU CAN NOT START WITH A NEGATIVE BALANCE!"));
                }
            }
           

            // ONCE EACH ITEM IS TESTED, THEN DO ANY TESTS THAT RELY ON MULTIPLE INPUTS.


            if (validationState.Any)
            {
                Console.WriteLine("checking validation state");
                TempData["Exceptions"] = JsonConvert.SerializeObject(validationState);
            }
            // If we go in here, we've not encountered any validation issues, so we are good to proceed.


            else
            { 
                Client newClient = new()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = DateOnly.Parse(birthDate),
                    HomeAddress = homeAddress,
                    Accounts = new List<Account> {
                        new Account()
                        {
                            AccountTypeId = int.Parse(accountType),
                            Balance = Math.Round(Decimal.Parse(openBalance), 2),
                            InterestAppliedDate = DateOnly.Parse(theDate)
                        }
                    }
                };
                _context.Clients.Add(newClient);
                _context.SaveChanges();
            }
            return RedirectToAction("AddNew");
        }    
    }
}
