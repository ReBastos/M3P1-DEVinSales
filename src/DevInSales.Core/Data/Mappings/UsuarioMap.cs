using DevInSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.ToTable("Usuarios");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(p => p.Username)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(p => p.Senha)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(p => p.Role)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

        }
    }
}
