using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TestCore.UnitTests;

namespace Core.Utilities.UnitTests;

public class GlobalCounterTest : UnitTestBase
{
    [Test]
    public void TestGetCount_NotExistKey_OneThread()
    {
        var key = Guid.NewGuid().ToString();
        var count = GlobalCounter.GetCount(key);
        count.Should().Be(1);
    }

    [Test]
    public void TestGetCount_ExistKey_OneThread()
    {
        var key = Guid.NewGuid().ToString();
        GlobalCounter.GetCount(key).Should().Be(1);
        GlobalCounter.GetCount(key).Should().Be(1);
    }

    [Test]
    public void TestGetCountWithIncrement_NotExistKey_OneThread()
    {
        var key = Guid.NewGuid().ToString();
        var count = GlobalCounter.GetCountWithIncrement(key);
        count.Should().Be(1);
    }

    [Test]
    public void TestGetCountWithIncrement_ExistKey_OneThread()
    {
        var key = Guid.NewGuid().ToString();
        GlobalCounter.GetCountWithIncrement(key).Should().Be(1);
        GlobalCounter.GetCountWithIncrement(key).Should().Be(2);
    }

    [TestCase(5, 5)]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    [TestCase(2, 20)]
    [TestCase(1, 10)]
    public async Task TestGetCount_ManyThreads(int keysCount, int threadsCount)
    {
        var keysPool = Enumerable.Range(1, keysCount).Select(x => $"{keysCount}_{threadsCount}_{x}").ToArray();

        var mainTask = Task.Delay(100);
        var processTasks = Enumerable.Range(1, threadsCount)
           .Select(process => mainTask.ContinueWith(_ => GetCountWithIncrementProcess(process, keysPool)))
           .ToArray();
        await Task.WhenAll(processTasks);
        await Task.Delay(100);

        var totalCount = 0;
        foreach (var key in keysPool)
        {
            var count = GlobalCounter.GetCount(key);
            totalCount += count;
            await TestContext.Out.WriteLineAsync($"Key: {key}, Count: {count}");
        }

        totalCount.Should().Be(threadsCount * 10 + keysCount);
    }

    private static void GetCountWithIncrementProcess(int processNumber, string[] keys)
    {
        for (var i = 0; i < 10; i++)
        {
            var index = new Random().Next(keys.Length - 1);
            var key = keys[index];
            var count = GlobalCounter.GetCountWithIncrement(keys[index]);
            TestContext.Out.WriteLine($"Process: {processNumber}, Key: {key}, Count: {count}");
        }
        TestContext.Out.WriteLine($"Process: {processNumber} finished");
    }
}