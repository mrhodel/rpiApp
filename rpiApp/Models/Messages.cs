using CommunityToolkit.Mvvm.Messaging.Messages;

namespace rpiApp.Models;

public class CameraInfoMessage(string value) : ValueChangedMessage<string>(value) { }
public class LedChangedMessage(string value) : ValueChangedMessage<string>(value) { }
public class AppClosingMessage(string value) : ValueChangedMessage<string>(value) { }
