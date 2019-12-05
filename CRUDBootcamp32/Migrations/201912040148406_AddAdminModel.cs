namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_M_Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        RegisterDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TB_M_Admin");
        }
    }
}
