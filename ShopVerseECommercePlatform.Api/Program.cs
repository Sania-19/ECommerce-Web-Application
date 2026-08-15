using Microsoft.AspNetCore.StaticFiles;
using ShopVerseECommercePlatform.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Adding Project Services
builder.Services.AddApiService(builder.Configuration, builder.Environment);
var app = builder.Build();

var imagePath = Path.Combine(
    app.Environment.WebRootPath,
    "Files",
    "019fc158-bbb2-70d0-bfc1-2a237849c27a.webp"
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
var provider = new FileExtensionContentTypeProvider();

provider.Mappings[".webp"] = "image/webp";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.UseCors("ECommercePolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
