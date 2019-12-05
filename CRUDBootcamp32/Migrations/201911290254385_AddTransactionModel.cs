namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_M_Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        TransactionDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_M_Item", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_M_Transaction", "Item_Id", "dbo.TB_M_Item");
            DropIndex("dbo.TB_M_Transaction", new[] { "Item_Id" });
            DropTable("dbo.TB_M_Transaction");
        }
    }
}
