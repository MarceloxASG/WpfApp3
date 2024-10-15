using Data;
using Entity;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            DProducto dInvoice = new DProducto();
            var invoices = dInvoice.Get();

            dgInvoices.ItemsSource = invoices;
        }

        private void btnFilterInvoice_Click(object sender, RoutedEventArgs e)
        {
            string invoiceNumber = txtInvoiceNumber.Text;

            if (!string.IsNullOrEmpty(invoiceNumber)) 
            {
                var productos = new DProducto().Get(invoiceNumber); 
                dgInvoices.ItemsSource = productos; 
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número de factura válido.");
            }
        }

        private void btnInsertInvoice_Click(object sender, RoutedEventArgs e)
        {
            InsertInvoiceWindow insertWindow = new InsertInvoiceWindow(); // Crear una nueva ventana para insertar
            insertWindow.ShowDialog(); // Mostrar la ventana como diálogo
        }


        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (dgInvoices.SelectedItem != null)
            {
                Producto selectedInvoice = (Producto)dgInvoices.SelectedItem; // Obtener la factura seleccionada
                var invoiceID = selectedInvoice.InvoiceID;

                // Llamar al método que elimina la factura
                new DProducto().DeleteInvoice(invoiceID);

                // Recargar las facturas en el DataGrid después de eliminar
                var productos = new DProducto().Get();
                dgInvoices.ItemsSource = productos;
            }
            else
            {
                MessageBox.Show("Seleccione una factura para eliminar.");
            }
        }
    }
}