using Diplom.DataBase.Classes;
using Diplom.DataBase.Interfaces;
using Diplom.Dialogs;
using Diplom.Dialogs.Manager;
using Diplom.Excel;
using Diplom.Excel.Interfaces;
using Diplom.Models;
using Diplom.Provider;
using Diplom.View;
using Diplom.ViewModels;
using DryIoc;
using MaterialDesignThemes.Wpf;
using MaterialDesignXaml.DialogsHelper;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// ViewModel locator.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            ViewModelLocationProvider.Register<LoginWindow, LoginViewModel>();
        }

        /// <summary>
        /// Create shell.
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        /// <summary>
        /// Register types.
        /// </summary>
        /// <param name="containerRegistry">Container.</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //provider
            containerRegistry.RegisterSingleton<IDataProvider, DataProvider>();

            //identifier
            containerRegistry.RegisterDelegate<IDialogIdentifier>(x => new DialogIdentifier("RootIdentifier"), Reuse.Singleton, "rootdialog");

            //snackbar
            containerRegistry.RegisterSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();

            //navigation
            containerRegistry.RegisterForNavigation<TrucksView, TrucksViewModel>(RegionViews.Trucks);
            containerRegistry.RegisterForNavigation<SemitrailersView, SemitrailersViewModel>(RegionViews.Semitrailers);
            containerRegistry.RegisterForNavigation<DriversView, DriversViewModel>(RegionViews.Drivers);
            containerRegistry.RegisterForNavigation<StaffView, StaffViewModel>(RegionViews.Staff);
            containerRegistry.RegisterForNavigation<RoutesView, RoutesViewModel>(RegionViews.Routes);

            //export
            containerRegistry.RegisterSingleton<IExporter<IEnumerable<Truck>>, TrucksExporter>();
            containerRegistry.RegisterSingleton<IExporter<IEnumerable<Semitrailer>>, SemitrailersExporter>();
            containerRegistry.RegisterSingleton<IExporter<IEnumerable<Driver>>, DriversExporter>();
            containerRegistry.RegisterSingleton<IExporter<IEnumerable<Models.Staff>>, StaffExporter>();
            containerRegistry.RegisterSingleton<IExporter<IEnumerable<Route>>, RoutesExporter>();

            //db
            containerRegistry.RegisterSingleton<IInvoker, Invoker>();
            containerRegistry.RegisterSingleton<IContext, Context>();
            containerRegistry.RegisterSingleton<IUsers, Users>();
            containerRegistry.RegisterSingleton<ITable<Truck>, Trucks>();
            containerRegistry.RegisterSingleton<ITable<Driver>, Drivers>();
            containerRegistry.RegisterSingleton<ITable<Semitrailer>, Semitrailers>();
            containerRegistry.RegisterSingleton<ITable<Models.Staff>, DataBase.Classes.Staff>();
            containerRegistry.RegisterSingleton<ITable<Route>, Routes>();

            //dialogs
            containerRegistry.RegisterSingleton<IWindowsManager, WindowsManager>();
            containerRegistry.RegisterSingleton<IDialogManager, DialogManager>();
            containerRegistry.RegisterSingleton<IDialogsFactoryView, DialogsFactoryView>();
            containerRegistry.RegisterDialogView<DriverSelectorView>();
            containerRegistry.RegisterDialogView<SemitrailerSelectorView>();
            containerRegistry.RegisterDialogView<TruckEditorView>();
            containerRegistry.RegisterDialogView<SemitrailEditorView>();
            containerRegistry.RegisterDialogView<DriverEditorView>();
            containerRegistry.RegisterDialogView<StaffEditorView>();
            containerRegistry.RegisterDialogView<ProgramSettingsView>();
            containerRegistry.RegisterDialogView<WaybillStaffSelectorView>();
            containerRegistry.RegisterDialogView<RouteEditorView>();
            containerRegistry.RegisterDialogView<TruckSelectorView>();
        }
    }

    static class ContainerHelper
    {
        public static void RegisterDialogView<T>(this IContainerRegistry containerRegistry) where T : FrameworkElement
        {
            containerRegistry.Register<FrameworkElement, T>(typeof(T).Name);
        }

        public static void RegisterDelegate<T>(this IContainerRegistry containerRegistry, Func<IResolverContext, T> func, IReuse reuse, string key = null)
        {
            containerRegistry.GetContainer().RegisterDelegate(func, reuse, serviceKey: key);
        }

        public static IDialogIdentifier ResolveRootDialogIdentifier(this IContainer container)
        {
            return container.Resolve<IDialogIdentifier>("rootdialog");
        }
    }
}
