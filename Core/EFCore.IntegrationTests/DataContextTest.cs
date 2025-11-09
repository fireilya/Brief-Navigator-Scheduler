using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Core.EFCore.IntegrationTests.TestsData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestCore.Common;
using TestCore.IntegrationTests;

namespace Core.EFCore.IntegrationTests;

public class DataContextTest : IntegrationTestBase
{
    [Test]
    public async Task TestCreateViaDataContextFactoryWithoutDisposing()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await DataContextFactory.Create().InsertAsync(testEntity);

        // Assert
        var foundEntity = await DataContextFactory.Create().FindAsync<TestEntity, Guid>(testEntity.Id);

        // Бд теряет точность у DateTime и TimeSpan, этой проверкой я хочу убедиться в том,
        // что мы достали запись из бд, а не из оперативной памяти
        foundEntity.Should().NotBeEquivalentTo(testEntity);

        foundEntity.Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }

    [Test]
    public async Task TestCreateViaDataContextFactoryWithDisposing()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await using (var dataContext = DataContextFactory.Create())
        {
            await dataContext.InsertAsync(testEntity);
        }

        // Assert
        await using (var dataContext = DataContextFactory.Create())
        {
            var foundEntity = await dataContext.FindAsync<TestEntity, Guid>(testEntity.Id);

            // Бд теряет точность у DateTime и TimeSpan, этой проверкой я хочу убедиться в том,
            // что мы достали запись из бд, а не из оперативной памяти
            foundEntity.Should().NotBeEquivalentTo(testEntity);

            foundEntity.Should()
               .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
        }
    }

    [Test]
    [Ignore(
        "В таком исполнении контекст не освобождает ресурсы и продолжает использоваться, " +
        "поэтому мы извлекаем запись из памяти, а не из бд. " +
        "Предположил бы, что это из-за scoped lifetime, но даже умышленно создавая новый scope контекст продолжает жить"
    )]
    public async Task TestCreateViaDataContext()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await using (ServiceProvider.CreateAsyncScope())
        {
            await using (var context = DataContext)
            {
                await context.InsertAsync(testEntity);
            }
        }

        // Assert
        await using (ServiceProvider.CreateAsyncScope())
        {
            await using (var context = DataContext)
            {
                var foundEntity = await context.FindAsync<TestEntity, Guid>(testEntity.Id);

                // Бд теряет точность у DateTime и TimeSpan, этой проверкой я хочу убедиться в том,
                // что мы достали запись из бд, а не из оперативной памяти
                foundEntity.Should().NotBeEquivalentTo(testEntity);

                foundEntity.Should()
                   .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
            }
        }
    }

    [Test]
    public async Task TestCreateDataContextWithoutDispose_ButUseIQueryableInsteadFind()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await DataContext.InsertAsync(testEntity);

        // Assert
        var foundEntities = await DataContext.SelectAsync<TestEntity, Guid>(x => x.Id, testEntity.Id);
        foundEntities.Should().HaveCount(1);
        var foundEntity = foundEntities[0];

        // Бд теряет точность у DateTime и TimeSpan, этой проверкой я хочу убедиться в том,
        // что мы достали запись из бд, а не из оперативной памяти
        foundEntity.Should().NotBeEquivalentTo(testEntity);

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
        foundEntity.Should().NotBeNull();
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
    public async Task TestFind_EntityNotExist()
    {
        // Assert
        var testEntity = await DataContext.FindAsync<TestEntity, Guid>(Guid.NewGuid());
        testEntity.Should().BeNull();
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
        (await DataContextFactory.Create().FindAsync<TestEntity, Guid>(testEntity.Id))
           .Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());

        var updatedEntity = Fixture.Build<TestEntity>()
           .With(x => x.Id, testEntity.Id)
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await DataContextFactory.Create().UpdateAsync(updatedEntity);

        // Assert
        var foundEntity = await DataContextFactory.Create().FindAsync<TestEntity, Guid>(testEntity.Id);
        foundEntity.Should().NotBeNull();
        foundEntity.Should()
           .BeEquivalentTo(updatedEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }
}