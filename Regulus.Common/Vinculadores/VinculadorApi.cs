using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Regulus.Common.Configuradores;
using Regulus.Common.Formatadores;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Regulus.Common.Vinculadores
{
    public class VinculadorApi : IVinculadorDependencia
    {
        public void Vincular(IServiceCollection services)
        {

            #region Definindo a entrada e saída de dados com content negotiation padronizada
            services.AddControllers(op =>
            {
                op.RespectBrowserAcceptHeader = true;
                op.ReturnHttpNotAcceptable = true;
                op.OutputFormatters.Add(new FormatadorCsv());
            })
                .AddXmlSerializerFormatters()
                .AddJsonOptions(ops =>
                {
                    ops.JsonSerializerOptions.PropertyNamingPolicy = null;
                    ops.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                    ops.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    ops.JsonSerializerOptions.WriteIndented = true;
                    ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    ops.JsonSerializerOptions.MaxDepth = 64;
                    ops.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            #endregion

            #region Versionamento da API (Considerada pelo header ou pela query string)
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            #endregion

            #region Swagger documentation setup
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionConfiguration>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerConfiguration>();
            });
            #endregion

            #region CORS Setup
            services.AddCors(options =>
            {
                options.AddPolicy("All",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            #endregion
        }
    }
}
