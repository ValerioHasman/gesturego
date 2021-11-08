using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base(nameOrConnectionString: "StringConexao") { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            var usu = mb.Entity<Usuario>();
            usu.ToTable("usu_usuario");
            usu.Property(x => x.Id).HasColumnName("usu_id");
            usu.Property(x => x.Nome).HasColumnName("usu_nome");
            usu.Property(x => x.Email).HasColumnName("usu_email");
            usu.Property(x => x.Senha).HasColumnName("usu_senha");
            usu.Property(x => x.ImgPerfil).HasColumnName("usu_ImgPerfil");

            var per = mb.Entity<Perfil>();
            per.ToTable("per_perfil");
            per.Property(x => x.Id).HasColumnName("per_id");
            per.Property(x => x.Descricao).HasColumnName("per_descricao");

            var img = mb.Entity<Imagem>();
            img.ToTable("img_Imagens");
            img.Property(x => x.Id).HasColumnName("img_id");
            img.Property(x => x.Img_nome).HasColumnName("img_nome");
            img.Property(x => x.Img_tipo).HasColumnName("img_tipo");
            
            var pos = mb.Entity<Posts>();
            pos.ToTable("pos_posts");
            pos.Property(x => x.Pos_id).HasColumnName("pos_id");
            pos.Property(x => x.Pos_Titulo).HasColumnName("pos_titulo");
            pos.Property(x => x.UsuarioId).HasColumnName("usu_id");
            pos.Property(x => x.ImagemId).HasColumnName("img_id");
            pos.Property(x => x.Pos_imgUpload).HasColumnName("pos_imgUpload");
            pos.Property(x => x.Pos_Status).HasColumnName("pos_status");

            var com = mb.Entity<Comentarios>();
            com.ToTable("com_comentarios");
            com.Property(x => x.Id).HasColumnName("com_id");
            com.Property(x => x.PostsPos_id).HasColumnName("pos_id");
            com.Property(x => x.UsuarioId).HasColumnName("usu_id");
            com.Property(x => x.Comentario).HasColumnName("com_comentario");
            com.Property(x => x.Data).HasColumnName("com_data");

        }


    } 
}