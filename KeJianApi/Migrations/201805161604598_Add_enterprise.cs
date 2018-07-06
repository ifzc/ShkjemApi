namespace KeJianApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_enterprise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Img = c.String(),
                        Remark = c.String(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Enterprises");
        }
    }
}
