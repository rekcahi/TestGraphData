using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ChartBindTest
{
    /// <summary>
    /// Summary description for WebServiceChart
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebServiceChart : System.Web.Services.WebService
    {
        public class RevenueEntity
        {
            public int year { get; set; }
            public string month_name { get; set; }
            public int startWeekDays { get; set; }
            public int andWeekDays { get; set; }
            public string week { get; set; }
            public string day { get; set; }
            public int kw { get; set; }
            public int month_number { get; set; }
            public Boolean drilldown { get; set; }

            public int Indeks { get; set; }
            public int MeterID { get; set; }
            public string[] MeterDate { get; set; }
            public double[] AktifEnergyT { get; set; }

            public enum Month
            {
                NotSet = 0,
                //January = 1,
                //February = 2,
                //March = 3,
                //April = 4,
                //May = 5,
                //June = 6,
                //July = 7,
                //August = 8,
                //September = 9,
                //October = 10,
                //November = 11,
                //December = 12
                Ocak = 1,
                Şubat = 2,
                Mart = 3,
                Nisan = 4,
                Mayıs = 5,
                Haziran = 6,
                Temmuz = 7,
                Ağustos = 8,
                Eylül = 9,
                Ekim = 10,
                Kasım = 11,
                Aralık = 12
            }
        }
        [WebMethod]
        public List<RevenueEntity> GetRevenueByYear()
        {
            
            List<RevenueEntity> YearRevenues = new List<RevenueEntity>();
            DataSet ds = new DataSet();
            string connection = @"data source=.;initial catalog=TestDB;integrated security = True; User ID = sa; Password = 123";

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "select year,SUM(amount)amount from tblRevenue group by year";
                    cmd.CommandText = "SELECT month_name_data.month_name, month_name_data.kw,tbl_year.year FROM month_name_data INNER JOIN tbl_year ON month_name_data.yearId = tbl_year.yearId";
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "dsRevenue");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["dsRevenue"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["dsRevenue"].Rows)
                        {
                            YearRevenues.Add(new RevenueEntity
                            {
                                 year = Convert.ToInt32(dr["year"]),
                                 month_name = dr["month_name"].ToString(),//x value
                                 kw = Convert.ToInt32(dr["kw"]),//y value
                                drilldown = true
                            });
                        }
                    }
                }
            }
            return YearRevenues;
        }


        [WebMethod]
        public List<RevenueEntity> GetByYear()
        {
            int year_ = 0;
            int month = 0;
            List<RevenueEntity> YearData = new List<RevenueEntity>();
            double[] monthdata = new double[13];
            string[] monthname = {" ", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
            int a = 0;
            double aktifEnergy = 0;
            
            DataSet ds = new DataSet();
            string connection = @"data source=.;initial catalog=TestDB;integrated security = True; User ID = sa; Password = 123";

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    year_=int.Parse( DateTime.Now.ToString("yyyy"));
                    
                    //cmd.CommandText = "select year,SUM(amount)amount from tblRevenue group by year";
                    cmd.CommandText = "SELECT * from MeterData where MeterDate like '%"+ year_ + "%'";
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "dsMeter");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["dsMeter"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["dsMeter"].Rows)
                        {
                           month =int.Parse(dr["MeterDate"].ToString().Substring(3, 2));
                            
                            switch (month)
                            {
                                case 1:
                                    monthdata[1]+= Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 2:
                                     monthdata[2] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 3:
                                    monthdata[3] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 4:
                                    monthdata[4] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 5:
                                    monthdata[5] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 6:
                                    monthdata[6] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 7:
                                    monthdata[7] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 8:
                                    monthdata[8] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 9:
                                    monthdata[9] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 10:
                                    monthdata[11] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 11:
                                    monthdata[11] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 12:
                                    monthdata[12] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                           
                                default:
                                    Console.WriteLine("Invalid Month");
                                    break;
                            }
                            
                        }


                        YearData.Add(new RevenueEntity
                        {
                            year= year_,
                            MeterDate = monthname,
                            AktifEnergyT = monthdata,//x value
                                                
                            drilldown = true

                        });
                    }
                }
            }
            return YearData;
        }

        [WebMethod]
        public List<RevenueEntity> GetByYearParameter(int yearData)
        {
            int year_ = 0;
            int month = 0;
            List<RevenueEntity> YearData = new List<RevenueEntity>();
            double[] monthdata = new double[13];
            string[] monthname = { " ", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int a = 0;
            double aktifEnergy = 0;

            DataSet ds = new DataSet();
            string connection = @"data source=.;initial catalog=TestDB;integrated security = True; User ID = sa; Password = 123";

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //  year_ = int.Parse(DateTime.Now.ToString("yyyy"));
                    year_ = yearData;

                    //cmd.CommandText = "select year,SUM(amount)amount from tblRevenue group by year";
                    cmd.CommandText = "SELECT * from MeterData where MeterDate like '%" + year_ + "%'";
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "dsMeter");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["dsMeter"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["dsMeter"].Rows)
                        {
                            month = int.Parse(dr["MeterDate"].ToString().Substring(3, 2));

                            switch (month)
                            {
                                case 1:
                                    monthdata[1] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 2:
                                    monthdata[2] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 3:
                                    monthdata[3] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 4:
                                    monthdata[4] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 5:
                                    monthdata[5] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 6:
                                    monthdata[6] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 7:
                                    monthdata[7] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 8:
                                    monthdata[8] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 9:
                                    monthdata[9] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 10:
                                    monthdata[11] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 11:
                                    monthdata[11] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 12:
                                    monthdata[12] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;

                                default:
                                    Console.WriteLine("Invalid Month");
                                    break;
                            }

                        }


                        YearData.Add(new RevenueEntity
                        {
                            year = year_,
                            MeterDate = monthname,
                            AktifEnergyT = monthdata,//x value

                            drilldown = true

                        });
                    }
                }
            }
            return YearData;
        }

        [WebMethod]
        public List<RevenueEntity> GetMonthData()
        {
            List<RevenueEntity> QuarterRevenues = new List<RevenueEntity>();
            DataSet ds = new DataSet();
            int day = 0;
            int month = 0;
            int month__ = 0;
            string realMonthName = DateTime.Now.ToString("MMMM");
            int year__ = 0;
            
            string connection = @"data source=.;initial catalog=TestDB;integrated security = True; User ID = sa; Password = 123";
            year__ = int.Parse(DateTime.Now.ToString("yyyy"));
            month__ = int.Parse(DateTime.Now.ToString("MM"));
            //month__ = 2;


            int daysInMonth = DateTime.DaysInMonth(year__, month__);//bu yıldaki bu ayın kaç gün çektiği

            double[] daydata = new double[daysInMonth+1];
            string[] days = new string[daysInMonth+1];
            for (int i = 0; i <daydata.Length; i++)
            {
                days[i] = i.ToString();

            }
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                  //  SELECT* FROM Product WHERE Price BETWEEN 10 AND 20;
                    cmd.CommandText = "SELECT *  FROM  [dbo].[MeterData] WHERE " + month__ + "=SUBSTRING(MeterDate, 4, 2) and " + year__ + "=SUBSTRING(MeterDate,7,4)  ";
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "dsQuarter");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["dsQuarter"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["dsQuarter"].Rows)
                        {
                            
                            month = int.Parse(dr["MeterDate"].ToString().Substring(3, 2));

                            //if (month == month__)

                            //{
                                day = int.Parse(dr["MeterDate"].ToString().Substring(0, 2));

                                switch (day)
                                {
                                    case 1:
                                        daydata[1] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 2:
                                        daydata[2] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 3:
                                        daydata[3] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 4:
                                        daydata[4] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 5:
                                        daydata[5] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 6:
                                        daydata[6] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 7:
                                        daydata[7] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 8:
                                        daydata[8] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 9:
                                        daydata[9] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 10:
                                        daydata[10] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 11:
                                        daydata[11] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 12:
                                        daydata[12] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                  
                                    case 13:
                                        daydata[13] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 14:
                                        daydata[14] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 15:
                                        daydata[15] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 16:
                                        daydata[16] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 17:
                                        daydata[17] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 18:
                                        daydata[18] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 19:
                                        daydata[19] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 20:
                                        daydata[20] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 21:
                                        daydata[21] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 22:
                                        daydata[22] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 23:
                                        daydata[23] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 24:
                                        daydata[24] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 25:
                                        daydata[25] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 26:
                                        daydata[26] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 27:
                                        daydata[27] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 28:
                                        daydata[28] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 29:
                                        daydata[29] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 30:
                                        daydata[30] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                    case 31:
                                        daydata[31] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        break;
                                  
                                    default:
                                        Console.WriteLine("Invalid Day");
                                        break;
                                }
                         
                        }

                        QuarterRevenues.Add(new RevenueEntity
                        {
                            year = year__,
                            MeterDate = days,
                            AktifEnergyT = daydata,
                            month_name=realMonthName,
                            //x value
                                                     //Indeks = Convert.ToInt32(dr["Indeks"].ToString()),//y value
                                                     //MeterID=Convert.ToInt32(dr["MeterID"].ToString()),
                                                     //KapasitifEnergyRc=dr["KapasitifEnergyRc"].ToString()
                            drilldown = true

                        });
                    }
                }
            }
            return QuarterRevenues;
        }

        [WebMethod]
        public List<RevenueEntity> GetMonthDataParameter(int yearData,int monthData)
        {
            List<RevenueEntity> QuarterRevenues = new List<RevenueEntity>();
            DataSet ds = new DataSet();
            int day = 0;
            int month = 0;
            int month__ = 0;
            string realMonthName = DateTime.Now.ToString("MMMM");
            int year__ = 0;

            string connection = @"data source=.;initial catalog=TestDB;integrated security = True; User ID = sa; Password = 123";
            year__ = yearData;
            if (monthData == 0)
            {
                month__ = int.Parse(DateTime.Now.ToString("MM"));
            }
            else
                month__ = monthData;

            //month__ = 2;


            int daysInMonth = DateTime.DaysInMonth(year__, month__);//bu yıldaki bu ayın kaç gün çektiği

            double[] daydata = new double[daysInMonth + 1];
            string[] days = new string[daysInMonth + 1];
            for (int i = 0; i < daydata.Length; i++)
            {
                days[i] = i.ToString();

            }
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //  SELECT* FROM Product WHERE Price BETWEEN 10 AND 20;
                    cmd.CommandText = "SELECT *  FROM  [dbo].[MeterData] WHERE " + month__ + "=SUBSTRING(MeterDate, 4, 2) and " + year__ + "=SUBSTRING(MeterDate,7,4)  ";
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "dsQuarter");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["dsQuarter"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["dsQuarter"].Rows)
                        {

                            month = int.Parse(dr["MeterDate"].ToString().Substring(3, 2));

                            //if (month == month__)

                            //{
                            day = int.Parse(dr["MeterDate"].ToString().Substring(0, 2));

                            switch (day)
                            {
                                case 1:
                                    daydata[1] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 2:
                                    daydata[2] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 3:
                                    daydata[3] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 4:
                                    daydata[4] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 5:
                                    daydata[5] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 6:
                                    daydata[6] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 7:
                                    daydata[7] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 8:
                                    daydata[8] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 9:
                                    daydata[9] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 10:
                                    daydata[10] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 11:
                                    daydata[11] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 12:
                                    daydata[12] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;

                                case 13:
                                    daydata[13] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 14:
                                    daydata[14] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 15:
                                    daydata[15] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 16:
                                    daydata[16] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 17:
                                    daydata[17] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 18:
                                    daydata[18] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 19:
                                    daydata[19] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 20:
                                    daydata[20] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 21:
                                    daydata[21] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 22:
                                    daydata[22] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 23:
                                    daydata[23] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 24:
                                    daydata[24] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 25:
                                    daydata[25] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 26:
                                    daydata[26] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 27:
                                    daydata[27] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 28:
                                    daydata[28] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 29:
                                    daydata[29] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 30:
                                    daydata[30] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;
                                case 31:
                                    daydata[31] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                    break;

                                default:
                                    Console.WriteLine("Invalid Day");
                                    break;
                            }

                        }

                        QuarterRevenues.Add(new RevenueEntity
                        {
                            year = year__,
                            MeterDate = days,
                            AktifEnergyT = daydata,
                            month_name = realMonthName,
                            //x value
                            //Indeks = Convert.ToInt32(dr["Indeks"].ToString()),//y value
                            //MeterID=Convert.ToInt32(dr["MeterID"].ToString()),
                            //KapasitifEnergyRc=dr["KapasitifEnergyRc"].ToString()
                            drilldown = true

                        });
                    }
                }
            }
            return QuarterRevenues;
        }
        [WebMethod]
        public List<RevenueEntity> GetWeekData()
        {
            List<RevenueEntity> QuarterRevenues = new List<RevenueEntity>();
            DataSet ds = new DataSet();

            string[] dayname ={"","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday", "Sunday"};
            double[] weekdata = new double[8];
            string realMonthName = DateTime.Now.ToString("MMMM");

           
            List<int> listDay = new List<int>();
            string dayName = "";
            int year__ = 0;
            int month__ = 0;
            int day = 0;
            int dayFirst = 0;
            int dayLast = 0;
          
            year__ = int.Parse(DateTime.Now.ToString("yyyy"));
            month__ = int.Parse(DateTime.Now.ToString("MM"));
            dayFirst = int.Parse(DateTime.Now.ToString("dd"));
            dayLast = dayFirst+6;
            dayName = DateTime.Now.ToString("dddd");
            
            switch (dayName)
            {
                case "Pazartesi":
                    
                    break;
                case "Salı":
                    dayFirst -= 1;//7
                    dayLast -= 1;//13
                   
                    break;
                case "Çarşamba":
                    dayFirst -= 2;//7
                    dayLast -= 2;//13

                    break;
                case "Perşembe":
                    dayFirst -= 3;
                    dayLast -= 3;

                    break;
                case "Cuma":
                    dayFirst -= 4;
                    dayLast -= 4;
                    break;
                case "Cumartesi":
                    dayFirst -= 5;
                    dayLast -= 5;

                    break;
                case "Pazar":
                    dayFirst -= 6;
                    dayLast -= 6;

                    break;
                    
                default:
                    Console.WriteLine("Invalid Day");
                    break;
            }
            
            string connection = @"data source=.;initial catalog=TestDB;integrated security = True; User ID = sa; Password=123";

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT *  FROM  [dbo].[MeterData] WHERE " + month__ + "=SUBSTRING(MeterDate, 4, 2) and " + year__ + "=SUBSTRING(MeterDate,7,4) and " + dayFirst + "<=SUBSTRING(MeterDate,1,2) and SUBSTRING(MeterDate,1,2)<=" + dayLast + " ORDER BY SUBSTRING(MeterDate,1,2) ";

                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "dsQuarter");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["dsQuarter"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["dsQuarter"].Rows)
                        {

                            day = int.Parse(dr["MeterDate"].ToString().Substring(0, 2));
                            if (!(listDay.Contains(day)))
                            {
                                listDay.Add(day);
                                listDay.Sort();
                            }
                            int[] myArray = listDay.ToArray();

                            dayName = DateTime.Now.ToString("dddd");

                            for (int x = 0; x < myArray.Length; x++)
                            {
                                
                                switch (x)
                                {
                                    case 0:
                                        if (myArray.Length == 1)
                                        {
                                            double testae = Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                            weekdata[1] += Convert.ToDouble(dr["AktifEnergyT"].ToString());

                                        }
                                     
                                        break;
                                    case 1:
                                        if (myArray.Length == 2)
                                        {
                                            weekdata[2] += Convert.ToDouble(dr["AktifEnergyT"].ToString());
                                        }
                                          
                                        break;
                                    case 2:
                                        if (myArray.Length == 3)
                                        {
                                            weekdata[3] += Convert.ToDouble(dr["AktifEnergyT"].ToString());

                                        }

                                        break;
                                    case 3:
                                        if (myArray.Length == 4)
                                        {
                                            weekdata[4] += Convert.ToDouble(dr["AktifEnergyT"].ToString());

                                        }

                                        break;
                                    case 4:
                                        if (myArray.Length ==5)
                                        {
                                            weekdata[5] += Convert.ToDouble(dr["AktifEnergyT"].ToString());

                                        }

                                        break;
                                    case 5:
                                        if (myArray.Length == 6)
                                        {
                                            weekdata[6] += Convert.ToDouble(dr["AktifEnergyT"].ToString());

                                        }
                                        break;
                                    case 6:
                                        if (myArray.Length == 7)
                                        {
                                            weekdata[7] += Convert.ToDouble(dr["AktifEnergyT"].ToString());

                                        }

                                        break;
                                }
                            }
  
                        }

                        QuarterRevenues.Add(new RevenueEntity
                        {
                            startWeekDays=dayFirst,
                            andWeekDays=dayLast,
                            month_name=realMonthName,
                            year = year__,
                            MeterDate = dayname,
                            AktifEnergyT = weekdata,//x value
                                                   //Indeks = Convert.ToInt32(dr["Indeks"].ToString()),//y value
                                                   //MeterID=Convert.ToInt32(dr["MeterID"].ToString()),
                                                   //KapasitifEnergyRc=dr["KapasitifEnergyRc"].ToString()
                            drilldown = true

                        });
                    }
                }
            }
            return QuarterRevenues;
        }

        [WebMethod]
        public List<RevenueEntity> GetRevenueByQuarter(string year)
        {
            List<RevenueEntity> QuarterRevenues = new List<RevenueEntity>();
            DataSet ds = new DataSet();

            string connection = @"data source=.;initial catalog=TestDB;integrated security = True; User ID = sa; Password = 123";

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "select quarter,SUM(amount)amount from tblRevenue where year='" + year + "' group by quarter";
                    cmd.CommandText = "SELECT month_number_data.month_number, month_number_data.kw FROM month_name_data INNER JOIN month_number_data ON month_name_data.monthnameId = month_number_data.monthnameId";
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "dsQuarter");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["dsQuarter"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["dsQuarter"].Rows)
                        {
                            QuarterRevenues.Add(new RevenueEntity
                            {
                                month_number = Convert.ToInt32(dr["month_number"]),//x value
                                kw = Convert.ToInt32(dr["kw"]),//y value

                            });
                        }
                    }
                }
            }
            return QuarterRevenues;
        }
    }
}
