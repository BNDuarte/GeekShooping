using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.Email.Models.Base
{
    [Table("email_logs")]
    public class EmailLog : BaseEntity
    {
        [ForeignKey("email")]
        public string Email { get; set; }
        [ForeignKey("log")]
        public string Log { get; set; }
        [ForeignKey("sent_date")]
        public DateTime SentDate { get; set; }
    }
}