namespace OA.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_Set_Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sys_Role", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sys_Role", "Name", c => c.String());
        }
    }
}
