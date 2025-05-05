using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

const string pgCnnStr =
    "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=";

var dbName = args[0];

await using var dataSource = NpgsqlDataSource.Create(pgCnnStr + "postgres");

await using (var cmd = dataSource.CreateCommand("SELECT 1 FROM pg_database WHERE datname=@DbName"))
{
    cmd.Parameters.AddWithValue("DbName", dbName);
    var result = (int?)await cmd.ExecuteScalarAsync();
    if (result is null)
    {
        await using var createDbCmd = dataSource.CreateCommand(
            $"""
                CREATE DATABASE {dbName}
                WITH
                    ENCODING = 'utf-8'
                    CONNECTION LIMIT = -1;
             """);
        await createDbCmd.ExecuteNonQueryAsync();
    }
}

var services = new ServiceCollection();
var provider = services.AddFluentMigratorCore()
    .ConfigureRunner(
        rb => rb.AddPostgres()
            .WithGlobalConnectionString(pgCnnStr + dbName)
            .ScanIn(Assembly.GetExecutingAssembly()))
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);
using (var scope = provider.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}