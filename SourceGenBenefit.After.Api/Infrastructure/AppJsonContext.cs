using System.Text.Json.Serialization;
using Mediator;
using SourceGenBenefit.Contracts;

namespace SourceGenBenefit.After.Api.Infrastructure;

[JsonSerializable(typeof(IReadOnlyList<TestEntityDto>))]
[JsonSerializable(typeof(CreateTestEntity))]
[JsonSerializable(typeof(Unit))]
[JsonSourceGenerationOptions]
public partial class AppJsonContext : JsonSerializerContext;