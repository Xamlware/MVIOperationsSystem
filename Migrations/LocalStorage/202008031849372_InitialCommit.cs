namespace MVIOperationsSystem.Migrations.LocalStorage
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalStorage",
                c => new
                    {
                        KeyString = c.String(nullable: false, maxLength: 128),
                        ValueString = c.String(),
                    })
                .PrimaryKey(t => t.KeyString);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LocalStorage");
        }
    }
}
