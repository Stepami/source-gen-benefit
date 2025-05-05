using SourceGenBenefit.Before;
using SourceGenBenefit.Before.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddBefore(nullLogger: false, nullRepository: false);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapControllers();
app.Run();