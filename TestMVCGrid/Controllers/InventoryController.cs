using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using TestMVCGrid.Models;
using System.Data;

namespace TestMVCGrid.Controllers
{
    public class InventoryController : Controller
    {
        public ActionResult Inventory()
        { 
            List<Models.InventoryModel> model=new List<InventoryModel> ();
          //  InventoryModel inventoryModel = new InventoryModel();

            DataTable dt = new DataTable();
            SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testConnection"].ToString();


            SqlCommand cmd = new SqlCommand("GetInventoryTbl", objConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            using (var tbl = new SqlDataAdapter(cmd))
            {
                objConnection.Open();
                tbl.Fill(dt);


                foreach (DataRow row in dt.Rows)
                {
                    var inventoryModel = new InventoryModel()
                    {
                        item = row[0].ToString(),
                        quantity = Convert.ToInt16(row[1].ToString()),
                        price = Convert.ToInt16(row[2].ToString()),
                        subtotal = Convert.ToInt16(row[3].ToString())
                    };                   

                    model.Add(inventoryModel);
                }
            }

            return View(model);
        }

        public JsonResult Insert(InventoryModel inventory)
        {
            SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testConnection"].ToString();


            SqlCommand cmd = new SqlCommand("CreateItem", objConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            objConnection.Open();

            SqlParameter paramItem = new SqlParameter();
            paramItem.ParameterName = "@item";
            paramItem.Value = inventory.item;

            cmd.Parameters.Add(paramItem);

            SqlParameter paramQuantity = new SqlParameter();
            paramQuantity.ParameterName = "@Quantity";
            paramQuantity.Value = inventory.quantity;

            cmd.Parameters.Add(paramQuantity);

            SqlParameter paramPrice = new SqlParameter();
            paramPrice.ParameterName = "@price";
            paramPrice.Value = inventory.price;

            cmd.Parameters.Add(paramPrice);
            cmd.ExecuteNonQuery();

            objConnection.Close();

            return Json(inventory); 
        }

      

    }
}