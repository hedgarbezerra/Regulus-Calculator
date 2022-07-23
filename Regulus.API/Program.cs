using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Regulus.Common.Vinculadores;
using Serilog;
using System.Text.Json;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
#region Configurações de serviços
IVinculadorDependencia vinculadorApi = new VinculadorApi();


vinculadorApi.Vincular(builder.Services);
#endregion


var app = builder.Build();
#region setting up logging 

//Log.Logger = new LoggerConfiguration()
//          .Enrich.FromLogContext()
//          .WriteTo.MSSqlServer("",
//              autoCreateSqlTable: true,
//              restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
//              tableName: "")
//          .CreateLogger();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()?
        .Error;

    if (exception != null)
        return;

    var response = new { message = exception.Message, stacktrace = exception.StackTrace };
    context.Response.ContentType = "application/json";

    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
}));
#endregion

#region Configuração do aplicativo
IApiVersionDescriptionProvider apiVersioningProvider = builder.Services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var description in apiVersioningProvider?.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion