using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Common
{
    public class HeaderAndFooterEvent : PdfPageEventHelper
    {
        public static PdfTemplate Tpl = null;
        public static BaseFont Bf = null;
        public static int PageNumber = 0;
        private Phrase header;
        private Phrase footer;
        readonly Font font = ExamAnalysiseReportFormat.BaseFontAndSize("黑体", 10, Font.NORMAL);

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            if (PageNumber == -1) return;

            header = new Phrase("分析报告--这是页眉", font);
            footer = new Phrase("第" + (writer.PageNumber - 1) + "页--这是页脚", font);
            var cb = writer.DirectContent;

            ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, header,
                                       document.Right - 140 + document.LeftMargin, document.Top + 10, 0);

            ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, footer,
                                       document.Right - 60 + document.LeftMargin, document.Bottom - 10, 0);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            if (PageNumber != -1)
            {
                writer.PageCount = PageNumber++;
            }
        }


        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            BaseFont bf = BaseFont.CreateFont(@"c:\windows\fonts\simsun_1.ttf", BaseFont.IDENTITY_H, false); //调用的字体
            Tpl.BeginText();
            Tpl.SetFontAndSize(bf, 16);
            Tpl.ShowText((writer.PageNumber - 2).ToString());
            Tpl.EndText();
            Tpl.ClosePath();
        }
    }
}
