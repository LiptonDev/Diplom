using Diplom.Properties;
using Prism.Regions;
using System.Windows;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Менеджер регионов.
        /// </summary>
        readonly IRegionManager regionManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            this.regionManager = regionManager;

            var settings = Settings.Default;
            Left = settings.left;
            Top = settings.top;
            Width = settings.width;
            Height = settings.height;

            if (settings.state == WindowState.Minimized)
                settings.state = WindowState.Normal;

            WindowState = settings.state;

            RegionManager.SetRegionManager(this, this.regionManager);
        }

        /// <summary>
        /// Переход на страницу грузовиков.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            regionManager.RequestNavigateInRootRegion(RegionViews.Trucks);
        }

        /// <summary>
        /// Сохранение настроек окна.
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var settings = Settings.Default;
            settings.left = Left;
            settings.top = Top;
            settings.width = Width;
            settings.height = Height;
            settings.state = WindowState;

            settings.Save();
        }
    }
}
