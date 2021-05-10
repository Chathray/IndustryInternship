using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Idis.WebApi
{
    public class TagDescriptionsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag> {
                new OpenApiTag { Name = "Event", Description = "Browse the Event catalog" },
                new OpenApiTag { Name = "Intern", Description = "Browse the Intern catalog" },
                new OpenApiTag { Name = "User", Description = "Browse the User catalog" },
                new OpenApiTag { Name = "Question", Description = "Browse the Question catalog" }
            };
        }
    }
}
