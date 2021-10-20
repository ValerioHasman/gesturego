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
            img.Property(x => x.img_id).HasColumnName("img_id");
            img.Property(x => x.img_nome).HasColumnName("img_nome");
            img.Property(x => x.img_tipo).HasColumnName("img_tipo");

        }

        public System.Data.Entity.DbSet<Gesture_Go_v1.Models.VMSessao> VMSessaos { get; set; }
    } 
}