namespace Gesture_Go_v1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Gesture_Go_v1.Models.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Gesture_Go_v1.Models.Contexto context)
        {
            context.Perfil.AddOrUpdate(p => p.Descricao,
                new Models.Perfil { Id = 1, Descricao = "Admim" },
                new Models.Perfil { Id = 2, Descricao = "Comum" });

            context.Usuario.AddOrUpdate(p => p.Email,
                new Models.Usuario { Id = 1, Nome = "Adm", Email = "adm@a.com", Senha = "le6bmD9qBhPZk5wfQmvEbMgBbXYFKVKMYAdKaCsNV2sxU3ptaHJzbs+RiTKMxx1D8KAlbjkX4NwknIXuZHEvuA==", PerfilId = 1},
                new Models.Usuario { Id = 2, Nome = "comum", Email = "comum@a.com", Senha = "le6bmD9qBhPZk5wfQmvEbMgBbXYFKVKMYAdKaCsNV2sxU3ptaHJzbs+RiTKMxx1D8KAlbjkX4NwknIXuZHEvuA==", PerfilId = 2 });

           context.Imagem.AddOrUpdate(p => p.img_id,
                new Models.Imagem {img_id = 1, img_tipo = "Animal", img_nome = "download.jpg" },
                new Models.Imagem {img_id = 2, img_tipo = "Animal", img_nome = "04-09_gato_SITE.webp" });

                }
    }
}
