namespace Board.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Importance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        StatusID = c.Int(nullable: false),
                        ImportanceID = c.Int(nullable: false),
                        PriorityID = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Importance", t => t.ImportanceID, cascadeDelete: true)
                .ForeignKey("dbo.Priority", t => t.PriorityID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.StatusID)
                .Index(t => t.ImportanceID)
                .Index(t => t.PriorityID);
            
            CreateTable(
                "dbo.PostFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Extension = c.String(),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SubPost",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Message = c.String(),
                        RegisterDate = c.DateTime(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Post", t => t.PostID)
                .Index(t => t.UserID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.SubPostFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Extension = c.String(),
                        SubPostID = c.Int(nullable: false),
                        Post_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubPost", t => t.SubPostID, cascadeDelete: true)
                .ForeignKey("dbo.Post", t => t.Post_ID)
                .Index(t => t.SubPostID)
                .Index(t => t.Post_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubPostFile", "Post_ID", "dbo.Post");
            DropForeignKey("dbo.SubPost", "PostID", "dbo.Post");
            DropForeignKey("dbo.SubPost", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Post", "UserID", "dbo.User");
            DropForeignKey("dbo.SubPostFile", "SubPostID", "dbo.SubPost");
            DropForeignKey("dbo.Post", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Post", "PriorityID", "dbo.Priority");
            DropForeignKey("dbo.PostFile", "PostID", "dbo.Post");
            DropForeignKey("dbo.Post", "ImportanceID", "dbo.Importance");
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.SubPostFile", new[] { "Post_ID" });
            DropIndex("dbo.SubPostFile", new[] { "SubPostID" });
            DropIndex("dbo.SubPost", new[] { "PostID" });
            DropIndex("dbo.SubPost", new[] { "UserID" });
            DropIndex("dbo.PostFile", new[] { "PostID" });
            DropIndex("dbo.Post", new[] { "PriorityID" });
            DropIndex("dbo.Post", new[] { "ImportanceID" });
            DropIndex("dbo.Post", new[] { "StatusID" });
            DropIndex("dbo.Post", new[] { "UserID" });
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.SubPostFile");
            DropTable("dbo.SubPost");
            DropTable("dbo.Status");
            DropTable("dbo.Priority");
            DropTable("dbo.PostFile");
            DropTable("dbo.Post");
            DropTable("dbo.Importance");
        }
    }
}
