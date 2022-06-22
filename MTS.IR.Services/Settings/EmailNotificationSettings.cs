using System;

namespace MTS.IR.Services.Settings
{
    public class EmailNotificationSettings
    {
        public bool NotificationServiceIsActive { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public TimeSpan SendTimeInterval { get; set; }
        public TimeSpan SendTimeStart { get; set; }
        public int MailingPacketSize { get; set; }
        public int CreateRecipientsSize { get; set; }
        public int NumberOfAttemptsSending { get; set; }
        public TimeSpan TimeIntervalOfAttemptsSending { get; set; }
        public string BaseURLNewsService { get; set; }
        public TimeSpan EmailConfirmationExpirationTime { get; set; }
        public EmailSenderSettings EmailSender { get; set; }
    }
}
