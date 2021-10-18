using ASPSearchCreateCsv.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ASPSearchCreateCsv
{
    public partial class SearchTextCsv : System.Web.UI.Page
    {
        string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        protected void Page_Load(object sender, EventArgs e)
        {

            TextBoxSearch.Attributes.Add("onkeypress", string.Format("return handleEnter('{0}', event)", ButtonSearch.ClientID));
            RecordCount();
        }

        void RecordCount()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domainName);
                var response = client.GetAsync("api/CSVDatas/Count").Result;
                if (response.IsSuccessStatusCode)
                {
                    LabelRecord.Text = "Records : " + response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        protected void ButtonInsertData_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && FileUpload1.PostedFile.ContentType.Equals("application/vnd.ms-excel"))
            {
                string savepath = Server.MapPath("~/uploads/");
                savepath += FileUpload1.FileName;
                FileUpload1.SaveAs(savepath);
                //Delete All Previous Uploaded CSV
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(domainName);
                    var response = client.GetAsync("api/CSVDatas/DeleteAll").Result;
                }
                //Bulk Insert New CSV Data
                CSVSaveToDB(domainName, savepath);
                LabelInfoUpload.Text = "File Loaded Completely";
                LabelInfoUpload.ForeColor = Color.YellowGreen;
                LabelInfoUpload.Visible = true;
            }
            else
            {
                LabelInfoUpload.Text = "Please Select Correct File Type";
                LabelInfoUpload.ForeColor = Color.Red;
                LabelInfoUpload.Visible = true;
            }
            RecordCount();
        }

        void CSVSaveToDB(string domain, string filePath)
        {
            List<CSVData> listcsv = new List<CSVData>();
            string[] fileContent = File.ReadAllLines(filePath);
            if (fileContent.Count() > 0)
            {

                //Add row data
                for (int i = 1; i < fileContent.Count(); i++)
                {
                    
                    CSVData csv = new CSVData();
                    string[] strarr = fileContent[i].Split(',');
                    csv.ID = Guid.Parse(strarr[0].Replace("\"", string.Empty));
                    csv.Content = strarr[1].Replace("\"", string.Empty);
                    csv.MatchedTimes = 0;
                    listcsv.Add(csv);

                    
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(domain);
                    var response = client.PostAsJsonAsync("api/CSVDatas", listcsv).Result;
                }

            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domainName);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/CSVDatas/Search/" + TextBoxSearch.Text).Result;
                if (response.IsSuccessStatusCode)
                {
                    var matchedcsv = response.Content.ReadAsStringAsync().Result;
                    var listcsv = JsonConvert.DeserializeObject<List<CSVData>>(matchedcsv);
                    GridView1.DataSource = listcsv;
                    GridView1.DataBind();
                }
            }

        }

        protected void ButtonGotoCreateCSV_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateCsv.aspx");
        }
    }


}