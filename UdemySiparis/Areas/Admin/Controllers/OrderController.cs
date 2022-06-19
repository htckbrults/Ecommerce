using Microsoft.AspNetCore.Mvc;
using UdemySiparis.Data.Repository.IRepository;
using UdemySiparis.Models.ViewModels;

namespace UdemySiparis.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public IActionResult Index()
        {
            var OrderList = _unitOfWork.OrderProduct.GetAll(x=>x.OrderStatus != "Delivered");
            return View(OrderList);
        }
    }
}
