using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DProducto
    {
        private string connectionString = "Data Source=LAB1507-07\\SQLEXPRESS;Initial Catalog=FacturaDB;User ID=usuario01;Password=123456;";

        public List<Producto> Get(string invoiceNumber = null)
        {
            var invoices = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetFacturas", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                
                if (!string.IsNullOrEmpty(invoiceNumber))
                {
                    cmd.Parameters.AddWithValue("@invoice_number", invoiceNumber);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var invoice = new Producto
                        {
                            InvoiceID = reader.GetInt32(reader.GetOrdinal("invoice_id")),
                            CustomerID = reader.GetInt32(reader.GetOrdinal("customer_id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("date")),
                            Total = reader.GetDecimal(reader.GetOrdinal("total")),
                            Active = reader.GetBoolean(reader.GetOrdinal("active")),
                            InvoiceNumber = reader.GetString(reader.GetOrdinal("invoice_number"))
                        };
                        invoices.Add(invoice);
                    }
                }
            }
            return invoices;
        }

        public void InsertInvoice(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("InsertFactura", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_id", producto.CustomerID);
                cmd.Parameters.AddWithValue("@date", producto.Date);
                cmd.Parameters.AddWithValue("@total", producto.Total);
                cmd.Parameters.AddWithValue("@active", producto.Active);
                cmd.Parameters.AddWithValue("@invoice_number", producto.InvoiceNumber);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteInvoice(int invoiceID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteFactura", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@invoice_id", invoiceID);
                cmd.ExecuteNonQuery();
            }
        }
    }

}
