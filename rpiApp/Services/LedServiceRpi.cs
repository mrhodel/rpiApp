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
    readonly GpioController controller = new();
    readonly IPropertiesService properties;
    public LedServiceRpi(IPropertiesService properties)
    {
        WeakReferenceMessenger.Default.Register<LedChangedMessage>(this);
        WeakReferenceMessenger.Default.Register<AppClosingMessage>(this);
        this.properties = properties;
        controller.OpenPin(Properties.RedLedPin, PinMode.Output);
        controller.OpenPin(Properties.GreenLedPin, PinMode.Output);
        controller.OpenPin(Properties.BlueLedPin, PinMode.Output);
        controller.OpenPin(Properties.YellowLedPin, PinMode.Output);
        controller.OpenPin(Properties.WhiteLedPin, PinMode.Output);
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
        controller.Write(pin, properties.Properties.LedIndicators.HasFlag(led) ? PinValue.High : PinValue.Low);
    }

    public void Receive(LedChangedMessage message)
    {
        Debug.WriteLine("Led changed: " + message.Value);
        SetLeds();
    }

    public void Receive(AppClosingMessage message)
    {
        Debug.WriteLine(message.Value + ", Turning off LEDs");
        properties.Properties.LedIndicators = 0;
        SetLeds();
    }
}
