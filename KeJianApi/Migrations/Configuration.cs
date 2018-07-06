namespace KeJianApi.Migrations
{
    using KeJianApi.Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<KeJianApi.Models.KeJianDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KeJianApi.Models.KeJianDb context)
        {
            context.KUsers.AddOrUpdate(
                new KUser()
                {
                    LoginName = "admin",
                    Password = "123qwe",
                    IsAction = true,
                    CreateTime = DateTime.Now
                });
        }
    }
}
