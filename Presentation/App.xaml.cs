
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
        private IHost builder;

        public App()
        {
            builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                //services.AddDbContext<DataContext>(x=>x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ProjectCSharp\SqlProject\Infrastructure\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30", x=>x.MigrationsAssembly(nameof(Infrastructure))));
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ProjectCSharp\SqlProject\Infrastructure\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30"), ServiceLifetime.Transient);
                    
                        //Repositories
                services.AddScoped<RoleRepository>();
                services.AddScoped<AddressRepository>();
                services.AddScoped<CustomerRepository>();
                services.AddScoped<AuthRepository>();
                services.AddScoped<ContactRepository>();
                        //Service
                services.AddScoped<RoleService>();
                services.AddScoped<AddressService>();   
                services.AddScoped<CustomerService>();
                services.AddScoped<ContactService>();
                services.AddScoped<RoleService>();

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();

                services.AddTransient<CustomerListViewModel>();
                services.AddTransient<CustomerListView>();                                                
                services.AddTransient<AddCustomerViewModel>();
                services.AddTransient<AddCustomerView>();
                services.AddTransient<DetailsCustomerViewModel>();
                services.AddTransient<DetailCustomerView>();
                services.AddTransient<UpdateCustomerViewModel>();
                services.AddTransient<UpdateCustomerView>();



            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            builder.Start();

            var mainWindow = builder.Services.GetRequiredService<MainWindow>();
            var mainViewModel = builder.Services.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel= builder.Services.GetRequiredService<CustomerListViewModel>();
            mainWindow.Show();

        }



    }

}
