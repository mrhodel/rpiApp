// https://github.com/dotnet/iot/tree/main/src/devices/Camera

using CommunityToolkit.Mvvm.Messaging;
using Iot.Device.Camera.Settings;
using Iot.Device.Common;
using rpiApp.Models;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpiApp.Services;

public interface ICameraService
{
    Properties CameraParameters { get; set; }
    void GetInfoAsync();
    void SetLeds();
}

public class CameraServiceLinux : ICameraService
{
    readonly GpioController controller = new();

    public CameraServiceLinux()
    {
        controller.OpenPin(Properties.RedLedPin, PinMode.Output);
        controller.OpenPin(Properties.GreenLedPin, PinMode.Output);
        controller.OpenPin(Properties.BlueLedPin, PinMode.Output);
        controller.OpenPin(Properties.YellowLedPin, PinMode.Output);
        controller.OpenPin(Properties.WhiteLedPin, PinMode.Output);
        SetLeds();
    }
    public Properties CameraParameters { get; set; } = new("Properties");

    public void SetLeds()
    {
        controller.Write(Properties.RedLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Red) ? PinValue.High : PinValue.Low);
        controller.Write(Properties.GreenLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Green) ? PinValue.High : PinValue.Low);
        controller.Write(Properties.BlueLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Blue) ? PinValue.High : PinValue.Low);
        controller.Write(Properties.YellowLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Yellow) ? PinValue.High : PinValue.Low);
        controller.Write(Properties.WhiteLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.White) ? PinValue.High : PinValue.Low);
    }

    public async void GetInfoAsync()
    {
        ProcessSettings settings = new()
        {
            Filename = "rpicam-still",
            WorkingDirectory = null,
        };

        using var proc = new ProcessRunner(settings);
        var builder = new CommandOptionsBuilder(false).WithListCameras();
        var text = await proc.ExecuteReadOutputAsStringAsync(builder.GetArguments());
        WeakReferenceMessenger.Default.Send(new CameraInfoMessage(text));
    }
}

public class CameraServicePC : ICameraService
{
    public Properties CameraParameters { get; set; } = new("Properties");

    public void SetLeds()
    {
        Debug.WriteLine(Properties.RedLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Red) ? PinValue.High.ToString() : PinValue.Low.ToString());
        Debug.WriteLine(Properties.GreenLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Green) ? PinValue.High.ToString() : PinValue.Low.ToString());
        Debug.WriteLine(Properties.BlueLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Blue) ? PinValue.High.ToString() : PinValue.Low.ToString());
        Debug.WriteLine(Properties.YellowLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.Yellow) ? PinValue.High.ToString() : PinValue.Low.ToString());
        Debug.WriteLine(Properties.WhiteLedPin, CameraParameters.LedIndicators.HasFlag(Properties.Led.White) ? PinValue.High.ToString() : PinValue.Low.ToString());
    }



    public async void GetInfoAsync()
    {
        var sb = new StringBuilder();
        foreach (var i in Enumerable.Range(1, 50))
        {
            sb.AppendLine($"Line {i}");
        }
        await Task.Delay(10);

        WeakReferenceMessenger.Default.Send(new CameraInfoMessage(sb.ToString()));
    }
}

