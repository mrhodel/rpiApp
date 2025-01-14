using PropertyModels.Collections;
using PropertyModels.ComponentModel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace rpiApp.Models;

public class CameraParameters : ReactiveObject
{
    public readonly string Description;

    public CameraParameters(string description)
    {
        Description = description;
    }

    [Category("Frame")]
    [DisplayName("Frame Rate")]
    [Range(0.1f, 120.0f)]
    [FloatPrecision(0)]
    public double FrameRate { get; set; } = 60.0;

    [Category("Frame")]
    [Trackable(1, 120, Increment = 1, FormatString = "{0:0}")]
    public int FrameRateInt { get; set; } = 10;

    [Category("Frame")]
    [DisplayName("Frame Size")]
    [Range(0.1f, 120.0f)]
    [FloatPrecision(3)]
    public double FrameSize { get; set; } = 60.0;

    [Category("Focus")]
    [DisplayName("lens-position")]
    [Range(0, 100)]
    public int LensPosition { get; set; } = 50;

    [Category("Focus")]
    [DisplayName("autofocus-mode")]
    public SelectableList<string> AutoFocusMode { get; set; } = new SelectableList<string>(["manual", "auto", "continuous"], "auto");

    [Flags]
    public enum PhoneService
    {
        [EnumDisplayName("Land Line (1)")]
        LandLine = 1,
        [EnumDisplayName("Cell (2)")]
        Cell = 2,
        [EnumDisplayName("Fax (4)")]
        Fax = 4,
        [EnumDisplayName("Cell (Internet (8)")]
        Internet = 8,
        [EnumDisplayName("Other (16)")]
        Other = 16,

        [EnumExclude]
        CanNotSeeThis = 64,
    }

    public PhoneService Flags { get; set; } = PhoneService.Other;

}
