using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Dnd_Api.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountRole> AccountRoles { get; set; }

    public virtual DbSet<AccountUser> AccountUsers { get; set; }

    public virtual DbSet<Dnd5Alignment> Dnd5Alignments { get; set; }

    public virtual DbSet<Dnd5Background> Dnd5Backgrounds { get; set; }

    public virtual DbSet<Dnd5BattleMasterManeuver> Dnd5BattleMasterManeuvers { get; set; }

    public virtual DbSet<Dnd5Character> Dnd5Characters { get; set; }

    public virtual DbSet<Dnd5Class> Dnd5Classes { get; set; }

    public virtual DbSet<Dnd5ClassSkill> Dnd5ClassSkills { get; set; }

    public virtual DbSet<Dnd5ClassSpell> Dnd5ClassSpells { get; set; }

    public virtual DbSet<Dnd5DraconicAncestry> Dnd5DraconicAncestries { get; set; }

    public virtual DbSet<Dnd5DruidFormLimit> Dnd5DruidFormLimits { get; set; }

    public virtual DbSet<Dnd5EldrichtInvocation> Dnd5EldrichtInvocations { get; set; }

    public virtual DbSet<Dnd5Feat> Dnd5Feats { get; set; }

    public virtual DbSet<Dnd5FightingStyle> Dnd5FightingStyles { get; set; }

    public virtual DbSet<Dnd5Inventory> Dnd5Inventories { get; set; }

    public virtual DbSet<Dnd5Item> Dnd5Items { get; set; }

    public virtual DbSet<Dnd5ItemRarity> Dnd5ItemRarities { get; set; }

    public virtual DbSet<Dnd5ItemType> Dnd5ItemTypes { get; set; }

    public virtual DbSet<Dnd5LandDruidDomain> Dnd5LandDruidDomains { get; set; }

    public virtual DbSet<Dnd5LandDruidDomainSpell> Dnd5LandDruidDomainSpells { get; set; }

    public virtual DbSet<Dnd5Metamagic> Dnd5Metamagics { get; set; }

    public virtual DbSet<Dnd5Monster> Dnd5Monsters { get; set; }

    public virtual DbSet<Dnd5MonsterType> Dnd5MonsterTypes { get; set; }

    public virtual DbSet<Dnd5Profbylevel> Dnd5Profbylevels { get; set; }

    public virtual DbSet<Dnd5Race> Dnd5Races { get; set; }

    public virtual DbSet<Dnd5Racial> Dnd5Racials { get; set; }

    public virtual DbSet<Dnd5Size> Dnd5Sizes { get; set; }

    public virtual DbSet<Dnd5SneakAttackD6> Dnd5SneakAttackD6s { get; set; }

    public virtual DbSet<Dnd5SorceryPointConversation> Dnd5SorceryPointConversations { get; set; }

    public virtual DbSet<Dnd5Spell> Dnd5Spells { get; set; }

    public virtual DbSet<Dnd5SpellSlot> Dnd5SpellSlots { get; set; }

    public virtual DbSet<Dnd5SubclassName> Dnd5SubclassNames { get; set; }

    public virtual DbSet<Dnd5Subrace> Dnd5Subraces { get; set; }

    public virtual DbSet<Dnd5WarlockPactBoon> Dnd5WarlockPactBoons { get; set; }

    public virtual DbSet<Dnd5WarlockSpellLevel> Dnd5WarlockSpellLevels { get; set; }

    public virtual DbSet<Dnd5WildMagicSurge> Dnd5WildMagicSurges { get; set; }

    public virtual DbSet<MapBattlemap> MapBattlemaps { get; set; }

    public virtual DbSet<MapScene> MapScenes { get; set; }

    public virtual DbSet<MapSceneItem> MapSceneItems { get; set; }

    public virtual DbSet<MapToken> MapTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=dnd;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<AccountUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Role).WithMany(p => p.AccountUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_role_id");
        });

        modelBuilder.Entity<Dnd5Alignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Background>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5BattleMasterManeuver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Charisma).HasDefaultValueSql("'10'");
            entity.Property(e => e.Constitution).HasDefaultValueSql("'10'");
            entity.Property(e => e.Dexterity).HasDefaultValueSql("'10'");
            entity.Property(e => e.HitDiceNumber).HasDefaultValueSql("'1'");
            entity.Property(e => e.Intelligence).HasDefaultValueSql("'10'");
            entity.Property(e => e.Language).HasDefaultValueSql("'common'");
            entity.Property(e => e.Level).HasDefaultValueSql("'1'");
            entity.Property(e => e.Speed).HasDefaultValueSql("'30'");
            entity.Property(e => e.Strength).HasDefaultValueSql("'10'");
            entity.Property(e => e.Wisidom).HasDefaultValueSql("'10'");

            entity.HasOne(d => d.Alignment).WithMany(p => p.Dnd5Characters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_characters_alignment");

            entity.HasOne(d => d.Background).WithMany(p => p.Dnd5Characters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_characters_background_id");

            entity.HasOne(d => d.Class).WithMany(p => p.Dnd5Characters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_characters_class_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.Dnd5Characters).HasConstraintName("FK_dnd5_characters_owner_id");

            entity.HasOne(d => d.Race).WithMany(p => p.Dnd5Characters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_characters_race_id");

            entity.HasOne(d => d.Size).WithMany(p => p.Dnd5Characters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_characters_size_id");

            entity.HasOne(d => d.Token).WithMany(p => p.Dnd5Characters).HasConstraintName("FK_dnd5_characters_token_id");
        });

        modelBuilder.Entity<Dnd5Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5ClassSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.UnlockLevel).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Class).WithMany(p => p.Dnd5ClassSkills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_céass_skills_class_id");
        });

        modelBuilder.Entity<Dnd5ClassSpell>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_class_spells_class_id");

            entity.HasOne(d => d.Spell).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_class_spells_spell_id");
        });

        modelBuilder.Entity<Dnd5EldrichtInvocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Feat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5FightingStyle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Inventory>(entity =>
        {
            entity.HasOne(d => d.Item).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_inventory_item_id");

            entity.HasOne(d => d.Player).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_inventory_player_id");
        });

        modelBuilder.Entity<Dnd5Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Rarity).WithMany(p => p.Dnd5Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_items_rarity_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Dnd5Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_items_type_id");
        });

        modelBuilder.Entity<Dnd5ItemRarity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5ItemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5LandDruidDomain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5LandDruidDomainSpell>(entity =>
        {
            entity.HasOne(d => d.Land).WithMany().HasConstraintName("FK_land_druid_domain_spells_land_id");
        });

        modelBuilder.Entity<Dnd5Metamagic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Monster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Alignment).WithMany(p => p.Dnd5Monsters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_monsters_alignment_id");

            entity.HasOne(d => d.Size).WithMany(p => p.Dnd5Monsters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_monsters_size_id");

            entity.HasOne(d => d.Token).WithMany(p => p.Dnd5Monsters).HasConstraintName("FK_dnd5_monsters_token_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Dnd5Monsters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_monsters_type_id");
        });

        modelBuilder.Entity<Dnd5MonsterType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Profbylevel>(entity =>
        {
            entity.HasKey(e => e.Level).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Race>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.SpeedWalk).HasDefaultValueSql("'30'");
        });

        modelBuilder.Entity<Dnd5Racial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Race).WithMany(p => p.Dnd5Racials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd_racials_race_id");
        });

        modelBuilder.Entity<Dnd5Size>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5Spell>(entity =>
        {
            entity.HasKey(e => e.SpellId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Dnd5SpellSlot>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dnd5_spell_slots_class_id");
        });

        modelBuilder.Entity<Dnd5SubclassName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.MainClass).WithMany(p => p.Dnd5SubclassNames).HasConstraintName("FK_dnd5_subclass_name_main_class_id");
        });

        modelBuilder.Entity<Dnd5Subrace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Race).WithMany(p => p.Dnd5Subraces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_subrace_race_id");
        });

        modelBuilder.Entity<Dnd5WildMagicSurge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<MapBattlemap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<MapScene>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<MapToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
