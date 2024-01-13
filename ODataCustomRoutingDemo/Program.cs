using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
using ODataCustomRoutingDemo.Models;
using ODataCustomRoutingDemo.Swagger;

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });

    options.DocInclusionPredicate((version, apiDesc) 
        => apiDesc.RelativePath!.Contains(version));

    options.DocumentFilter<DocumentFilter>();
    options.OperationFilter<OperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();