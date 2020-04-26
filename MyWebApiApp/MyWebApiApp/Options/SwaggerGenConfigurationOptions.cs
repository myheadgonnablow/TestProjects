using Microsoft.OpenApi.Models;

namespace MyWebApiApp.Options
{
    public class SwaggerGenConfigurationOptions
    {
        public string Name { get; set; }
        public OpenApiInfo ApiInfo { get; set; }
    }
}
