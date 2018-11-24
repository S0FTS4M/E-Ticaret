using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace ETicaretAndroidAPP.Model
{
    [Table("Product")]
    class Product
    {
        [PrimaryKey,Column("ProductId"),MaxLength(8)]
        public string ProductID { get; set; }
        [Column("ProductName"),MaxLength(30)]
        public string ProductName { get; set; }
        [Column("ProductType"),MaxLength(30)]
        public string ProductType { get; set; }
        [Column("ProductCategory"),MaxLength(30)]
        public string ProductCategory { get; set; }
        [Column("ProductPrice")]
        public double ProductPrice { get; set; }
        [Column("ProductDate")]
        public DateTime ProductDate { get; set; }
        [Column("ProductAmount")]
        public int ProductCount { get; set; }
        [Column("ProductImage")]
        public int ProductImage { get; set; }
    }
}