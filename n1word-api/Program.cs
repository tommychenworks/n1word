using n1word_api.Interfaces;
using n1word_api.Models;
using n1word_api.Repositories;
using n1word_api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<WordConfig>(builder.Configuration.GetSection("WordConfig"));

builder.Services.AddScoped<IWebCrawlerService, WebCrawlerService>();
builder.Services.AddScoped<IWordRepository, WordRepository>();
builder.Services.AddScoped<IDocService, DocService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/Error");

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }