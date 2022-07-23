using Microsoft.AspNetCore.Diagnostics;
using Regulus.Common.Vinculadores;
using Serilog;
using System.Text.Json;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

#region Configura��es de servi�os
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

#region Configura��o do aplicativo

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion