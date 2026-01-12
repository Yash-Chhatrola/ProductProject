using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductMVCProject.Data;
using ProductMVCProject.Models;

namespace ProductMVCProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.ToListAsync();
            return View(products); 
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Price, Description, Stock")]Product model)
        {
            if (ModelState.IsValid)
            {
                await _context.Products.AddAsync(model);
                await _context.SaveChangesAsync();
                TempData["Notification"] = "Product Created Successfully";
                TempData["NotificationType"] = "success";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, Name, Price, Description, Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                TempData["Notification"] = "Product Updated Successfully";
                TempData["NotificationType"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Notification"] = "Product Deleted Successfully";
                TempData["NotificationType"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
