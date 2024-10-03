
# UniversalNotifier.NET

**UniversalNotifier.NET** is a simple and flexible notification system for .NET applications. It allows you to send notifications via multiple channels (starting with email via MailGun, with plans for SMS and other services) quickly and easily. This package is designed to streamline notification handling and provide a unified interface.

## Features

- Send notifications via email (using [MailGun](https://www.mailgun.com/)).
- Future plans for SMS and additional notification methods.
- Simple and easy-to-use API.

## Installation

You can install the package via NuGet Package Manager:

```bash
Install-Package UniversalNotifier.NET
```

Or using the .NET CLI:

```bash
dotnet add package UniversalNotifier.NET
```

## Getting Started

### Sending an Email via MailGun

Below is a simple example of how to send an email notification using **UniversalNotifier.NET** with MailGun:

```csharp
using UniversalNotifier;

var notifier = new MailGunNotifier("your-api-key", "your-domain");
notifier.SendEmail("recipient@example.com", "Subject of the Email", "This is the email body.");
```

### Configuration

To use **MailGun**, you will need:
- A MailGun API key
- Your MailGun domain

Pass these values to the `MailGunNotifier` when initializing the object.

## Future Plans

- Support for SMS notifications (e.g., Twilio, Nexmo)
- Additional email services (e.g., SendGrid, SMTP)
- Push notifications

## Contributing

Contributions are welcome! Feel free to submit pull requests for new features or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or feedback, feel free to open an issue or reach out to me.
