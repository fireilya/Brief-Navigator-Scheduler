using System;
using System.Threading.Tasks;
using AutoFixture;
using Core.EFCore.IntegrationTests.TestsData;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestCore.Common;
using TestCore.IntegrationTests;

namespace Core.EFCore.IntegrationTests;

public class DataContextVsDataContextFactoryTest : IntegrationTestBase
{
    private IDataContext DataContextFromPool => ServiceProvider.GetRequiredService<IDataContext>();
    private IDataContext DataContextFromFactory => DataContext;

    [Test]
    public async Task TestCreateViaDataContextFactoryWithoutDisposing()
    {
        // Arrange
        var testEntity = Fixture.Build<TestEntity>()
           .With(x => x.CreatedAtUtc, Fixture.Create<DateTime>().ToUniversalTime)
           .Create();

        // Act
        await DataContextFromFactory.InsertAsync(testEntity);

        // Assert
        var foundEntity = await DataContextFromFactory.FindAsync<TestEntity, Guid>(testEntity.Id);

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
        await using (var dataContext = DataContextFromFactory)
        {
            await dataContext.InsertAsync(testEntity);
        }

        // Assert
        await using (var dataContext = DataContextFromFactory)
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
            await using (var context = DataContextFromPool)
            {
                await context.InsertAsync(testEntity);
            }
        }

        // Assert
        await using (ServiceProvider.CreateAsyncScope())
        {
            await using (var context = DataContextFromPool)
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
        await DataContextFromPool.InsertAsync(testEntity);

        // Assert
        var foundEntities = await DataContextFromPool
           .SelectAsync<TestEntity, Guid>(x => x.Id, testEntity.Id);
        foundEntities.Should().HaveCount(1);
        var foundEntity = foundEntities[0];

        // Бд теряет точность у DateTime и TimeSpan, этой проверкой я хочу убедиться в том,
        // что мы достали запись из бд, а не из оперативной памяти
        foundEntity.Should().NotBeEquivalentTo(testEntity);

        foundEntity.Should()
           .BeEquivalentTo(testEntity, options => options.WithDateTimeCloseTo().WithTimeSpanCloseTo());
    }
}