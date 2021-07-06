namespace PhotoHistory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoTag",
                c => new
                    {
                        PhotoTagId = c.Int(nullable: false, identity: true),
                        PhotoId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoTagId)
                .ForeignKey("dbo.Photo", t => t.PhotoId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PhotoId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false),
                        TagType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            AlterColumn("dbo.Photo", "PhotoUploadDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.PhotoTag", "PhotoId", "dbo.Photo");
            DropIndex("dbo.PhotoTag", new[] { "TagId" });
            DropIndex("dbo.PhotoTag", new[] { "PhotoId" });
            AlterColumn("dbo.Photo", "PhotoUploadDate", c => c.DateTime(nullable: false));
            DropTable("dbo.Tag");
            DropTable("dbo.PhotoTag");
        }
    }
}
