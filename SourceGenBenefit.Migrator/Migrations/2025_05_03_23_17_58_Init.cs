using FluentMigrator;

namespace SourceGenBenefit.Migrator.Migrations;

[Migration(2025_05_03_23_17_58)]
public class Init : Migration
{
    public override void Up()
    {
        Create.Table("test_entities")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("number").AsInt32().NotNullable()
            .WithColumn("amount").AsDecimal().NotNullable()
            .WithColumn("description").AsString(256).NotNullable()
            .WithColumn("flag").AsBoolean().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("test_entities");
    }
}