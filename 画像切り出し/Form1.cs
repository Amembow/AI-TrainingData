using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 画像切り出し
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            string[] filename;
            try 
            {
                filename = Directory.GetFiles(path,"*.bmp",System.IO.SearchOption.AllDirectories);

                foreach (string f in filename)
                {
                    Console.WriteLine(f);
                    Bitmap bitmap = new Bitmap(f);
                    
                    var bmproi = ImageRoi(bitmap, new Rectangle(150, 80, 100, 100));
                    bmproi.Save(textBox2.Text+ "/unko.bmp",System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
            catch(Exception ee) 
            {
                Console.WriteLine(ee.Message);
            }

        }

        /// <summary>
        /// Bitmapの一部を切り出したBitmapオブジェクトを返す
        /// </summary>
        /// <param name="srcRect">元のBitmapクラスオブジェクト</param>
        /// <param name="roi">切り出す領域</param>
        /// <returns>切り出したBitmapオブジェクト</returns>
        public Bitmap ImageRoi(Bitmap src, Rectangle roi)
        {
            //////////////////////////////////////////////////////////////////////
            // srcRectとroiの重なった領域を取得（画像をはみ出した領域を切り取る）

            // 画像の領域
            var imgRect = new Rectangle(0, 0, src.Width, src.Height);
            // はみ出した部分を切り取る(重なった領域を取得)
            var roiTrim = Rectangle.Intersect(imgRect, roi);
            // 画像の外の領域を指定した場合
            if (roiTrim.IsEmpty == true) return null;

            //////////////////////////////////////////////////////////////////////
            // 画像の切り出し

            // 切り出す大きさと同じサイズのBitmapオブジェクトを作成
            var dst = new Bitmap(roiTrim.Width, roiTrim.Height, src.PixelFormat);
            // BitmapオブジェクトからGraphicsオブジェクトの作成
            var g = Graphics.FromImage(dst);
            // 描画先
            var dstRect = new Rectangle(0, 0, roiTrim.Width, roiTrim.Height);
            // 描画
            g.DrawImage(src, dstRect, roiTrim, GraphicsUnit.Pixel);



            // 解放
            g.Dispose();

            return dst;
        }


    }
}
