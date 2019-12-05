namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleModelChangeAdmintoUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TB_M_Admin", newName: "TB_M_User");
            CreateTable(
                "dbo.TB_M_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TB_M_User", "Role_Id", c => c.Int());
            CreateIndex("dbo.TB_M_User", "Role_Id");
            AddForeignKey("dbo.TB_M_User", "Role_Id", "dbo.TB_M_Role", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_M_User", "Role_Id", "dbo.TB_M_Role");
            DropIndex("dbo.TB_M_User", new[] { "Role_Id" });
            DropColumn("dbo.TB_M_User", "Role_Id");
            DropTable("dbo.TB_M_Role");
            RenameTable(name: "dbo.TB_M_User", newName: "TB_M_Admin");
        }
    }
}
