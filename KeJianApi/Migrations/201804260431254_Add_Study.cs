namespace KeJianApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Study : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Img = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                        Del = c.String(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataDictionaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Honors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Img = c.String(),
                        Remark = c.String(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginName = c.String(),
                        Password = c.String(),
                        IsAction = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsMess = c.Boolean(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        Company = c.String(),
                        Email = c.String(),
                        Content = c.String(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Img = c.String(),
                        Type = c.Int(nullable: false),
                        Content = c.String(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recruitments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Type = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Studies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Del = c.String(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
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
            DropTable("dbo.Teams");
            DropTable("dbo.Studies");
            DropTable("dbo.Recruitments");
            DropTable("dbo.News");
            DropTable("dbo.Messages");
            DropTable("dbo.KUsers");
            DropTable("dbo.Honors");
            DropTable("dbo.DataDictionaries");
            DropTable("dbo.Courses");
            DropTable("dbo.Cases");
        }
    }
}
