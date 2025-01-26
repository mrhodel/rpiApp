/*
 * ChartingViewModel.cs
 * 1/26/2025 Mike Hodel
 */
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace rpiApp.ViewModels;

public partial class ChartingViewModel : ViewModelBase
{
    private readonly List<DateTimePoint> valuesA = [];
    private readonly List<DateTimePoint> valuesB = [];
    private readonly DateTimeAxis customAxis;
    private readonly bool isReading = true;
    private readonly Random random = new();
    private bool isPaused = false;

    public ObservableCollection<ISeries> Series { get; set; }
    public Axis[] XAxes { get; set; }
    public object Sync { get; set; } = new object();
    public string PauseText { get; set; } = "Pause";

    public ChartingViewModel()
    {
        Series = [
            new LineSeries<DateTimePoint>
            {
                Values = valuesB,
                Fill = null,
                Stroke = new SolidColorPaint(SKColors.LightGreen, strokeWidth: 1.0f),
                LineSmoothness = 0.2f,
                GeometryFill = null,
                GeometryStroke = null,
            },
            new LineSeries<DateTimePoint>
            {
                Values = valuesA,
                Fill = null,
                Stroke = new SolidColorPaint(SKColors.LightBlue, strokeWidth: 1.0f),
                LineSmoothness = 0.2f,
                GeometryFill = null,
                GeometryStroke = null,
            }
        ];

        customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
        {
            CustomSeparators = GetSeparators(),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            SeparatorsPaint = new SolidColorPaint(SKColors.Gray.WithAlpha(100))
        };

        XAxes = [customAxis];
        _ = ReadDataAsync();
    }

    private async Task ReadDataAsync()
    {
        while (isReading)
        {
            await Task.Delay(100);
            if (isPaused) continue;
            ReadData();
        }
    }

    private void ReadData()
    {
        // Because we are updating the chart from a different thread 
        // we need to use a lock to access the chart data. 
        // this is not necessary if your changes are made on the UI thread. 
        lock (Sync)
        {
            valuesA.Add(new DateTimePoint(DateTime.Now, random.Next(0, 10)));
            if (valuesA.Count > 250)
            {
                valuesA.RemoveAt(0);
            }

            valuesB.Add(new DateTimePoint(DateTime.Now, random.Next(10, 20)));
            if (valuesB.Count > 250)
            {
                valuesB.RemoveAt(0);
            }

            // we need to update the separators every time we add a new point 
            customAxis.CustomSeparators = GetSeparators();
        }
    }

    private static double[] GetSeparators()
    {
        var now = DateTime.Now;

        return
        [
            now.AddSeconds(-25).Ticks,
            now.AddSeconds(-20).Ticks,
            now.AddSeconds(-15).Ticks,
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
        ];
    }

    private static string Formatter(DateTime date)
    {
        var secsAgo = (DateTime.Now - date).TotalSeconds;

        return secsAgo < 1 ? "now" : $"{secsAgo:N0}s ago";
    }

    public void OnPause()
    {
        isPaused = !isPaused;
        PauseText = isPaused ? "Resume" : "Pause";
    }

    public void OnClearData()
    {
        lock (Sync)
        {
            valuesA.Clear();
            valuesB.Clear();
            customAxis.CustomSeparators = GetSeparators();
        }
    }
}
