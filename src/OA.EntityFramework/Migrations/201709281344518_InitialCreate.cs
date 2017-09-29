namespace OA.EntityFramework.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OA_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(nullable: false),
                        Description = c.String(),
                        DeleteTime = c.DateTime(),
                        DeleteUser = c.Long(),
                        IsDel = c.Boolean(nullable: false),
                        LastTime = c.DateTime(),
                        Modifier = c.Long(),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OA_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OA_Commodity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Describe = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        Picture = c.String(),
                        DeleteTime = c.DateTime(),
                        DeleteUser = c.Long(),
                        IsDel = c.Boolean(nullable: false),
                        LastTime = c.DateTime(),
                        Modifier = c.Long(),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OA_Commodity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sys_Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Account = c.String(),
                        NickName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        IsLock = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        DeleteUser = c.Long(),
                        IsDel = c.Boolean(nullable: false),
                        LastTime = c.DateTime(),
                        Modifier = c.Long(),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Sys_Account_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sys_Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserId = c.Int(),
                        RoleId = c.Int(),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sys_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sys_UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sys_UserLogin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Account = c.String(),
                        Result = c.String(),
                        ClientInfo = c.String(),
                        ClientIp = c.String(),
                        ClientName = c.String(),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sys_UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Creator = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sys_UserRole");
            DropTable("dbo.Sys_UserLogin");
            DropTable("dbo.Sys_UserClaim");
            DropTable("dbo.Sys_Role");
            DropTable("dbo.Sys_Permission");
            DropTable("dbo.Sys_Account",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Sys_Account_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.OA_Commodity",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OA_Commodity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.OA_Category",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OA_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
