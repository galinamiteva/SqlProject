using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ViewModels;
using Presentation.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Presentation;

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
            services.AddDbContext<DataContext>(x=>x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ProjectCSharp\SqlProject\Infrastructure\Data\customers_dbase.mdf;Integrated Security=True;Connect Timeout=30"));
            
            //Repositories
            services.AddScoped<RoleRepository>();
            services.AddScoped<AddressRepository>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<AuthRepository>();
            services.AddScoped<ContactRepository>();
            
            //Services
            services.AddScoped<RoleService>();
            services.AddScoped<AddressService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<AuthService>();
            services.AddScoped<ContactService>();


            services.AddScoped<MainViewModel>();
            services.AddScoped<MainWindow>();

            services.AddScoped<CustomerListViewModel>();
            services.AddScoped<CustomerListView>();

            services.AddScoped<AddCustomerViewModel>();
            services.AddScoped<AddCustomerView>();

            services.AddScoped<DetailsCustomerViewModel>();
            services.AddScoped<DetailCustomerView>();

            services.AddScoped<UpdateCustomerViewModel>();
            services.AddScoped<UpdateCustomerView>();

            services.AddScoped<RoleListViewModel>();
            services.AddScoped<RoleListView>();

            services.AddScoped<AddressListViewModel>();
            services.AddScoped<AddressListView>();

        }).Build();


    }

    protected override void OnStartup(StartupEventArgs e)
    {
        builder.Start();

        var mainWindow = builder.Services.GetRequiredService<MainWindow>();
        var mainViewModel = builder.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = builder.Services.GetRequiredService<CustomerListViewModel>();
        mainWindow.Show();
    }
}
