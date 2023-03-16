using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories;

[BindProperties]

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public Category Category { get; set; }

    public CreateModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(Category category)
    {
        if(category.Name == Category.DisplayOrder.ToString()) 
        {
            ModelState.AddModelError("Category.Name", "The Display Order can not match the Name");
        }
        if(ModelState.IsValid)
        {
            await _db.Category.AddAsync(category);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Category created successfully!";
            return RedirectToPage("Index");
        }

        return Page();
    }
}
