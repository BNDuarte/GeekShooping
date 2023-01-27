using GeekShooping.Email.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.Email.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }


        public DbSet<EmailLog> Emails { get; set; }
    }
}