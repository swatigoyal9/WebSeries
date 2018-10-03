using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace WebSeriesService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebSeries" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select WebSeries.svc or WebSeries.svc.cs at the Solution Explorer and start debugging.
	public class WebSeries : IWebSeries
	{
		public string SearchInventory(string SearchString, string SearchBy)
		{
			string searchQuery;

			string Connectionstring = ConfigurationManager.ConnectionStrings["WebDB"].ConnectionString;

			using (SqlConnection con = new SqlConnection(Connectionstring))
			{
				if (String.Equals(SearchBy, "WebId"))
				{
					searchQuery = "Select [WebURL] from [WebTable] where [WebId] like '" + SearchString + "%'";
				}
				else
				{
					searchQuery = "Select [WebURL] from [WebTable] where [WebName] like '%" + SearchString + "%'";
				}
				con.Open();
				SqlCommand command = new SqlCommand(searchQuery, con);
				DataTable SearchTable = new DataTable();

				SearchTable.Load(command.ExecuteReader());

				string JsonTable = JsonConvert.SerializeObject(SearchTable);
				return JsonTable;

			}
		}

	}
}
