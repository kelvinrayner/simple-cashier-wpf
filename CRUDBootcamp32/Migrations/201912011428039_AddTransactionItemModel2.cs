namespace CRUDBootcamp32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionItemModel2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TransactionItems", newName: "TB_M_TransactionItem");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TB_M_TransactionItem", newName: "TransactionItems");
        }
    }
}
