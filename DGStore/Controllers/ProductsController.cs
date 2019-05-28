using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DGStore.Data;
using DGStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
		private readonly IHostingEnvironment _evn;
		public ProductsController(ApplicationDbContext context , IHostingEnvironment evn)
		{
			_context = context;
			_evn = evn;
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

			var imageAbsolutePath = _evn.WebRootPath + product.ImagePath;
			if (System.IO.File.Exists(imageAbsolutePath))
			System.IO.File.Delete(imageAbsolutePath);

			try
			{
				_context.Remove(product);
				_context.SaveChanges();
				//Delete Successful
				TempData["ProductDeleteStatus"] = true;
			}
			catch (Exception)
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
		[ValidateAntiForgeryToken]
		public async Task <IActionResult> Create (IFormFile image,Product product)
        {
			if (ModelState.IsValid)
			{
				if (image.Length == 0 || image == null)
					throw new Exception("عکسی انتخاب نشده");
				if (image.Length > 1048576)
					throw new Exception("عکس انتخاب شده بزرگتر از حد مجاز است");
				if (image.ContentType != "image/jpg" && image.ContentType != "image/jpeg" && image.ContentType != "image/png" && image.ContentType != "image/gif")
					throw new Exception("عکس انتخاب شده در قالب مجاز نمی باشد");


				var imageName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + System.IO.Path.GetExtension(image.FileName);
				var imagePath = System.IO.Path.Combine("UserUploads/Products", imageName);
				var imageAbsolutePath = Path.Combine(_evn.WebRootPath, imagePath);

				using (var fs = new FileStream(imageAbsolutePath, FileMode.Create))
				{
					image.CopyTo(fs);
				}
				product.ImagePath = "/UserUploads/Products/" + imageName;


				try
				{
					_context.Add(product);
					await _context.SaveChangesAsync();
					//Create Successful
					TempData["ProductCreateStatus"] = true;
				}
				catch (Exception)
				{
					//Create Failed
					TempData["ProductCreateStatus"] = false;
				}


				return RedirectToAction(nameof(Index));
			}
			return View(product);
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, IFormFile image, Product product)
		{
			if (id != product.Id)
				return NotFound();
			if (ModelState.IsValid)
			{
				if (image == null)
				{
					try
					{
						_context.Update(product);
						await _context.SaveChangesAsync();
						//Edit Successful
						TempData["ProductEditStatus"] = true;
					}
					catch (Exception e)
					{
						//Edit Failed
						TempData["ProductEditStatus"] = false;
					}

					return RedirectToAction(nameof(Index));
				}
				else
				{
					if (image.Length == 0 || image == null)
						throw new Exception("عکسی انتخاب نشده");
					if (image.Length > 1048576)
						throw new Exception("عکس انتخاب شده بزرگتر از حد مجاز است");
					if (image.ContentType != "image/jpg" && image.ContentType != "image/jpeg" && image.ContentType != "image/png" && image.ContentType != "image/gif")
						throw new Exception("عکس انتخاب شده در قالب مجاز نمی باشد");


					var imageName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + System.IO.Path.GetExtension(image.FileName);
					var imagePath = System.IO.Path.Combine("UserUploads/Products", imageName);
					var imageAbsolutePath = Path.Combine(_evn.WebRootPath, imagePath);

					using (var fs = new FileStream(imageAbsolutePath, FileMode.Create))
					{
						image.CopyTo(fs);
					}
					var imageToDeleteAbsolutePath = _evn.WebRootPath + product.ImagePath;
					if (System.IO.File.Exists(imageToDeleteAbsolutePath))
						System.IO.File.Delete(imageToDeleteAbsolutePath);


					product.ImagePath = "/UserUploads/Products/" + imageName;
					try
					{
						_context.Update(product);
						await _context.SaveChangesAsync();
						//Edit Successful
						TempData["ProductEditStatus"] = true;
					}
					catch (Exception e)
					{
						//Edit Failed
						TempData["ProductEditStatus"] = false;
					}

					return RedirectToAction(nameof(Index));
				}
		}
		return View(product);
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