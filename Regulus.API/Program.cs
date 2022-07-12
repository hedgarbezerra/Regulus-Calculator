using Regulus.Common.Vinculadores;

var builder = WebApplication.CreateBuilder(args);

#region Configurações de serviços
IVinculadorDependencia vinculadorApi = new VinculadorApi();

vinculadorApi.Vincular(builder.Services);
#endregion






var app = builder.Build();
#region Configuração do aplicativo



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

#endregion