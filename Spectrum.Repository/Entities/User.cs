using SQLite;
using System;

namespace Spectrum.Repository.Entities
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public long Id { set; get; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [MaxLength(15)]
        public string Password { get; set; }
        public DateTime Start { get; set; }
    }
}
