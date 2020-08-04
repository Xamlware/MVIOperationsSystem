namespace MVIOperationsSystem.Migrations.LocalStorage
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addxamlware : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LocalStorageRecord", newName: "LocalStorage");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LocalStorage", newName: "LocalStorageRecord");
        }
    }
}
