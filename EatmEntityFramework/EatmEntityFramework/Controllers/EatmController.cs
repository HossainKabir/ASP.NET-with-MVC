using EatmEntityFramework.Models;
using EatmEntityFramework.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace EatmEntityFramework.Controllers
{
    public class EatmController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EatmController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Eatm
        public ActionResult Index()
        {
            var accounts = _context.Accounts.ToList();
            return View(accounts);
        }

        public ActionResult Login()
        {
            //ViewBag.Error = null;
            return View();
        }

        [HttpPost]
        public ActionResult WithdrawResult(WithdrawViewModel wvm)
        {
            if (ModelState.IsValid)
            {
                var availableBalance = wvm.CurrentBalance;
                var withdrawAmount = wvm.WithdrawAmount;
                var id = wvm.Id;

                var todayDate = DateTime.Now.ToShortDateString();
                var totalDayTransaction = (from c in _context.Traansactions.ToList()
                                           where (c.Date.Equals(todayDate) && c.AccountId == id)
                                           select (c)).Count();
                if (totalDayTransaction <= 2)
                {
                    if (withdrawAmount > 0 && withdrawAmount < availableBalance)
                    {
                        ViewBag.Error = "Wihtdraw Successfull";
                        TempData["WithdrawSuccess"] = "Wihtdraw Successfull";
                        availableBalance -= wvm.WithdrawAmount;
                        (from p in _context.Accounts
                         where p.AccountId == wvm.Id
                         select p).ToList()
                            .ForEach(x => x.Balance = availableBalance);
                        //_context.SaveChanges();
                        var transaction = new Transaction()
                        {
                            AccountId = id,
                            Amount = withdrawAmount,
                            Date = DateTime.Now.Date.ToShortDateString(),

                        };
                        _context.Traansactions.Add(transaction);
                        _context.SaveChanges();



                    }
                    else if (withdrawAmount > availableBalance || withdrawAmount <= 0)
                    {
                        ViewBag.Error = "Unsifficient Balance.";
                        TempData["WithdrawError"] = "Unsifficient Balance";
                    }
                }
                else
                {
                    TempData["TransactionLimit"] = "Finished Daily Transaction Limit.";
                }
            }
            return RedirectToAction("HomeToOption", "Eatm");
        }

        [HttpPost]
        public ActionResult VerifyAccount(Account account)
        {
            var wvm = new WithdrawViewModel();

            if (account != null)
            {
                var cardNumber = account.CardNumber;
                var pinNumber = account.Password;

                var result = _context.Accounts.SingleOrDefault(m => m.CardNumber == cardNumber);
                var verifyCustomer = (from c in _context.Accounts.ToList()
                                      where ((c.CardNumber.Equals(cardNumber)) && (c.Password.Equals(pinNumber)))
                                      select (c)).FirstOrDefault();

                var date = DateTime.Now.ToShortDateString();
                //var allTransactions = _context.Traansactions.ToList();
                account = verifyCustomer;

                if (account != null) Session["Name"] = account.AccountHolderName;

                if (verifyCustomer != null)
                {
                    wvm.CurrentBalance = verifyCustomer.Balance;
                    wvm.Id = verifyCustomer.AccountId;
                    wvm.Transactions = _context.Traansactions.Where(m => m.AccountId == wvm.Id).ToList();
                    TempData["WithdrawError"] = null;
                    TempData["WithdrawSuccess"] = null;
                    //return RedirectToAction("ShowVerifiedView", "Eatm");
                    return View(wvm);

                }
                else
                {
                    ViewBag.Error = "Invalid Card Number and Password";
                    TempData["Error"] = "Invalid Card Number and Password";
                    return RedirectToAction("Login", "Eatm");
                }


            }
            else
            {
                TempData["Error"] = "Invalid Card Number and Password";

                return RedirectToAction("Login", "Eatm");

            }
        }

        public ActionResult HomeOption(Account account)
        {
            var wvm = new WithdrawViewModel();

            wvm.Id = account.AccountId;
            wvm.CurrentBalance = account.Balance;
            wvm.WithdrawAmount = 0;
            wvm.Transactions = _context.Traansactions.Where(m => m.AccountId == wvm.Id).ToList();
            return View(wvm);
            //return RedirectToAction("ShowVerifiedView", "Eatm", new RouteValueDictionary(wvm));
        }

        public ActionResult HomeToOption()
        {
            object name = null;
            if (Session["Name"] != null)
            {
                name = Session["Name"];
            }

            var verifyCustomer = (from c in _context.Accounts.ToList()
                                  where (c.AccountHolderName.Equals(name))
                                  select (c)).FirstOrDefault();


            return RedirectToAction("HomeOption", "Eatm", new RouteValueDictionary(verifyCustomer));
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Eatm");
        }

        public ActionResult PopUpBox()
        {
            var todayDate = DateTime.Now.ToShortDateString();
            var totalDayTransaction = (from c in _context.Traansactions.ToList()
                                       where (c.Date.Equals(todayDate) && c.AccountId == 2)
                                       select (c)).Count();
            ViewBag.TotalTransaction = totalDayTransaction;
            return View();
        }

        public ActionResult ShowVerifiedView(WithdrawViewModel wvm)
        {
            return View(wvm);
        }
    }
}