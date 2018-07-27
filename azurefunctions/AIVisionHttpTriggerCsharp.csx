#r "System.Configuration"
#r "System.Data"
#r "Newtonsoft.Json"
using System;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
   log.Info("C# HTTP trigger function processed a request.");
    string item = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "item", true) == 0)
        .Value;
    if(item == null || item == String.Empty){
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
var str = ConfigurationManager.ConnectionStrings["sqldb_connection"].ConnectionString;
using (SqlConnection conn = new SqlConnection(str))
   {
       conn.Open();
var text = "SELECT Top 1 Item, Count from dbo.AIVision WHERE Item = '" + item + "' Order by DateCreated DESC";
       EventData ret = new EventData();
using (SqlCommand cmd = new SqlCommand(text, conn))
       {
           SqlDataReader reader = await cmd.ExecuteReaderAsync();
try
           {
while (reader.Read())
               {
                   log.Info(String.Format("{0}, {1}",
                       reader[0], reader[1]));
                   ret.Item = (string)reader[0];
                   ret.Count = (int)reader[1];
               }
           }
finally
           {
// Always call Close when done reading.
               reader.Close();
           }
var json = JsonConvert.SerializeObject(ret, Formatting.Indented);
return new HttpResponseMessage(HttpStatusCode.OK) 
           {
               Content = new StringContent(json, Encoding.UTF8, "application/json")
           };        
       }
   }
}
public class EventData
{
public String Item { get; set; }
public int Count { get; set; }
}