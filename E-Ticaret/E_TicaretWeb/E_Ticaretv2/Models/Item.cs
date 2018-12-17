using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.Models
{
    public class Item
    {
        private Product pd = new Product();
        private int quantity;

        public Product Pd
        {
            get { return pd; }
            set { pd = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Item()
        {

        }
        public Item(Product _product, int _amount)
        {
            pd = _product;
            quantity = _amount;
        }
    }
}