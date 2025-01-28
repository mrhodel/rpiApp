/*
 * LedServiceRpi.cs
 * 1/26/2025 Mike Hodel
*/
using CommunityToolkit.Mvvm.Messaging;
using rpiApp.Models;
using System.Device.Gpio;
using System.Diagnostics;

namespace rpiApp.Services;

public class LedServiceRpi : ILedService, IRecipient<LedChangedMessage>, IRecipient<AppClosingMessage>
{
    readonly GpioController _controller = new();
    readonly IPropertiesService _propertiesService;
    public LedServiceRpi(IPropertiesService propertiesService)
    {
        WeakReferenceMessenger.Default.Register<LedChangedMessage>(this);
        WeakReferenceMessenger.Default.Register<AppClosingMessage>(this);
        this._propertiesService = propertiesService;
        _controller.OpenPin(Properties.RedLedPin, PinMode.Output);
        _controller.OpenPin(Properties.GreenLedPin, PinMode.Output);
        _controller.OpenPin(Properties.BlueLedPin, PinMode.Output);
        _controller.OpenPin(Properties.YellowLedPin, PinMode.Output);
        _controller.OpenPin(Properties.WhiteLedPin, PinMode.Output);
        SetLeds();
    }

    public void SetLeds()
    {
        SetLed(Properties.RedLedPin, Properties.Led.Red);
        SetLed(Properties.GreenLedPin, Properties.Led.Green);
        SetLed(Properties.BlueLedPin, Properties.Led.Blue);
        SetLed(Properties.YellowLedPin, Properties.Led.Yellow);
        SetLed(Properties.WhiteLedPin, Properties.Led.White);
    }

    private void SetLed(int pin, Properties.Led led)
    {
        _controller.Write(pin, _propertiesService.Properties.LedIndicators.HasFlag(led) ? PinValue.High : PinValue.Low);
    }

    public void Receive(LedChangedMessage message)
    {
        Debug.WriteLine("Led changed: " + message.Value);
        SetLeds();
    }

    public void Receive(AppClosingMessage message)
    {
        Debug.WriteLine(message.Value + ", Turning off LEDs");
        _propertiesService.Properties.LedIndicators = 0;
        SetLeds();
    }
}
