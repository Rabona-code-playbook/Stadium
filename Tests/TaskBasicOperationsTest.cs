using Models;
using Xunit.Abstractions;

namespace Tests;
public class TaskBasicOperationsTest
{
    private readonly ITestOutputHelper output;

    public TaskBasicOperationsTest(ITestOutputHelper testOutputHelper)
    {
        output = testOutputHelper;
    }

    [Fact]
    public void NotAwait()
    {
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: parent");
        var winner = PredictWinnerUsingMagicHeruristics(new Team { name = "Colombia"}, new Team { name = "Argentina" });
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: returned {winner}");
        Assert.True(true);
    }

    [Fact]
    public void UsingResult()
    {
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: parent");
        var winner = PredictWinnerUsingMagicHeruristics(new Team { name = "Colombia" }, new Team { name = "Argentina" }).Result;
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: returned {winner}");
        Assert.True(true);
    }

    [Fact]
    public async void AsyncAction()
    {
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: parent");
        var winner = await PredictWinnerUsingMagicHeruristics(new Team { name = "Colombia" }, new Team { name = "Argentina" });
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: returned {winner}");
        Assert.True(true);
    }

    [Fact]
    public async void AsyncActionConfigureAwait()
    {
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: parent");
        var winner = await PredictWinnerUsingMagicHeruristics(new Team { name = "Colombia" }, new Team { name = "Argentina" }).ConfigureAwait(false);
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: returned {winner}");
        Assert.True(true);
    }

    /// <summary>
    /// Predicts Winner Team based on magic heuristics
    /// </summary>
    /// <param name="home"></param>
    /// <param name="away"></param>
    /// <returns></returns>
    private async Task<Team> PredictWinnerUsingMagicHeruristics(Team home, Team away)
    {
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: PredictWinnerUsingMagicHeruristics");
        await Task.Delay(10);
        output.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: PredictWinnerUsingMagicHeruristics 2");
        return new Random().Next(1, 100) % 2 == 0 ? home : away;
        
    }


}
