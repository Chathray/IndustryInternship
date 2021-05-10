using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Idis.WebApi
{
    public class ExamplesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Example = GetExampleOrNullFor(context.Type);
        }

        private static IOpenApiAny GetExampleOrNullFor(Type type)
        {
            return type.Name switch
            {
                nameof(RegisterRequest) => ModelSample.RegisterRequest.ToOpenApiObject(),
                nameof(PasswordUpdateRequest) => ModelSample.PasswordUpdateRequest.ToOpenApiObject(),
                nameof(CreateEventRequest) => ModelSample.EventRequest.ToOpenApiObject(),
                nameof(AuthenticationRequest) => ModelSample.AuthenticationRequest.ToOpenApiObject(),
                nameof(CreateInternRequest) => ModelSample.CreateInternRequest.ToOpenApiObject(),
                nameof(EvaluateRequest) => ModelSample.EvaluateRequest.ToOpenApiObject(),
                nameof(CreateQuestionRequest) => ModelSample.CreateQuestionRequest.ToOpenApiObject(),
                nameof(Application.TrainingModel) => ModelSample.TrainingModel.ToOpenApiObject(),
                _ => null,
            };
        }
    }
}
