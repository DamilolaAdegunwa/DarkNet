namespace OA.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCount_Changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sys_Account", "UserName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Sys_Account", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Sys_Account", "Account", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Sys_Permission", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Sys_Role", "ParentId", c => c.Int());
            AlterColumn("dbo.Sys_UserClaim", "ClaimType", c => c.String(nullable: false));
            AlterColumn("dbo.Sys_UserClaim", "ClaimValue", c => c.String(nullable: false));
            AlterColumn("dbo.Sys_UserLogin", "Account", c => c.String(nullable: false));
            AlterColumn("dbo.Sys_UserLogin", "Result", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sys_UserLogin", "Result", c => c.String());
            AlterColumn("dbo.Sys_UserLogin", "Account", c => c.String());
            AlterColumn("dbo.Sys_UserClaim", "ClaimValue", c => c.String());
            AlterColumn("dbo.Sys_UserClaim", "ClaimType", c => c.String());
            AlterColumn("dbo.Sys_Role", "ParentId", c => c.String());
            AlterColumn("dbo.Sys_Permission", "Name", c => c.String());
            AlterColumn("dbo.Sys_Account", "Account", c => c.String());
            AlterColumn("dbo.Sys_Account", "Password", c => c.String());
            AlterColumn("dbo.Sys_Account", "UserName", c => c.String());
        }
    }
}
