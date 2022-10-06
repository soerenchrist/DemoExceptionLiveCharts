using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp;

namespace DemoExceptionLiveCharts;

public class ExampleChartView : ContentView
{
    private CartesianChart _chart;
    private LineSeries<ObservablePoint> _series;
    private ISeries[] _chartSeries;

    public ExampleChartView()
    {
        var grid = new Grid
        {
            RowDefinitions = new RowDefinitionCollection()
            {
                new(GridLength.Auto),
                new(GridLength.Star)
            }
        };
        var button = new Button
        {
            Text = "Render"
        };
        button.Clicked += (sender, args) =>
            RenderChart();
        grid.Children.Add(button);

        _series = new LineSeries<ObservablePoint>();

        _series.Fill = null;
        _series.Stroke = new SolidColorPaint(SKColors.Blue);
        _series.Name = "Sample value";
        _series.LineSmoothness = 0;

        _chartSeries = new ISeries[]
        {
            _series
        };
        _chart = new CartesianChart
        {
            XAxes = new[]
            {
                new Axis
                {
                    Name = "Duration",
                    Labeler = val => $"{val} s"
                },
            },
            Series = _chartSeries,
            AutoUpdateEnabled = true,
            HeightRequest = 200,
            UpdaterThrottler = TimeSpan.FromSeconds(1),
            AnimationsSpeed = TimeSpan.FromSeconds(4)
        };
        Grid.SetRow(_chart, 1);
        grid.Children.Add(_chart);
        Content = grid;
    }

    private void RenderChart()
    {
        var random = new Random();
        var values = new List<ObservablePoint>();
        for (int i = 0; i < 100; i++)
        {
            var newValue = random.Next(-100, 100);
            values.Add(new ObservablePoint(i, newValue));
        }

        MainThread.BeginInvokeOnMainThread(() => { _series.Values = values; });
    }
}