
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ViewModels;
using Presentation.Views;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? builder;

        public App()
        {
            builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddDbContext<DataContext>(x=>x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ProjectCSharp\SqlProject\Infrastructure\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30"));

                services.AddScoped<RoleRepository>();
                services.AddScoped<AddressRepository>();
                services.AddScoped<CustomerRepository>();
                services.AddScoped<AuthRepository>();
                services.AddScoped<ContactRepository>();

                services.AddScoped<RoleService>();
                services.AddScoped<AddressService>();   
                services.AddScoped<CustomerService>();
                services.AddScoped<ContactService>();
                services.AddScoped<RoleService>();

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<CustomerListViewModel>();
                services.AddSingleton<CustomerListView>();                
                services.AddSingleton<AddCustomerViewModel>();
                services.AddSingleton<AddCustomerView>();

            
            
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            builder!.Start();

            var mainWindow = builder!.Services.GetRequiredService<MainWindow>();
            var mainViewModel = builder.Services.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel= builder.Services.GetRequiredService<CustomerListViewModel>();
            mainWindow.Show();

        }


    }

}
