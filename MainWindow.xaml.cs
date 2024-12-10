using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_SQLSERVER.Helpers;
using WPF_SQLSERVER.Infraestructure;

namespace WPF_SQLSERVER;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AppDbContext _dbContext;
    private readonly ProductController _productController;
    public MainWindow(AppDbContext dbContext, ProductController productController)
    {
        InitializeComponent();
        _dbContext = dbContext;
        _productController = productController;
        InitializedDatabase();
        
        CargarProductos();
    }

    private void InitializedDatabase()
    {
        Console.WriteLine("Trying to connect");
        _dbContext.Database.EnsureCreated();
        Console.WriteLine("Finish");
    }
    private async void CargarProductos()
    {
        await _productController.ShowProductsASync(ProductosDataGrid);
    }


    private async void Agregar_Click(object sender, RoutedEventArgs e)
    {
        await _productController.AddNewProduct(textBoxName, textBoxDescription, textBoxPrice, textBoxStock);
        CargarProductos();
    }

    private async void Actualizar_Click(object sender, RoutedEventArgs e)
    {
        await _productController.UpdateProduct(textBoxId, textBoxName, textBoxDescription, textBoxPrice, textBoxStock);
        CargarProductos();
    }

    private async void Eliminar_Click(object sender, RoutedEventArgs e)
    {
        await _productController.DeleteProduct(textBoxId);
        CargarProductos();
        _productController.ClearFields(textBoxId, textBoxName, textBoxDescription, textBoxPrice, textBoxStock);


    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void ProductosDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        _productController.SelectProduct(ProductosDataGrid, textBoxId, textBoxName, textBoxDescription, textBoxPrice, textBoxStock);
    }

    private void ProductosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _productController.ClearFields(textBoxId, textBoxName, textBoxDescription, textBoxPrice, textBoxStock);
    }
}
