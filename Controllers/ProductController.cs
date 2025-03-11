using System;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels.ProductVms;
using System.Transactions;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly FirstRunDbContext dbContext;

        public ProductController(FirstRunDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Filter(SearchProductVm vm)
        {
            var filteredproducts = await dbContext.Products.
                Include(x => x.Category).
                Where(x => x.Name.Contains(vm.Name)).
                OrderBy(x => x.Name).
                ToListAsync();

            if (filteredproducts.Count == 0)
            {
                ViewBag.Message = "No products found.";
            }
            
            vm.Products = filteredproducts;
            return View(vm);
        }

        public async Task<IActionResult> Index(ProductVm vm)
        {
            var products = await dbContext.Products.
                Include(x => x.Category).
                OrderBy(x => x.Name).
                ToListAsync();
            
            vm.Products = products;

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var productCategories = await dbContext.ProductCategories.
                OrderBy(x => x.Name).
                ToListAsync();

            var vm = new CreateProductVm();
            vm.ProductCategories = productCategories;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm vm)
        {
            using(var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(vm);
                    }

                    var productCategory = await dbContext.ProductCategories.
                        Where(x => x.Id == vm.ProductCategoryId).FirstOrDefaultAsync();

                    if (productCategory == null)
                    {
                        throw new Exception("Product category not found");
                    }

                    var product = new Product();
                    product.Name = vm.Name;
                    product.Category = productCategory;
                    product.Description = vm.Description;
                    product.Price = 0;
                    product.IsAvailable = true;

                    dbContext.Products.Add(product);
                    await dbContext.SaveChangesAsync();

                    txn.Complete();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var productCategories = await dbContext.ProductCategories.
                    OrderBy(x => x.Name).
                    ToListAsync();
                
                var product = await dbContext.Products.
                    Where(x => x.Id == id).FirstOrDefaultAsync();

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                var vm = new UpdateProductVm();
                vm.ProductCategories = productCategories;

                vm.Name = product.Name;
                vm.ProductCategoryId = product.Category.Id;
                vm.Description = product.Description;


                return View(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CreateProductVm vm)
        {
            using(var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(vm);
                    }

                    var productCategory = await dbContext.ProductCategories.
                        Where(x => x.Id == vm.ProductCategoryId).FirstOrDefaultAsync();

                    if (productCategory == null)
                    {
                        throw new Exception("Product category not found");
                    }

                    var product = await dbContext.Products.
                        Where(x => x.Id == id).ExecuteUpdateAsync(setters => setters.
                            SetProperty(x => x.Name, vm.Name)
                            .SetProperty(x => x.CategoryId, vm.ProductCategoryId)
                            .SetProperty(x => x.Description, vm.Description)
                            .SetProperty(x => x.Price, 0)
                            .SetProperty(x => x.IsAvailable, true)
                        );
                    
                    if (product == null)
                    {
                        throw new Exception("Product not found");
                    }

                    // product.Name = vm.Name;
                    // product.Category = productCategory;
                    // product.Description = vm.Description;
                    // product.Price = 0;
                    // product.IsAvailable = true;

                    // dbContext.Products.ExecuteUpdate(product);
                    // await dbContext.SaveChangesAsync();

                    txn.Complete();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            using(var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var product = await dbContext.Products.
                        Where(x => x.Id == id).ExecuteDeleteAsync();

                    if (product == null)
                    {
                        throw new Exception("Product not found");
                    }

                    // dbContext.Products.Remove(product);
                    // await dbContext.SaveChangesAsync();

                    txn.Complete();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}