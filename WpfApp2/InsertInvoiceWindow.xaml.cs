using Data;
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

namespace WpfApp2
{
    /// <summary>
    /// Lógica de interacción para InsertInvoiceWindow.xaml
    /// </summary>
    public partial class InsertInvoiceWindow : Window
    {
        public InsertInvoiceWindow()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores de los TextBoxes
            int customerID = int.Parse(txtCustomerID.Text);
            DateTime date = DateTime.Parse(txtDate.Text);
            decimal total = decimal.Parse(txtTotal.Text);
            string invoiceNumber = txtInvoiceNumber.Text;

            // Llamar al método para insertar la factura
            new DProducto().InsertInvoice(new Entity.Producto
            {
                InvoiceNumber = invoiceNumber,
                CustomerID = customerID,
                Date = date,
                Total = total,
            });

            MessageBox.Show("Factura insertada correctamente.");
            this.Close(); // Cerrar la ventana después de la inserción
        }
    }

}
