using Avalonia.Controls;
using LibVLCSharp.Shared;
using rpiApp.Services;
using System;

namespace rpiApp.ViewModels;

public partial class CameraViewModel : ViewModelBase
{
    private readonly LibVLC libVlc = new(enableDebugLogs: true);
    readonly ICameraService CameraService;


    public MediaPlayer MediaPlayer { get; }

    public CameraViewModel(ICameraService cameraService)
    {
        MediaPlayer = new MediaPlayer(libVlc);
        CameraService = cameraService;

        libVlc.Log += (sender, e) =>
        {
            // Output the log message to the Visual Studio Output window
            System.Diagnostics.Debug.WriteLine($"[{e.Level}] {e.Module}: {e.Message}");
        };
    }

    public void Play()
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        using var media = new Media(libVlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
        //using var media = new Media(libVlc, new Uri("dshow://"));
        //media.AddOption(" :dshow-vdev=Integrated Webcam :dshow-adev=  :live-caching=300)");
        //media.AddOption(":dshow-vdev=Integrated Webcam");
        //media.AddOption(":dshow-adev=none");
        //media.AddOption(":live-caching=300");
        MediaPlayer.Play(media);
    }

    public void Stop()
    {
        MediaPlayer.Stop();
    }

    public void Dispose()
    {
        MediaPlayer?.Dispose();
        libVlc?.Dispose();
    }

}
