namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalPriceInTransactionItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_M_TransactionItem", "TotalPrice", c => c.Int(nullable: false));
            DropColumn("dbo.TB_M_Transaction", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TB_M_Transaction", "TotalPrice", c => c.Int(nullable: false));
            DropColumn("dbo.TB_M_TransactionItem", "TotalPrice");
        }
    }
}
