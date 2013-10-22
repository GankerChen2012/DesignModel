using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Common;
using Microsoft.Office.Interop.Owc11;

namespace ImageUtils
{
    public class ImageBackup
    {

        private string UrlPath(string name)
        {
            return Utils.UrlPath(name, "GIF");
        }
        //饼图
        public string ChartTypePie()
        {
            //创建X坐标的值，表示月份 
            int[] Month = { 1, 2, 3, 4, 5, 6 };
            //创建Y坐标的值，表示销售额 
            double[] Count = { 120, 240, 220, 343, 32, 54 };
            string strDataName = "";
            string strData = "";

            //创建图表空间 
            var mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            var mychart = mychartSpace.Charts.Add(0);

            //设置每块饼的数据 
            for (int i = 0; i < Count.Length - 1; i++)
            {
                strDataName += Month[i] + "\t";
                strData += Count[i].ToString() + "\t";
            }
            strDataName += Month[Count.Length - 1];
            strData += Count[Count.Length - 1].ToString();

            //设置图表类型，本例使用饼图
            mychart.Type = ChartChartTypeEnum.chChartTypePie;
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //添加图表块 
            mychart.SeriesCollection.Add(0);
            //分类属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, strDataName);
            //值属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, strData);

            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "200\t100\t343\t270\t235\t343");


            //给第一个扇区设置自定义背景图片
            mychart.SeriesCollection[0].Points[0].Interior.SetTextured("E:\\Projects\\DotNetNuke\\images\\add.gif", ChartTextureFormatEnum.chTile, 1, ChartTexturePlacementEnum.chFrontSides);
            //给第二个扇区设置从中心向四周辐射的单色渐变
            mychart.SeriesCollection[0].Points[1].Interior.SetOneColorGradient(ChartGradientStyleEnum.chGradientFromCenter, ChartGradientVariantEnum.chGradientVariantStart, 0.3, "Blue");
            //给第三个扇区设置倾斜双色渐变
            mychart.SeriesCollection[0].Points[2].Interior.SetTwoColorGradient(ChartGradientStyleEnum.chGradientDiagonalDown, ChartGradientVariantEnum.chGradientVariantCenter, "Green", "Red");
            //给第四个扇区设置OWC预设的纹理，并设置纹理的背景色为淡绿色，前景色为红色
            //OWC提供了很多纹理，这是其中一种
            //具体的纹理样式可以参看帮助中的ChartPatternTypeEnum枚举
            mychart.SeriesCollection[0].Points[3].Interior.SetPatterned(ChartPatternTypeEnum.chPatternDiagonalBrick, "Red", "LightGreen");
            //给第五个扇区设置OWC预设的倾斜，
            //OWC提供了许多种的倾斜，这是其中一种
            //具体的倾斜可以参看帮助中的ChartPresetGradientTypeEnum枚举
            mychart.SeriesCollection[0].Points[4].Interior.SetPresetGradient(ChartGradientStyleEnum.chGradientDiagonalUp, ChartGradientVariantEnum.chGradientVariantEdges, ChartPresetGradientTypeEnum.chGradientGoldII);


            //显示百分比 
            ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            mytb.HasPercentage = true;

            var image = UrlPath("饼图");
            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);
            return image;
        }

        /// <summary>
        /// 雷达图--学科均衡性分析
        /// </summary>
        /// <param name="strDataName">分类管理名字</param>
        /// <param name="data1">元数据</param>
        /// <param name="data2">比较数据</param>
        /// <returns></returns>
        public string ChartTypeRadarLine(string strDataName, string data1, string data2)
        {
            //创建图表空间 
            var mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            var mychart = mychartSpace.Charts.Add(0);
            //设置图表类型，本例使用雷达图
            mychart.Type = ChartChartTypeEnum.chChartTypeRadarLineFilled;
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //添加图表块 
            var chart1 = mychart.SeriesCollection.Add(0);
            //分类属性 
            chart1.SetData(ChartDimensionsEnum.chDimCategories,
                           (int)ChartSpecialDataSourcesEnum.chDataLiteral, strDataName);
            //值属性 
            chart1.SetData(ChartDimensionsEnum.chDimValues,
                           (int)ChartSpecialDataSourcesEnum.chDataLiteral, data1);

            chart1.Interior.Color = "red";


            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, data2);

            //显示百分比 
            ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasPercentage = true;


            var image = UrlPath("雷达");

            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;
        }

        //竖状柱状图--优势科目、弱势科目分析
        public string ChartTypeColumnClustered()
        {
            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);
            //设置图表类型，本例使用柱状图
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;

            //设置图表的一些属性 
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //添加图表块 
            mychart.SeriesCollection.Add(0);
            //设置图例位置为底端
            mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionTop;

            //设置图表块的属性 
            //分类属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "语文\t数学\t英语\t物理\t化学\t生物");
            //值属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "100\t120\t143\t170\t135\t143");

            ChSeries chSeries = mychart.SeriesCollection.Add(0);
            chSeries.Type = ChartChartTypeEnum.chChartTypeLineMarkers;
            chSeries.SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                             "100\t120\t90\t120\t110\t132");
            //chSeries.Interior.SetPresetGradient(ChartGradientStyleEnum.chGradientFromCenter,
            //    ChartGradientVariantEnum.chGradientVariantCenter,ChartPresetGradientTypeEnum.chGradientChrome);

            chSeries = mychart.SeriesCollection.Add(0);
            chSeries.Type = ChartChartTypeEnum.chChartTypeLineMarkers;
            chSeries.SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                             "120\t150\t70\t150\t120\t112");

            chSeries = mychart.SeriesCollection.Add(0);
            chSeries.Type = ChartChartTypeEnum.chChartTypeLineMarkers;
            chSeries.SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                             "150\t130\t40\t150\t130\t132");

            //显示百分比 
            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasPercentage = true;

            var image = UrlPath("柱状图");

            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;
        }

        //竖状柱状图--学科分布与特点分析
        public string ChartTypeColumnClustered2()
        {
            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);

            //设置图表类型，本例使用柱状图
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
            //设置图表的一些属性 
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //添加图表块 
            mychart.SeriesCollection.Add(0);
            //设置图表块的属性 
            //分类属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, "80\t90\t100\t110\t120\t130");
            //值属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, "0\t2\t4\t6\t8\t4");

            ChSeries chSeries = mychart.SeriesCollection.Add(0);
            chSeries.Type = ChartChartTypeEnum.chChartTypeSmoothLine;
            //chSeries.Ungroup(true);

            ChAxis chAxis = mychart.Axes.Add(chSeries.Scalings[ChartDimensionsEnum.chDimValues]);
            chAxis.Position = ChartAxisPositionEnum.chAxisPositionRight;

            //给定系列的值
            chSeries.SetData
                (ChartDimensionsEnum.chDimValues,
                 (int)ChartSpecialDataSourcesEnum.chDataLiteral, "0\t2\t4\t6\t4\t2");

            //设置曲线的粗细,锯齿
            chSeries.Line.Miter = ChartLineMiterEnum.chLineMiterRound;
            chSeries.Line.DashStyle = ChartLineDashStyleEnum.chLineSolid;
            //chSeries.Line.set_Weight(LineWeightEnum.owcLineWeightHairline);


            //chSeries = mychart.SeriesCollection.Add(0);
            //chSeries.Type = ChartChartTypeEnum.chChartTypeLineMarkers;
            //chSeries.Ungroup(true);

            //chAxis = mychart.Axes.Add(chSeries.Scalings[ChartDimensionsEnum.chDimValues]);
            //chAxis.Position = ChartAxisPositionEnum.chAxisPositionRight;

            ////给定系列的值
            //chSeries.SetData
            //    (ChartDimensionsEnum.chDimValues,
            //     (int)ChartSpecialDataSourcesEnum.chDataLiteral, "100\t220\t90\t150\t323\t434");


            //显示百分比 
            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasPercentage = true;

            var image = UrlPath("柱状图");

            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;
        }

        //竖状柱状图--四率分析
        public string ChartTypeColumnClustered3()
        {

            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);

            //设置图表类型，本例使用柱状图
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnStacked100;
            //设置图表的一些属性 
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //添加图表块 
            mychart.SeriesCollection.Add(0);
            //设置图表块的属性 
            const ChartDimensionsEnum category = ChartDimensionsEnum.chDimValues;
            const int dataSources = (int)ChartSpecialDataSourcesEnum.chDataLiteral;

            //分类属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "极差率");
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories, dataSources,
                                                "总分\t语文\t数学\t英语\t物理\t化学\t生物");
            //值属性 
            mychart.SeriesCollection[0].SetData(category, dataSources, "6%\t6%\t4%\t4%\t0%\t8%\t2%");
            mychart.SeriesCollection[0].Ungroup(true);

            var chart2 = mychart.SeriesCollection.Add(0);
            chart2.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "优秀率");
            chart2.SetData(category, dataSources, "16%\t66%\t45%\t4%\t3%\t8%\t2%");
            //chart2.Ungroup(true);

            var chart3 = mychart.SeriesCollection.Add(0);
            chart3.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "良好率");
            chart3.SetData(category, dataSources, "26%\t62%\t43%\t4%\t10%\t8%\t2%");
            //chart3.Ungroup(true);

            var chart4 = mychart.SeriesCollection.Add(0);
            chart4.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "及格率");
            chart4.SetData(category, dataSources, "46%\t46%\t42%\t4%\t40%\t8%\t2%");
            //chart4.Ungroup(true);

            //显示值
            ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            mytb.HasValue = true;
            mytb = mychart.SeriesCollection[1].DataLabelsCollection.Add();
            mytb.HasValue = true;
            mytb = mychart.SeriesCollection[2].DataLabelsCollection.Add();
            mytb.HasValue = true;
            mytb = mychart.SeriesCollection[3].DataLabelsCollection.Add();
            mytb.HasValue = true;

            var image = UrlPath("柱状图");
            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;

        }

        //竖状柱状图--各分数段人数比例分析
        public string ChartTypeColumnClustered4()
        {
            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);

            //设置图表类型，本例使用柱状图
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
            //设置图表的一些属性 
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //添加图表块 
            mychart.SeriesCollection.Add(0);
            //设置图表块的属性 
            //分类属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, "1\t2\t3\t4\t5\t6\t7");
            //值属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, "200\t100\t343\t270");

            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "200\t100\t343\t270");
            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "200\t100\t343\t270");
            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "200\t100\t343\t270");

            //ChSeries chSeries = mychart.SeriesCollection.Add(0);
            //chSeries.Type = ChartChartTypeEnum.chChartTypeSmoothLine;
            //chSeries.Ungroup(true);

            //ChAxis chAxis = mychart.Axes.Add(chSeries.Scalings[ChartDimensionsEnum.chDimValues]);
            //chAxis.Position = ChartAxisPositionEnum.chAxisPositionRight;

            ////给定系列的值
            //chSeries.SetData
            //    (ChartDimensionsEnum.chDimValues,
            //     (int)ChartSpecialDataSourcesEnum.chDataLiteral, "0\t21\t0\t0\t");

            //chSeries = mychart.SeriesCollection.Add(0);
            //chSeries.Type = ChartChartTypeEnum.chChartTypeLineMarkers;
            //chSeries.Ungroup(true);

            //chAxis = mychart.Axes.Add(chSeries.Scalings[ChartDimensionsEnum.chDimValues]);
            //chAxis.Position = ChartAxisPositionEnum.chAxisPositionRight;

            ////给定系列的值
            //chSeries.SetData
            //    (ChartDimensionsEnum.chDimValues,
            //     (int)ChartSpecialDataSourcesEnum.chDataLiteral, "100\t220\t90\t150\t323\t434");


            //显示百分比 
            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasPercentage = true;

            var image = UrlPath("柱状图");
            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;
        }

        //横状柱状图
        public string ChartTypeBarClustered()
        {
            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);


            //设置图表的一些属性 
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            mychart.Axes[1].NumberFormat = "0%";//显示%

            //设置图例位置为底端
            mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionBottom;
            //添加图表块 
            var chart1 = mychart.SeriesCollection.Add(0);
            //设置图表块的属性 
            chart1.Type = ChartChartTypeEnum.chChartTypeBarClustered;
            //分类属性 
            chart1.SetData(ChartDimensionsEnum.chDimCategories,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, "解答题\t填空题\t选择题");
            //值属性 
            chart1.SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, "0.7\t0.26\t0.48");
            //chart1.Interior.Color = Color.Red;

            //var chart2 = mychart.SeriesCollection.Add(0);
            //chart2.Type = ChartChartTypeEnum.chChartTypeScatterLine;
            //chart2.SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral, "0.82\t0.14\t0.27");


            //var chart2 = mychart.SeriesCollection.Add(0);
            //////chart2.Line.Color = ChartColorIndexEnum.chColorNone;
            //chart2.Type = ChartChartTypeEnum.chChartTypeLineMarkers;
            //chart1.SetData(ChartDimensionsEnum.chDimCategories, (int)ChartSpecialDataSourcesEnum.chDataLiteral, "0.82\t0.14\t0.27");
            //chart2.SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral, "解答题\t填空题\t选择题");
            //chart2.Marker.Style = ChartMarkerStyleEnum.chMarkerStyleStar;
            //chart2.Caption = "";
            ////chart2.Interior.Color = Color.Red;

            //var trend = chart2.Trendlines.Add();
            //trend.Type = ChartTrendlineTypeEnum.chTrendlineTypeLinear;
            //trend.Caption = "ds";
            //trend.IsDisplayingEquation = false;
            //trend.IsDisplayingRSquared = false;
            ////主次网格线不显示
            //chChart.Axes[1].HasMinorGridlines = false;
            //chChart.Axes[1].HasMajorGridlines = false;
            ////设定Y轴格式
            //chChart.Axes[1].NumberFormat = "0%";
            ////去掉线
            //chChart.SeriesCollection[3].Line.Color = ChartColorIndexEnum.chColorNone;
            ////标记形状
            //chChart.SeriesCollection[3].Marker.Style = ChartMarkerStyleEnum.chMarkerStyleDiamond;
            //显示百分比
            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasPercentage = true;

            var name = "横坐标柱状图" + DateTime.Now.Ticks;
            var image = UrlPath(name);
            //生成图片
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;
        }

        //折线图--成绩波动分析
        public string ChartTypeLineStackedMarkers(string name)
        {
            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);
            //设置图表类型
            mychart.Type = ChartChartTypeEnum.chChartTypeLineStackedMarkers;

            mychart.Axes[1].Line.Color = ChartColorIndexEnum.chColorNone;

            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = name;
            //设置图例位置
            mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionLeft;
            //添加图表块 
            //分类属性 
            const ChartDimensionsEnum category = ChartDimensionsEnum.chDimValues;
            const int dataSources = (int)ChartSpecialDataSourcesEnum.chDataLiteral;

            var chart1 = mychart.SeriesCollection.Add(0);
            //X柱
            chart1.SetData(ChartDimensionsEnum.chDimCategories, dataSources, "T1\tT2\tT3\tT4\tT5\tT6\tT7\tT8\t本次考试");
            //分类名字
            //chart1.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "总分");
            //赋值
            chart1.SetData(category, dataSources, "-0.1\t-0.8\t-0.1\t0.3\t0.3\t1\t1.5\t1.1\t-0.6");
            chart1.Ungroup(true);
            chart1.Caption = name;

            var chart2 = mychart.SeriesCollection.Add(0);
            chart2.Caption = "平均分";
            chart2.SetData(category, dataSources, "-0.2\t-0.7\t-0.1\t0.3\t0.4\t1\t1.1\t1.1\t-0.6");
            chart2.Ungroup(true);

            var chart4 = mychart.SeriesCollection.Add(0);
            chart4.SetData(category, dataSources, "-0.4\t-0.5\t-1.0\t0.3\t0.6\t1\t-1.1\t1.1\t-0.6");
            chart4.Ungroup(true);
            chart4.Caption = "三次平均";

            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasBubbleSize = false;
            //显示百分比 
            //mytb.HasPercentage = true;

            var path = "折线图" + DateTime.Now.Ticks;
            var image = UrlPath(path);
            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 800, 200);

            return image;
        }

        //折线图2--成绩变化趋势分析
        public string ChartTypeLineStackedMarkers2()
        {

            //创建X坐标的值，表示月份 
            int[] Month = { 1, 2, 3 };
            //创建Y坐标的值，表示销售额 
            double[] Count = { 120, 240, 220 };
            string strDataName = "";
            string strData = "";

            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);

            //设置每块饼的数据 
            for (int i = 0; i < Count.Length - 1; i++)
            {
                strDataName += Month[i] + "\t";
                strData += Count[i].ToString() + "\t";
            }
            strDataName += Month[Count.Length - 1];
            strData += Count[Count.Length - 1].ToString();



            //设置图表类型，本例使用柱状图
            mychart.Type = ChartChartTypeEnum.chChartTypeLineStackedMarkers;
            //设置图表的一些属性 
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";

            //设置图例位置为底端
            mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionBottom;
            //添加图表块 
            mychart.SeriesCollection.Add(0);

            //设置图表块的属性 
            //分类属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, strDataName);
            //值属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral, strData);

            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "200\t120\t343");
            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "100\t200\t143");
            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "300\t300\t243");
            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "400\t180\t303");
            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "230\t230\t313");
            mychart.SeriesCollection.Add(0);
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                                                (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                                                "290\t200\t349");

            //显示百分比 
            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasPercentage = true;

            var image = UrlPath("折线图");
            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;

        }

        //仪表--所在档次分析
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
            string url = ag.YibiaoPic(imgp);

            return url;
        }

        //竖状柱状图--上线率分析
        public string ChartTypeColumnClustered5()
        {
            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);

            //设置图表类型，本例使用柱状图
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnStacked;
            //设置图表的一些属性 
            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //添加图表块 
            mychart.SeriesCollection.Add(0);
            //设置图表块的属性 
            const ChartDimensionsEnum category = ChartDimensionsEnum.chDimValues;
            const int dataSources = (int)ChartSpecialDataSourcesEnum.chDataLiteral;

            //分类属性 
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "本科线");
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories, dataSources,
                                                "总分\t语文\t数学\t英语\t物理\t化学\t生物");
            //值属性 
            mychart.SeriesCollection[0].SetData(category, dataSources, "6%\t6%\t4%\t4%\t0%\t8%\t2%");
            mychart.SeriesCollection[0].Ungroup(true);

            var chart2 = mychart.SeriesCollection.Add(0);
            chart2.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "一本线");
            chart2.SetData(category, dataSources, "16%\t66%\t45%\t4%\t3%\t8%\t2%");
            //chart2.Ungroup(true);

            //显示值
            ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            mytb.HasValue = true;
            mytb = mychart.SeriesCollection[1].DataLabelsCollection.Add();
            mytb.HasValue = true;

            var image = UrlPath("柱状图");
            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 400, 300);

            return image;

        }

        public void MainTemplete()
        {
            //创建图表空间 
            ChartSpace mychartSpace = new ChartSpace();
            //在图表空间内添加一个图表对象 
            ChChart mychart = mychartSpace.Charts.Add(0);
            //设置图表类型
            mychart.Type = ChartChartTypeEnum.chChartTypeLineStackedMarkers;

            //旋转
            //mychart.Rotation = 360;
            //mychart.Inclination = 10;
            //背景颜色
            //mychart.PlotArea.Interior.Color = "red";
            //底色
            //mychart.PlotArea.Floor.Interior.Color = "green";
            //mychart.Overlap = 50;

            //是否需要图例 
            mychart.HasLegend = true;
            //是否需要主题 
            mychart.HasTitle = true;
            //主题内容 
            mychart.Title.Caption = "test";
            //设置图例位置
            mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionLeft;
            //添加图表块 
            //分类属性 
            const ChartDimensionsEnum category = ChartDimensionsEnum.chDimValues;
            const int dataSources = (int)ChartSpecialDataSourcesEnum.chDataLiteral;

            var chart1 = mychart.SeriesCollection.Add(0);
            //X柱
            chart1.SetData(ChartDimensionsEnum.chDimCategories, dataSources, "T1\tT2\tT3\tT4\tT5\tT6\tT7\tT8\t本次考试");
            //分类名字
            chart1.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "总分");
            //赋值
            chart1.SetData(category, dataSources, "-0.1\t-0.8\t-0.1\t0.3\t0.3\t1\t1.5\t1.1\t-0.6");
            //chart1.Ungroup(true);
            //chart1.Interior.Color = "red";

            //ChSeries chSeries = mychart.SeriesCollection.Add(0);
            //chSeries.Type = ChartChartTypeEnum.chChartTypeSmoothLine;
            //ChAxis chAxis = mychart.Axes.Add(chSeries.Scalings[ChartDimensionsEnum.chDimValues]);
            //chAxis.Position = ChartAxisPositionEnum.chAxisPositionRight;

            var chart2 = mychart.SeriesCollection.Add(0);
            chart2.SetData(category, dataSources, "-0.2\t-0.7\t-0.1\t0.3\t0.4\t1\t1.1\t1.1\t-0.6");
            chart2.Ungroup(true);
            chart2.Caption = "平均分";
            // chart2.Line.Color = ChartColorIndexEnum.chColorNone;//不要线

            var chart3 = mychart.SeriesCollection.Add(0);
            chart3.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "二次平均");
            chart3.SetData(category, dataSources, "-0.3\t-0.6\t-1.3\t0.3\t0.5\t1\t1.1\t1.1\t-0.6");
            chart3.Ungroup(true);

            var chart4 = mychart.SeriesCollection.Add(0);
            chart4.SetData(ChartDimensionsEnum.chDimSeriesNames, dataSources, "三次平均");
            chart4.SetData(category, dataSources, "-0.4\t-0.5\t-1.0\t0.3\t0.6\t1\t-1.1\t1.1\t-0.6");
            chart4.Ungroup(true);

            //mychart.Axes[0].HasMajorGridlines = true;//竖着的线
            //mychart.Axes[0].HasMinorGridlines = false;//竖着的线
            //mychart.Axes[0].HasTickLabels = false;//X坐标的值

            //mychart.Axes[1]X坐标

            //给定标题
            //objChart.HasTitle = true;
            //objChart.Title.Caption = topTitle;
            //objChart.Title.Font.Size = 18;
            //objChart.Title.Font.Bold = true;
            //objChart.Title.Font.Color = "#ff3300";//标题颜色

            //objChart.Interior.Color = "#003399";
            //objChart.PlotArea.BackWall.Interior.Color = "black";
            //objChart.PlotArea.Interior.Color = "green";
            ////objChart.PlotArea.Interior.Color = "black";//图表区域背景颜色

            //objChart.PlotArea.Floor.Interior.Color = "black";
            ////给定x,y轴的图示说明
            //objChart.Axes[0].HasTitle = true;

            //objChart.Axes[0].Title.Caption = xTitle;
            //objChart.Axes[0].Title.Font.Size = 15;
            //objChart.Axes[0].Title.Font.Bold = true;
            //objChart.Axes[0].Title.Font.Color = "#ff3300";

            //objChart.Axes[0].MajorGridlines.Line.Color = "white";
            //objChart.Axes[0].Font.Size = 14;
            //objChart.Axes[0].Font.Color = "#ff3300";//X轴刻度颜色
            //objChart.Axes[0].Font.Bold = true;
            //objChart.Axes[0].MajorTickMarks = OWC11.ChartTickMarkEnum.chTickMarkAutomatic;




            //趋势图
            //var trend = chart2.Trendlines.Add();
            //trend.Type = ChartTrendlineTypeEnum.chTrendlineTypeLinear;
            //trend.Caption = "ds";
            //trend.IsDisplayingEquation = false;
            //trend.IsDisplayingRSquared = false;


            //Explosion：返回或设置指定饼图或圆环图扇面的分离程度值。有效范围为 0 到 1000。分离程度值等于图表半径的百分比
            //objChart.SeriesCollection[0].Points[2].Explosion = 45;


            //将柱状图的第一条柱设置为红色
            //Point：代表图中的一部分，比如柱图的一条柱，饼图的一个扇区
            //Interior：表示指定对象的内部
            //mychart.SeriesCollection[0].Points[0].Interior.Color = "Red";


            //给第一个扇区设置自定义背景图片
            //objChart.SeriesCollection[0].Points[0].Interior.SetTextured("E:\\Projects\\DotNetNuke\\images\\add.gif", ChartTextureFormatEnum.chTile,1,ChartTexturePlacementEnum.chFrontSides);
            //给第二个扇区设置从中心向四周辐射的单色渐变
            //objChart.SeriesCollection[0].Points[1].Interior.SetOneColorGradient(ChartGradientStyleEnum.chGradientFromCenter,ChartGradientVariantEnum.chGradientVariantStart,0.3,"Blue");
            //给第三个扇区设置倾斜双色渐变
            //objChart.SeriesCollection[0].Points[2].Interior.SetTwoColorGradient(ChartGradientStyleEnum.chGradientDiagonalDown,ChartGradientVariantEnum.chGradientVariantCenter,"Green","Red");
            //给第四个扇区设置OWC预设的纹理，并设置纹理的背景色为淡绿色，前景色为红色
            //OWC提供了很多纹理，这是其中一种
            //具体的纹理样式可以参看帮助中的ChartPatternTypeEnum枚举
            // objChart.SeriesCollection[0].Points[3].Interior.SetPatterned(ChartPatternTypeEnum.chPatternDiagonalBrick,"Red","LightGreen");
            //给第五个扇区设置OWC预设的倾斜，
            //OWC提供了许多种的倾斜，这是其中一种
            //具体的倾斜可以参看帮助中的ChartPresetGradientTypeEnum枚举
            // objChart.SeriesCollection[0].Points[4].Interior.SetPresetGradient(ChartGradientStyleEnum.chGradientDiagonalUp,ChartGradientVariantEnum.chGradientVariantEdges,ChartPresetGradientTypeEnum.chGradientGoldII);


            //  objChart.SeriesCollection[0].Trendlines.Add();                             
            //   是否显示函数
            // objChart.SeriesCollection[0].Trendlines[0].IsDisplayingEquation = true;    
            //   是否显示平方
            // objChart.SeriesCollection[0].Trendlines[0].IsDisplayingRSquared = true;     
            //   设置趋势线标题
            // objChart.SeriesCollection[0].Trendlines[0].Caption = "TrendLine";          
            //   设置趋势线类型，
            //   OWC提供了4种趋势线类型，具体可参看帮助的ChartTrendlineTypeEnum枚举
            // objChart.SeriesCollection[0].Trendlines[0].Type = OWC11.ChartTrendlineTypeEnum.chTrendlineTypePolynomial;


            //var trend = chart2.Trendlines.Add();
            //trend.Type = ChartTrendlineTypeEnum.chTrendlineTypeLinear;
            //trend.Caption = "ds";
            //trend.IsDisplayingEquation = false;
            //trend.IsDisplayingRSquared = false;
            ////主次网格线不显示
            //chChart.Axes[1].HasMinorGridlines = false;
            //chChart.Axes[1].HasMajorGridlines = false;
            ////设定Y轴格式
            //chChart.Axes[1].NumberFormat = "0%";
            ////去掉线
            //chChart.SeriesCollection[3].Line.Color = ChartColorIndexEnum.chColorNone;
            ////标记形状
            //chChart.SeriesCollection[3].Marker.Style = ChartMarkerStyleEnum.chMarkerStyleDiamond;
            //显示百分比
            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasPercentage = true;

            //ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            //mytb.HasBubbleSize = false;
            //显示百分比 
            //mytb.HasPercentage = true;
            //显示值
            //mytb.HasValue = true;

            var image = UrlPath("折线图");
            //生成图片 
            mychartSpace.ExportPicture(image, "GIF", 800, 200);

        }
    }
}
