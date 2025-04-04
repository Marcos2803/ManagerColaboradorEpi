using gestao.EpiData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace gestao.EpiData.Factory
{
    public class Factory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var connectionstring = "Data Source=localhost,1401;Initial Catalog=DbGestaoEpi;User ID=sa;Password=@MLab12366;Trust Server Certificate=True";
            var optionBulder = new DbContextOptionsBuilder<DataContext>();
            optionBulder.UseSqlServer(connectionstring);
            return new DataContext(optionBulder.Options);
        }
    }
}
