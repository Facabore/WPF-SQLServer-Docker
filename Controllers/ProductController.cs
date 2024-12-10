using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_SQLSERVER.Entities.Products;
using WPF_SQLSERVER.Services.Products;

namespace WPF_SQLSERVER.Helpers
{
    public class ProductController
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public void ClearFields(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                if (textBox != null)
                    textBox.Text = string.Empty;
            }
        }

        public async Task ShowProductsASync(DataGrid dataGrid)
        {
            try
            {
                var products = await _productService.GetAll();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(Guid));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Description", typeof(string));
                dataTable.Columns.Add("Price", typeof(decimal));
                dataTable.Columns.Add("Stock", typeof(int));

                foreach (var product in products)
                {
                    dataTable.Rows.Add(product.Id, product.Name, product.Description, product.Price, product.Stock);
                }

                dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                throw new Exception($"Somethings goes wrong: { ex.Message} ");
            }


        }

        public async Task AddNewProduct(TextBox textBoxName, TextBox textBoxDescription, TextBox textBoxPrice, TextBox textBoxStock)
        {
            try
            {
                var existingProduct = await _productService.GetByName(textBoxName.Text);
                if(existingProduct.Any())
                {
                    throw new Exception("The product already exists.");
                }

                if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    throw new Exception("The name cannot be empty");
                }
                if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
                {
                    throw new Exception("The description cannot be empty.");
                }
                if (!decimal.TryParse(textBoxPrice.Text, out var price) || price <= 0)
                {
                    throw new Exception("The price must be a valid number and greater than 0.");
                }
                if (!int.TryParse(textBoxStock.Text, out var stock) || stock < 0)
                {
                    throw new Exception("Stock must be an integer and cannot be negative.");
                }

                var newProduct = Product.Create(
                    textBoxName.Text,
                    textBoxDescription.Text,
                    price,
                    stock);

                await _productService.Add(newProduct);

                MessageBox.Show("Product successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear fields

                ClearFields(textBoxName, textBoxDescription, textBoxPrice, textBoxStock);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SelectProduct(DataGrid dataGrid, TextBox textBoxId, TextBox textBoxName, TextBox textBoxDescription, TextBox textBoxPrice, TextBox textBoxStock)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    DataRowView dataRow = (DataRowView)dataGrid.SelectedItem;

                    textBoxId.Text = dataRow.Row.ItemArray[0]?.ToString();
                    textBoxName.Text = dataRow.Row.ItemArray[1]?.ToString();
                    textBoxDescription.Text = dataRow.Row.ItemArray[2]?.ToString();
                    textBoxPrice.Text = dataRow.Row.ItemArray[3]?.ToString();
                    textBoxStock.Text = dataRow.Row.ItemArray[4]?.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public async Task UpdateProduct(TextBox textBoxId, TextBox textBoxName, TextBox textBoxDescription, TextBox textBoxPrice, TextBox textBoxStock)
        {
            try
            {
                if (!Guid.TryParse(textBoxId.Text, out var id))
                {
                    throw new Exception("The id is not valid.");
                }
                if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    throw new Exception("The name cannot be empty.");
                }
                if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
                {
                    throw new Exception("The description cannot be empty.");
                }
                if (!decimal.TryParse(textBoxPrice.Text, out var price) || price <= 0)
                {
                    throw new Exception("The price must be a valid number and greater than 0.");
                }
                if (!int.TryParse(textBoxStock.Text, out var stock) || stock < 0)
                {
                    throw new Exception("Stock must be an integer and cannot be negative.");
                }

                var product = await _productService.GetById(id);

                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

                product.Update(
                    textBoxName.Text,
                    textBoxDescription.Text,
                    price,
                    stock);

                await _productService.Update(product);

                MessageBox.Show("Product successfully updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear fields

                ClearFields(textBoxId, textBoxName, textBoxDescription, textBoxPrice, textBoxStock);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task DeleteProduct(TextBox textBoxId)
        {
            try
            {
                if (!Guid.TryParse(textBoxId.Text, out var id))
                {
                    throw new Exception("The id is not valid.");
                }

                var product = await _productService.GetById(id);

                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

                var result = MessageBox.Show(
                    "Are you sure you want to delete this product?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
    
                await _productService.Delete(product);

                MessageBox.Show("Product successfully deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                textBoxId.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
