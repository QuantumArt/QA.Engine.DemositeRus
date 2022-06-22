namespace MTS.IR.Services.Settings
{
    public class EmailSenderSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public bool UseSsl { get; set; }
    }

}
