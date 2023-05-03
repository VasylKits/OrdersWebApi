using Domain.Entities;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class OrdersInitializer
{
    public static IHost Seed(this IHost webHost)
    {
        using var scope = webHost.Services.CreateScope();
        {
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                context.Database.EnsureCreated();

                var users = context.Users.FirstOrDefault();

                if (users == null)
                {
                    context.Users.AddRange(
                        new User { Login = "johndoe", Password = "pass1234", FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1), Gender = "M" },
                        new User { Login = "janedoe", Password = "pass1234", FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1992, 5, 10), Gender = "F" },
                        new User { Login = "laracroft", Password = "pass1234", FirstName = "Lara", LastName = "Croft", DateOfBirth = new DateTime(1987, 12, 10), Gender = "F" },
                        new User { Login = "bobsmith", Password = "pass1234", FirstName = "Bob", LastName = "Smith", DateOfBirth = new DateTime(1985, 12, 31), Gender = "M" }
                        );

                    context.SaveChanges();
                }

                var orders = context.Orders.FirstOrDefault();

                if (orders == null)
                {
                    context.Orders.AddRange(
                        new Order { UserId = 1, OrderDate = DateTime.Now.AddDays(-5), OrderCost = 1234, ItemsDescription = "Even more items", ShippingAddress = "789 Oak St" },
                        new Order { UserId = 2, OrderDate = DateTime.Now.AddDays(-3), OrderCost = 789, ItemsDescription = "Some more items", ShippingAddress = "65 My St" },
                        new Order { UserId = 3, OrderDate = DateTime.Now.AddDays(-2), OrderCost = 765, ItemsDescription = "Some more items", ShippingAddress = "126 Green St" },
                        new Order { UserId = 2, OrderDate = DateTime.Now.AddDays(-1), OrderCost = 555, ItemsDescription = "Some more items", ShippingAddress = "87 Frank St" },
                        new Order { UserId = 1, OrderDate = DateTime.Now, OrderCost = 50, ItemsDescription = "Some items", ShippingAddress = "123 Main St" },
                        new Order { UserId = 2, OrderDate = DateTime.Now, OrderCost = 75, ItemsDescription = "Some more items", ShippingAddress = "456 Elm St" },
                        new Order { UserId = 3, OrderDate = DateTime.Now, OrderCost = 125, ItemsDescription = "Some more items", ShippingAddress = "42 New St" }
                        );
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        return webHost;
    }        
}