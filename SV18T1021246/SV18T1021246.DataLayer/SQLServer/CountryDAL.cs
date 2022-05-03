using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer.SQLServer
{
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }

        public IList<Country> List()
        {
            List<Country> data = new List<Country>();

            using(SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Countries", cn);
                cmd.CommandType = System.Data.CommandType.Text;

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = dbReader["CountryName"].ToString()
                    });
                }

                cn.Close();
            }

            return data;
        }
    }
}
