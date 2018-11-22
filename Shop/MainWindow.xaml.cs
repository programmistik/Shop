using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window  
    {
        ShopEntities Entity = new ShopEntities();

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty CollectionProperty = DependencyProperty.Register
            (
            "Collection", 
            typeof(ICollection<Product>), 
            typeof(MainWindow), 
            new PropertyMetadata(new ObservableCollection<Product>())
            );

        public ICollection<Product> Collection
        {
            get
            {
                return (ICollection<Product>)GetValue(CollectionProperty);
            }
            set
            {
                SetValue(CollectionProperty, value);
            }

        }

        public string Filter
        {
            get
            {
                return (string)GetValue(FilterProperty);
            }
            set
            {
                SetValue(FilterProperty, value);
            }

        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;            

            Entity.Products.Load();

            Collection = Entity.Products.Local;
            lbProductList.ItemsSource = Collection;

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == FilterProperty)
            {
                (Resources["Collection"] as CollectionViewSource).View.Refresh();
                lbProductList.ItemsSource = (Resources["Collection"] as CollectionViewSource).View;
            }

        }


        private void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = false;
            var product = e.Item as Product;

            if (product != null && (string.IsNullOrEmpty(Filter) || product.ProductName.ToLower().Contains(Filter.ToLower())))
            {
                e.Accepted = true;
            }

        }

        

        private void DelClick(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is Product)
            {
                var obj = cmd.DataContext as Product;
                MessageBoxResult rsltMessageBox = MessageBox.Show(
                $"Are you sure to delete\n{obj.ProductName}?", "Conformation dialog", MessageBoxButton.YesNo,MessageBoxImage.Warning);
                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    Collection.Remove(obj);
                    SaveToDB();
                }
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is Product)
            {
                var obj = cmd.DataContext as Product;
                AddEditWindow AddWin = new AddEditWindow();
                AddWin.Product = obj;
               
                AddWin.Title = $"Edit: {obj.ProductName}";
                AddWin.ShowDialog();
                SaveToDB();
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            AddEditWindow AddWin = new AddEditWindow();
            AddWin.Title = "Add new product";
            AddWin.Product = new Product();
            AddWin.ShowDialog();
            if (AddWin.Product != null)
            {
                var obj = AddWin.Product as Product;
                obj.SupplierId = 7;
                Collection.Add(obj);
                SaveToDB();
            }
        }

        private void SaveToDB()
        {
            try
            {
                Entity.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }

    public class IsDiscontinuedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (value.ToString() == "True")
                    return "Green";
            }
            return "Black";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // never used
            return null;
        }
    }
}
