﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using System.Drawing;
using StoreFront.UI.MVC.Utilities;
using X.PagedList;

namespace StoreFront.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductsController : Controller
    {
        private readonly EducatedMoneyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(EducatedMoneyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            //var product = _context.Products.Include(p => p.Category).Include(p => p.Location);
            if (User.IsInRole("Admin"))
            {
                var product = _context.Products.Include(p => p.Category).Include(p => p.Location);

                return View(await product.ToListAsync());
            }
            else
            {

                var product = _context.Products.Where(p => !p.Discontinued)
                    .Include(p => p.Category)
                    .Include(p => p.Location);


                return View(await product.ToListAsync());
            }
        }

        //GET: Products/TableView
        [AllowAnonymous]
        public async Task<IActionResult> TableView()
        {
            if (User.IsInRole("Admin"))
            {
                var product = _context.Products.Include(p => p.Category).Include(p => p.Location);

                return View(await product.ToListAsync());
            }
            else
            {

                var product = _context.Products.Where(p => !p.Discontinued)
                    .Include(p => p.Category)
                    .Include(p => p.Location);


                return View(await product.ToListAsync());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> HomePage()
        {

            if (User.IsInRole("Admin"))
            {
                var product = _context.Products.Include(p => p.Category).Include(p => p.Location);

                return View(await product.ToListAsync());
            }
            else
            {

                var product = _context.Products.Where(p => !p.Discontinued)
                    .Include(p => p.Category)
                    .Include(p => p.Location);



                return View(await product.ToListAsync());

            }

        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["LocationId"] = new SelectList(_context.ClassLocations, "LocationId", "CampusName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Price,Discontinued,Description,ProductImage,ProductName,CategoryId,LocationId,IsOnline,Image")] Product product)
        {

            if (ModelState.IsValid)
            {


                if (product.Image != null)
                {

                    string ext = Path.GetExtension(product.Image.FileName);


                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };


                    if (validExts.Contains(ext.ToLower()) && product.Image.Length < 4_194_303)
                    {


                        product.ProductImage = Guid.NewGuid() + ext;


                        string webRootPath = _webHostEnvironment.WebRootPath;


                        string fullImagePath = webRootPath + "/images/";


                        using (var memoryStream = new MemoryStream())
                        {

                            await product.Image.CopyToAsync(memoryStream);


                            using (var img = Image.FromStream(memoryStream))
                            {


                                int maxImageSize = 500;
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, product.ProductImage, img, maxImageSize, maxThumbSize);


                            }
                        }
                    }
                }
                else
                {

                    product.ProductImage = "noimage.png";
                }


                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.ClassLocations, "LocationId", "CampusName", product.LocationId);
            return View(product);

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["LocationId"] = new SelectList(_context.ClassLocations, "LocationId", "CampusName", product.LocationId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["LocationId"] = new SelectList(_context.ClassLocations, "LocationId", "CampusName", product.LocationId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Price,Discontinued,Description,ProductImage,ProductName,CategoryId,LocationId,IsOnline,Image")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


                string oldImageName = product.ProductImage;


                if (product.Image != null)
                {

                    string ext = Path.GetExtension(product.Image.FileName);


                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };


                    if (validExts.Contains(ext.ToLower()) && product.Image.Length < 4_194_303)
                    {

                        product.ProductImage = Guid.NewGuid() + ext;


                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullImagePath = webRootPath + "/images/";


                        if (oldImageName != "noimage.png")
                        {
                            ImageUtility.Delete(fullImagePath, oldImageName);
                        }


                        using (var memoryStream = new MemoryStream())
                        {

                            await product.Image.CopyToAsync(memoryStream);


                            using (var img = Image.FromStream(memoryStream))
                            {

                                int maxImageSize = 500;
                                int maxThumbSize = 100;


                                ImageUtility.ResizeImage(fullImagePath, product.ProductImage, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }
                }



                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.ClassLocations, "LocationId", "CampusName", product.LocationId);
            return View(product);

            //if (id != product.ProductId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(product);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProductExists(product.ProductId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            //ViewData["LocationId"] = new SelectList(_context.ClassLocations, "LocationId", "CampusName", product.LocationId);
            //return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'EducatedMoneyContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        

    }
}
