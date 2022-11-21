using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

public partial class EmployeeManagementContext : DbContext
{
    public EmployeeManagementContext()
    {
    }

    public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Description> Descriptions { get; set; }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesRole> EmployeesRoles { get; set; }

    public virtual DbSet<Fio> Fios { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Importance> Importances { get; set; }

    public virtual DbSet<LoginDatum> LoginData { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskCondition> TaskConditions { get; set; }

    public virtual DbSet<Term> Terms { get; set; }

    public virtual DbSet<UsersRole> UsersRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.Property(e => e.City).HasMaxLength(90);
            entity.Property(e => e.Country).HasMaxLength(90);
            entity.Property(e => e.FullAdress)
                .HasMaxLength(250)
                .HasColumnName("Full_Adress");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(10)
                .HasColumnName("House_Number");
            entity.Property(e => e.Street).HasMaxLength(90);
        });

        modelBuilder.Entity<Description>(entity =>
        {
            entity.Property(e => e.Description1)
                .HasMaxLength(2500)
                .HasColumnName("Description");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Email>(entity =>
        {
            entity.Property(e => e.Email1)
                .HasMaxLength(30)
                .HasColumnName("Email");

            entity.HasOne(d => d.Person).WithMany(p => p.Emails)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Emails_Persons");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.PersonId, "IX_Employees").IsUnique();

            entity.HasIndex(e => e.TaskId, "IX_Employees_2").IsUnique();

            entity.HasIndex(e => e.LoginDataId, "IX_Employees_3").IsUnique();

            entity.Property(e => e.Avatar).HasMaxLength(110);
            entity.Property(e => e.Salary).HasColumnType("money");

            entity.HasOne(d => d.LoginData).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.LoginDataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_LoginData");

            entity.HasOne(d => d.Person).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Persons");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_EmployeesRoles");

            entity.HasOne(d => d.Task).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Tasks");
        });

        modelBuilder.Entity<EmployeesRole>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_EmployeesRoles").IsUnique();

            entity.HasOne(d => d.Description).WithOne(p => p.EmployeesRole)
                .HasForeignKey<EmployeesRole>(d => d.DescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeesRoles_Descriptions");

            entity.HasOne(d => d.UserRole).WithMany(p => p.EmployeesRoles)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeesRoles_UsersRoles");
        });

        modelBuilder.Entity<Fio>(entity =>
        {
            entity.ToTable("FIOs");

            entity.Property(e => e.FirstName)
                .HasMaxLength(70)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(70)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Patronymic).HasMaxLength(70);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.Path).HasMaxLength(110);

            entity.HasOne(d => d.Project).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Images_Projects");
        });

        modelBuilder.Entity<Importance>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_Importances").IsUnique();

            entity.HasOne(d => d.Description).WithOne(p => p.Importance)
                .HasForeignKey<Importance>(d => d.DescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Importances_Descriptions");
        });

        modelBuilder.Entity<LoginDatum>(entity =>
        {
            entity.Property(e => e.Login).HasMaxLength(25);
            entity.Property(e => e.Password).HasMaxLength(30);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(e => e.FioId, "IX_Persons").IsUnique();

            entity.HasIndex(e => e.AdressId, "IX_Persons_1").IsUnique();

            entity.Property(e => e.AdressId).HasColumnName("Adress_Id");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.FioId).HasColumnName("FIO_Id");

            entity.HasOne(d => d.Adress).WithOne(p => p.Person)
                .HasForeignKey<Person>(d => d.AdressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persons_Adresses");

            entity.HasOne(d => d.Fio).WithOne(p => p.Person)
                .HasForeignKey<Person>(d => d.FioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persons_FIOs");
        });

        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.ToTable("Phone_Numbers");

            entity.Property(e => e.PhoneNumber1)
                .HasMaxLength(15)
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.Person).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phone_Numbers_Persons");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_Projects").IsUnique();

            entity.Property(e => e.DescriptionId).HasColumnName("Description_Id");

            entity.HasOne(d => d.Description).WithOne(p => p.Project)
                .HasForeignKey<Project>(d => d.DescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Descriptions");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_Tasks_1").IsUnique();

            entity.HasIndex(e => e.TermId, "IX_Tasks_4").IsUnique();

            entity.HasOne(d => d.Description).WithOne(p => p.Task)
                .HasForeignKey<Task>(d => d.DescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Descriptions");

            entity.HasOne(d => d.Importance).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ImportanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Importances");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Projects");

            entity.HasOne(d => d.TaskCondition).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskConditionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_TaskConditions");

            entity.HasOne(d => d.Term).WithOne(p => p.Task)
                .HasForeignKey<Task>(d => d.TermId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Terms");
        });

        modelBuilder.Entity<TaskCondition>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_TaskConditions").IsUnique();

            entity.Property(e => e.DescriptionId).HasColumnName("Description_Id");

            entity.HasOne(d => d.Description).WithOne(p => p.TaskCondition)
                .HasForeignKey<TaskCondition>(d => d.DescriptionId)
                .HasConstraintName("FK_TaskConditions_Descriptions");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ToComplete).HasColumnType("datetime");
        });

        modelBuilder.Entity<UsersRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
