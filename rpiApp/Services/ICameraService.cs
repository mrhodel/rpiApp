// https://github.com/dotnet/iot/tree/main/src/devices/Camera

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

public class CameraServiceLinux : ICameraService
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
        foreach (var i in Enumerable.Range(1, 50))
        {
            sb.AppendLine($"Line {i}");
        }
        await Task.Delay(10);

        WeakReferenceMessenger.Default.Send(new CameraInfoMessage(sb.ToString()));
    }
}

