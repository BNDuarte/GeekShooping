using GeekShooping.OrderAPI.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.OrderAPI.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }


        public DbSet<OrderDetail> Details { get; set; }
        public DbSet<OrderHeader> Headers { get; set; }
    }
}