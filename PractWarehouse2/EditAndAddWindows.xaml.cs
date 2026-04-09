using PractWarehouse2.ModelsDB;
using System;
using System.Collections.Generic;
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

namespace PractWarehouse2
{
    public partial class EditAndAddWindows : Window
    {
        public EditAndAddWindows()
        {
            InitializeComponent();

            
            if (Data.product != null)
            {
                Title = "Редактирование товара";
                btnSave.Content = "Изменить";
                tbProductName.Text = Data.product.ProductName;
                tbCategory.Text = Data.product.Category;
                tbManufacturer.Text = Data.product.Manufacturer;
            }
            else
            {
                Title = "Добавление товара";
                btnSave.Content = "Добавить";
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbProductName.Text))
                errors.AppendLine("Введите название товара");

            if (string.IsNullOrWhiteSpace(tbCategory.Text))
                errors.AppendLine("Введите категорию");

            if (string.IsNullOrWhiteSpace(tbManufacturer.Text))
                errors.AppendLine("Введите производителя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (Pract4Warehouse1Context _db = new Pract4Warehouse1Context())
                {
                    if (Data.product != null)
                    {
                      
                        var product = _db.Products.Find(Data.product.ProductId);
                        if (product != null)
                        {
                            product.ProductName = tbProductName.Text;
                            product.Category = tbCategory.Text;
                            product.Manufacturer = tbManufacturer.Text;
                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        

                        Product newProduct = new Product()
                        {
                           
                            ProductName = tbProductName.Text,
                            Category = tbCategory.Text,
                            Manufacturer = tbManufacturer.Text
                        };

                        _db.Products.Add(newProduct);
                        _db.SaveChanges();
                    }
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
