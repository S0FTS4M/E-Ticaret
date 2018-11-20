using SQLite;
namespace ETicaretAndroidAPP
{
    [Table("CustomerAccount")]
    public class CustomerAccount
    {
        [PrimaryKey, Column("UserName"),MaxLength(8)]
        public string UserName { get; set; }
        [Column("eMail")]
        public string EMail { get; set; }
        [Column("Password"),MaxLength(16)]
        public string Password { get; set; }
        [Column("PhoneNumber"),MaxLength(11)]
        public string PhoneNumber { get; set; }

    }
}