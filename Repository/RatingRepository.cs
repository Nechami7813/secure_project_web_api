using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class RatingRepository : IRatingRepository
{
    public IConfiguration _configuration { get; }
    public RatingRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public int AddRating(Rating rating)
    {
        {
            string connString = _configuration.GetConnectionString("OurShop");
            string sql = "INSERT INTO RATING (HOST,METHOD,PATH,REFERER,USER_AGENT,Record_Date)VALUES(@HOST,@METHOD,@PATH,@REFERER,@USER_AGENT,@Record_Date); " + "SELECT CAST(scope_identity() AS INT);";
            using (SqlConnection connection = new SqlConnection(connString))
            {


                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@HOST", SqlDbType.NVarChar);
                cmd.Parameters["@HOST"].Value = rating.Host;
                cmd.Parameters.Add("@METHOD", SqlDbType.NChar);
                cmd.Parameters["@METHOD"].Value = rating.Method;
                cmd.Parameters.Add("@PATH", SqlDbType.NVarChar);
                cmd.Parameters["@PATH"].Value = rating.Path;
                cmd.Parameters.Add("@REFERER", SqlDbType.NVarChar);
                cmd.Parameters["@REFERER"].Value = rating.Referer;
                cmd.Parameters.Add("@USER_AGENT", SqlDbType.NVarChar);
                cmd.Parameters["@USER_AGENT"].Value = rating.UserAgent;
                cmd.Parameters.Add("@Record_Date", SqlDbType.DateTime);
                cmd.Parameters["@Record_Date"].Value = rating.RecordDate;
                try
                {
                    connection.Open();
                    rating.RatingId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return rating.RatingId;
        }
    }
}
