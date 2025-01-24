using PropertyModels.Collections;
using PropertyModels.ComponentModel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace rpiApp.Models;

public class CameraParameters(string description) : ReactiveObject
{
    public readonly string Description = description;

    [Category("Frame")]
    [DisplayName("framerate")]
    [Range(-1.0f, 120.0f)]
    [FloatPrecision(0)]
    public double FrameRate { get; set; } = -1;

    [Category("Focus")]
    [DisplayName("lens-position")]
    [Description("The position of the lens")]
    [Trackable]
    [Range(0, 100)]
    public int LensPosition { get; set; } = 50;

    [Category("Focus")]
    [DisplayName("autofocus-mode")]
    public SelectableList<string> AutoFocusMode { get; set; } = new SelectableList<string>(["manual", "auto", "continuous"], "auto");

    [Flags]
    public enum Led
    {
        [EnumDisplayName("Red (1)")]
        Red = 1,
        [EnumDisplayName("Green (2)")]
        Green = 2,
        [EnumDisplayName("Blue (4)")]
        Blue = 4,
        [EnumDisplayName("Yellow (8)")]
        Yellow = 8,
        [EnumDisplayName("White (16)")]
        White = 16,


        [EnumExclude]
        CanNotSeeThis = 64,
    }

    [DisplayName("led Indicators")]
    public Led LedIndicators { get; set; }

}
