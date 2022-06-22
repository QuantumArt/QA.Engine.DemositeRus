using System;

namespace MTS.IR.Interfaces.Dto
{
    public class MeetingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string Text { get; set; }
        public bool IsArchive { get; set; }
    }
}
