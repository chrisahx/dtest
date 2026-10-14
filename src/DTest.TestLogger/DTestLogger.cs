using System;
using System.Text.Json;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;


namespace DTest.TestLogger;


[FriendlyName("dtest")]
[ExtensionUri("logger://dtest/v1")]
public sealed class DTestLogger : ITestLogger
{
    public void Initialize(TestLoggerEvents events, string testRunDirectory)
    {
        events.TestRunStart += OnRunStarted;
        events.TestResult += OnTestResult;
        events.TestRunComplete += OnRunCompleted;
        events.TestRunMessage += OnMessage;
    }


    private static void OnRunStarted(object? sender, TestRunStartEventArgs e)
    {
        Emit(new
        {
            Type = "run_started",
            Timestamp = DateTimeOffset.UtcNow
        });
    }

    private static void OnTestResult(
        object? sender,
        TestResultEventArgs e)
    {
        var result = e.Result;

        Emit(new
        {
            Type = "test_result",
            Name = result.TestCase.DisplayName,
            FullyQualifiedName =
                result.TestCase.FullyQualifiedName,
            Outcome = result.Outcome.ToString(),
            DurationMs = result.Duration.TotalMilliseconds,
            ErrorMessage = result.ErrorMessage,
            StackTrace = result.ErrorStackTrace
        });
    }

    private static void OnRunCompleted(
        object? sender,
        TestRunCompleteEventArgs e)
    {
        Emit(new
        {
            Type = "run_completed",
            Timestamp = DateTimeOffset.UtcNow,
            IsAborted = e.IsAborted,
            IsCanceled = e.IsCanceled
        });
    }

    private static void OnMessage(
        object? sender,
        TestRunMessageEventArgs e)
    {
        Emit(new
        {
            Type = "message",
            Level = e.Level.ToString(),
            Message = e.Message
        });
    }

    private static readonly object WriteLock = new();

    private static void Emit(object data)
    {
        var json = JsonSerializer.Serialize(data);

        lock (WriteLock)
        {
            Console.WriteLine("DTEST_EVENT:" + json);
        }
    }
}
