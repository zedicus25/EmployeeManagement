using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Server.Models
{
    public partial class EmployeeManagement : DbContext
    {
        public EmployeeManagement()
            : base("name=EmployeeManagement")
        {
        }

        public virtual DbSet<Adress> Adresses { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<EmployeeRoleDescription> EmployeeRoleDescriptions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeesRole> EmployeesRoles { get; set; }
        public virtual DbSet<FIO> FIOs { get; set; }
        public virtual DbSet<ImportanceDescription> ImportanceDescriptions { get; set; }
        public virtual DbSet<Importance> Importances { get; set; }
        public virtual DbSet<LoginData> LoginDatas { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Phone_Numbers> Phone_Numbers { get; set; }
        public virtual DbSet<ProjectDescription> ProjectDescriptions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTaskDescription> ProjectTaskDescriptions { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<TaskConditionDescription> TaskConditionDescriptions { get; set; }
        public virtual DbSet<TaskCondition> TaskConditions { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<UsersRole> UsersRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adress>()
                .HasMany(e => e.Persons)
                .WithRequired(e => e.Adress)
                .HasForeignKey(e => e.Adress_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeRoleDescription>()
                .HasMany(e => e.EmployeesRoles)
                .WithRequired(e => e.EmployeeRoleDescription)
                .HasForeignKey(e => e.DescriptionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EmployeesRole>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.EmployeesRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<FIO>()
                .HasMany(e => e.Persons)
                .WithRequired(e => e.FIO)
                .HasForeignKey(e => e.FIO_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportanceDescription>()
                .HasMany(e => e.Importances)
                .WithOptional(e => e.ImportanceDescription)
                .HasForeignKey(e => e.DescriptionId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Emails)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Phone_Numbers)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectDescription>()
                .HasMany(e => e.Projects)
                .WithOptional(e => e.ProjectDescription)
                .HasForeignKey(e => e.DescriptionId);

            modelBuilder.Entity<ProjectTaskDescription>()
                .HasMany(e => e.ProjectTasks)
                .WithOptional(e => e.ProjectTaskDescription)
                .HasForeignKey(e => e.DescriptionId);

            modelBuilder.Entity<TaskConditionDescription>()
                .HasMany(e => e.TaskConditions)
                .WithOptional(e => e.TaskConditionDescription)
                .HasForeignKey(e => e.DescriptionId);

            modelBuilder.Entity<UsersRole>()
                .HasMany(e => e.EmployeesRoles)
                .WithRequired(e => e.UsersRole)
                .HasForeignKey(e => e.UserRoleId)
                .WillCascadeOnDelete(false);
        }
    }
}
