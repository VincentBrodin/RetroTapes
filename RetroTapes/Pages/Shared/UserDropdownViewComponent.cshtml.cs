
using Microsoft.AspNetCore.Mvc;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Shared;

public class UserDropdownViewComponent: ViewComponent
{
    public List<Staff> Staff { get; set; } = [];
    private readonly IRepository<Staff> _staffRepo;

    public UserDropdownViewComponent(IRepository<Staff> staffRepo)
    {
        _staffRepo = staffRepo;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _staffRepo.AllAsync();
        return View(model);
    }
}
