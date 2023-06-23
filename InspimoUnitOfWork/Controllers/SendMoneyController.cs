using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkDesignPattern.Models;

namespace UnitOfWorkDesignPattern.Controllers
{
    public class SendMoneyController : Controller
    {
        private readonly ICustomerService _customerService;

        public SendMoneyController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(CustomerViewModel model)
        {
            var value1 = _customerService.TGetByID(model.SenderID);
            var value2 = _customerService.TGetByID(model.ReceiverID);

            value1.CustomerBalanca -= model.Amount;
            value2.CustomerBalanca += model.Amount;

            List<Customer> modifiedCustomers = new List<Customer>()
            {
                value1, value2
            }; 

            _customerService.TMultiUpdate(modifiedCustomers);
            return View();

        }
    }
}
