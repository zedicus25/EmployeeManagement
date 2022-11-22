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
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeesRole> EmployeesRoles { get; set; }
        public virtual DbSet<FIO> FIOs { get; set; }
        public virtual DbSet<Importance> Importances { get; set; }
        public virtual DbSet<LoginData> LoginDatas { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Phone_Numbers> Phone_Numbers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
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

            modelBuilder.Entity<Description>()
                .HasMany(e => e.EmployeesRoles)
                .WithRequired(e => e.Description)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Description>()
                .HasMany(e => e.Importances)
                .WithRequired(e => e.Description)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Description>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Description)
                .HasForeignKey(e => e.Description_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Description>()
                .HasMany(e => e.TaskConditions)
                .WithOptional(e => e.Description)
                .HasForeignKey(e => e.Description_Id);

            modelBuilder.Entity<Description>()
                .HasMany(e => e.ProjectTasks)
                .WithRequired(e => e.Description)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EmployeesRole>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.EmployeesRole)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FIO>()
                .HasMany(e => e.Persons)
                .WithRequired(e => e.FIO)
                .HasForeignKey(e => e.FIO_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Importance>()
                .HasMany(e => e.ProjectTasks)
                .WithRequired(e => e.Importance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoginData>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.LoginData)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Emails)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Phone_Numbers)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.ProjectTasks)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectTask>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.ProjectTask)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<TaskCondition>()
                .HasMany(e => e.ProjectTasks)
                .WithRequired(e => e.TaskCondition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Term>()
                .HasMany(e => e.ProjectTasks)
                .WithRequired(e => e.Term)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsersRole>()
                .HasMany(e => e.EmployeesRoles)
                .WithRequired(e => e.UsersRole)
                .HasForeignKey(e => e.UserRoleId)
                .WillCascadeOnDelete(false);
        }
    }
}
