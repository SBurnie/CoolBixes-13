using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Big_Bike_Store.Models;

namespace Big_Bike_Store.Controllers
{
    public class BikesController : Controller
    {
        private readonly AdventureWorksLT2017gr1Context db;

        public BikesController(AdventureWorksLT2017gr1Context context)
        {
            db = context;
        }

        // GET: Bikes
       /* public async Task<IActionResult> Index()
        {
            var adventureWorksLT2017gr1Context = _context.ProductCategory.Include(p => p.ParentProductCategory);
            return View(await adventureWorksLT2017gr1Context.ToListAsync());
        }
        */

          public IActionResult Index()
        {
            /* SELECT *
             * FROM SalesLT.ProductCategory
             * WHERe ParentProductCategoryID == 1
             * */

            var bikeList = db.ProductCategory.Where(b => b.ParentProductCategoryId == 1);

            return View(bikeList);

        }

        public IActionResult Road()
        {
            var validRoadProducts = db.VProductAndDescription
                .Where(b => b.Culture == "en" && b.SellEndDate == null && b.ProductCategoryId == 6)
                .Select(b => new BikeList
                {
                    ProductModel = b.ProductModel,
                    Description = b.Description,
                    ProductModelId = b.ProductModelId
                }).Distinct().ToList();

          

            return View(validRoadProducts);
        }

        public IActionResult Mountain()
        {
            var validMountainProducts = db.VProductAndDescription
                .Where(b => b.Culture == "en" && b.SellEndDate == null && b.ProductCategoryId == 5)
                .Select(b => new BikeList
                {
                    ProductModel = b.ProductModel,
                    Description = b.Description,
                    ProductModelId = b.ProductModelId
                }).Distinct().ToList();

            return View(validMountainProducts);
        }

        public IActionResult Touring()
        {
            var validTouringProducts = db.VProductAndDescription
                .Where(b => b.Culture == "en" && b.SellEndDate == null && b.ProductCategoryId == 7)
                .Select(b => new BikeList
                {
                    ProductModel = b.ProductModel,
                    Description = b.Description,
                    ProductModelId = b.ProductModelId
                }).Distinct().ToList();

            return View(validTouringProducts);
        }

        // GET: Bikes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var productCategory = await db.ProductCategory
                .Include(p => p.ParentProductCategory)
                .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
                */

            var productCategory = db.Product.Where(b => b.ProductModelId == id && b.SellEndDate == null);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: Bikes/Create
        public IActionResult Create()
        {
            ViewData["ParentProductCategoryId"] = new SelectList(db.ProductCategory, "ProductCategoryId", "Name");
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductCategoryId,ParentProductCategoryId,Name,Rowguid,ModifiedDate")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Add(productCategory);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentProductCategoryId"] = new SelectList(db.ProductCategory, "ProductCategoryId", "Name", productCategory.ParentProductCategoryId);
            return View(productCategory);
        }

        // GET: Bikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await db.ProductCategory.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            ViewData["ParentProductCategoryId"] = new SelectList(db.ProductCategory, "ProductCategoryId", "Name", productCategory.ParentProductCategoryId);
            return View(productCategory);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductCategoryId,ParentProductCategoryId,Name,Rowguid,ModifiedDate")] ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(productCategory);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.ProductCategoryId))
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
            ViewData["ParentProductCategoryId"] = new SelectList(db.ProductCategory, "ProductCategoryId", "Name", productCategory.ParentProductCategoryId);
            return View(productCategory);
        }

        // GET: Bikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await db.ProductCategory
                .Include(p => p.ParentProductCategory)
                .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategory = await db.ProductCategory.FindAsync(id);
            db.ProductCategory.Remove(productCategory);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
            return db.ProductCategory.Any(e => e.ProductCategoryId == id);
        }
    }
}
