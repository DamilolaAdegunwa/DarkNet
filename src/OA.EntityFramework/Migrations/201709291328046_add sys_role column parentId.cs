namespace OA.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsys_rolecolumnparentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sys_Role", "ParentId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sys_Role", "ParentId");
        }
    }
}
