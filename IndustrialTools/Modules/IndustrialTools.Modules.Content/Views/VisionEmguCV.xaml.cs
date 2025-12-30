using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IndustrialTools.Modules.Content.Views
{
    /// <summary>
    /// VisionEmguCV.xaml 的交互逻辑
    /// </summary>
    public partial class VisionEmguCV : UserControl
    {
        public VisionEmguCV()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
              Mat imgscr = CvInvoke.Imread("E:\\GitHub\\IndustrialTools\\IndustrialTools\\" +
                  "Common\\IndustrialTools.Lib\\Images\\3.png");      //读取图像
              CvInvoke.Imshow("img", imgscr);                         //显示图像
           
        }
    }
}
