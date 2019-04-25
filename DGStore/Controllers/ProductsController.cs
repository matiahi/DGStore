using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace DGStore.Controllers
{
    public class ProductsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var list = _context.Products.ToList();
            return View(list);
        }
        public IActionResult Details(int? Id)
        {
            if (Id == null)
                return NotFound();
            
            var product = _context.Products.FirstOrDefault(p => p.Id == Id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}