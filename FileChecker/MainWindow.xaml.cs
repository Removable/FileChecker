using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FileChecker.Util;
using Microsoft.Win32;

namespace FileChecker
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BtnFilePath.Click += SelectFile;
        }

        /// <summary>
        ///     MD5计算结果双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectText(object sender, MouseButtonEventArgs e)
        {
            var s = (TextBox) sender;
            s.SelectAll();
        }

        /// <summary>
        ///     选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFile(object sender, RoutedEventArgs e)
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                var filePath = open.FileName;
                if (!File.Exists(filePath)) MessageBox.Show("文件不存在！");

                CalcFileHash(filePath);
            }
        }

        /// <summary>
        ///     获得文件哈希值
        /// </summary>
        /// <param name="filePath"></param>
        public void CalcFileHash(string filePath)
        {
            try
            {
                //显示路径
                TbFilePath.Text = filePath;
                //显示进度条
                Pbar.Visibility = Visibility.Visible;
                Pbar.Value = 10;

                #region 异步计算哈希值

                var md5 = string.Empty;
                var sha1 = string.Empty;

                var bgw = new BackgroundWorker();
                bgw.WorkerReportsProgress = true;

                //要异步的内容
                bgw.DoWork += delegate
                {
                    md5 = GetFileHash.GetMd5(filePath);
                    bgw.ReportProgress(50);
                    sha1 = GetFileHash.GetSha1(filePath);
                    bgw.ReportProgress(100);
                };

                //进度报告
                bgw.ProgressChanged += (s, a) => { Pbar.Value = a.ProgressPercentage; };

                //后台工作完成后
                bgw.RunWorkerCompleted += delegate
                {
                    TbMd5.Text = md5;
                    TbSha1.Text = sha1;
                    Pbar.Visibility = Visibility.Hidden;
                };

                bgw.RunWorkerAsync();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["ArbitraryArgName"] != null)
            {
                var fName = Application.Current.Properties["ArbitraryArgName"].ToString(); //ArbitraryArgName要与APP里面设置的名字相同
                CalcFileHash(fName);
            }
        }
    }
}