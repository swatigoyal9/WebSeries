using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Runtime;
using Newtonsoft.Json;

namespace WebApplication2
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}



		protected void Button1_Click(object sender, EventArgs e)
		{
			string searchstring = TextBox2.Text;
			string srearchby = DropDownList1.SelectedValue;

			ServiceReference1.WebSeriesClient client = new ServiceReference1.WebSeriesClient();
			string JsonTable = client.SearchInventory(searchstring, srearchby);
			DataTable SearchTable = new DataTable();
			SearchTable = JsonConvert.DeserializeObject<DataTable>(JsonTable);
			if (SearchTable.Rows.Count == 0)
			{
				GridView1.Visible = false;
				Label1.Visible = true;
				Label1.Text = "Sorry couldnt find anything for this search";
			}
			else
			{
				GridView1.Visible = true;
				Label1.Visible = false;
				GridView1.DataSource = SearchTable;
				GridView1.DataBind();
			}
		}


		}
	}
