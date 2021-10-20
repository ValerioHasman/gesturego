namespace Gesture_Go_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Banco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.img_Imagens",
                c => new
                    {
                        img_id = c.Int(nullable: false, identity: true),
                        img_tipo = c.String(nullable: false, unicode: false),
                        img_nome = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.img_id);
            
            CreateTable(
                "dbo.per_perfil",
                c => new
                    {
                        per_id = c.Int(nullable: false, identity: true),
                        per_descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.per_id);
            
            CreateTable(
                "dbo.usu_usuario",
                c => new
                    {
                        usu_id = c.Int(nullable: false, identity: true),
                        usu_nome = c.String(nullable: false, maxLength: 200, unicode: false, storeType: "nvarchar"),
                        usu_email = c.String(nullable: false, unicode: false),
                        usu_senha = c.String(nullable: false, unicode: false),
                        usu_ImgPerfil = c.String(unicode: false),
                        PerfilId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usu_id)
                .ForeignKey("dbo.per_perfil", t => t.PerfilId, cascadeDelete: true);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.usu_usuario", "PerfilId", "dbo.per_perfil");
            DropIndex("dbo.usu_usuario", new[] { "PerfilId" });
            DropTable("dbo.VMSessaos");
            DropTable("dbo.usu_usuario");
            DropTable("dbo.per_perfil");
            DropTable("dbo.img_Imagens");
        }
    }
}
