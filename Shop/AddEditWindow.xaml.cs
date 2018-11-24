using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shop
{
    /// <summary>
    /// Interaction logic for AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ProductProperty = DependencyProperty.Register
            ("Product",
            typeof(Product),
            typeof(MainWindow),
            new PropertyMetadata(new Product())
            );

        public Product Product
        {
            get { return (Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }


        public AddEditWindow()
        {
            InitializeComponent();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty SupplierCollectionProperty = DependencyProperty.Register
            (
            "SupplierCollection",
            typeof(ICollection<Supplier>),
            typeof(MainWindow),
            new PropertyMetadata(new ObservableCollection<Supplier>())
            );

        public ICollection<Supplier> SupplierCollection
        {
            get { return (ICollection<Supplier>)GetValue(SupplierCollectionProperty); }
            set { SetValue(SupplierCollectionProperty, value); }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // do nothing
            Product = null;
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cbSupplier.Text))
            {
                MessageBox.Show("Select supplier.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                BindingExpression be = tbProductName.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                be = tbPackage.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                be = tbUnitPrice.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                be = chbDiscount.GetBindingExpression(CheckBox.IsCheckedProperty);
                be.UpdateSource();
                be = cbSupplier.GetBindingExpression(ComboBox.SelectedItemProperty);
                be.UpdateSource();
                Close();
            }
        }
    }
        
}
