using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TestMVCGrid.Models
{
    public class InventoryModel
    {
        public int quantity { get; set; }
        public string item { get; set; }
        public int price { get; set; }
        public int subtotal { get; set; }

        

    }
}