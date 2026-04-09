using PractWarehouse2.ModelsDB;
using System.Windows;
using System.Windows.Controls;

namespace PractWarehouse2
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Authorization  A = new Authorization();
            A.ShowDialog();
            if (Data.Enter == false) Close();

            SetRight();

            SetStatus();
        }
        private void SetRight()
        {
            if (Data.UserRole == "Администратор")
            {

            }
            else if (Data.UserLastname == "Гость")
            {
                btnAdd.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
                tbSearch.IsEnabled = false;
            }
            else if (Data.UserRole == "Менеджер")
            {
                btnDelete.IsEnabled = false;
            }
            else if (Data.UserLastname != "Гость" && Data.UserRole == "Пользователь")
            {
                btnAdd.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }
        }
        private void SetStatus()
        {
            mainWindow.Title = Data.UserLastname + " " + Data.UserName + " " + Data.UserPatronymic + " " + Data.UserRole;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Data.product = null;
            EditAndAddWindows aad= new EditAndAddWindows();
           
            aad.ShowDialog();
            LoadDBProducts();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для редактирования!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Data.product = (Product)dgProducts.SelectedItem;
            EditAndAddWindows edit = new EditAndAddWindows();
            
            edit.ShowDialog();
            LoadDBProducts();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Product product = (Product)dgProducts.SelectedItem;

           
            using (Pract4Warehouse1Context _db = new Pract4Warehouse1Context())
            {
                var hasStock = _db.StockBalances.Any(sb => sb.ProductId == product.ProductId);

                if (hasStock)
                {
                    MessageBox.Show("Нельзя удалить товар, который есть на складах!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            MessageBoxResult result = MessageBox.Show($"Удалить товар \"{product.ProductName}\"?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (Pract4Warehouse1Context _db = new Pract4Warehouse1Context())
                    {
                        var deleteProduct = _db.Products.Find(product.ProductId);
                        if (deleteProduct != null)
                        {
                            _db.Products.Remove(deleteProduct);
                            _db.SaveChanges();
                            LoadDBProducts();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadDBProducts();
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBProducts();
        }
        private void LoadDBProducts()
        {
            try
            {
                using (Pract4Warehouse1Context _db = new Pract4Warehouse1Context())
                {
                    var products = _db.Products.ToList();
                    if (!string.IsNullOrEmpty(tbSearch.Text))
                    {
                        products = products.Where(p => p.ProductName.Contains(tbSearch.Text)).ToList();
                    }

                    dgProducts.ItemsSource = products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}