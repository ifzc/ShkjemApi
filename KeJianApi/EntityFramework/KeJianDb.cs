using System.Data.Entity;

namespace KeJianApi.Models
{
    public class KeJianDb : DbContext
    {
        public IDbSet<Messages> Messages { get; set; }

        public IDbSet<News> News { get; set; }

        public IDbSet<KUser> KUsers { get; set; }

        public IDbSet<Recruitment> Recruitment { get; set; }

        public IDbSet<Cases> Cases { get; set; }

        public IDbSet<Team> Team { get; set; }

        public IDbSet<Course> Course { get; set; }

        public IDbSet<DataDictionary> DataDictionary { get; set; }

        public IDbSet<Honor> Honor { get; set; }

        public IDbSet<Study> Study { get; set; }

        public IDbSet<Enterprise> Enterprise { get; set; }

        public KeJianDb()
            : base("name=Default")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KeJianDb>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
