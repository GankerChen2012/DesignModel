using System.Collections.Generic;
using System.Data;
using Common;

namespace Database
{
    public class StudentData
    {
        Dictionary<string, string> dic;
        string category;

        public void Value()
        {
            StOneExplain = 12;
            Total = 321;

            ALevel = "300-400";
            BLevel = "400-500";
            CLevel = "500-600";
            DLevel = "600-700";

            Utils.ListCategoryClassify(SectionFiveDt(), out dic, out category);
            SfFiveDicData = dic;
            SfFiveCategoryDataName = category;

            SfExplain = "哇哈哈@这句是红色@哇哈哈 \n";
            SfExplain += "哇哈哈@#这句是蓝色#@哇哈哈";

            SseExplain = "这一整段都是红色，@这句是加粗了的@，@#当然也可以有蓝色#@。";
            SseSuggest = "这个是补充说明。";

        }

        //SectionOne
        public int StOneExplain { get; set; }

        //SectionTwo
        public DataTable SectionTwoDt()
        {
            var dt = new DataTable();
            dt.Columns.Add("");
            dt.Columns.Add("title1");
            dt.Columns.Add("title2");
            dt.Columns.Add("title3");
            dt.Columns.Add("title4");
            dt.Columns.Add("title5");
            dt.Columns.Add("title6");
            dt.Columns.Add("title7");


            string[] list = { "test1", "750", "150", "150", "150", "100", "100", "100" };
            string[] list1 = { "test2", "632.5", "121", "139", "130.5", "88", "93", "85" };
            string[] list2 = { "test3", "698", "132", "148", "142", "99", "100", "95" };
            string[] list3 = { "test4", "511.19", "102.98", "108.52", "105.54", "46.76", "74.71", "72.67 " };
            string[] list4 = { "test5", "506.5", "95.77", "109.71", "106.77", "49.21", "74.25", "68.79" };
            string[] list5 = { "test6", "605.5", "115", "139", "116.5", "62", "93", "80" };
            string[] list6 = { "test7", "2", "3", "1", "7", "6", "2", "9" };
            string[] list7 = { "test8", "↑1", "↑1", "↑2", "↓3", "↑4", "↑6", "↓4" };
            string[] list8 = { "test9", "14", "16", "5", "27", "56", "6", "38 " };
            string[] list9 = { "test10", "↑3", "↑2", "↑1", "↓9", "↑6", "↑10", "↓7" };
            dt.Rows.Add(list);
            dt.Rows.Add(list1);
            dt.Rows.Add(list2);
            dt.Rows.Add(list3);
            dt.Rows.Add(list4);
            dt.Rows.Add(list5);
            dt.Rows.Add(list6);
            dt.Rows.Add(list7);
            dt.Rows.Add(list8);
            dt.Rows.Add(list9);
            return dt;
        }

        //SectionThree
        public string StExplain { get; set; }
        public int Total { get; set; }
        public string ALevel { get; set; }
        public string BLevel { get; set; }
        public string CLevel { get; set; }
        public string DLevel { get; set; }


        //SectionFour
        public string SfExplain { get; set; }
        public DataTable SectionFourDt()
        {
            var dt = new DataTable();
            dt.Columns.Add(" ");
            dt.Columns.Add("title1");
            dt.Columns.Add("title2");
            dt.Columns.Add("title3");
            dt.Columns.Add("title4");
            dt.Columns.Add("title5");
            dt.Columns.Add("title6");

            string[] list = { "test1", "115", "139", "116.5", "62", "93", "80" };
            string[] list1 = { "test2", "108", "127.76", "120.68", "76.5", "86.68", "80.6" };
            string[] list2 = { "test3", "9.98", "10.36", "11.2", "12.2", "10.8", "12.6" };
            string[] list3 = { "test4", "0.70", "1.08", "-0.37", "-1.19", "0.59", "-0.05 " };
            dt.Rows.Add(list);
            dt.Rows.Add(list1);
            dt.Rows.Add(list2);
            dt.Rows.Add(list3);

            return dt;
        }

        //SectionFive
        public Dictionary<string, string> SfFiveDicData { get; set; }
        public string SfFiveCategoryDataName { get; set; }
        public string SfiveExplain { get; set; }
        public DataTable SectionFiveDt()
        {
            var dt = new DataTable();
            dt.Columns.Add(" ");
            dt.Columns.Add("T1");
            dt.Columns.Add("T2");
            dt.Columns.Add("T3");
            dt.Columns.Add("T4");
            dt.Columns.Add("T5");
            dt.Columns.Add("T6");
            dt.Columns.Add("T7");
            dt.Columns.Add("T8");
            dt.Columns.Add("T9");

            string[] list = { "C1", "150", "80", "90", "110", "120", "130", "110", "120", "130" };
            string[] list1 = { "C2", "100", "100", "120", "110", "130", "140", "110", "120", "130" };
            string[] list2 = { "C3", "9.98", "10.36", "11.2", "12.2", "10.8", "12.6", "110", "120", "130" };
            dt.Rows.Add(list);
            dt.Rows.Add(list1);
            dt.Rows.Add(list2);

            return dt;
        }

        //SectionSix
        public string SsExplain { get; set; }
        public int CanAboveNum { get; set; }
        public string AboveSubject { get; set; }
        public string WeaknessSubject { get; set; }
        public string GoodnessSubject { get; set; }
        public DataTable SectionSixDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("");
            dt.Columns.Add("T1");
            dt.Columns.Add("T2");
            dt.Columns.Add("T3");
            dt.Columns.Add("T4");
            dt.Columns.Add("T5");
            dt.Columns.Add("T6");
            dt.Columns.Add("T7");

            string[] list = { "C1", "605.5", "115", "139", "116.5", "62", "93", "80" };
            string[] list1 = { "C2", "632.6", "116", "137", "125.5", "78.6", "91.4", "84.1" };
            string[] list2 = { "C3", "+17", "-", "+5", "+9", "-", "+2", "+2" };
            dt.Rows.Add(list);
            dt.Rows.Add(list1);
            dt.Rows.Add(list2);

            return dt;
        }

        //SectionSeven
        public string BestSubject { get; set; }
        public string SseExplain { get; set; }
        public string SseSuggest { get; set; }
    }



}
