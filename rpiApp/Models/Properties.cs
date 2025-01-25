using CommunityToolkit.Mvvm.Messaging;
using PropertyModels.Collections;
using PropertyModels.ComponentModel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace rpiApp.Models;

public class Properties(string description) : ReactiveObject
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

    public const int RedLedPin = 26;
    public const int GreenLedPin = 19;
    public const int BlueLedPin = 13;
    public const int YellowLedPin = 20;
    public const int WhiteLedPin = 21;

    [Flags]
    public enum Led
    {
        [EnumDisplayName("Red")]
        Red = 1,
        [EnumDisplayName("Green")]
        Green = 2,
        [EnumDisplayName("Blue")]
        Blue = 4,
        [EnumDisplayName("Yellow")]
        Yellow = 8,
        [EnumDisplayName("White")]
        White = 16,

        [EnumExclude]
        CanNotSeeThis = 64,
    }

    [DisplayName("led Indicators")]
    [ValidateLeds]
    public Led LedIndicators { get; set; } = Led.White;


    public class ValidateLedsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                WeakReferenceMessenger.Default.Send(new LedChangedMessage(value.ToString()!));
            }

            return ValidationResult.Success;
        }
    }
}
