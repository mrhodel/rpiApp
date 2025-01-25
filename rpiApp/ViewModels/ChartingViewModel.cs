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
    private readonly List<DateTimePoint> values = [];
    private readonly DateTimeAxis customAxis;
    public ObservableCollection<ISeries> Series { get; set; }
    public Axis[] XAxes { get; set; }
    public object Sync { get; } = new object();
    public bool IsReading { get; set; } = true;

    public ChartingViewModel()
    {
        Series = [new LineSeries<DateTimePoint>
        {
            Values = values,
            Fill = null,
            Stroke = new SolidColorPaint(SKColors.LightGreen, strokeWidth: 1.0f),
            LineSmoothness = 0.5f,
            GeometryFill = null,
            GeometryStroke = null,
        }];

        customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
        {
            CustomSeparators = GetSeparators(),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
        };

        XAxes = [customAxis];
        _ = ReadDataAsync();
    }
    private async Task ReadDataAsync()
    {
        Random random = new();
        while (IsReading)
        {
            await Task.Delay(100);
            // Because we are updating the chart from a different thread 
            // we need to use a lock to access the chart data. 
            // this is not necessary if your changes are made on the UI thread. 
            lock (Sync)
            {
                values.Add(new DateTimePoint(DateTime.Now, random.Next(0, 10)));
                if (values.Count > 250) values.RemoveAt(0);

                // we need to update the separators every time we add a new point 
                customAxis.CustomSeparators = GetSeparators();
            }
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

    public void OnClearData()
    {
        lock (Sync)
        {
            values.Clear();
        }
    }
}
