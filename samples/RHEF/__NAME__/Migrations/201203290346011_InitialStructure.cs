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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SampleChildObjects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ParentObject_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SampleParentObjects", t => t.ParentObject_Id)
                .Index(t => t.ParentObject_Id);
            
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
                        MiddleName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        Suffix = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                        RoleForDatabase = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("SampleChildObjects", new[] { "ParentObject_Id" });
            DropForeignKey("SampleChildObjects", "ParentObject_Id", "SampleParentObjects");
            DropTable("SampleUsers");
            DropTable("SampleChildObjects");
            DropTable("SampleParentObjects");
        }
    }
}
