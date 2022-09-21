using CSharpFunctionalExtensions;

namespace NftFaucetRadzen.Utils;

public static class ResultWrapper
{
    public static Result Wrap(Action action)
    {
        try
        {
            action();
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }

    public static Result<T> Wrap<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            return Result.Failure<T>(e.Message);
        }
    }

    public static async Task<Result> Wrap(Func<Task> func)
    {
        try
        {
            var task = func();
            await task;
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }

    public static async Task<Result<T>> Wrap<T>(Func<Task<T>> func)
    {
        try
        {
            var task = func();
            return await task;
        }
        catch (Exception e)
        {
            return Result.Failure<T>(e.Message);
        }
    }

    public static Task<Result> Wrap(Task task) => Wrap(() => task);
    public static Task<Result<T>> Wrap<T>(Task<T> task) => Wrap(() => task);
}
