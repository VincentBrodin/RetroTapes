using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetroTapes.Data;
using RetroTapes.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add db services
builder.Services.AddDbContext<SakilaContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddScoped<IRepository<Actor>, GenericRepository<Actor>>();
builder.Services.AddScoped<IRepository<Address>, GenericRepository<Address>>();
builder.Services.AddScoped<IRepository<Category>, GenericRepository<Category>>();
builder.Services.AddScoped<IRepository<City>, GenericRepository<City>>();
builder.Services.AddScoped<IRepository<Country>, GenericRepository<Country>>();
builder.Services.AddScoped<IRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddScoped<IRepository<CustomerList>, GenericRepository<CustomerList>>();
builder.Services.AddScoped<IRepository<Film>, FilmRepository>();
builder.Services.AddScoped<IRepository<FilmActor>, GenericRepository<FilmActor>>();
builder.Services.AddScoped<IRepository<FilmCategory>, GenericRepository<FilmCategory>>();
builder.Services.AddScoped<IRepository<FilmList>, GenericRepository<FilmList>>();
builder.Services.AddScoped<IRepository<FilmText>, GenericRepository<FilmText>>();
builder.Services.AddScoped<IRepository<Inventory>, GenericRepository<Inventory>>();
builder.Services.AddScoped<IRepository<Language>, GenericRepository<Language>>();
builder.Services.AddScoped<IRepository<Payment>, GenericRepository<Payment>>();
builder.Services.AddScoped<IRepository<Rental>, RentalRepository>();
builder.Services.AddScoped<IRepository<SalesByFilmCategory>, GenericRepository<SalesByFilmCategory>>();
builder.Services.AddScoped<IRepository<SalesByStore>, GenericRepository<SalesByStore>>();
builder.Services.AddScoped<IRepository<Staff>, GenericRepository<Staff>>();
builder.Services.AddScoped<IRepository<StaffList>, GenericRepository<StaffList>>();
builder.Services.AddScoped<IRepository<Store>, GenericRepository<Store>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
