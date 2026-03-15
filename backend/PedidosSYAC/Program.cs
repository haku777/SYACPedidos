using Microsoft.EntityFrameworkCore;
using PedidosSYAC.Services.Interfaces;
using PedidosSYAC.DataAccess;
using PedidosSYAC.Services.Services;
using PedidosSYAC.Services.Services.Interfaces;
using PedidosSYAC.Services.Services.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<PedidosContext>(options => {
    options.UseSqlServer((builder.Configuration.GetConnectionString("PedidosConection")));
});

builder.Services.AddScoped<IProductos, ProdutosService>();
builder.Services.AddScoped<IClientes, ClientesService>();
builder.Services.AddScoped<IEstados, EstadosService>();
builder.Services.AddScoped<IPedidos, PedidosService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

app.UseCors("AngularApp");

app.UseMiddleware<ErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PedidosContext>();
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();