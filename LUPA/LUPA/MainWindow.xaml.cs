﻿using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LUPA.DataContainers;
using System.Collections.Generic;
using System.Linq;

namespace LUPA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly SolidColorBrush keyPointColor = Brushes.IndianRed;
        public readonly SolidColorBrush contourPointColor = Brushes.LightSeaGreen;
        public readonly SolidColorBrush customObjectColor = Brushes.SaddleBrown;
        Map map;
        System.Windows.Point position;

        public MainWindow()
        {
            InitializeComponent();
            map = new Map();
        }

        /// <summary>
        /// Draws a Key/Contour Point (depends on which radio button is checked)
        /// </summary>
        private void Map_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            position = e.GetPosition(this);
            position.Y -= TopToolbar.ActualHeight;
            if (KeyPointBtn.IsChecked == true)
            {
                DrawPoint(keyPointColor, position);  // rysuje na canvasie
                AddKeyPoint(position);    //dodaje do mapy
            }
            else if (ContourPointBtn.IsChecked == true)
            {
                DeleteCurrentContour(); //usuwa obecny kontur
                DrawPoint(contourPointColor, position);  //rysuje na canvasie
                AddContourPoint(position);  //dodaje do mapy w dobrym miejscu
                DrawContourLinesInOrder();   //rysuje linie na podstawie mapy w kolejnosci            
            }
        }

        /// <summary>
        /// Removes a Key/Contour Point (depends on which radio button is checked)
        /// </summary>
        private void Map_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Rectangle clickedShape)
            {
                if (clickedShape.Stroke == keyPointColor && KeyPointBtn.IsChecked == true)
                {
                    Map.Children.Remove(clickedShape);   
                }
                if (clickedShape.Stroke == contourPointColor && ContourPointBtn.IsChecked == true)
                {
                    Map.Children.Remove(clickedShape);
                }
                RemovePointFromDataContainer(clickedShape);
            }
        }

        private double CalculateDistBetweenPoints(System.Windows.Point srcPt, System.Windows.Point endPt)
        {
            return Math.Sqrt(Math.Pow((endPt.X - srcPt.X), 2) + Math.Pow((endPt.Y - srcPt.Y), 2));
        }

        private double CalculateDistBetweenPoints(Point srcPt, Point endPt)
        {
            return Math.Sqrt(Math.Pow((endPt.X - srcPt.X), 2) + Math.Pow((endPt.Y - srcPt.Y), 2));
        }

        private double CalculateDistBetweenPoints(System.Windows.Point srcPt, Point endPt)
        {
            return Math.Sqrt(Math.Pow((endPt.X - srcPt.X), 2) + Math.Pow((endPt.Y - srcPt.Y), 2));
        }

        private void DeleteCurrentContour()
        {
            var contourLines = Map.Children.OfType<Line>().ToList();
            foreach (var line in contourLines)
            {
                Map.Children.Remove(line);
            }
        }
        /*
        private void DrawContourLines()
        {
            var contourPoints = GetContourPointsFromMap();
            Polygon p = new Polygon
            {
                Stroke = contourPointColor,
                StrokeThickness = 2,
                Points = contourPoints
            };
            Map.Children.Add(p);
        }

        private PointCollection GetContourPointsFromMap()
        {
            PointCollection pc = new PointCollection();
            var utilPoint = new System.Windows.Point(0, 0);
            var contourPts = Map.Children.OfType<Rectangle>().Where(x => x.Stroke == contourPointColor).ToList();

            foreach (var cp in contourPts)
            {
                double x, y;
                x = Canvas.GetLeft(cp);
                y = Canvas.GetTop(cp);
                pc.Add(new System.Windows.Point(x, y));
            }
            return pc;
        }
        */
        private void RemovePointFromDataContainer(Rectangle clickedShape)
        {
            double x = Canvas.GetLeft(clickedShape);
            double y = Canvas.GetTop(clickedShape) + TopToolbar.ActualHeight;
            map.KeyPoints.RemoveAll(o => o.X == x && o.Y == y);
            map.ContourPoints.RemoveAll(o => o.X == x && o.Y == y);
        }

        private void DrawPoint(Brush color, System.Windows.Point position)
        {           
            var rec = new Rectangle()
            {
                StrokeThickness = 5,
                Stroke = color
            };
            Map.Children.Add(rec);
            Canvas.SetTop(rec, position.Y);
            Canvas.SetLeft(rec, position.X);
        }

        private void DrawLine(Brush color, double srcPtX, double srcPtY, double endPtX, double endPtY)
        {
            Line line = new Line()
            {
                Stroke = color,
                StrokeThickness = 2,
                X1 = srcPtX,
                Y1 = srcPtY,
                X2 = endPtX,
                Y2 = endPtY
            };
            Map.Children.Add(line);
        }

        private void AddKeyPoint(System.Windows.Point position)
        {
            map.KeyPoints.Add(new KeyPoint(position.X, position.Y, "defaultKeyPointName"));
        }

        private void AddContourPoint(System.Windows.Point position)
        {
            var distances = new List<double>();
            double dist;
            foreach (var cp in map.ContourPoints)
            {
                dist = CalculateDistBetweenPoints(position, new System.Windows.Point(cp.X, cp.Y));
                distances.Add(dist);
            }
            distances.Sort();
            Point firstPt, scndPt;
            firstPt = FindDistantPoint(position, distances[0]);
            scndPt = FindDistantPoint(position, distances[1]);
            int firstIndex = map.ContourPoints.IndexOf(firstPt);
            int scndIndex = map.ContourPoints.IndexOf(scndPt);
            if (firstIndex < scndIndex)
            {
                map.ContourPoints.Insert(scndIndex, new Point(position.X, position.Y));
            }
            else
            {
                map.ContourPoints.Insert(firstIndex, new Point(position.X, position.Y));
            }
        }


        private Point FindDistantPoint(System.Windows.Point position, double distance)
        {
            Point point = new Point(0, 0);
            double dist;
            foreach (var cp in map.ContourPoints)
            {
                dist = CalculateDistBetweenPoints(position, cp);
                if (dist == distance)
                {
                    return cp;
                }
            }
            return point;
        }

        private void KeyPointBtn_Checked(object sender, RoutedEventArgs e)
        {
            KeyPointBtn.Background = keyPointColor;
            ContourPointBtn.Background = Brushes.Azure;
        }

        private void ContourBtn_Checked(object sender, RoutedEventArgs e)
        {
            ContourPointBtn.Background = contourPointColor;
            KeyPointBtn.Background = Brushes.Azure;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "TXT Files (*.txt)|*.txt",
            };

            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                Map.Children.Clear();
                map = Parser.ParseFile(ofd.FileName);
                DrawMap();
            }
        }

        private void DrawMap()
        {
            DrawContourPoints();
            DrawKeyPoints();
            DrawCustomObjects();
            DrawContourLinesInOrder();
        }

        private void DrawContourPoints()
        {
            System.Windows.Point position = new System.Windows.Point();
            foreach (var cp in map.ContourPoints)
            {               
                position.X = cp.X;
                position.Y = cp.Y;
                DrawPoint(contourPointColor, position);
            }
        }

        private void DrawKeyPoints()
        {
            System.Windows.Point position = new System.Windows.Point();
            foreach (var kp in map.KeyPoints)
            {
                position.X = kp.X;
                position.Y = kp.Y;
                DrawPoint(keyPointColor, position);
            }
        }

        private void DrawCustomObjects()
        {
            System.Windows.Point position = new System.Windows.Point();
            foreach (var co in map.CustomObjects)
            {
                position.X = co.X;
                position.Y = co.Y;
                DrawPoint(customObjectColor, position);
            }
        }
        
        private void DrawContourLinesInOrder()
        {
            double srcPtX, srcPtY, endPtX, endPtY;
            for (int i = 0; i < map.ContourPoints.Count - 1; i++)
            {
                srcPtX = map.ContourPoints[i].X;
                srcPtY = map.ContourPoints[i].Y;
                endPtX = map.ContourPoints[i + 1].X;
                endPtY = map.ContourPoints[i + 1].Y;
                DrawLine(contourPointColor, srcPtX, srcPtY, endPtX, endPtY);
            }
            DrawLastContourLine();
        }

        private void DrawLastContourLine()
        {
            double srcPtX, srcPtY, endPtX, endPtY;
            int totalContourPointsNum = map.ContourPoints.Count - 1;
            srcPtX = map.ContourPoints[0].X;
            srcPtY = map.ContourPoints[0].Y;
            endPtX = map.ContourPoints[totalContourPointsNum].X;
            endPtY = map.ContourPoints[totalContourPointsNum].Y;
            DrawLine(contourPointColor, srcPtX, srcPtY, endPtX, endPtY);
        }        

        private void ChangeBackground_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "PNG Files (*.png)|*.png",
            };

            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                ImageBrush ib = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(ofd.FileName, UriKind.Relative))
                };
                Map.Background = ib;
            }
        }
    }
}
