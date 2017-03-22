﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameShop.Model;

namespace GameShop.DAL
{
    class ProductContext
    {
        public List<Products> getByCategoryID(int cid)
        {
            SqlCommand command;
            List<Products> ps = new List<Products>();
            SqlConnection connection = null;
            SqlDataReader data = null;
            string sql = "SELECT [ProductID], [ProductName], [Description], " +
                         "[Price], [Sale], [CategoryID], [SupplierID], [PublishDate], " +
                         "[Rating] FROM [Products] WHERE CategoryID = @param";
            try
            {
                connection = DBContext.openConnection();
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@param", cid);
                data = command.ExecuteReader();
                while (data.Read())
                {
                    int id = Convert.ToInt16(data["ProductID"]);
                    string name = Convert.ToString(data["ProductName"]);
                    string des = Convert.ToString(data["Description"]);
                    double price = Convert.ToDouble(data["Price"]);
                    int sale = Convert.ToInt16(data["Sale"]);
                    int cateID = Convert.ToInt16(data["CategoryID"]);
                    int spID = Convert.ToInt16(data["SupplierID"]);
                    DateTime date = Convert.ToDateTime(data["PublishDate"]);
                    int rate = Convert.ToInt16(data["Rating"]);
                    Products p = new Products(id, name, des, price, sale, date, rate, new Categories(cateID, "") , new Suppliers(spID, ""));
                    ps.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                data.Close();
                DBContext.closeConnection(connection);
            }
            return ps;
        }

        public Products getByID(int pid)
        {
            SqlCommand command;
            Products p = null;
            SqlConnection connection = null;
            SqlDataReader data = null;
            string sql = "SELECT [ProductID], [ProductName], [Description], [Price], [Sale],s.CompanyName,c.CategoryName, [PublishDate],[Rating] FROM [Products] p Join Categories c On p.CategoryID = c.CategoryID JOIN Suppliers s ON s.SupplierID = p.SupplierID WHERE ProductID = @pid";
            try
            {
                connection = DBContext.openConnection();
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@pid", pid);
                data = command.ExecuteReader();
                while (data.Read())
                {
                    int id = Convert.ToInt16(data["ProductID"]);
                    string name = Convert.ToString(data["ProductName"]);
                    string des = Convert.ToString(data["Description"]);
                    double price = Convert.ToDouble(data["Price"]);
                    int sale = Convert.ToInt16(data["Sale"]);
                    string cateID = data["CategoryName"].ToString();
                    string spID = data["CompanyName"].ToString();
                    DateTime date = Convert.ToDateTime(data["PublishDate"]);
                    int rate = Convert.ToInt16(data["Rating"]);
                    p = new Products(id, name, des, price, sale, date, rate, new Categories(1, cateID), new Suppliers(1, spID));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                data.Close();
                DBContext.closeConnection(connection);
            }
            return p;
        }

    }
}
