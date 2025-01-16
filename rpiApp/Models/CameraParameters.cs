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
