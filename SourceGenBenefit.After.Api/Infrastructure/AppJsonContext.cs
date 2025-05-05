using System.Text.Json.Serialization;
using Mediator;
using SourceGenBenefit.After.Create;
using SourceGenBenefit.After.GetList;

namespace SourceGenBenefit.After.Api.Infrastructure;

[JsonSerializable(typeof(IReadOnlyList<TestEntityDto>))]
[JsonSerializable(typeof(CreateTestEntityCommand))]
[JsonSerializable(typeof(Unit))]
[JsonSourceGenerationOptions]
public partial class AppJsonContext : JsonSerializerContext;