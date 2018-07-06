namespace KeJianApi.Migrations
{
    using KeJianApi.Models;
    using System.Data.Entity;

    internal sealed class BaseInitializer : CreateDatabaseIfNotExists<KeJianDb>
    {
        protected override void Seed(KeJianDb context)
        {

        }
    }
}
