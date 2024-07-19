using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Siace.Data.Models;

public partial class SiaceDbContext : DbContext
{
    public SiaceDbContext()
    {
    }

    public SiaceDbContext(DbContextOptions<SiaceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contestacione> Contestaciones { get; set; }

    public virtual DbSet<ContestacionesRespuesta> ContestacionesRespuestas { get; set; }

    public virtual DbSet<Encuesta> Encuestas { get; set; }

    public virtual DbSet<Pilare> Pilares { get; set; }

    public virtual DbSet<Pregunta> Preguntas { get; set; }

    public virtual DbSet<Respuesta> Respuestas { get; set; }

    public virtual DbSet<TiposControle> TiposControles { get; set; }

    public virtual DbSet<TiposPregunta> TiposPreguntas { get; set; }

    public virtual DbSet<TiposPreguntasRespuesta> TiposPreguntasRespuestas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MARMXLM\\SQLEXPRESS;Database=dbSiace;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contestacione>(entity =>
        {
            entity.HasKey(e => e.ConId);

            entity.ToTable("CONTESTACIONES");

            entity.Property(e => e.ConId).HasColumnName("CON_ID");
            entity.Property(e => e.ConEncId).HasColumnName("CON_ENC_ID");
            entity.Property(e => e.ConFecha)
                .HasColumnType("datetime")
                .HasColumnName("CON_FECHA");
            entity.Property(e => e.ConFechaFin)
                .HasColumnType("datetime")
                .HasColumnName("CON_FECHA_FIN");
            entity.Property(e => e.ConNombre)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CON_NOMBRE");
            entity.Property(e => e.ConUsrId).HasColumnName("CON_USR_ID");

            entity.HasOne(d => d.ConEnc).WithMany(p => p.Contestaciones)
                .HasForeignKey(d => d.ConEncId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTESTACIONES_ENCUESTAS");
        });

        modelBuilder.Entity<ContestacionesRespuesta>(entity =>
        {
            entity.HasKey(e => e.CorId).HasName("PK_CONTESTACIONES_RESPUESTAS_1");

            entity.ToTable("CONTESTACIONES_RESPUESTAS");

            entity.Property(e => e.CorId).HasColumnName("COR_ID");
            entity.Property(e => e.CorConId).HasColumnName("COR_CON_ID");
            entity.Property(e => e.CorImagen)
                .HasColumnType("image")
                .HasColumnName("COR_IMAGEN");
            entity.Property(e => e.CorNoContesto).HasColumnName("COR_NO_CONTESTO");
            entity.Property(e => e.CorPreId).HasColumnName("COR_PRE_ID");
            entity.Property(e => e.CorResId).HasColumnName("COR_RES_ID");
            entity.Property(e => e.CorValor)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("COR_VALOR");

            entity.HasOne(d => d.CorCon).WithMany(p => p.ContestacionesRespuesta)
                .HasForeignKey(d => d.CorConId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTESTACIONES_RESPUESTAS_CONTESTACIONES");

            entity.HasOne(d => d.CorPre).WithMany(p => p.ContestacionesRespuesta)
                .HasForeignKey(d => d.CorPreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTESTACIONES_RESPUESTAS_PREGUNTAS");

            entity.HasOne(d => d.CorRes).WithMany(p => p.ContestacionesRespuesta)
                .HasForeignKey(d => d.CorResId)
                .HasConstraintName("FK_CONTESTACIONES_RESPUESTAS_RESPUESTA");
        });

        modelBuilder.Entity<Encuesta>(entity =>
        {
            entity.HasKey(e => e.EncId);

            entity.ToTable("ENCUESTAS");

            entity.Property(e => e.EncId).HasColumnName("ENC_ID");
            entity.Property(e => e.EncActivo).HasColumnName("ENC_ACTIVO");
            entity.Property(e => e.EncDescripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ENC_DESCRIPCION");
            entity.Property(e => e.EncFechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("ENC_FECHA_REGISTRO");
            entity.Property(e => e.EncUsrIdRegistro).HasColumnName("ENC_USR_ID_REGISTRO");
        });

        modelBuilder.Entity<Pilare>(entity =>
        {
            entity.HasKey(e => e.PilId);

            entity.ToTable("PILARES");

            entity.Property(e => e.PilId).HasColumnName("PIL_ID");
            entity.Property(e => e.PilDesc)
                .HasColumnType("text")
                .HasColumnName("PIL_DESC");
            entity.Property(e => e.PilPond).HasColumnName("PIL_POND");
        });

        modelBuilder.Entity<Pregunta>(entity =>
        {
            entity.HasKey(e => e.PreId);

            entity.ToTable("PREGUNTAS");

            entity.Property(e => e.PreId).HasColumnName("PRE_ID");
            entity.Property(e => e.PreActivo).HasColumnName("PRE_ACTIVO");
            entity.Property(e => e.PreClaveCompuesta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRE_CLAVE_COMPUESTA");
            entity.Property(e => e.PreEncId).HasColumnName("PRE_ENC_ID");
            entity.Property(e => e.PreNoSabe).HasColumnName("PRE_NO_SABE");
            entity.Property(e => e.PrePilId).HasColumnName("PRE_PIL_ID");
            entity.Property(e => e.PrePreIdTrigger).HasColumnName("PRE_PRE_ID_TRIGGER");
            entity.Property(e => e.PrePregunta)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PRE_PREGUNTA");
            entity.Property(e => e.PreRangoFin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PRE_RANGO_FIN");
            entity.Property(e => e.PreRangoIni)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PRE_RANGO_INI");
            entity.Property(e => e.PreResIdTrigger).HasColumnName("PRE_RES_ID_TRIGGER");
            entity.Property(e => e.PreTicId).HasColumnName("PRE_TIC_ID");
            entity.Property(e => e.PreTipId).HasColumnName("PRE_TIP_ID");
            entity.Property(e => e.PreTipoDato)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PRE_TIPO_DATO");
            entity.Property(e => e.PreValoracion).HasColumnName("PRE_VALORACION");

            entity.HasOne(d => d.PreEnc).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.PreEncId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PREGUNTAS_ENCUESTAS");

            entity.HasOne(d => d.PrePil).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.PrePilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRE_PREGUNTAS_PILARES");

            entity.HasOne(d => d.PreTip).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.PreTipId)
                .HasConstraintName("FK_PREGUNTAS_TIPOS_PREGUNTAS");
        });

        modelBuilder.Entity<Respuesta>(entity =>
        {
            entity.HasKey(e => e.ResId).HasName("PK_RESPUESTA");

            entity.ToTable("RESPUESTAS");

            entity.Property(e => e.ResId).HasColumnName("RES_ID");
            entity.Property(e => e.ResPreId).HasColumnName("RES_PRE_ID");
            entity.Property(e => e.ResValor)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("RES_VALOR");
            entity.Property(e => e.ResValorEvaluacion).HasColumnName("RES_VALOR_EVALUACION");

            entity.HasOne(d => d.ResPre).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.ResPreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESPUESTA_PREGUNTAS");
        });

        modelBuilder.Entity<TiposControle>(entity =>
        {
            entity.HasKey(e => e.TicId);

            entity.ToTable("TIPOS_CONTROLES");

            entity.Property(e => e.TicId).HasColumnName("TIC_ID");
            entity.Property(e => e.TicDescripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TIC_DESCRIPCION");
        });

        modelBuilder.Entity<TiposPregunta>(entity =>
        {
            entity.HasKey(e => e.TipId);

            entity.ToTable("TIPOS_PREGUNTAS");

            entity.Property(e => e.TipId).HasColumnName("TIP_ID");
            entity.Property(e => e.TipDescripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("TIP_DESCRIPCION");
            entity.Property(e => e.TipTicId).HasColumnName("TIP_TIC_ID");
            entity.Property(e => e.TipValoracion).HasColumnName("TIP_VALORACION");

            entity.HasOne(d => d.TipTic).WithMany(p => p.TiposPregunta)
                .HasForeignKey(d => d.TipTicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIPOS_PREGUNTAS_TIPOS_CONTROLES");
        });

        modelBuilder.Entity<TiposPreguntasRespuesta>(entity =>
        {
            entity.HasKey(e => e.TprId);

            entity.ToTable("TIPOS_PREGUNTAS_RESPUESTAS");

            entity.Property(e => e.TprId).HasColumnName("TPR_ID");
            entity.Property(e => e.TprRespuesta)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TPR_RESPUESTA");
            entity.Property(e => e.TprTipId).HasColumnName("TPR_TIP_ID");
            entity.Property(e => e.TprValorEvaluacion).HasColumnName("TPR_VALOR_EVALUACION");

            entity.HasOne(d => d.TprTip).WithMany(p => p.TiposPreguntasRespuesta)
                .HasForeignKey(d => d.TprTipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIPOS_PREGUNTAS_RESPUESTAS_TIPOS_PREGUNTAS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
