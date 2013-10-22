using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Linq;

namespace Common
{
    public class ExamAnalysiseReportFormat
    {
        // 函数描述：插入中间的信息----
        public static Paragraph InsertTopCommonParagraph(string text)
        {
            var font = BaseFontAndSize("黑体", 14, Font.NORMAL);
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 16,
                    IndentationLeft = 130,
                    IndentationRight = 20,
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };
            paragraph.SetLeading(1, 2.2f);
            //paragraph.IndentationLeft = 24;
            paragraph.Alignment = 0;
            return paragraph;
        }
        // 函数描述：插入底部信息----
        public static Paragraph InsertTopButtomParagraph(string text)
        {
            var font = BaseFontAndSize("宋体", 16, Font.NORMAL);
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 16,
                    IndentationLeft = 0,
                    IndentationRight = 20,
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };

            paragraph.SetLeading(1, 2);
            //paragraph.IndentationLeft = 24;
            paragraph.Alignment = Element.ALIGN_CENTER;
            return paragraph;
        }
        // 函数描述: 格式化报告的标题
        public static Paragraph InsertTopTitleContent(string text)
        {
            var font = BaseFontAndSize("华文中宋", 26, Font.BOLD);
            var paragraph = new Paragraph(text, font)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 5,
                    SpacingAfter = 5
                };
            paragraph.SetLeading(1, 1.3f);
            return paragraph;
        }
        // 函数描述：插入底部信息
        public static Paragraph InsertTopFootParagraph(string text)
        {
            var font = BaseFontAndSize("华文中宋", 18, Font.BOLD);
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 16,
                    IndentationLeft = 0,
                    IndentationRight = 20,
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };
            paragraph.SetLeading(1, 2);
            //paragraph.IndentationLeft = 24;
            paragraph.Alignment = Element.ALIGN_CENTER;
            return paragraph;
        }
        // 函数描述：插入空白段落
        public static Paragraph InsertBlankParagraph(int blankNumber)
        {
            var font = BaseFontAndSize("宋体", 12, Font.NORMAL);
            var paragraph = new Paragraph(" ",font)
                {
                    FirstLineIndent = 16,
                    IndentationLeft = 20,
                    IndentationRight = 20,
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };
            paragraph.SetLeading(1, blankNumber);
            //paragraph.IndentationLeft = 24;
            paragraph.Alignment = Element.ALIGN_LEFT;

            return paragraph;
        }
        // 函数描述：插附件标题----
        public static Chapter InsertAttachmentChapterParagraph(string text, int chapterNumber)
        {
            var font = BaseFontAndSize("宋体", 12, Font.BOLD);
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 20,
                    IndentationLeft = 0,
                    SpacingBefore = 5,
                    SpacingAfter = 5,
                    Alignment = Element.ALIGN_LEFT
                };
            //paragraph.SetLeading(1.5f, 1.5f);
            var chapter = new Chapter(paragraph, chapterNumber)
                {
                    TriggerNewPage = true
                };
            return chapter;
        }
        // 函数描述：插一级标题
        public static Chapter InsertChapterParagraph(string text, int chapterNumber)
        {
            var font = BaseFontAndSize("宋体", 12, Font.BOLD);
            text += "\n" + "---------------------------------------------------------------------------------------";
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 20,
                    IndentationLeft = 0,
                    SpacingBefore = 0,
                    SpacingAfter = 0,
                    Alignment = Element.ALIGN_LEFT
                };
            //paragraph.SetLeading(1.5f, 1.5f);
            var chapter = new Chapter(paragraph, chapterNumber)
                {
                    TriggerNewPage = false
                };
            return chapter;
        }
        //  函数描述：插入二级标题
        public static Paragraph InsertSectionParagraph(string text)
        {
            var font = BaseFontAndSize("黑体", 12, Font.BOLD);
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 20,
                    IndentationLeft = 0,
                    SpacingBefore = 5,
                    SpacingAfter = 5,
                    Alignment = Element.ALIGN_LEFT
                };
            return paragraph;
        }
        // 函数描述：格式化插入的 PdfPTable
        public static Paragraph InsertTableParagraph(PdfPTable table, int firstLineIndent=0)
        {
            var paragraph = new Paragraph();
            if (table == null)
                paragraph.Add("");
            else
                paragraph.Add(table);
            paragraph.FirstLineIndent = firstLineIndent;
            paragraph.IndentationLeft = 0;
            paragraph.SpacingBefore = 1;
            paragraph.SpacingAfter = 1;
            if (table != null) 
                table.WidthPercentage = 90;
            paragraph.Alignment = Element.ALIGN_LEFT;

            return paragraph;
        }
        // 函数描述：将插入报告中的图片进行格式话和定位
        public static PdfPTable InsertImageParagraph(Image image)
        {
            var pdfTable = new PdfPTable(1);
            var pdfCell = new PdfPCell();
            pdfCell.AddElement(image);
            //pdfCell.BorderColor = BaseColor.WHITE;
            pdfCell.BorderWidth = 0;
            pdfTable.AddCell(pdfCell);
            return pdfTable;
        }
        // 函数描述：插入普通的文本内容
        public static Paragraph InsertSectionContent(string text)
        {
            var font = BaseFontAndSize("宋体", 12, Font.NORMAL);
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 24,
                    IndentationLeft = 20,
                    IndentationRight = 20,
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };
            paragraph.SetLeading(1, 2);
            paragraph.Alignment = Element.ALIGN_LEFT;

            return paragraph;
        }
        public static Paragraph InsertSectionContent(string text, BaseColor color)
        {
            var font = BaseFontAndSize("宋体", 12, Font.NORMAL,color);
            var paragraph = new Paragraph(text, font)
            {
                FirstLineIndent = 24,
                IndentationLeft = 20,
                IndentationRight = 20,
                SpacingBefore = 1,
                SpacingAfter = 1
            };
            paragraph.SetLeading(1, 2);
            paragraph.Alignment = Element.ALIGN_LEFT;

            return paragraph;
        }
        public static Paragraph InsertSectionContent(string text, int fontSize, bool isHasColor = false, int type = 0)
        {
            var font = BaseFontAndSize("宋体", fontSize, Font.NORMAL);
            var font1 = BaseFontAndSize("宋体", fontSize, Font.BOLD,BaseColor.RED);
            var font2 = BaseFontAndSize("宋体", fontSize, Font.BOLD, new BaseColor(0, 162, 234));

            var font3 = BaseFontAndSize("宋体", fontSize, Font.NORMAL, BaseColor.RED);

            //正行字都变色
            if (type == 3)
            {
                font = font3;
            }

            var paragraph = new Paragraph
                {
                    FirstLineIndent = 24,
                    IndentationLeft = 20,//距左边差距
                    IndentationRight = 20,//距右边差距
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };
            //行高间距
            paragraph.SetLeading(1, fontSize == 8 ? 1 : 2);
            //左对齐
            paragraph.Alignment = Element.ALIGN_LEFT;
            if (isHasColor)//是否要变色
            {
                var strList = text.Split('@');
                for (var i = 0; i < strList.Length;i++ )
                {
                    var val = strList[i];
                    var fo = font;
                    if (i%2 != 0) //“@@”里面的文字变色
                    {
                        var co = val.Split('#');
                        if (co.Length >= 2)
                        {
                            val = co[1];
                            fo = font2;
                        }
                        else
                        {
                            fo = font1;
                        }
                    }
                    //加入块
                    var ch = new Chunk(val, fo);
                    paragraph.Add(ch);
                }
            }
            else
            {
                var ch = new Chunk(text, font);
                paragraph.Add(ch);
            }
            return paragraph;
        }
        // 函数描述：对表格内容进行格式化----
        public static PdfPCell InsertTableContent(string text)
        {
            var font = BaseFontAndSize("宋体", 10, Font.NORMAL);
            var paragraph1 = new Paragraph(" ", font);
            paragraph1.SetLeading(1, 1);
            var paragraph = new Paragraph(text, font)
                {
                    FirstLineIndent = 24,
                    IndentationLeft = 20,
                    IndentationRight = 20,
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };
            paragraph.SetLeading(1, 1.5f);
            paragraph.Alignment = Element.ALIGN_LEFT;

            var pdfPCell = new PdfPCell();
            pdfPCell.AddElement(paragraph1);
            pdfPCell.AddElement(paragraph1);
            pdfPCell.AddElement(paragraph);
            pdfPCell.Padding = 1;
            pdfPCell.SetLeading(1, 1);
            pdfPCell.BorderWidth=0;

            return pdfPCell;
        }
        // 函数描述: 格式化报告的标题
        public static Paragraph InsertTitleContent(string text, bool isHasColor = true)
        {
            var font = BaseFontAndSize("华文中宋", 16, Font.BOLD);
            var font1 = BaseFontAndSize("华文中宋", 16, Font.BOLD,new BaseColor(0,162,234));
            var paragraph = new Paragraph
                {
                FirstLineIndent = 24,
                IndentationLeft = 20,
                IndentationRight = 20,
                SpacingBefore = 1,
                SpacingAfter = 1
            };
            paragraph.SetLeading(1, 2);
            paragraph.Alignment = Element.ALIGN_CENTER;

            if (isHasColor)
            {
                var strList = text.Split('@');
                for (var i = 0; i < strList.Length; i++)
                {
                    var fo = i % 2 == 0 ? font : font1;
                    var ch = new Chunk(strList[i], fo);
                    paragraph.Add(ch);
                }
            }
            else
            {
                var ch = new Chunk(text, font);
                paragraph.Add(ch);
            }

            return paragraph;
        }
        // 函数描述: 格式化报告的标题----
        public static Paragraph InsertPictureTitle(string text)
        {
            var font = BaseFontAndSize("华文中宋", 12, Font.NORMAL);
            var paragraph = new Paragraph(text, font)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 5,
                    SpacingAfter = 5
                };
            paragraph.SetLeading(1, 2);
            return paragraph;
        }
        // 函数描述：格式化“摘要”二字----
        public static Paragraph InsertSummaryParagraph(string text, int spaceNum)
        {
            var font = BaseFontAndSize("黑体", 12, Font.BOLD);
            var paragraph = new Paragraph(text, font)
                {
                    Alignment = Element.ALIGN_JUSTIFIED,
                    SpacingBefore = 1,
                    IndentationLeft = 0,
                    Font = {Size = 12},
                    FirstLineIndent = 20,
                    SpacingAfter = spaceNum
                };
            paragraph.SpacingBefore = spaceNum;
            paragraph.SetLeading(1, 2);  //设置行间距
            paragraph.Alignment = Element.ALIGN_LEFT;
            return paragraph;
        }
        // 函数描述：摘要内容样式----
        public static Paragraph InsertAbstractComment(string text, string fontName)
        {
            var font = BaseFontAndSize(fontName, 12, Font.NORMAL);
            var paragraph = new Paragraph(text, font)
                {
                    Alignment = Element.ALIGN_JUSTIFIED,
                    IndentationRight = 24,
                    IndentationLeft = 40,
                    SpacingBefore = 1,
                    SpacingAfter = 1
                };
            paragraph.SetLeading(1, 2);
            return paragraph;
        }
        // 函数描述：设置字体的样式
        public static Font BaseFontAndSize(string fontName, int size, int style, BaseColor baseColor)
        {
            BaseFont.AddToResourceSearch("iTextAsian.dll");
            BaseFont.AddToResourceSearch("iTextAsianCmaps.dll");
            string fileName;
            switch (fontName)
            {
                case "黑体":
                    fileName = "SIMHEI.TTF";
                    break;
                case "华文中宋":
                    fileName = "STZHONGS.TTF";
                    break;
                case "宋体":
                    fileName = "simsun_1.ttf";
                    break;
                default:
                    fileName = "simsun_1.ttf";
                    break;
            }
            var baseFont = BaseFont.CreateFont(@"c:/windows/fonts/" + fileName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var fontStyle = style < -1 ? Font.NORMAL : style;
            var font = new Font(baseFont, size, fontStyle, baseColor);
            return font;
        }
        public static Font BaseFontAndSize(string fontName, int size, int style)
        {
            return BaseFontAndSize(fontName, size, style, BaseColor.BLACK);
        }
        //设置文本
        public static Paragraph InsertExplainContent(string text, int size)
        {
            var font = BaseFontAndSize("华文中宋", size, Font.BOLD);
            var paragraph = new Paragraph(text, font)
            {
                Alignment = Element.ALIGN_LEFT,
                FirstLineIndent = 20,
            };
            return paragraph;
        }
        //Chuck的集合
        public static Paragraph InsertChuckList(Chunk[] chunk)
        {
            var paragraph = new Paragraph
            {
                FirstLineIndent = 24,
                IndentationLeft = 20,
                IndentationRight = 20,
                SpacingBefore = 1,
                SpacingAfter = 1
            };
            paragraph.SetLeading(1, 2);
            paragraph.Alignment = Element.ALIGN_LEFT;

            foreach (var ch in chunk)
            {
                paragraph.Add(ch);
            }
            return paragraph;

        }
        /// <summary>
        /// 返回table
        /// </summary>
        /// <param name="dataTable">表格</param>
        /// <param name="analyseClassName">特殊处理行的第一列数据</param>
        /// <param name="flagCloumnIndex">为1该行字体变色，不为1就是该行背景变色</param>
        /// <param name="type">1：第一行和第一列变色,默认为1</param>
        /// <returns></returns>
        public static PdfPTable CreatePdfTable(DataTable dataTable, string analyseClassName, int flagCloumnIndex,int type = 1)
        {
            PdfPTable pdfPTable = null;

            var headColor = BaseColor.WHITE;
            var firstListColor = BaseColor.WHITE;
            var selectedColor = BaseColor.BLACK;
            //第一行 第一列背景颜色
            if (type == 1)
            {
                headColor = new BaseColor(230, 230, 230);
                firstListColor = new BaseColor(191, 191, 191);
                selectedColor = new BaseColor(146, 208, 80);
            }

            var fontTop = BaseFontAndSize("宋体", 9, Font.BOLD);
            var font1 = BaseFontAndSize("宋体", 9, Font.NORMAL);
            var font2 = BaseFontAndSize("宋体", 9, Font.BOLD, BaseColor.RED);
            var font3 = BaseFontAndSize("宋体", 9, Font.BOLD, new BaseColor(0, 162, 234));

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                //设置表的属性
                pdfPTable = new PdfPTable(dataTable.Columns.Count)
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                
                var columnWidths = new float[dataTable.Columns.Count];
                pdfPTable.WidthPercentage = 90;//表的宽度
                var index = 0;
                //设置表头
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    //第一行第一列 显示为“” 且宽度为8f
                    columnWidths[index] = index == 0 ? 8f : 3f;
                    if (index == 0)
                        dataColumn.ColumnName = " ";
                    
                    //表头的背景颜色
                    var pdfCell = new PdfPCell();
                    pdfCell.AddElement(new Paragraph(dataColumn.ColumnName, fontTop) { Alignment = Element.ALIGN_CENTER });
                    pdfCell.BackgroundColor = headColor;
                    pdfPTable.AddCell(pdfCell);
                  
                    index++;
                }
                pdfPTable.SetWidths(columnWidths);

                //获取表格中的数据
                foreach (DataRow row in dataTable.Rows)
                {
                    index = 0;
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        var pdfCell = new PdfPCell();
                        var fo=font1;
                        var str = row[dataColumn].ToString();
                        //是否是需要特殊处理的行
                        if (row[0].ToString().Trim().Equals(analyseClassName))
                        {
                            //让该行字体为红色
                            if (flagCloumnIndex == 1)
                            {
                                if (index == 0)
                                {
                                    fo = fontTop;
                                    pdfCell.BackgroundColor = firstListColor;
                                }
                                else
                                {
                                    fo = font2;
                                }
                            }
                            else//该行背景变色
                            {
                                pdfCell.BackgroundColor = selectedColor;
                            }
                        }
                        else
                        {
                            
                            if (index == 0)
                            {
                                fo = fontTop;
                                pdfCell.BackgroundColor = firstListColor;
                            }
                            else
                            {
                                //根据条件 显示不同的字体
                                if (str.Contains("↑"))
                                {
                                    fo = font3;
                                }
                                else if (str.Contains("↓"))
                                {
                                    fo = font2;
                                }
                            }
                        }
                        pdfCell.AddElement(new Paragraph(str, fo) { Alignment = Element.ALIGN_CENTER });
                        index = 1;
                        pdfPTable.AddCell(pdfCell);
                    }
                }
            }
            return pdfPTable;
        }

        /// <summary>
        /// 返回table
        /// </summary>
        /// <param name="dataTable">表格</param>
        /// <param name="specialClassName">特殊处理的列名</param>
        /// <param name="styleName">需要跨行显示的列</param>
        /// <param name="type">第一列第一行表头变色(type==1 不同列显示不同颜色，目前只有试卷分析(7小题失分详情)用到这个)</param>
        /// <returns></returns>
        public static PdfPTable CreatePdfTable(DataTable dataTable, string specialClassName,string styleName, int type=0)
        {
            PdfPTable pdfPTable = null;
           
            //第一行 第一列背景颜色
            var headColor = new BaseColor(230, 230, 230);
            var selectedColor = new BaseColor(146, 208, 80);

            var specialHead = new BaseColor(153, 255, 51);
            var specialList1 = new BaseColor(237, 125, 49);
            var specialList2 = new BaseColor(255,192,0);

            var fontTop = BaseFontAndSize("宋体", 9, Font.BOLD);
            var font1 = BaseFontAndSize("宋体", 9, Font.NORMAL);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                //设置表的属性
                pdfPTable = new PdfPTable(dataTable.Columns.Count)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                };

                var columnWidths = new float[dataTable.Columns.Count];
                pdfPTable.WidthPercentage = 90;//表的宽度
                var index = 0;
                //设置表头
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    //第一行第一列 显示为“” 且宽度为8f
                    columnWidths[index] = 8f;
                    var color=headColor;
                    if (type == 1 && dataColumn.ColumnName.Trim().Equals(specialClassName))
                    {
                        color = specialHead;
                    }
                    //表头的背景颜色
                    var pdfCell = new PdfPCell();
                    pdfCell.AddElement(new Paragraph(dataColumn.ColumnName, fontTop) { Alignment = Element.ALIGN_CENTER });
                    pdfCell.BackgroundColor = color;
                    pdfPTable.AddCell(pdfCell);

                    index++;
                }
                pdfPTable.SetWidths(columnWidths);

                //获取表格中的数据
                var lastStr = "";
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        var pdfCell = new PdfPCell();
                        var fo = font1;
                        var str = row[dataColumn].ToString();
                        //是否是需要特殊处理的列
                        if (dataColumn.ColumnName.Trim().Equals(specialClassName))
                        {
                            var color = selectedColor;
                            if (type == 1 && row[2].ToString() != row[3].ToString())
                            {
                                switch (row[1].ToString().Trim())
                                {
                                    case "选择题": color = specialList1; break;
                                    case "解答题": color = specialList2; break;
                                }
                            }
                            pdfCell.BackgroundColor = color;

                        }
                        else
                        {
                            var columnName = dataColumn.ColumnName;
                            //包含列名
                            if (columnName.Trim().Equals(styleName))
                            {
                                //如果是相同数据 就不处理了
                                if (lastStr == str) continue;

                                lastStr = str;
                                //获取 列名占据多少行
                                var sql = styleName + "='" + str + "'";
                                var count = dataTable.Select(sql).Count();
                                pdfCell.Rowspan = count;
                            }
                        }
                        pdfCell.AddElement(new Paragraph(str, fo) {Alignment = Element.ALIGN_CENTER});
                        pdfPTable.AddCell(pdfCell);
                    }
                }
            }
            return pdfPTable;

        }



    }
}
