using System;
using System.IO;
using System.Linq;

namespace ASPSearchCreateCsv
{
    public partial class CreateCsv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        static Random random = new Random();
        static string GenerateRandomString(int minlength, int maxlength)
        {
            int length = random.Next(minlength, maxlength);
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 ".ToCharArray();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            StringWriter sw = new StringWriter();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Testing.csv");
            Response.ContentType = "text/csv";
            sw.WriteLine("\"ID\",\"Content\"");
            for (int i = 0; i < Convert.ToInt32(TextboxRecord.Text); i++)
            {
                string randomstring = GenerateRandomString(1000, 2000);
                //int bytecount = Encoding.Unicode.GetByteCount(randomstring);
                //int length = randomstring.Length;
                sw.WriteLine(string.Format("\"{0}\",\"{1}\"", Guid.NewGuid().ToString(), randomstring));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void ButtonGotoSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchTextCsv.aspx");
        }
    }
}