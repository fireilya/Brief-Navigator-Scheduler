using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.EFCore.IntegrationTests.TestsData;

[Table("InnerTestEntity")]
public class InnerTestEntity
{
    [Column("id"), Key]
    public required Guid Id { get; set; }

    [Column("name"), MaxLength(128)]
    public required string Name { get; set; }
}