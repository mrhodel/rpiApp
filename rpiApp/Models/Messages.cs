using CommunityToolkit.Mvvm.Messaging.Messages;

namespace rpiApp.Models;

public class CameraInfoMessage(string value) : ValueChangedMessage<string>(value) { }

