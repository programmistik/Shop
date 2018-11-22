using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // do nothing
            Product = null;
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression be = tbProductName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = tbPackage.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = tbUnitPrice.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = chbDiscount.GetBindingExpression(CheckBox.IsCheckedProperty);
            be.UpdateSource();
            Close();
        }

        
    }
}
