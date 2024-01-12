using Microsoft.AspNetCore.OData;
using ODataCustomRoutingDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var v1Model = EdmModelBuilder.BuildV1();
var v2Model = EdmModelBuilder.BuildV2();

builder.Services.AddControllers().AddOData(opt => opt
    .AddRouteComponents("odata/v1", v1Model)
    .AddRouteComponents("odata/v2", v2Model)
    .Count().Filter().Expand().Select().OrderBy().SetMaxTop(5)
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();