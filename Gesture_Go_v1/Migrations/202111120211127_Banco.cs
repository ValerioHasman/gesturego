namespace Gesture_Go_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Banco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.com_comentarios",
                c => new
                    {
                        com_id = c.Int(nullable: false, identity: true),
                        pos_id = c.Int(nullable: false),
                        usu_id = c.Int(nullable: false),
                        com_comentario = c.String(unicode: false),
                        com_data = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.com_id)
                .ForeignKey("dbo.pos_posts", t => t.pos_id, cascadeDelete: true)
                .ForeignKey("dbo.usu_usuario", t => t.usu_id, cascadeDelete: true)
                .Index(t => t.pos_id)
                .Index(t => t.usu_id);
            
            CreateTable(
                "dbo.pos_posts",
                c => new
                    {
                        pos_id = c.Int(nullable: false, identity: true),
                        pos_titulo = c.String(maxLength: 200, storeType: "nvarchar"),
                        pos_status = c.Boolean(nullable: false),
                        pos_imgUpload = c.String(unicode: false),
                        data = c.DateTime(nullable: false, precision: 0),
                        usu_id = c.Int(nullable: false),
                        img_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pos_id)
                .ForeignKey("dbo.img_Imagens", t => t.img_id, cascadeDelete: true)
                .ForeignKey("dbo.usu_usuario", t => t.usu_id, cascadeDelete: true)
                .Index(t => t.usu_id)
                .Index(t => t.img_id);
            
            CreateTable(
                "dbo.img_Imagens",
                c => new
                    {
                        img_id = c.Int(nullable: false, identity: true),
                        img_tipo = c.String(nullable: false, unicode: false),
                        img_nome = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.img_id);
            
            CreateTable(
                "dbo.usu_usuario",
                c => new
                    {
                        usu_id = c.Int(nullable: false, identity: true),
                        usu_nome = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        usu_email = c.String(nullable: false, unicode: false),
                        usu_senha = c.String(nullable: false, unicode: false),
                        usu_ImgPerfil = c.String(unicode: false),
                        PerfilId = c.Int(nullable: false),
                        Hash = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.usu_id)
                .ForeignKey("dbo.per_perfil", t => t.PerfilId, cascadeDelete: true)
                .Index(t => t.PerfilId);
            
            CreateTable(
                "dbo.per_perfil",
                c => new
                    {
                        per_id = c.Int(nullable: false, identity: true),
                        per_descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.per_id);
            
            CreateTable(
                "dbo.his_historicoPratica",
                c => new
                    {
                        his_id = c.Int(nullable: false, identity: true),
                        usu_id = c.Int(nullable: false),
                        his_data = c.DateTime(nullable: false, precision: 0),
                        his_tempoPratica = c.DateTime(nullable: false, precision: 0),
                        his_qtdImagens = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.his_id)
                .ForeignKey("dbo.usu_usuario", t => t.usu_id, cascadeDelete: true)
                .Index(t => t.usu_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.his_historicoPratica", "usu_id", "dbo.usu_usuario");
            DropForeignKey("dbo.com_comentarios", "usu_id", "dbo.usu_usuario");
            DropForeignKey("dbo.pos_posts", "usu_id", "dbo.usu_usuario");
            DropForeignKey("dbo.usu_usuario", "PerfilId", "dbo.per_perfil");
            DropForeignKey("dbo.pos_posts", "img_id", "dbo.img_Imagens");
            DropForeignKey("dbo.com_comentarios", "pos_id", "dbo.pos_posts");
            DropIndex("dbo.his_historicoPratica", new[] { "usu_id" });
            DropIndex("dbo.usu_usuario", new[] { "PerfilId" });
            DropIndex("dbo.pos_posts", new[] { "img_id" });
            DropIndex("dbo.pos_posts", new[] { "usu_id" });
            DropIndex("dbo.com_comentarios", new[] { "usu_id" });
            DropIndex("dbo.com_comentarios", new[] { "pos_id" });
            DropTable("dbo.his_historicoPratica");
            DropTable("dbo.per_perfil");
            DropTable("dbo.usu_usuario");
            DropTable("dbo.img_Imagens");
            DropTable("dbo.pos_posts");
            DropTable("dbo.com_comentarios");
        }
    }
}
