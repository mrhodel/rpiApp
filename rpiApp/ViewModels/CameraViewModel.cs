/*
 * CameraViewModel.cs
 * 1/26/2025 Mike Hodel
*/


//using LibVLCSharp.Shared;

namespace rpiApp.ViewModels;

public partial class CameraViewModel : ViewModelBase
{
    //private readonly LibVLC libVlc = new(enableDebugLogs: false);
    //public MediaPlayer MediaPlayer { get; }

    public CameraViewModel()
    {
        //MediaPlayer = new MediaPlayer(libVlc);
    }

    //public void Play()
    //{
    //    if (Design.IsDesignMode)
    //    {
    //        return;
    //    }

    //    using var media = new Media(libVlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
    //    MediaPlayer.Play(media);
    //}

    //public void Stop()
    //{
    //    MediaPlayer.Stop();
    //}

    //public void Dispose()
    //{
    //    MediaPlayer?.Dispose();
    //    libVlc?.Dispose();
    //}
}
