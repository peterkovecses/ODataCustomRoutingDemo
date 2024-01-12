using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace ODataCustomRoutingDemo.Models;

public static class EdmModelBuilder
{
    public static IEdmModel BuildV1()
    {
        var builder = new ODataConventionModelBuilder();
        var products = builder.EntitySet<V1.Product>("Products").EntityType;
        products.HasKey(product => product.Id);

        return builder.GetEdmModel();
    }

    public static IEdmModel BuildV2()
    {
        var builder = new ODataConventionModelBuilder();
        var products = builder.EntitySet<V2.Product>("Products").EntityType;
        products.HasKey(product => product.Id);

        return builder.GetEdmModel();
    }
}
