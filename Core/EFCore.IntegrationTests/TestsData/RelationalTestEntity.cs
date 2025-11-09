using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.EFCore.IntegrationTests.TestsData;

[Table("relational_test_entities")]
public class RelationalTestEntity
{
    [Column("id"), Key]
    public required Guid Id { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("inner_entities")]
    public required InnerTestEntity[] InnerTestEntities { get; set; }
}