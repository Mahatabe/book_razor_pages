using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories;

[BindProperties]

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public Category Category { get; set; }

    public EditModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        Category = _db.Category.Find(id);
       // Category = _db.Category.FirstOrDefault(u=> u.Id==id);
    }

    public async Task<IActionResult> OnPost(Category category)
    {
        if(category.Name == Category.DisplayOrder.ToString()) 
        {
            ModelState.AddModelError("Category.Name", "The Display Order can not match the Name");
        }
        if(ModelState.IsValid)
        {
             _db.Category.Update(category);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Category edited successfully!";
            return RedirectToPage("Index");
        }

        return Page();
    }
}
