using System;
using System.Collections.Generic;
using System.Drawing;
using Common;
using Microsoft.Office.Interop.Owc11;

namespace ImageUtils
{
    public class ImageHelp
    {
        private const ChartDimensionsEnum DimCategories = ChartDimensionsEnum.chDimCategories;
        private const int DataLiteral = (int)ChartSpecialDataSourcesEnum.chDataLiteral;
        private const ChartDimensionsEnum DimValues = ChartDimensionsEnum.chDimValues;

        private string UrlPath(string name)
        {
            return Utils.UrlPath(name, "GIF");
        }

        /// <summary>
        /// 仪表--所在档次分析
        /// </summary>
        /// <param name="val">指针指示的分数</param>
        /// <returns></returns>
        public string YiBiao(int val)
        {
            var ag = new YiBiao.AGauge();
            var rectangle = new Rectangle(0, 0, 260, 112);
            var imgp = new YiBiao.ImagePaint
            {
                Value = val,
                Width = 260,
                Height = 112,
                BackColor = Color.White,
                ClientRectangle = rectangle,
            };
            var url = ag.YibiaoPic(imgp);
            return url;
        }

        /// <summary>
        /// 雷达图--学科均衡性分析
        /// </summary>
        /// <param name="strDataName">分类管理名字</param>
        /// <param name="data1">元数据</param>
        /// <param name="data2">比较数据</param>
        /// <returns></returns>
        public string ChartTypeRadarLine(string strDataName, string data1,string data2)
        {
            var mychartSpace = new ChartSpace();
            var mychart = mychartSpace.Charts.Add(0);
            mychart.Type = ChartChartTypeEnum.chChartTypeRadarLineFilled;
            mychart.HasTitle = false;
            
            var chart1 = mychart.SeriesCollection.Add(0);
            chart1.SetData(DimCategories, DataLiteral, strDataName);
            chart1.SetData(DimValues, DataLiteral, data2);
            chart1.Interior.Color = Color.Red;

            var chart2 =  mychart.SeriesCollection.Add(0);
            chart2.SetData(DimValues, DataLiteral, data1);
            chart2.Interior.Color = Color.Blue;

            var image = UrlPath("雷达");
            mychartSpace.ExportPicture(image, "GIF", 400, 300);
            return image;
        }

        //竖状柱状图+折线图
        public string ChartTypeColumnClustered4(string strDataName,Dictionary<string,string> list , string caption)
        {
            var mychartSpace = new ChartSpace();
            ChChart mychart = mychartSpace.Charts.Add(0);
            mychart.HasLegend = true;
            mychart.HasTitle = true;
            mychart.Title.Caption = caption;
            mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionBottom;
            mychart.Axes[1].NumberFormat = "0%";

            var dicList = new string[list.Keys.Count];
            var i = 0;
            foreach (var dic in list)
            {
                dicList[i] = dic.Value;
                i++;
            }
            var type = dicList[0].Split('\t');
            
            var clearList="";
            for (var j = 0; j < type.Length-1; j++)
            {
                clearList += 1 - Convert.ToSingle(type[j]) + "\t";
            }
            clearList += 1 - Convert.ToSingle(type[type.Length - 1]);

            var chart1 = mychart.SeriesCollection.Add(0);
            chart1.Type = ChartChartTypeEnum.chChartTypeColumnStacked100;
            chart1.SetData(DimCategories, DataLiteral, strDataName);
            chart1.SetData(DimValues, DataLiteral, clearList);
            chart1.Interior.Color = ChartColorIndexEnum.chColorNone;
            chart1.Border.Color = ChartColorIndexEnum.chColorNone;
            chart1.Caption = "";


            var chart3 = mychart.SeriesCollection.Add(0);
            chart3.SetData(DimValues, DataLiteral, dicList[0]);
            chart3.Caption = "你的得分率";

            var chart2 = mychart.SeriesCollection.Add(0);
            chart2.Type = ChartChartTypeEnum.chChartTypeLineStackedMarkers;
            chart2.SetData(DimValues, DataLiteral, dicList[1]);
            chart2.Caption = "同层面得分率";

            var image = UrlPath("柱状折线图");
            mychartSpace.ExportPicture(image, "GIF", 400, 300);
            return image;
        }

        //竖状柱状图--叠加--
        public string ChartTypeColumnClustered3(string strDataName, Dictionary<string, string> list, string caption)
        {
            var mychartSpace = new ChartSpace();
            var mychart = mychartSpace.Charts.Add(0);
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnStacked;
            mychart.HasLegend = true;
            mychart.HasTitle = true;
            mychart.Title.Caption = caption;
            mychart.Axes[1].NumberFormat = "0%";
            foreach (var dic in list)
            {
                var keys = mychart.SeriesCollection.Add(0);
                keys.SetData(DimValues, DataLiteral, dic.Value);
                //keys.DataLabelsCollection.Add().HasValue = true;
                keys.Caption = dic.Key;
                keys.DataLabelsCollection.Add().NumberFormat = "0.0%";
            }
            mychart.SeriesCollection[0].SetData(DimCategories, DataLiteral, strDataName);

            var image = UrlPath("柱状叠加图");
            mychartSpace.ExportPicture(image, "GIF", 400, 300);
            return image;
        }
        
        //竖状柱状图--叠加--100%Y柱(封顶)
        public string ChartTypeColumnClustered1(string strDataName, Dictionary<string, string> list,string title)
        {
            var mychartSpace = new ChartSpace();
            var mychart = mychartSpace.Charts.Add(0);
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnStacked100;
            mychart.HasLegend = true;
            mychart.HasTitle = true;
            mychart.Title.Caption = title;
            mychart.Axes[1].NumberFormat = "0%";
            foreach (var dic in list)
            {
                var keys =  mychart.SeriesCollection.Add(0);
                keys.SetData(DimValues, DataLiteral, dic.Value);
                //keys.DataLabelsCollection.Add().HasValue = true;
                keys.Caption = dic.Key;
                keys.DataLabelsCollection.Add().NumberFormat = "0.0%";
            }
            mychart.SeriesCollection[0].SetData(DimCategories, DataLiteral, strDataName);

            var image = UrlPath("柱状叠加封顶图");
            mychartSpace.ExportPicture(image, "GIF", 400, 300);
            return image;
        }

        //竖状柱状图+曲线图
        public string ChartTypeColumnClustered2(string strDataName, string data1, string data2, string data3, string data4, string caption)
        {
            var mychartSpace = new ChartSpace();
            var mychart = mychartSpace.Charts.Add(0);
            mychart.HasLegend = false;
            mychart.HasTitle = true;
            mychart.Title.Caption = caption;

            var chart1 = mychart.SeriesCollection.Add(0);
            chart1.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
            chart1.SetData(DimCategories,DataLiteral, strDataName);
            chart1.SetData(DimValues, DataLiteral, data1);
            chart1.DataLabelsCollection.Add().HasValue = true;
           
            var chSeries1 = mychart.SeriesCollection.Add(0);
            chSeries1.Type = ChartChartTypeEnum.chChartTypeSmoothLine;
            chSeries1.SetData(DimValues, DataLiteral, data2);

            var chSeries2 = mychart.SeriesCollection.Add(0);
            chSeries2.Type = ChartChartTypeEnum.chChartTypeSmoothLine;
            chSeries2.SetData(DimValues, DataLiteral, data3);

            var chSeries3 = mychart.SeriesCollection.Add(0);
            chSeries3.Type = ChartChartTypeEnum.chChartTypeSmoothLine;
            chSeries3.SetData(DimValues, DataLiteral, data4);

            var image = UrlPath("柱状曲线图");
            mychartSpace.ExportPicture(image, "GIF", 400, 300);
            return image;
        }

        //折线图--
        public string ChartTypeLineStackedMarkers(string strDataName, Dictionary<string, string> list, string caption)
        {
            var mychartSpace = new ChartSpace();
            var mychart = mychartSpace.Charts.Add(0);
            mychart.Type = ChartChartTypeEnum.chChartTypeLineStackedMarkers;
            //mychart.Axes[1].Line.Color = ChartColorIndexEnum.chColorNone;
            mychart.HasLegend = true;
            mychart.HasTitle = true;
            mychart.Title.Caption = caption;
            mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionLeft;

            string[] color = {"Red", "Blue", "Green", "Yellow"};

            var i = 0;
            foreach (var dic in list)
            {
                var keys = mychart.SeriesCollection.Add(0);
                keys.SetData(DimValues, DataLiteral, dic.Value);
                //keys.DataLabelsCollection.Add().HasValue = true;
                keys.Caption = dic.Key;
                //keys.Marker.Style = ChartMarkerStyleEnum.chMarkerStyleCircle;
                keys.Line.Color = color[i];
                keys.Ungroup(true);
                i++;
            }
            mychart.SeriesCollection[0].SetData(DimCategories, DataLiteral, strDataName);

            var image = UrlPath("折线图");
            mychartSpace.ExportPicture(image, "GIF", 800, 200);
            return image;
        }





    }
}
