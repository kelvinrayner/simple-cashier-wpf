namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionItemModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Int(nullable: false),
                        Item_Id = c.Int(),
                        Transaction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_M_Item", t => t.Item_Id)
                .ForeignKey("dbo.TB_M_Transaction", t => t.Transaction_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.Transaction_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionItems", "Transaction_Id", "dbo.TB_M_Transaction");
            DropForeignKey("dbo.TransactionItems", "Item_Id", "dbo.TB_M_Item");
            DropIndex("dbo.TransactionItems", new[] { "Transaction_Id" });
            DropIndex("dbo.TransactionItems", new[] { "Item_Id" });
            DropTable("dbo.TransactionItems");
        }
    }
}
