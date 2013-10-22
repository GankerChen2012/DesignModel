using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Common;

namespace ImageUtils
{
    class YiBiao
    {
        public class ImagePaint
        {
            public int Value { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public Font Font { get; set; }
            public Graphics Graphic { get; set; }
            public Color BackColor { get; set; }
            public Rectangle ClientRectangle { get; set; }
        }

        public class AGauge
        {
            #region enum, var, delegate, event

            public enum NeedleColorEnum
            {
                Gray = 0,
                Red = 1,
                Green = 2,
                Blue = 3,
                Yellow = 4,
                Violet = 5,
                Magenta = 6
            };

            private const Byte Numofranges = 4;

            private Single fontBoundY1;
            private Single fontBoundY2;
            private Bitmap gaugeBitmap;
            private Boolean drawGaugeBackground = true;

            private Single mValue = 10;
            private readonly Boolean[] mValueIsInRange = { false, false, false, false, false };
            private Byte mCapIdx = 1;
            private readonly Color[] mCapColor = { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black };
            private readonly String[] mCapText = { "", "", "", "", "" };
            private readonly Point[] mCapPosition = { new Point(10, 10), new Point(10, 10), new Point(10, 10), new Point(10, 10), new Point(10, 10) };

            private Point mCenter = new Point(100, 100);
            private Single mMinValue = -150; //最小
            private Single mMaxValue = 900; //最大

            private Color mBaseArcColor = Color.Gray;
            private Int32 mBaseArcRadius = 80;//最外面 灰色的框框
            private Int32 mBaseArcStart = 135;//仪表开始的角度
            private Int32 mBaseArcSweep = 270;//旋转角度
            private Int32 mBaseArcWidth = 2;//最外面 灰色的框框的宽度

            private Color mScaleLinesInterColor = Color.Black;
            private Int32 mScaleLinesInterInnerRadius = 73;
            private Int32 mScaleLinesInterOuterRadius = 80;
            private Int32 mScaleLinesInterWidth = 1;

            private Int32 mScaleLinesMinorNumOf = 9;
            private Color mScaleLinesMinorColor = Color.Gray;
            private Int32 mScaleLinesMinorInnerRadius = 75;
            private Int32 mScaleLinesMinorOuterRadius = 80;
            private Int32 mScaleLinesMinorWidth = 1;

            private Single mScaleLinesMajorStepValue = 50.0f;
            private Color mScaleLinesMajorColor = Color.Black;
            private Int32 mScaleLinesMajorInnerRadius = 70;
            private Int32 mScaleLinesMajorOuterRadius = 80;
            private Int32 mScaleLinesMajorWidth = 2;

            //内外圈涂色属性
            private Byte mRangeIdx;
            private readonly Boolean[] mRangeEnabled = { true, true, true, true }; //是否填充
            private readonly Color[] mRangeColor = { Color.Red, Color.Yellow, Color.GreenYellow, Color.Green }; //填充色
            private readonly String[] mRangeText = { "D档次", "C档次", "B档次", "A档次" };
            private readonly Single[] mRangeStartValue = { 0.0f, 300.0f, 400.0f, 500.0f }; //间距涂色起点
            private readonly Single[] mRangeEndValue = { 300.0f, 400.0f, 500.0f, 750.0f }; //间距涂色终点
            private readonly Int32[] mRangeInnerRadius = { 70, 70, 70, 70, 70 }; //内圈涂色位置
            private readonly Int32[] mRangeOuterRadius = { 80, 80, 80, 80, 80 }; //外圈涂色位置

            //显示字体属性
            private Int32 mScaleNumbersRadius = 90; //字体显示位置
            private Color mScaleNumbersColor = Color.Black;
            private String mScaleNumbersFormat;
            private Int32 mScaleNumbersStartScaleLine;
            private Int32 mScaleNumbersStepScaleLines = 1;
            private Int32 mScaleNumbersRotation;

            //指针属性
            private Int32 mNeedleType = 1;
            private Int32 mNeedleRadius = 80;
            private NeedleColorEnum mNeedleColor1 = NeedleColorEnum.Gray;
            private Color mNeedleColor2 = Color.DimGray;
            private Int32 mNeedleWidth = 2;


            public class ValueInRangeChangedEventArgs : EventArgs
            {
                public Int32 ValueInRange;

                public ValueInRangeChangedEventArgs(Int32 valueInRange)
                {
                    ValueInRange = valueInRange;
                }
            }

            public delegate void ValueInRangeChangedDelegate(Object sender, ValueInRangeChangedEventArgs e);

            public event ValueInRangeChangedDelegate ValueInRangeChanged;

            #endregion

            #region properties

            public Boolean AllowDrop
            {
                get { return false; }
            }

            public Boolean AutoSize
            {
                get { return false; }
            }

            public Boolean ForeColor
            {
                get { return false; }
            }

            public Boolean ImeMode
            {
                get { return false; }
            }

            public Single Value
            {
                get { return mValue; }
                set
                {
                    if (!(Math.Abs(mValue - value) > 0)) return;
                    mValue = Math.Min(Math.Max(value, mMinValue), mMaxValue);
                    drawGaugeBackground = true;

                    for (var counter = 0; counter < mRangeStartValue.Length - 1; counter++)
                    {
                        if ((mRangeStartValue[counter] <= mValue) && (mValue <= mRangeEndValue[counter]) && (mRangeEnabled[counter]))
                        {
                            if (!mValueIsInRange[counter])
                            {
                                if (ValueInRangeChanged != null)
                                {
                                    ValueInRangeChanged(this, new ValueInRangeChangedEventArgs(counter));
                                }
                            }
                        }
                        else
                        {
                            mValueIsInRange[counter] = false;
                        }
                    }
                }
            }

            public Byte CapIdx
            {
                get { return mCapIdx; }
                set
                {
                    if ((mCapIdx != value) && (value < 5))
                    {
                        mCapIdx = value;
                    }
                }
            }

            public Color[] CapColors { get; set; }

            public String CapText
            {
                get { return mCapText[mCapIdx]; }
                set
                {
                    if (mCapText[mCapIdx] == value) return;
                    mCapText[mCapIdx] = value;
                    CapsText = mCapText;
                    drawGaugeBackground = true;
                }
            }
            public String[] CapsText
            {
                get { return mCapText; }
                set
                {
                    for (var counter = 0; counter < 5; counter++)
                    {
                        mCapText[counter] = value[counter];
                    }
                }
            }

            public Point CapPosition
            {
                get { return mCapPosition[mCapIdx]; }
                set
                {
                    if (mCapPosition[mCapIdx] == value) return;
                    mCapPosition[mCapIdx] = value;
                    CapsPosition = mCapPosition;
                    drawGaugeBackground = true;
                }
            }

            public Point[] CapsPosition { get; set; }


            public Point Center
            {
                get { return mCenter; }
                set
                {
                    if (mCenter == value) return;
                    mCenter = value;
                    drawGaugeBackground = true;
                }
            }

            public Single MinValue
            {
                get { return mMinValue; }
                set
                {
                    if ((mMinValue == value) || (!(value < mMaxValue))) return;
                    mMinValue = value;
                    drawGaugeBackground = true;
                }
            }

            public Single MaxValue
            {
                get { return mMaxValue; }
                set
                {
                    if ((mMaxValue == value) || (!(value > mMinValue))) return;
                    mMaxValue = value;
                    drawGaugeBackground = true;
                }
            }

            public Color BaseArcColor
            {
                get { return mBaseArcColor; }
                set
                {
                    if (mBaseArcColor == value) return;
                    mBaseArcColor = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 BaseArcRadius
            {
                get { return mBaseArcRadius; }
                set
                {
                    if (mBaseArcRadius == value) return;
                    mBaseArcRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 BaseArcStart
            {
                get { return mBaseArcStart; }
                set
                {
                    if (mBaseArcStart == value) return;
                    mBaseArcStart = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 BaseArcSweep
            {
                get { return mBaseArcSweep; }
                set
                {
                    if (mBaseArcSweep == value) return;
                    mBaseArcSweep = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 BaseArcWidth
            {
                get { return mBaseArcWidth; }
                set
                {
                    if (mBaseArcWidth == value) return;
                    mBaseArcWidth = value;
                    drawGaugeBackground = true;
                }
            }

            public Color ScaleLinesInterColor
            {
                get { return mScaleLinesInterColor; }
                set
                {
                    if (mScaleLinesInterColor == value) return;
                    mScaleLinesInterColor = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesInterInnerRadius
            {
                get { return mScaleLinesInterInnerRadius; }
                set
                {
                    if (mScaleLinesInterInnerRadius == value) return;
                    mScaleLinesInterInnerRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesInterOuterRadius
            {
                get { return mScaleLinesInterOuterRadius; }
                set
                {
                    if (mScaleLinesInterOuterRadius == value) return;
                    mScaleLinesInterOuterRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesInterWidth
            {
                get { return mScaleLinesInterWidth; }
                set
                {
                    if (mScaleLinesInterWidth == value) return;
                    mScaleLinesInterWidth = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesMinorNumOf
            {
                get { return mScaleLinesMinorNumOf; }
                set
                {
                    if (mScaleLinesMinorNumOf == value) return;
                    mScaleLinesMinorNumOf = value;
                    drawGaugeBackground = true;
                }
            }

            public Color ScaleLinesMinorColor
            {
                get { return mScaleLinesMinorColor; }
                set
                {
                    if (mScaleLinesMinorColor == value) return;
                    mScaleLinesMinorColor = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesMinorInnerRadius
            {
                get { return mScaleLinesMinorInnerRadius; }
                set
                {
                    if (mScaleLinesMinorInnerRadius == value) return;
                    mScaleLinesMinorInnerRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesMinorOuterRadius
            {
                get { return mScaleLinesMinorOuterRadius; }
                set
                {
                    if (mScaleLinesMinorOuterRadius == value) return;
                    mScaleLinesMinorOuterRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesMinorWidth
            {
                get { return mScaleLinesMinorWidth; }
                set
                {
                    if (mScaleLinesMinorWidth == value) return;
                    mScaleLinesMinorWidth = value;
                    drawGaugeBackground = true;
                }
            }

            public Single ScaleLinesMajorStepValue
            {
                get { return mScaleLinesMajorStepValue; }
                set
                {
                    if ((mScaleLinesMajorStepValue == value) || (!(value > 0))) return;
                    mScaleLinesMajorStepValue = Math.Max(Math.Min(value, mMaxValue), mMinValue);
                    drawGaugeBackground = true;
                }
            }

            public Color ScaleLinesMajorColor
            {
                get { return mScaleLinesMajorColor; }
                set
                {
                    if (mScaleLinesMajorColor == value) return;
                    mScaleLinesMajorColor = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesMajorInnerRadius
            {
                get { return mScaleLinesMajorInnerRadius; }
                set
                {
                    if (mScaleLinesMajorInnerRadius == value) return;
                    mScaleLinesMajorInnerRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesMajorOuterRadius
            {
                get { return mScaleLinesMajorOuterRadius; }
                set
                {
                    if (mScaleLinesMajorOuterRadius == value) return;
                    mScaleLinesMajorOuterRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleLinesMajorWidth
            {
                get { return mScaleLinesMajorWidth; }
                set
                {
                    if (mScaleLinesMajorWidth == value) return;
                    mScaleLinesMajorWidth = value;
                    drawGaugeBackground = true;
                }
            }

            public Byte RangeIdx
            {
                get { return mRangeIdx; }
                set
                {
                    if ((mRangeIdx == value) || (0 > value) || (value >= Numofranges)) return;
                    mRangeIdx = value;
                    drawGaugeBackground = true;
                }
            }

            public Boolean RangeEnabled
            {
                get { return mRangeEnabled[mRangeIdx]; }
                set
                {
                    if (mRangeEnabled[mRangeIdx] == value) return;
                    mRangeEnabled[mRangeIdx] = value;
                    RangesEnabled = mRangeEnabled;
                    drawGaugeBackground = true;
                }
            }
            public Boolean[] RangesEnabled { get; set; }

            public Color RangeColor
            {
                get { return mRangeColor[mRangeIdx]; }
                set
                {
                    if (mRangeColor[mRangeIdx] == value) return;
                    mRangeColor[mRangeIdx] = value;
                    RangesColor = mRangeColor;
                    drawGaugeBackground = true;
                }
            }
            public Color[] RangesColor { get; set; }

            public Single RangeStartValue
            {
                get { return mRangeStartValue[mRangeIdx]; }
                set
                {
                    if ((mRangeStartValue[mRangeIdx] == value) || (!(value < mRangeEndValue[mRangeIdx]))) return;
                    mRangeStartValue[mRangeIdx] = value;
                    RangesStartValue = mRangeStartValue;
                    drawGaugeBackground = true;
                }
            }
            public Single[] RangesStartValue { get; set; }

            public Single RangeEndValue
            {
                get { return mRangeEndValue[mRangeIdx]; }
                set
                {
                    if ((mRangeEndValue[mRangeIdx] == value) || (!(mRangeStartValue[mRangeIdx] < value))) return;
                    mRangeEndValue[mRangeIdx] = value;
                    RangesEndValue = mRangeEndValue;
                    drawGaugeBackground = true;
                }
            }
            public Single[] RangesEndValue { get; set; }

            public Int32 RangeInnerRadius
            {
                get { return mRangeInnerRadius[mRangeIdx]; }
                set
                {
                    if (mRangeInnerRadius[mRangeIdx] == value) return;
                    mRangeInnerRadius[mRangeIdx] = value;
                    RangesInnerRadius = mRangeInnerRadius;
                    drawGaugeBackground = true;
                }
            }
            public Int32[] RangesInnerRadius { get; set; }


            public Int32 RangeOuterRadius
            {
                get { return mRangeOuterRadius[mRangeIdx]; }
                set
                {
                    if (mRangeOuterRadius[mRangeIdx] == value) return;
                    mRangeOuterRadius[mRangeIdx] = value;
                    RangesOuterRadius = mRangeOuterRadius;
                    drawGaugeBackground = true;
                }
            }
            public Int32[] RangesOuterRadius { get; set; }


            public Int32 ScaleNumbersRadius
            {
                get { return mScaleNumbersRadius; }
                set
                {
                    if (mScaleNumbersRadius == value) return;
                    mScaleNumbersRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public Color ScaleNumbersColor
            {
                get { return mScaleNumbersColor; }
                set
                {
                    if (mScaleNumbersColor == value) return;
                    mScaleNumbersColor = value;
                    drawGaugeBackground = true;
                }
            }

            public String ScaleNumbersFormat
            {
                get { return mScaleNumbersFormat; }
                set
                {
                    if (mScaleNumbersFormat == value) return;
                    mScaleNumbersFormat = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleNumbersStartScaleLine
            {
                get { return mScaleNumbersStartScaleLine; }
                set
                {
                    if (mScaleNumbersStartScaleLine == value) return;
                    mScaleNumbersStartScaleLine = Math.Max(value, 1);
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleNumbersStepScaleLines
            {
                get { return mScaleNumbersStepScaleLines; }
                set
                {
                    if (mScaleNumbersStepScaleLines == value) return;
                    mScaleNumbersStepScaleLines = Math.Max(value, 1);
                    drawGaugeBackground = true;
                }
            }

            public Int32 ScaleNumbersRotation
            {
                get { return mScaleNumbersRotation; }
                set
                {
                    if (mScaleNumbersRotation == value) return;
                    mScaleNumbersRotation = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 NeedleType
            {
                get { return mNeedleType; }
                set
                {
                    if (mNeedleType == value) return;
                    mNeedleType = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 NeedleRadius
            {
                get { return mNeedleRadius; }
                set
                {
                    if (mNeedleRadius == value) return;
                    mNeedleRadius = value;
                    drawGaugeBackground = true;
                }
            }

            public NeedleColorEnum NeedleColor1
            {
                get { return mNeedleColor1; }
                set
                {
                    if (mNeedleColor1 == value) return;
                    mNeedleColor1 = value;
                    drawGaugeBackground = true;
                }
            }

            public Color NeedleColor2
            {
                get { return mNeedleColor2; }
                set
                {
                    if (mNeedleColor2 == value) return;
                    mNeedleColor2 = value;
                    drawGaugeBackground = true;
                }
            }

            public Int32 NeedleWidth
            {
                get { return mNeedleWidth; }
                set
                {
                    if (mNeedleWidth == value) return;
                    mNeedleWidth = value;
                    drawGaugeBackground = true;
                }
            }

            #endregion

            #region 设置字体

            private void FindFontBounds()
            {
                //find upper and lower bounds for numeric characters
                Int32 c2;
                var backBrush = new SolidBrush(Color.White);
                var foreBrush = new SolidBrush(Color.Black);

                var font = new Font("宋体", 12);

                var b = new Bitmap(5, 5);
                var g = Graphics.FromImage(b);
                var boundingBox = g.MeasureString("0123456789", font, -1, StringFormat.GenericTypographic);
                b = new Bitmap((Int32)(boundingBox.Width), (Int32)(boundingBox.Height));
                g = Graphics.FromImage(b);
                g.FillRectangle(backBrush, 0.0F, 0.0F, boundingBox.Width, boundingBox.Height);
                g.DrawString("0123456789", font, foreBrush, 0.0F, 0.0F, StringFormat.GenericTypographic);

                fontBoundY1 = 0;
                fontBoundY2 = 0;
                var c1 = 0;
                var boundfound = false;
                while ((c1 < b.Height) && (!boundfound))
                {
                    c2 = 0;
                    while ((c2 < b.Width) && (!boundfound))
                    {
                        if (b.GetPixel(c2, c1) != backBrush.Color)
                        {
                            fontBoundY1 = c1;
                            boundfound = true;
                        }
                        c2++;
                    }
                    c1++;
                }

                c1 = b.Height - 1;
                boundfound = false;
                while ((0 < c1) && (!boundfound))
                {
                    c2 = 0;
                    while ((c2 < b.Width) && (!boundfound))
                    {
                        if (b.GetPixel(c2, c1) != backBrush.Color)
                        {
                            fontBoundY2 = c1;
                            boundfound = true;
                        }
                        c2++;
                    }
                    c1--;
                }
            }

            #endregion

            #region base member

            public string YibiaoPic(ImagePaint imgp)
            {
                Value = imgp.Value;
                imgp.Font = new Font("宋体", 9);
                if (drawGaugeBackground)
                {
                    drawGaugeBackground = false;
                    FindFontBounds();

                    gaugeBitmap = new Bitmap(imgp.Width, imgp.Height);
                    var ggr = Graphics.FromImage(gaugeBitmap);
                    imgp.Graphic = Graphics.FromImage(gaugeBitmap);
                    ggr.FillRectangle(new SolidBrush(imgp.BackColor), imgp.ClientRectangle);
                    ggr.SmoothingMode = SmoothingMode.HighQuality;
                    ggr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    var gp = new GraphicsPath();

                    ImageBack1(gp, ggr, imgp);
                    ImageBack2(gp, ggr, imgp);

                    if (mScaleNumbersRotation != 0)
                    {
                        ggr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                    }

                    for (var counter = 0; counter < mCapText.Length; counter++)
                    {
                        if (mCapText[counter] != "")
                        {
                            ggr.DrawString(mCapText[counter], imgp.Font, new SolidBrush(mCapColor[counter]),mCapPosition[counter].X, mCapPosition[counter].Y,StringFormat.GenericTypographic);
                        }
                    }
                    ggr.Dispose();
                    gp.Dispose();
                }
               var url = Line(imgp);
               gaugeBitmap.Dispose();
               return url;
            }

            //画刻度
            private void ImageBack1(GraphicsPath gp, Graphics ggr, ImagePaint imgp)
            {
                for (var counter = 0; counter < mRangeEndValue.Length; counter++)
                {
                    if (!(mRangeEndValue[counter] > mRangeStartValue[counter]) || !mRangeEnabled[counter])
                    {
                        continue;
                    }

                    var rangeStartAngle = mBaseArcStart +(mRangeStartValue[counter] - mMinValue) * mBaseArcSweep /(mMaxValue - mMinValue);
                    var rangeSweepAngle = (mRangeEndValue[counter] - mRangeStartValue[counter]) * mBaseArcSweep / (mMaxValue - mMinValue);
                    gp.Reset();
                    gp.AddPie(
                        new Rectangle(mCenter.X - mRangeOuterRadius[counter],
                                      mCenter.Y - mRangeOuterRadius[counter], 2 * mRangeOuterRadius[counter],
                                      2 * mRangeOuterRadius[counter]), rangeStartAngle, rangeSweepAngle);
                    gp.Reverse();
                    gp.AddPie(
                        new Rectangle(mCenter.X - mRangeInnerRadius[counter],
                                      mCenter.Y - mRangeInnerRadius[counter], 2 * mRangeInnerRadius[counter],
                                      2 * mRangeInnerRadius[counter]), rangeStartAngle, rangeSweepAngle);
                    gp.Reverse();
                    ggr.SetClip(gp);
                    //填充
                    ggr.FillPie(new SolidBrush(mRangeColor[counter]),
                                new Rectangle(mCenter.X - mRangeOuterRadius[counter],
                                              mCenter.Y - mRangeOuterRadius[counter],
                                              2 * mRangeOuterRadius[counter], 2 * mRangeOuterRadius[counter]),
                                rangeStartAngle, rangeSweepAngle);
                }
                ggr.SetClip(imgp.ClientRectangle);

                if (mBaseArcRadius > 0)
                {
                    ggr.DrawArc(new Pen(mBaseArcColor, mBaseArcWidth),
                                new Rectangle(mCenter.X - mBaseArcRadius, mCenter.Y - mBaseArcRadius,
                                              2 * mBaseArcRadius, 2 * mBaseArcRadius), mBaseArcStart, mBaseArcSweep);
                }
            }

            private void ImageBack2(GraphicsPath gp, Graphics ggr, ImagePaint imgp)
            {
                float countValue = 0;
                var counter1 = 0;
                while (countValue <= (mMaxValue - mMinValue))
                {
                    var valueText = (mMinValue + countValue).ToString(mScaleNumbersFormat);
                    ggr.ResetTransform();
                    var boundingBox = ggr.MeasureString(valueText, imgp.Font, -1, StringFormat.GenericTypographic);

                    gp.Reset();
                    gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesMajorOuterRadius,
                                                mCenter.Y - mScaleLinesMajorOuterRadius,
                                                2 * mScaleLinesMajorOuterRadius, 2 * mScaleLinesMajorOuterRadius));
                    gp.Reverse();
                    gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesMajorInnerRadius,
                                                mCenter.Y - mScaleLinesMajorInnerRadius,
                                                2 * mScaleLinesMajorInnerRadius, 2 * mScaleLinesMajorInnerRadius));
                    gp.Reverse();
                    ggr.SetClip(gp);

                    ggr.DrawLine(new Pen(mScaleLinesMajorColor, mScaleLinesMajorWidth),
                                 (Center.X), (Center.Y),
                                 (Single)
                                 (Center.X +
                                  2 * mScaleLinesMajorOuterRadius *
                                  Math.Cos((mBaseArcStart + countValue * mBaseArcSweep / (mMaxValue - mMinValue)) * Math.PI /
                                           180.0)),
                                 (Single)
                                 (Center.Y +
                                  2 * mScaleLinesMajorOuterRadius *
                                  Math.Sin((mBaseArcStart + countValue * mBaseArcSweep / (mMaxValue - mMinValue)) * Math.PI /
                                           180.0)));

                    gp.Reset();
                    gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesMinorOuterRadius,
                                                mCenter.Y - mScaleLinesMinorOuterRadius,
                                                2 * mScaleLinesMinorOuterRadius, 2 * mScaleLinesMinorOuterRadius));
                    gp.Reverse();
                    gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesMinorInnerRadius,
                                                mCenter.Y - mScaleLinesMinorInnerRadius,
                                                2 * mScaleLinesMinorInnerRadius, 2 * mScaleLinesMinorInnerRadius));
                    gp.Reverse();
                    ggr.SetClip(gp);


                    ImageBack3(countValue, gp, ggr, imgp);

                    if (mScaleNumbersRotation != 0)
                    {
                        ggr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        ggr.RotateTransform(90.0F + mBaseArcStart + countValue * mBaseArcSweep / (mMaxValue - mMinValue));
                    }
                    ggr.TranslateTransform(
                        (Single)
                        (Center.X +
                         mScaleNumbersRadius *
                         Math.Cos((mBaseArcStart + countValue * mBaseArcSweep / (mMaxValue - mMinValue)) * Math.PI / 180.0f)),
                        (Single)
                        (Center.Y +
                         mScaleNumbersRadius *
                         Math.Sin((mBaseArcStart + countValue * mBaseArcSweep / (mMaxValue - mMinValue)) * Math.PI / 180.0f)),
                        MatrixOrder.Append);

                    if (counter1 >= ScaleNumbersStartScaleLine - 1)
                    {
                        ggr.DrawString(valueText, imgp.Font, new SolidBrush(mScaleNumbersColor), -boundingBox.Width / 2,
                                       -fontBoundY1 - (fontBoundY2 - fontBoundY1 + 1) / 2,
                                       StringFormat.GenericTypographic);
                    }

                    countValue += mScaleLinesMajorStepValue;
                    counter1++;
                }
                ggr.ResetTransform();
                ggr.SetClip(imgp.ClientRectangle);


            }

            public void ImageBack3(Single countValue, GraphicsPath gp, Graphics ggr, ImagePaint imgp)
            {
                if (countValue < (mMaxValue - mMinValue))
                {
                    for (var counter2 = 1; counter2 <= mScaleLinesMinorNumOf; counter2++)
                    {
                        if (((mScaleLinesMinorNumOf % 2) == 1) &&
                            ((mScaleLinesMinorNumOf / 2) + 1 == counter2))
                        {
                            gp.Reset();
                            gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesInterOuterRadius,
                                                        mCenter.Y - mScaleLinesInterOuterRadius,
                                                        2 * mScaleLinesInterOuterRadius,
                                                        2 * mScaleLinesInterOuterRadius));
                            gp.Reverse();
                            gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesInterInnerRadius,
                                                        mCenter.Y - mScaleLinesInterInnerRadius,
                                                        2 * mScaleLinesInterInnerRadius,
                                                        2 * mScaleLinesInterInnerRadius));
                            gp.Reverse();
                            ggr.SetClip(gp);

                            ggr.DrawLine(new Pen(mScaleLinesInterColor, mScaleLinesInterWidth),
                                         (Center.X),
                                         (Center.Y),
                                         (Single)
                                         (Center.X +
                                          2 * mScaleLinesInterOuterRadius *
                                          Math.Cos((mBaseArcStart +
                                                    countValue * mBaseArcSweep / (mMaxValue - mMinValue) +
                                                    counter2 * mBaseArcSweep /
                                                    ((
                                                         ((mMaxValue - mMinValue) / mScaleLinesMajorStepValue)) *
                                                     (mScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)),
                                         (Single)
                                         (Center.Y +
                                          2 * mScaleLinesInterOuterRadius *
                                          Math.Sin((mBaseArcStart +
                                                    countValue * mBaseArcSweep / (mMaxValue - mMinValue) +
                                                    counter2 * mBaseArcSweep /
                                                    ((
                                                         ((mMaxValue - mMinValue) / mScaleLinesMajorStepValue)) *
                                                     (mScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)));

                            gp.Reset();
                            gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesMinorOuterRadius,
                                                        mCenter.Y - mScaleLinesMinorOuterRadius,
                                                        2 * mScaleLinesMinorOuterRadius,
                                                        2 * mScaleLinesMinorOuterRadius));
                            gp.Reverse();
                            gp.AddEllipse(new Rectangle(mCenter.X - mScaleLinesMinorInnerRadius,
                                                        mCenter.Y - mScaleLinesMinorInnerRadius,
                                                        2 * mScaleLinesMinorInnerRadius,
                                                        2 * mScaleLinesMinorInnerRadius));
                            gp.Reverse();
                            ggr.SetClip(gp);
                        }
                        else
                        {
                            ggr.DrawLine(new Pen(mScaleLinesMinorColor, mScaleLinesMinorWidth),
                                         (Center.X),
                                         (Center.Y),
                                         (Single)
                                         (Center.X +
                                          2 * mScaleLinesMinorOuterRadius *
                                          Math.Cos((mBaseArcStart +
                                                    countValue * mBaseArcSweep / (mMaxValue - mMinValue) +
                                                    counter2 * mBaseArcSweep /
                                                    ((
                                                         ((mMaxValue - mMinValue) / mScaleLinesMajorStepValue)) *
                                                     (mScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)),
                                         (Single)
                                         (Center.Y +
                                          2 * mScaleLinesMinorOuterRadius *
                                          Math.Sin((mBaseArcStart +
                                                    countValue * mBaseArcSweep / (mMaxValue - mMinValue) +
                                                    counter2 * mBaseArcSweep /
                                                    ((
                                                         ((mMaxValue - mMinValue) / mScaleLinesMajorStepValue)) *
                                                     (mScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)));
                        }
                    }
                }
                ggr.SetClip(imgp.ClientRectangle);
            }

            private string Line(ImagePaint imgp)
            {
                var brushAngle = (Int32) (mBaseArcStart + (mValue - mMinValue)*mBaseArcSweep/(mMaxValue - mMinValue))%
                                 360;
                var needleAngle = brushAngle*Math.PI/180;

                var points = new PointF[3];
                var brush1 = Brushes.White;
                var brush2 = Brushes.White;
                var brush3 = Brushes.White;
                var brush4 = Brushes.White;

                var subcol = (((brushAngle + 225)%180)*100/180);
                var subcol2 = (((brushAngle + 135)%180)*100/180);

                imgp.Graphic = Graphics.FromImage(gaugeBitmap);
                imgp.Graphic.FillEllipse(new SolidBrush(mNeedleColor2), Center.X - mNeedleWidth*3,
                                         Center.Y - mNeedleWidth*3, mNeedleWidth*6, mNeedleWidth*6);
                switch (mNeedleColor1)
                {
                    case NeedleColorEnum.Gray:
                        brush1 = new SolidBrush(Color.FromArgb(80 + subcol, 80 + subcol, 80 + subcol));
                        brush2 = new SolidBrush(Color.FromArgb(180 - subcol, 180 - subcol, 180 - subcol));
                        brush3 = new SolidBrush(Color.FromArgb(80 + subcol2, 80 + subcol2, 80 + subcol2));
                        brush4 = new SolidBrush(Color.FromArgb(180 - subcol2, 180 - subcol2, 180 - subcol2));
                        imgp.Graphic.DrawEllipse(Pens.Gray, Center.X - mNeedleWidth*3, Center.Y - mNeedleWidth*3,
                                                 mNeedleWidth*6, mNeedleWidth*6);
                        break;
                    case NeedleColorEnum.Red:
                        brush1 = new SolidBrush(Color.FromArgb(145 + subcol, subcol, subcol));
                        brush2 = new SolidBrush(Color.FromArgb(245 - subcol, 100 - subcol, 100 - subcol));
                        brush3 = new SolidBrush(Color.FromArgb(145 + subcol2, subcol2, subcol2));
                        brush4 = new SolidBrush(Color.FromArgb(245 - subcol2, 100 - subcol2, 100 - subcol2));
                        imgp.Graphic.DrawEllipse(Pens.Red, Center.X - mNeedleWidth*3, Center.Y - mNeedleWidth*3,
                                                 mNeedleWidth*6, mNeedleWidth*6);
                        break;
                    case NeedleColorEnum.Green:
                        brush1 = new SolidBrush(Color.FromArgb(subcol, 145 + subcol, subcol));
                        brush2 = new SolidBrush(Color.FromArgb(100 - subcol, 245 - subcol, 100 - subcol));
                        brush3 = new SolidBrush(Color.FromArgb(subcol2, 145 + subcol2, subcol2));
                        brush4 = new SolidBrush(Color.FromArgb(100 - subcol2, 245 - subcol2, 100 - subcol2));
                        imgp.Graphic.DrawEllipse(Pens.Green, Center.X - mNeedleWidth*3, Center.Y - mNeedleWidth*3,
                                                 mNeedleWidth*6, mNeedleWidth*6);
                        break;
                    case NeedleColorEnum.Blue:
                        brush1 = new SolidBrush(Color.FromArgb(subcol, subcol, 145 + subcol));
                        brush2 = new SolidBrush(Color.FromArgb(100 - subcol, 100 - subcol, 245 - subcol));
                        brush3 = new SolidBrush(Color.FromArgb(subcol2, subcol2, 145 + subcol2));
                        brush4 = new SolidBrush(Color.FromArgb(100 - subcol2, 100 - subcol2, 245 - subcol2));
                        imgp.Graphic.DrawEllipse(Pens.Blue, Center.X - mNeedleWidth*3, Center.Y - mNeedleWidth*3,
                                                 mNeedleWidth*6, mNeedleWidth*6);
                        break;
                    case NeedleColorEnum.Magenta:
                        brush1 = new SolidBrush(Color.FromArgb(subcol, 145 + subcol, 145 + subcol));
                        brush2 = new SolidBrush(Color.FromArgb(100 - subcol, 245 - subcol, 245 - subcol));
                        brush3 = new SolidBrush(Color.FromArgb(subcol2, 145 + subcol2, 145 + subcol2));
                        brush4 = new SolidBrush(Color.FromArgb(100 - subcol2, 245 - subcol2, 245 - subcol2));
                        imgp.Graphic.DrawEllipse(Pens.Magenta, Center.X - mNeedleWidth*3, Center.Y - mNeedleWidth*3,
                                                 mNeedleWidth*6, mNeedleWidth*6);
                        break;
                    case NeedleColorEnum.Violet:
                        brush1 = new SolidBrush(Color.FromArgb(145 + subcol, subcol, 145 + subcol));
                        brush2 = new SolidBrush(Color.FromArgb(245 - subcol, 100 - subcol, 245 - subcol));
                        brush3 = new SolidBrush(Color.FromArgb(145 + subcol2, subcol2, 145 + subcol2));
                        brush4 = new SolidBrush(Color.FromArgb(245 - subcol2, 100 - subcol2, 245 - subcol2));
                        imgp.Graphic.DrawEllipse(Pens.Violet, Center.X - mNeedleWidth*3, Center.Y - mNeedleWidth*3,
                                                 mNeedleWidth*6, mNeedleWidth*6);
                        break;
                    case NeedleColorEnum.Yellow:
                        brush1 = new SolidBrush(Color.FromArgb(145 + subcol, 145 + subcol, subcol));
                        brush2 = new SolidBrush(Color.FromArgb(245 - subcol, 245 - subcol, 100 - subcol));
                        brush3 = new SolidBrush(Color.FromArgb(145 + subcol2, 145 + subcol2, subcol2));
                        brush4 = new SolidBrush(Color.FromArgb(245 - subcol2, 245 - subcol2, 100 - subcol2));
                        imgp.Graphic.DrawEllipse(Pens.Yellow, Center.X - mNeedleWidth * 3, Center.Y - mNeedleWidth * 3,
                                                 mNeedleWidth*6, mNeedleWidth*6);
                        break;
                }

                if (Math.Floor((Single) (((brushAngle + 225)%360)/180.0)) == 0)
                {
                    var brushBucket = brush1;
                    brush1 = brush2;
                    brush2 = brushBucket;
                }

                if (Math.Floor((Single) (((brushAngle + 135)%360)/180.0)) == 0)
                {
                    brush4 = brush3;
                }

                points[0].X = (Single) (Center.X + mNeedleRadius*Math.Cos(needleAngle));
                points[0].Y = (Single) (Center.Y + mNeedleRadius*Math.Sin(needleAngle));
                points[1].X = (Single) (Center.X - mNeedleRadius/20*Math.Cos(needleAngle));
                points[1].Y = (Single) (Center.Y - mNeedleRadius/20*Math.Sin(needleAngle));
                points[2].X =
                    (Single)
                    (Center.X - mNeedleRadius/5*Math.Cos(needleAngle) + mNeedleWidth*2*Math.Cos(needleAngle + Math.PI/2));
                points[2].Y =
                    (Single)
                    (Center.Y - mNeedleRadius/5*Math.Sin(needleAngle) + mNeedleWidth*2*Math.Sin(needleAngle + Math.PI/2));
                imgp.Graphic.FillPolygon(brush1, points);

                points[2].X =
                    (Single)
                    (Center.X - mNeedleRadius/5*Math.Cos(needleAngle) + mNeedleWidth*2*Math.Cos(needleAngle - Math.PI/2));
                points[2].Y =
                    (Single)
                    (Center.Y - mNeedleRadius/5*Math.Sin(needleAngle) + mNeedleWidth*2*Math.Sin(needleAngle - Math.PI/2));
                imgp.Graphic.FillPolygon(brush2, points);

                points[0].X = (Single) (Center.X - (mNeedleRadius/20 - 1)*Math.Cos(needleAngle));
                points[0].Y = (Single) (Center.Y - (mNeedleRadius/20 - 1)*Math.Sin(needleAngle));
                points[1].X =
                    (Single)
                    (Center.X - mNeedleRadius/5*Math.Cos(needleAngle) + mNeedleWidth*2*Math.Cos(needleAngle + Math.PI/2));
                points[1].Y =
                    (Single)
                    (Center.Y - mNeedleRadius/5*Math.Sin(needleAngle) + mNeedleWidth*2*Math.Sin(needleAngle + Math.PI/2));
                points[2].X =
                    (Single)
                    (Center.X - mNeedleRadius/5*Math.Cos(needleAngle) + mNeedleWidth*2*Math.Cos(needleAngle - Math.PI/2));
                points[2].Y =
                    (Single)
                    (Center.Y - mNeedleRadius/5*Math.Sin(needleAngle) + mNeedleWidth*2*Math.Sin(needleAngle - Math.PI/2));
                imgp.Graphic.FillPolygon(brush4, points);

                points[0].X = (Single) (Center.X - mNeedleRadius/20*Math.Cos(needleAngle));
                points[0].Y = (Single) (Center.Y - mNeedleRadius/20*Math.Sin(needleAngle));
                points[1].X = (Single) (Center.X + mNeedleRadius*Math.Cos(needleAngle));
                points[1].Y = (Single) (Center.Y + mNeedleRadius*Math.Sin(needleAngle));

                imgp.Graphic.DrawLine(new Pen(mNeedleColor2), Center.X, Center.Y, points[0].X, points[0].Y);
                imgp.Graphic.DrawLine(new Pen(mNeedleColor2), Center.X, Center.Y, points[1].X, points[1].Y);

                var rect = new Rectangle(10, 10, 40, 15); //定义矩形,参数为起点横
                for (var i = 0; i < mRangeColor.Count(); i++)
                {
                    rect.Location=new Point(210,20*i+40);
                    var b1 = new SolidBrush(mRangeColor[i]);//定义单色画刷
                    var b2 = new SolidBrush(Color.Black);//定义单色画刷
                    imgp.Graphic.FillRectangle(b1, rect);
                    imgp.Graphic.DrawString(mRangeText[i], new Font("宋体", 8, FontStyle.Bold), b2, new PointF(214, 20 * i + 42)); 
                }

                var url = Utils.UrlPath("仪表盘", "GIF");
                gaugeBitmap.Save(url, ImageFormat.Gif);

                return url;
            }

            #endregion

        }
    }
}
