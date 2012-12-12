namespace __NAME__.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SampleParentObjects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SampleChildObjects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ParentObjectId = c.Long(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SampleParentObjects", t => t.ParentObjectId, cascadeDelete: true)
                .Index(t => t.ParentObjectId);
            
            CreateTable(
                "SampleUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(maxLength: 255),
                        UserKey = c.Guid(nullable: false),
                        Email = c.String(maxLength: 255),
                        Title = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        Suffix = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        LastLoginDate = c.DateTime(),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("SampleChildObjects", new[] { "ParentObjectId" });
            DropForeignKey("SampleChildObjects", "ParentObjectId", "SampleParentObjects");
            DropTable("SampleUsers");
            DropTable("SampleChildObjects");
            DropTable("SampleParentObjects");
        }
    }
}
