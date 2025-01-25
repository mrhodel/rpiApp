using CommunityToolkit.Mvvm.Messaging;
using rpiApp.Models;
using System.Diagnostics;

namespace rpiApp.Services
{
    public class LedServicePC : ILedService, IRecipient<LedChangedMessage>, IRecipient<AppClosingMessage>
    {
        readonly IPropertiesService properties;
        public LedServicePC(IPropertiesService properties)
        {
            WeakReferenceMessenger.Default.Register<LedChangedMessage>(this);
            WeakReferenceMessenger.Default.Register<AppClosingMessage>(this);
            this.properties = properties;
            SetLeds();
        }

        public void SetLeds()
        {
            SetLed(Properties.RedLedPin, Properties.Led.Red);
            SetLed(Properties.GreenLedPin, Properties.Led.Green);
            SetLed(Properties.BlueLedPin, Properties.Led.Blue);
            SetLed(Properties.YellowLedPin, Properties.Led.Yellow);
            SetLed(Properties.WhiteLedPin, Properties.Led.White);
            Debug.WriteLine("");
        }

        private void SetLed(object pin, Properties.Led led)
        {
            Debug.WriteLine($"{led.ToString():5} Led, Pin {pin}, {(properties.Properties.LedIndicators.HasFlag(led) ? "On" : "Off")}");
        }

        public void Receive(LedChangedMessage message)
        {
            Debug.WriteLine("Led changed: " + message.Value);
            SetLeds();
        }

        public void Receive(AppClosingMessage message)
        {
            Debug.WriteLine(message.Value + ", Turning off all LEDs");
            properties.Properties.LedIndicators = 0;
            SetLeds();
        }
    }
}
