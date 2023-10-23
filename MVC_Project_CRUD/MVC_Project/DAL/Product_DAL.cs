using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MVC_Project.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace MVC_Project.DAL
{
    public class Product_DAL
    {
        string constring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();                        
  
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> productlist = new List<ProductModel>();     

            using(SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command =connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_all_product";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts=new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach(DataRow dr in dtProducts.Rows)
                {
                    productlist.Add(new ProductModel
                    {
                        Productid =Convert.ToInt32( dr["Productid"]),
                        Productname = dr["Productname"].ToString(),
                        Price = Convert.ToInt32(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                    });

                }




            }

                return productlist;
        }


        public bool InsertProduct(ProductModel product)
        {

            int id = 0;
            using(SqlConnection connection=new SqlConnection(constring)) { 
            
            
            SqlCommand command=new SqlCommand("Insert_products",connection);

            command.CommandType=CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Productname", product.Productname);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);

                connection.Open();
                id=command.ExecuteNonQuery();
                connection.Close();

            }

            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<ProductModel> GetProductbyId(int Productid)
        {
            List<ProductModel> productlist = new List<ProductModel>();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetProductbyid";
                command.Parameters.AddWithValue("@Productid",Productid);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productlist.Add(new ProductModel
                    {
                        Productid = Convert.ToInt32(dr["Productid"]),
                        Productname = dr["Productname"].ToString(),
                        Price = Convert.ToInt32(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                    });

                }




            }

            return productlist;
        }



        public bool Updateproduct(ProductModel product)
        {

            int id = 0;
            using (SqlConnection connection = new SqlConnection(constring))
            {


                SqlCommand command = new SqlCommand("UpdateProduct", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Productid", product.Productid);
                command.Parameters.AddWithValue("@Productname", product.Productname);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

            }

            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public string Deleteproducts(int productid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("Deleteproduct",connection);         

                command.CommandType=CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Productid", productid);
                command.Parameters.Add("@Returnmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@Returnmessage"].Value.ToString();
                connection.Close();
            
            }

                return result;
        }

    }
}