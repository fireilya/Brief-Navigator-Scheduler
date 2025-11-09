using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Equivalency;

namespace TestCore.Common;

public static class FluentAssertionsExtensions
{
    public static EquivalencyOptions<TExpectation> WithDateTimeCloseTo<TExpectation>(
        this EquivalencyOptions<TExpectation> equivalencyOptions,
        TimeSpan? precision = null
    ) => equivalencyOptions
       .Using<DateTime>(x => x.Expectation.Should()
           .BeCloseTo(x.Subject, precision ?? TimeSpan.FromMilliseconds(10))
        )
       .WhenTypeIs<DateTime>();

    public static EquivalencyOptions<TExpectation> WithTimeSpanCloseTo<TExpectation>(
        this EquivalencyOptions<TExpectation> equivalencyOptions,
        TimeSpan? precision = null
    ) => equivalencyOptions
       .Using<TimeSpan>(x => x.Expectation.Should()
           .BeCloseTo(x.Subject, precision ?? TimeSpan.FromMilliseconds(10))
        )
       .WhenTypeIs<TimeSpan>();

    public static void ContainCollection<TExpectation>(
        this GenericCollectionAssertions<TExpectation> equivalencyOptions,
        IEnumerable<TExpectation> expectationCollection,
        Func<EquivalencyOptions<TExpectation>,
            EquivalencyOptions<TExpectation>> config
    )
    {
        foreach (var expectation in expectationCollection)
        {
            equivalencyOptions.ContainEquivalentOf(expectation, config);
        }
    }
}