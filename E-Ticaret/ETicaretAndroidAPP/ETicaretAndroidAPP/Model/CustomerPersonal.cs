using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace ETicaretAndroidAPP.Model
{

    [Table("CustomerPersonal")]
    public class CustomerPersonal
    {
        [PrimaryKey, Column("UserName"), MaxLength(8)]
        public string UserName { get; set; }
        [Column("Name"), MaxLength(16)]
        public string Name { get; set; }
        [Column("Surname"), MaxLength(16)]
        public string Surname { get; set; }
        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }
        [Column("Address"), MaxLength(50)]
        public string Address { get; set; }

    }
}