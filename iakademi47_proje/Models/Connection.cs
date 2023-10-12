using Microsoft.Data.SqlClient;

namespace iakademi47_proje.Models
{
    public class Connection
    {
        public static SqlConnection ServerConnect
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection("Server=LAPTOP-GL2LON0G;Database=iakademi47Core_Proje;trusted_connection=True;TrustServerCertificate=True;");
                return sqlConnection;
            }
        }


    }
}
