using System.Windows;
using System.Windows.Controls;

namespace Diplom.Controls
{
    /// <summary>
    /// Логика взаимодействия для CarNumber.xaml
    /// </summary>
    public partial class CarNumber : UserControl
    {
        public CarNumber()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(string), typeof(CarNumber));
        public static readonly DependencyProperty RegionProperty = DependencyProperty.Register("Region", typeof(int), typeof(CarNumber));

        public string Number
        {
            get => (string)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        public int Region
        {
            get => (int)GetValue(RegionProperty);
            set => SetValue(RegionProperty, value);
        }
    }
}
