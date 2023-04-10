﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PostgresDatabase.Contexts;

#nullable disable

namespace PostgresDatabase.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    [Migration("20230409143501_PlayerGetsName")]
    partial class PlayerGetsName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.General.Configuration", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Key")
                        .HasName("pk_configuration");

                    b.ToTable("configuration", "config");
                });

            modelBuilder.Entity("Domain.Entities.General.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ExeArgs")
                        .HasColumnType("text")
                        .HasColumnName("exe_args");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PathToExe")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path_to_exe");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("service_pkey");

                    b.ToTable("service", "config");
                });

            modelBuilder.Entity("Domain.Entities.Generic.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("JoinTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("join_timestamp");

                    b.Property<DateTime?>("LeaveTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("leave_timestamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("SessionId")
                        .HasColumnType("integer")
                        .HasColumnName("session_id");

                    b.HasKey("Id")
                        .HasName("player_pkey");

                    b.HasIndex("SessionId")
                        .HasDatabaseName("ix_player_session_id");

                    b.ToTable("player", "main");
                });

            modelBuilder.Entity("Domain.Entities.Generic.ServiceSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<DateTime>("StartTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_timestamp");

                    b.Property<DateTime?>("StopTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("stop_timestamp");

                    b.HasKey("Id")
                        .HasName("session_pkey");

                    b.HasIndex("ServiceId")
                        .HasDatabaseName("ix_session_service_id");

                    b.ToTable("session", "main");
                });

            modelBuilder.Entity("Domain.Entities.Generic.Player", b =>
                {
                    b.HasOne("Domain.Entities.Generic.ServiceSession", "Session")
                        .WithMany("Players")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_player_sessions_session_id");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Domain.Entities.Generic.ServiceSession", b =>
                {
                    b.HasOne("Domain.Entities.General.Service", "Service")
                        .WithMany("Sessions")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_session_service_service_id");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Domain.Entities.General.Service", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Domain.Entities.Generic.ServiceSession", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
