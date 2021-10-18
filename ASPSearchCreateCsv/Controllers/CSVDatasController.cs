using ASPSearchCreateCsv.Data;
using ASPSearchCreateCsv.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ASPSearchCreateCsv.Controllers
{
    public class CSVDatasController : ApiController
    {
        private ASPSearchCreateCsvContext context = new ASPSearchCreateCsvContext();


        // GET: api/CSVDatas/Count
        [Route("api/CSVDatas/Count")]
        [HttpGet]
        public int CountRecordCsvDatas()
        {
            int countrecord = context.CSVDatas.Count();
            return countrecord;
        }

        // GET: api/CSVDatas/DeleteAll
        [Route("api/CSVDatas/DeleteAll")]
        [HttpGet]
        public HttpResponseMessage DeleteAllCSVDatas()
        {
            var executedeleteall = context.Database.ExecuteSqlCommand("DELETE dbo.CSVDatas");

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        // GET: api/CSVDatas
        public IQueryable<CSVData> GetCSVDatas()
        {
            return context.CSVDatas;
        }

        // GET: api/CSVDatas/sadfsadf
        [Route("api/CSVDatas/Search/{searchkeyword}")]
        [HttpGet]
        //[ResponseType(typeof(List<CSVData>))]
        public List<CSVData> SearchCSVDatas(string searchkeyword)
        {
            //var getdata = context.CSVDatas.Where(x => x.Content.Contains(searchkeyword)).ToList();
            //if(getdata.Count > 0)
            //{
            //    //disable detection of changes to improve performance
            //    context.Configuration.AutoDetectChangesEnabled = false;

            //    foreach (var item in getdata)
            //    {
            //        item.MatchedTimes += 1;
            //        context.CSVDatas.Add(item);
            //        context.Entry(item).State = EntityState.Modified;

            //    }
            //    context.SaveChanges();
            //    context.Configuration.AutoDetectChangesEnabled = true;



            //    return getdata;
            //}
            //else
            //{
            //    return new List<CSVData>();
            //}

            List<CSVData> listcsv = new List<CSVData>();
            var getalldata = context.CSVDatas.ToList();
            if (getalldata.Count > 0)
            {
                //    //disable detection of changes to improve performance
                context.Configuration.AutoDetectChangesEnabled = false;
                char[] charArr = searchkeyword.ToCharArray();

                for (int i = 0; i < getalldata.Count; i++)
                {
                    char[] charContent = getalldata[i].Content.ToCharArray();
                    for (int j = 0; j < charContent.Length; j++)
                    {
                        for (int k = 0; k < charArr.Length; k++)
                        {
                            if(j + k >= charContent.Length)
                            {
                                break;
                            }

                            if(charArr[k] != charContent[j+k])
                            {
                                break;
                            }

                            if(k == charArr.Length - 1)
                            {
                                listcsv.Add(getalldata[i]);
                                //Matched
                                getalldata[i].MatchedTimes += 1;
                                context.CSVDatas.Add(getalldata[i]);
                                context.Entry(getalldata[i]).State = EntityState.Modified;
                            }
                        }
                    }
                }
                context.SaveChanges();
                context.Configuration.AutoDetectChangesEnabled = true;



                return listcsv;
            }
            else
            {
                return new List<CSVData>();
            }


        }

        // PUT: api/CSVDatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCSVData(Guid id, CSVData cSVData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cSVData.ID)
            {
                return BadRequest();
            }

            context.Entry(cSVData).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CSVDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CSVDatas
        [ResponseType(typeof(List<CSVData>))]
        public IHttpActionResult PostCSVData(List<CSVData> listcsv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Guid));
            dt.Columns.Add("Content", typeof(String));
            dt.Columns.Add("MatchedTimes", typeof(int));
            foreach (var csv in listcsv)
            {
                string[] itemcsv = { csv.ID.ToString(), csv.Content.ToString(), csv.MatchedTimes.ToString() };
                DataRow row = dt.NewRow();
                row.ItemArray = itemcsv;
                dt.Rows.Add(row);
            }
            using (SqlConnection cn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=ASPSearchCreateCsvContext-20211017141048; Integrated Security=True; MultipleActiveResultSets=True; AttachDbFilename=|DataDirectory|ASPSearchCreateCsvContext-20211017141048.mdf"))
            {
                cn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                {
                    copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    copy.ColumnMappings.Add(2, 2);
                    copy.DestinationTableName = "CSVDatas";
                    copy.WriteToServer(dt);
                }
            }

            return CreatedAtRoute("DefaultApi", new List<CSVData>(), listcsv);

        }

        // DELETE: api/CSVDatas/5
        [ResponseType(typeof(CSVData))]
        public IHttpActionResult DeleteCSVData(Guid id)
        {
            CSVData cSVData = context.CSVDatas.Find(id);
            if (cSVData == null)
            {
                return NotFound();
            }

            context.CSVDatas.Remove(cSVData);
            context.SaveChanges();

            return Ok(cSVData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CSVDataExists(Guid id)
        {
            return context.CSVDatas.Count(e => e.ID == id) > 0;
        }
    }
}