using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGStore.Data;
using DGStore.Models;
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
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
                return NotFound();

            var product = _context.Products.FirstOrDefault(p => p.Id == Id);

            if (product == null)
                return NotFound();

            return View(product);
        }
        [HttpPost]
        public IActionResult DeleteDone(int Id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == Id);
            try
            {
                _context.Remove(product);
                _context.SaveChanges();
                //Delete Successful
                TempData["ProductDeleteStatus"] = true;
            }
            catch (Exception )
            {
                //Delete Failed
                TempData["ProductDeleteStatus"] = false;
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                _context.Add(product);
                _context.SaveChanges();
                //Create Successful
                TempData["ProductCreateStatus"] = true;
            }
            catch (Exception )
            {
                //Create Failed
                TempData["ProductCreateStatus"] = false;
            }
           

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            try
            {
                _context.Update(product);
                _context.SaveChanges();
                //Edit Successful
                TempData["ProductEditStatus"] = true;
            }
            catch (Exception )
            {
                //Edit Failed
                TempData["ProductEditStatus"] = false;
            }

            return RedirectToAction("Index");
        }
        public IEnumerable<Product> Search(string q)
        {
            return _context
                .Products
                .Where(p => p.Title.Contains(q))
                .Take(10)//Maximun records goes here
                .ToList();
        }
    }
}