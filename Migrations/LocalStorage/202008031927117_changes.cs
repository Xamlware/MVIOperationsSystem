namespace MVIOperationsSystem.Migrations.LocalStorage
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LocalStorage", newName: "LocalStorageRecord");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LocalStorageRecord", newName: "LocalStorage");
        }
    }
}
