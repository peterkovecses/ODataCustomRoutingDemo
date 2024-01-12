using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataCustomRoutingDemo.Models.V2;

namespace ODataCustomRoutingDemo.Controllers.V2;

[ODataRouteComponent("odata/v2")]
public class ProductsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(new[] { new Product() { Id = "1", Name = "Product 1" } });
}
