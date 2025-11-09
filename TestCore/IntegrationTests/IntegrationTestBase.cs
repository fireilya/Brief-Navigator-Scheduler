using System;
using AutoFixture;
using Core.EFCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace TestCore.IntegrationTests;

[TestFixture]
[System.ComponentModel.Category("InegrationTests")]
public abstract class IntegrationTestBase
{
    /// <summary>
    ///     Fixture для создания полностью заполненных моделей случайными значениями
    /// </summary>
    protected readonly Fixture Fixture = new();

    /// <summary>
    ///     Сконфигурированный ServiceProvider, по умолчанию предоставляет все
    ///     реализации тестируемой сборкии тестовой сборки
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = SetupFixtureBase.TestConfiguration.ServiceProvider;

    /// <summary>
    ///     Контекст для тестов. Можно работать с ним не используя тестируемый сервис.
    /// </summary>
    protected IDataContext DataContext => DataContextFactory.Create();

    private IDataContextFactory DataContextFactory { get; } =
        SetupFixtureBase.TestConfiguration.ServiceProvider.GetRequiredService<IDataContextFactory>();
}