using System.Collections.Generic;
using System.Data;
using Common;
using Database;
using ImageUtils;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ClassTemplate
{
    class StudentChapter
    {
        readonly ImageBackup img = new ImageBackup();
        public Chapter SectionOne(StudentData sd)
        {
            Chapter chapter = ExamAnalysiseReportFormat.InsertChapterParagraph("概述", 1);
            string tmp = string.Format("本次测试共{0}次。", sd.StOneExplain);
            chapter.Add(ExamAnalysiseReportFormat.InsertSectionContent(tmp));
            return chapter;
        }

        public Chapter SectionTwo(StudentData sd)
        {
            Chapter chapter = ExamAnalysiseReportFormat.InsertChapterParagraph("SectionTwo", 2);

            var dt = sd.SectionTwoDt();
            PdfPTable systemRankTable = ExamAnalysiseReportFormat.CreatePdfTable(dt, "test6", 0);
            chapter.Add(ExamAnalysiseReportFormat.InsertTableParagraph(systemRankTable));

            chapter.Add(ExamAnalysiseReportFormat.InsertExplainContent("图表说明：",8));
            return chapter;
        }

        public Chapter SectionThree(StudentData sd)
        {
            var chapter = ExamAnalysiseReportFormat.InsertChapterParagraph("档次分析", 3);

            var imagePath = img.YiBiao(sd.Total);//指针分数
            var image = Image.GetInstance(imagePath);
            var pdfTable1 = new PdfPTable(2);

            var tmp = "本次你的位置：\n";
            tmp += "在C档次";
            var par = ExamAnalysiseReportFormat.InsertSectionContent(tmp,12,true);
            var cell1 = new PdfPCell(par) {BorderWidth = 0};
            cell1.SetLeading(1,2);
            var cell2 = ExamAnalysiseReportFormat.InsertImageParagraph(image);
            pdfTable1.AddCell(cell1);
            pdfTable1.AddCell(cell2);
            chapter.Add(ExamAnalysiseReportFormat.InsertTableParagraph(pdfTable1));

            tmp = "图表说明：";
            chapter.Add(ExamAnalysiseReportFormat.InsertExplainContent(tmp,8));

            tmp = "仪表盘图中不同颜色的区域代表不同的档次。";
            tmp +=string.Format("（各档次如下：A档次：{0}，B档次：{1}，C档次：{2}，D档次：{3}。）",sd.ALevel,sd.BLevel,sd.CLevel,sd.DLevel);
            chapter.Add(ExamAnalysiseReportFormat.InsertSectionContent(tmp,8));

            return chapter;
        }

        public Chapter SectionFour(StudentData sd)
        {
            Chapter chapter = ExamAnalysiseReportFormat.InsertChapterParagraph("均衡性分析", 4);

            var dt = sd.SectionFourDt();
            PdfPTable systemRankTable = ExamAnalysiseReportFormat.CreatePdfTable(dt, "test4", 0);
            chapter.Add(ExamAnalysiseReportFormat.InsertTableParagraph(systemRankTable));

            var value1 = "";
            var value2 = "";
            var strDataName = "";
            for (var i = 0; i < dt.Columns.Count-1; i++)
            {
                var row = dt.Rows;
                strDataName += dt.Columns[i] + "\t";
                value1 += "0\t";
                value2 += row[dt.Rows.Count-1][i] + "\t";
            }
            value1 += "0";
            value2 += dt.Rows[dt.Rows.Count - 1][dt.Rows.Count - 1];

            var imagePath = img.ChartTypeRadarLine(strDataName, value1, value2);
            var image = Image.GetInstance(imagePath);
            var pdfTable1 = new PdfPTable(2);

            var cell1 = new PdfPCell(ExamAnalysiseReportFormat.InsertSectionContent(sd.SfExplain, 12, true, 2)) { BorderWidth = 0 };
            cell1.SetLeading(1,2);
            var cell2 = new PdfPCell(ExamAnalysiseReportFormat.InsertImageParagraph(image)) { BorderWidth = 0 };
            pdfTable1.AddCell(cell2);
            pdfTable1.AddCell(cell1);
            chapter.Add(ExamAnalysiseReportFormat.InsertTableParagraph(pdfTable1));
            
            chapter.Add(ExamAnalysiseReportFormat.InsertExplainContent("图表说明：",8));
            return chapter;
        }

        public Chapter SectionFive(StudentData sd)
        {
            Chapter chapter = ExamAnalysiseReportFormat.InsertChapterParagraph("波动情况", 5);

            var imagePath = img.ChartTypeColumnClustered();
            ChartTypeLineStackedMarker(chapter, imagePath);

            imagePath = img.ChartTypeColumnClustered2();
            ChartTypeLineStackedMarker(chapter, imagePath);

           
            chapter.Add( ExamAnalysiseReportFormat.InsertBlankParagraph(5));

            imagePath = img.ChartTypeColumnClustered3();
            ChartTypeLineStackedMarker(chapter, imagePath);

            imagePath = img.ChartTypeColumnClustered4();
            ChartTypeLineStackedMarker(chapter, imagePath);

            imagePath = img.ChartTypeBarClustered();
            ChartTypeLineStackedMarker(chapter, imagePath);

            imagePath = img.ChartTypeLineStackedMarkers2();
            ChartTypeLineStackedMarker(chapter, imagePath);

            imagePath = img.ChartTypeLineStackedMarkers("test");
            ChartTypeLineStackedMarker(chapter, imagePath);

            imagePath = img.ChartTypeColumnClustered5();
            ChartTypeLineStackedMarker(chapter, imagePath);

            imagePath = img.ChartTypeLineStackedMarkers2();
            ChartTypeLineStackedMarker(chapter, imagePath);





            chapter.Add(ExamAnalysiseReportFormat.InsertExplainContent("图表说明：", 8));
            return chapter;
        }


        public Chapter SectionSeven(StudentData sd)
        {
            Chapter chapter = ExamAnalysiseReportFormat.InsertChapterParagraph("综述", 7);
           
            chapter.Add(ExamAnalysiseReportFormat.InsertSectionContent(sd.SseExplain,12,true,3));
            chapter.Add(ExamAnalysiseReportFormat.InsertSectionContent(sd.SseSuggest, 12, true));

            return chapter;
        }

        private void ChartTypeLineStackedMarker(Chapter chapter, string imagePath)
        {
            var image = Image.GetInstance(imagePath);
            var pdfTable1 = new PdfPTable(2);
            var cell1 = new PdfPCell(ExamAnalysiseReportFormat.InsertImageParagraph(image)){BorderWidth = 0};
            var cell2 = new PdfPCell(ExamAnalysiseReportFormat.InsertImageParagraph(image)) { BorderWidth = 0 };
            pdfTable1.AddCell(cell1);
            pdfTable1.AddCell(cell2);
            chapter.Add(ExamAnalysiseReportFormat.InsertTableParagraph(pdfTable1));
        }

    }
}
