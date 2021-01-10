using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StokTakipUygulaması
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-1MAAJ30\SQLEXPRESS;Initial Catalog=StokTakipDB;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
