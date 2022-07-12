using Regulus.Common.Vinculadores;

var builder = WebApplication.CreateBuilder(args);

#region Configura��es de servi�os
IVinculadorDependencia vinculadorApi = new VinculadorApi();

vinculadorApi.Vincular(builder.Services);
#endregion






var app = builder.Build();
#region Configura��o do aplicativo



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

#endregion