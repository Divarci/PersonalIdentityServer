using RepositoryLayer.Extensions;
using ServiceLayer.Exceptions;
using ServiceLayer.Extensions;
using ServiceLayer.Filters;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.LoadRepositoryLayerExtensions(builder.Configuration);
builder.Services.LoadServiceLayerExtensions(builder.Configuration);




builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ValidateFilterAttribute());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var serviceScope = app.Services.CreateScope())
//{
//    var services = serviceScope.ServiceProvider;
//    var context = services.GetRequiredService<ConfigurationDbContext>();
//    DataSeed.ConfigureDbSeed(context);
//}


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomStatusCodePages();

app.UseHttpsRedirection();
app.UseIdentityServer();

app.UseCustomException();


//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
