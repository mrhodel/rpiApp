/*
 * ICameraService.cs
 * 1/26/2025 Mike Hodel
 * 
 * Ref: https://github.com/dotnet/iot/tree/main/src/devices/Camera
*/
using CommunityToolkit.Mvvm.Messaging;
using Iot.Device.Camera.Settings;
using Iot.Device.Common;
using rpiApp.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpiApp.Services;

public interface ICameraService
{
    void GetInfoAsync();
}

public class CameraServiceRpi : ICameraService
{
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
    public async void GetInfoAsync()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Camera info is only available when running on a Raspberry Pi with a camera attached");
        foreach (var i in Enumerable.Range(1, 50))
        {
            sb.AppendLine($"Line {i}");
        }
        await Task.Delay(10);
        WeakReferenceMessenger.Default.Send(new CameraInfoMessage(sb.ToString()));
    }
}
