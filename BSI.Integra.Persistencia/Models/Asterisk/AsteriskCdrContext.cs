using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BSI.Integra.Persistencia.Models.Asterisk
{
    public partial class AsteriskCdrContext : DbContext
    {
        public AsteriskCdrContext()
        {
        }

        public AsteriskCdrContext(DbContextOptions<AsteriskCdrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cdr> Cdr { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=45.55.97.163;port=3306;user=asterisk_us;password=sOIHDiuhds1q3erf8i;database=asteriskcdrdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Cdr>(entity =>
            {
                entity.HasKey(e => e.RecordingId);

                entity.ToTable("cdr", "asteriskcdrdb");

                entity.HasIndex(e => e.Accountcode)
                    .HasName("accountcode");

                entity.HasIndex(e => e.CallType)
                    .HasName("call_type");

                entity.HasIndex(e => e.Calldate)
                    .HasName("calldate");

                entity.HasIndex(e => e.Dst)
                    .HasName("dst");

                entity.HasIndex(e => e.RecordingId)
                    .HasName("recording_id");

                entity.Property(e => e.RecordingId)
                    .HasColumnName("recording_id")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Accountcode)
                    .IsRequired()
                    .HasColumnName("accountcode")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Amaflags)
                    .HasColumnName("amaflags")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Billsec)
                    .HasColumnName("billsec")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CallType)
                    .IsRequired()
                    .HasColumnName("call_type")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Calldate)
                    .HasColumnName("calldate")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.CampaingId)
                    .HasColumnName("campaing_id")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Channel)
                    .IsRequired()
                    .HasColumnName("channel")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Clid)
                    .IsRequired()
                    .HasColumnName("clid")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Cnam)
                    .IsRequired()
                    .HasColumnName("cnam")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Cnum)
                    .IsRequired()
                    .HasColumnName("cnum")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Custom1)
                    .HasColumnName("custom1")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Custom2)
                    .HasColumnName("custom2")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Custom3)
                    .HasColumnName("custom3")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Custom4)
                    .HasColumnName("custom4")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Custom5)
                    .HasColumnName("custom5")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Custom6)
                    .HasColumnName("custom6")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Custom7)
                    .HasColumnName("custom7")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Custom8)
                    .HasColumnName("custom8")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Dcontext)
                    .IsRequired()
                    .HasColumnName("dcontext")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Did)
                    .IsRequired()
                    .HasColumnName("did")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Disposition)
                    .IsRequired()
                    .HasColumnName("disposition")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Dst)
                    .IsRequired()
                    .HasColumnName("dst")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.DstCnam)
                    .IsRequired()
                    .HasColumnName("dst_cnam")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Dstchannel)
                    .IsRequired()
                    .HasColumnName("dstchannel")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Fechainicio)
                    .HasColumnName("fechainicio")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.IdActividadDetalle)
                    .HasColumnName("id_actividad_detalle")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lastapp)
                    .IsRequired()
                    .HasColumnName("lastapp")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Lastdata)
                    .IsRequired()
                    .HasColumnName("lastdata")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OutboundCnam)
                    .IsRequired()
                    .HasColumnName("outbound_cnam")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OutboundCnum)
                    .IsRequired()
                    .HasColumnName("outbound_cnum")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Recordingfile)
                    .IsRequired()
                    .HasColumnName("recordingfile")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Src)
                    .IsRequired()
                    .HasColumnName("src")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Uniqueid)
                    .IsRequired()
                    .HasColumnName("uniqueid")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Userfield)
                    .IsRequired()
                    .HasColumnName("userfield")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.VariableRespaldo)
                    .HasColumnName("variable_respaldo")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");
            });
        }
    }
}
