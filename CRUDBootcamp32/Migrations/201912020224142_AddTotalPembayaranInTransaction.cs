namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalPembayaranInTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_M_Transaction", "TotalPembayaran", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TB_M_Transaction", "TotalPembayaran");
        }
    }
}
