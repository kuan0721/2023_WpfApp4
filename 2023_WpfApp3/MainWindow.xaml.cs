using System;  // 引用.NET的基本System命名空間
using System.Linq;  // 引用Linq，用於集合數據查詢
using System.Windows;  // 引用WPF基本命名空間
using System.Windows.Controls;  // 引用WPF控制項，比如Button, Canvas等
using System.Windows.Input;  // 引用WPF的輸入相關類，例如MouseButtonEventArgs
using System.Windows.Media;  // 引用WPF的多媒體類，例如Brushes, Colors
using System.Windows.Shapes;  // 引用WPF的形狀類，例如Line, Ellipse等

namespace _2023_WpfApp3  // 定義命名空間
{
    public partial class MainWindow : Window  // MainWindow類，繼承自WPF的Window類
    {
        // 定義成員變數
        String shapeType = "Line";  // 繪圖形狀的類型，初始為"Line"
        Color strokeColor = Colors.Red;  // 線條顏色，初始為紅色
        Brush strokeBrush = new SolidColorBrush(Colors.Red);  // 線條的Brush，初始為紅色
        int strokeThickness = 1;  // 線條粗細，初始為1

        Point start, dest;  // 繪圖的起始點和結束點

        // 主窗口的構造方法
        public MainWindow()
        {
            InitializeComponent();  // 初始化XAML中定義的控制項
            strokeColorPicker.SelectedColor = strokeColor;  // 設定顏色選擇器的初始顏色
        }

        // 形狀選擇按鈕的事件處理方法
        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var targetRadioButton = sender as RadioButton;  // 把觸發事件的對象轉型為RadioButton
            shapeType = targetRadioButton.Tag.ToString();  // 從Tag屬性讀取形狀類型
        }

        // 線條粗細滑桿值改變的事件處理方法
        private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = Convert.ToInt32(strokeThicknessSlider.Value);  // 更新線條粗細的值
        }

        // 畫布上滑鼠移動的事件處理方法
        private void myCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas);  // 獲取當前滑鼠位置
            DisplayStatus();  // 更新狀態顯示

            if (e.LeftButton == MouseButtonState.Pressed)  // 判斷左鍵是否被按下
            {
                switch (shapeType)  // 根據當前的繪圖形狀類型
                {
                    case "Line":  // 如果是線條
                        var line = myCanvas.Children.OfType<Line>().LastOrDefault();  // 獲取最後添加的線條
                        line.X2 = dest.X;  // 設定線條的結束點X座標
                        line.Y2 = dest.Y;  // 設定線條的結束點Y座標
                        break;
                    case "Rectangle":  // 如果是矩形（這裡暫時沒有實現）
                        break;
                    case "Ellipse":  // 如果是橢圓（這裡暫時沒有實現）
                        break;
                }
            }
        }

        // 畫布上滑鼠左鍵按下的事件處理方法
        private void myCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);  // 獲取滑鼠按下時的位置
            myCanvas.Cursor = Cursors.Cross;  // 改變鼠標指針為十字形

            switch (shapeType)  // 根據選擇的繪圖形狀
            {
                case "Line":  // 如果是線條
                    DrawLine(Colors.Gray, 1);  // 畫一條灰色、粗細為1的線
                    break;
                case "Rectangle":  // 如果是矩形（這裡暫時沒有實現）
                    break;
                case "Ellipse":  // 如果是橢圓（這裡暫時沒有實現）
                    break;
            }
            DisplayStatus();  // 更新狀態顯示
        }

        // 顯示目前座標的方法
        private void DisplayStatus()
        {
            coordinateLabel.Content = $"座標點: ({Math.Round(start.X)}, {Math.Round(start.Y)}) : ({Math.Round(dest.X)}, {Math.Round(dest.Y)})";
        }

        // 畫布上滑鼠左鍵放開的事件處理方法（這裡暫時沒有實現）
        private void myCanvas_MouseLeftButtonUp(object sender
            , MouseButtonEventArgs e)
        {
        }

        // 畫線的方法
        private void DrawLine(Color color, int thickness)
        {
            Brush stroke = new SolidColorBrush(color);  // 創建筆刷
            Line line = new Line  // 創建Line對象
            {
                Stroke = stroke,  // 設定線條顏色
                X1 = start.X,  // 設定起始點X座標
                Y1 = start.Y,  // 設定起始點Y座標
                X2 = dest.X,  // 設定結束點X座標
                Y2 = dest.Y  // 設定結束點Y座標
            };
            myCanvas.Children.Add(line);  // 將線條添加到畫布的子對象中
        }
    }
}
