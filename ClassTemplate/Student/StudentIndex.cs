using System;
using System.IO;
using Common;
using Database;
using ImageUtils;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ClassTemplate.Student
{
    public class StudentIndex
    {
        public void Test()
        {
            //var img = new ImageHelp();
            //var imagePath = img.ChartTypeBarClustered();
            //img.ChartTypeColumnClustered();
            //img.ChartTypeLineStackedMarkers();
            //DataInterface di=new DataInterface();
            //di.Test();
        }

        readonly StudentData sd=new StudentData();
        public string CreateReport()
        {
            sd.Value();
            Document doc = null;
            PdfWriter writer = null;
            const string filePath = "F://1/";
            const string fileName = "F:/1/Test.pdf";

            //try
            //{
                doc = new Document(PageSize.A4, 25, 25, 50, 40);//定义pdf大小，设置上下左右边距
                
                if (false == Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                if (File.Exists(fileName))
                    File.Delete(fileName);

                writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));//生成pdf路径，创建文件流

                doc.Open();
                writer.PageEvent = new HeaderAndFooterEvent();

                HeaderAndFooterEvent.PageNumber = -1;
                First(doc, writer);
                doc.NewPage();

                var chapterOne = new StudentChapter();
               

                Header(doc, writer);//标头
                HeaderAndFooterEvent.PageNumber = 1;

                doc.Add(chapterOne.SectionOne(sd));
                doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(3));
                doc.Add(chapterOne.SectionTwo(sd));
                doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(3));
                doc.Add(chapterOne.SectionThree(sd));
                doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(3));
                doc.Add(chapterOne.SectionFour(sd));
                doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(3));
                doc.Add(chapterOne.SectionFive(sd));
                doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(3));
                doc.Add(chapterOne.SectionSeven(sd));
                doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(3));

                writer.Flush();
                writer.CloseStream = true;
                doc.Close();

                return fileName;
            //}
            //catch (Exception ex)
            //{
            //    if (doc != null && doc.IsOpen())
            //    {
            //        if (writer != null)
            //        {
            //            doc.Add(ExamAnalysiseReportFormat.InsertTitleContent("生成报表出错！"));
            //            writer.Flush();
            //            writer.CloseStream = true;
            //        }
            //        doc.Close();
            //    }
            //    //LogHelper.Error("CreateReport", ex);
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
            //    //手动回收垃圾
            //    GC.Collect();
            //    GC.WaitForFullGCComplete();
            //}
           
        }

        public bool Header(Document doc, PdfWriter writer)
        {
            writer.PageEvent = new HeaderAndFooterEvent();
            doc.Add(ExamAnalysiseReportFormat.InsertTitleContent("分析报告"));
            doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(3));
            return true;
        }

        public void First(Document doc, PdfWriter writer)
        {
            doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(10));

            var tmp = "分析报告";
            doc.Add(ExamAnalysiseReportFormat.InsertTopTitleContent(tmp));

            tmp = "(正文     页,附件 0 页)";
            doc.Add(ExamAnalysiseReportFormat.InsertTopButtomParagraph(tmp));

            //模版 显示总共页数
            HeaderAndFooterEvent.Tpl = writer.DirectContent.CreateTemplate(100, 100);
            PdfContentByte cb = writer.DirectContent;
            cb.AddTemplate(HeaderAndFooterEvent.Tpl, 256, 555);

            doc.Add(ExamAnalysiseReportFormat.InsertBlankParagraph(20));
            Image tImgCover = Image.GetInstance(@"F:\project_cw\PdfTools\PdfTools\CommonImage\123.gif");
            /* 设置图片的位置 */
            tImgCover.SetAbsolutePosition(216, 137);
            /* 设置图片的大小 */
            tImgCover.ScaleAbsolute(140, 140);
            //加载图片  
            doc.Add(tImgCover);
        }
    }
}
