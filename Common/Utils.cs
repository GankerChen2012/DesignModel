using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Common
{
    public class Utils
    {

        public static string UrlPath(string name, string suffix)
        {
            var path =@"F:\1\image\"  + name + "." + suffix;
            return path;
        }
        /// <summary>
        /// 求正态值
        /// </summary>
        /// <param name="mean">期望</param>
        /// <param name="standardDev">标准差</param>
        /// <param name="x">密度</param>
        /// <returns></returns>
        public static double NormalDist(double mean, double standardDev, double x)
        {
            var fact = standardDev * 2.0 * Math.Sqrt(Math.PI);
            var expo = (x - mean) * (x - mean) / (2.0 * standardDev * standardDev);
            return Math.Exp(-expo) / fact;
        }

        /// <summary>
        /// 以列为分类名称 以行为分类数据
        /// 把表信息分解成生成图片所需要的值
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="dic">生成图所需数据格式</param>
        /// <param name="categoryDataName">分类信息值</param>
        public static void ListCategoryClassify(DataTable dt, out Dictionary<string, string> dic, out string categoryDataName)
        {
            var strDataName = "";
            dic = new Dictionary<string, string>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var value = "";
                var str = dt.Rows[i][0].ToString();
                for (var j = 1; j < dt.Columns.Count - 1; j++)
                {
                    value += dt.Rows[i][j] + "\t";
                }
                value += dt.Rows[i][dt.Columns.Count - 1];
                strDataName += str + "\t";
                dic.Add(str, value);
            }
            strDataName = strDataName.Substring(0, strDataName.Length - 2);
            categoryDataName = strDataName;
        }

        /// <summary>
        /// 以表头为分类名称 以行为分类数据
        /// 把表信息分解成生成图片所需要的值
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="dic">生成图所需数据格式</param>
        /// <param name="categoryDataName">分类信息值</param>
        public static void ColumnsCategoryClassify(DataTable dt, out Dictionary<string, string> dic, out string categoryDataName)
        {
            var strDataName = "";
            dic = new Dictionary<string, string>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var value = "";
                var str = dt.Rows[i][0].ToString();
                for (var j = 1; j < dt.Columns.Count - 1; j++)
                {
                    value += dt.Rows[i][j] + "\t";
                }
                value += dt.Rows[i][dt.Columns.Count - 1];
                dic.Add(str, value);
            }
            for (var j = 1; j < dt.Columns.Count - 1; j++)
            {
                strDataName += dt.Columns[j] + "\t";
            }
            strDataName += dt.Columns[dt.Columns.Count - 1];
            categoryDataName = strDataName;
        }


    }
}
