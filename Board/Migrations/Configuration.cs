namespace Board.Migrations
{
    using Board.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Board.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Board.Models.DBContext context)
        {
            var role = new List<Role>
           {
               new Role {Description="HR"},
               new Role {Description="Sales"},
               new Role {Description="IT"},
               new Role {Description="Account Management"}
           };
            role.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var user = new List<User>
            {
                new User{FirstName="Carson", LastName="Alexander", Email="carson@gmail.com", Password="1", RoleID = 1},
                new User{FirstName="Meredith", LastName="Alonso", Email="meredith@gmail.com", Password="1", RoleID = 2},
                new User{FirstName="Yan", LastName="Li", Email="yan@gmail.com", Password="1", RoleID = 3},
                new User{FirstName="Arturo", LastName="Anand", Email="arturo@gmail.com", Password="1", RoleID = 4},
                new User{FirstName="Gytis", LastName="Barzdukas", Email="gytis@gmail.com", Password="1", RoleID = 3},
            };
            user.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var importance = new List<Importance>
            {
                new Importance {Description="High"},
                new Importance {Description="Normal"},
                new Importance {Description="Low"}
            };
            importance.ForEach(i => context.Importances.Add(i));
            context.SaveChanges();

            var status = new List<Status>
            {
                new Status {Description="Request"},
                new Status {Description="Processing"},
                new Status {Description="Done"}
            };
            status.ForEach(s => context.Status.Add(s));
            context.SaveChanges();

            var priority = new List<Priority>
            {
                new Priority {Description="Not assigned"},
                new Priority {Description="Urgent"},
                new Priority {Description="Normal"},
                new Priority {Description="Low"}
            };
            priority.ForEach(o => context.Priorities.Add(o));
            context.SaveChanges();

        }
    }
}
