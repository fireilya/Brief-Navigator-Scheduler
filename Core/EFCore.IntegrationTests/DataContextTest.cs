using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Core.EFCore.IntegrationTests.TestsData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestCore.Common;
using TestCore.IntegrationTests;

namespace Core.EFCore.IntegrationTests;

public class DataContextTest : IntegrationTestBase
{
    [Test]
    public async Task TestCreate()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await DataContext.InsertAsync(testEntity);

        // Assert
        var foundEntity = await DataContext.FindAsync<TestEntity, Guid>(testEntity.Id);
        foundEntity.Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestCreateMany()
    {
        // Arrange
        var testEntities = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .CreateMany(10)
           .ToArray();

        // Act
        await DataContext.InsertRangeAsync(testEntities);

        // Assert
        var foundEntities = await DataContext.SelectAsync<TestEntity, Guid>(
            x => x.Id,
            testEntities.Select(x => x.Id).ToArray()
        );
        foundEntities.Should().HaveCount(testEntities.Length);
        foundEntities.Should()
           .BeEquivalentTo(testEntities, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestFind_EntityExist()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();
        await DataContext.InsertAsync(testEntity);

        // Act
        var foundEntity = await DataContext.FindAsync<TestEntity, Guid>(testEntity.Id);

        // Assert
        foundEntity.Should().NotBeNull();
        foundEntity.Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestFind_EntityNotExist()
    {
        // Assert
        var testEntity = await DataContext.FindAsync<TestEntity, Guid>(Guid.NewGuid());
        testEntity.Should().BeNull();
    }

    [Test]
    public async Task TestRead_EntityExist()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();
        await DataContext.InsertAsync(testEntity);

        // Act
        var foundEntity = await DataContext.ReadAsync<TestEntity, Guid>(testEntity.Id);

        // Assert
        foundEntity.Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestRead_EntityNotExist()
    {
        // Assert
        await DataContext.Invoking(context => context.ReadAsync<TestEntity, Guid>(Guid.NewGuid()))
           .Should()
           .ThrowAsync<EntityNotFoundException>();
    }

    [Test]
    public async Task TestSelectByPrimaryKey()
    {
        // Arrange
        var testEntities = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .CreateMany(10)
           .ToArray();
        await DataContext.InsertRangeAsync(testEntities);

        // Act
        var foundEntities = await DataContext.SelectAsync<TestEntity, Guid>(
            x => x.Id,
            testEntities.Select(x => x.Id).ToArray()
        );

        // Assert
        foundEntities.Should().HaveCount(testEntities.Length);
        foundEntities.Should()
           .BeEquivalentTo(testEntities, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestSelectByOtherColumn()
    {
        // Arrange
        var testEntities = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .CreateMany(10)
           .ToArray();
        await DataContext.InsertRangeAsync(testEntities);

        // Act
        var foundEntities = await DataContext.SelectAsync<TestEntity, DateTime>(
            x => x.CreatedAtUtc,
            testEntities.Select(x => x.CreatedAtUtc).ToArray()
        );

        // Assert
        foundEntities.Should().HaveCount(testEntities.Length);
        foundEntities.Should()
           .BeEquivalentTo(testEntities, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestSelectByBoolColumn()
    {
        // Arrange
        const bool boolValue = true;
        var testEntities = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .With(x => x.TestBool, boolValue)
           .CreateMany(3)
           .ToArray();
        await DataContext.InsertRangeAsync(testEntities);

        // Act
        var foundEntities = await DataContext.SelectAsync<TestEntity, bool>(
            x => x.TestBool,
            boolValue
        );

        // Assert
        await TestContext.Out.WriteLineAsync($"found {foundEntities.Length} rows");
        foundEntities.Should().HaveCountGreaterThanOrEqualTo(testEntities.Length);
        foundEntities.Should()
           .ContainCollection(testEntities, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestGetTable()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();
        await DataContext.InsertAsync(testEntity);

        // Act
        var foundEntity = await DataContext.GetTable<TestEntity>()
           .Where(x => x.Name == testEntity.Name)
           .SingleAsync();

        // Assert
        foundEntity.Should().NotBeNull();
        foundEntity.Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestFullUpdate()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();
        await DataContext.InsertAsync(testEntity);
        (await DataContext.FindAsync<TestEntity, Guid>(testEntity.Id))
           .Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());

        var updatedEntity = Fixture.Build<TestEntity>()
           .With(x => x.Id, testEntity.Id)
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await DataContext.UpdateAsync(updatedEntity);

        // Assert
        var foundEntity = await DataContext.FindAsync<TestEntity, Guid>(testEntity.Id);
        foundEntity.Should().NotBeNull();
        foundEntity.Should()
           .BeEquivalentTo(updatedEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }
}