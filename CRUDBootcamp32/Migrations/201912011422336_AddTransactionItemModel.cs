namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionItemModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TB_M_Transaction", "Item_Id", "dbo.TB_M_Item");
            DropIndex("dbo.TB_M_Transaction", new[] { "Item_Id" });
            DropColumn("dbo.TB_M_Transaction", "Qty");
            DropColumn("dbo.TB_M_Transaction", "Item_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TB_M_Transaction", "Item_Id", c => c.Int());
            AddColumn("dbo.TB_M_Transaction", "Qty", c => c.Int(nullable: false));
            CreateIndex("dbo.TB_M_Transaction", "Item_Id");
            AddForeignKey("dbo.TB_M_Transaction", "Item_Id", "dbo.TB_M_Item", "Id");
        }
    }
}
