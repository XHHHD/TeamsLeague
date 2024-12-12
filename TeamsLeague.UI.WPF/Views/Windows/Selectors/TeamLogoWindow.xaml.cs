using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.UI.WPF.Exceptions;

namespace TeamsLeague.UI.WPF.Views.Windows.Selectors
{
    public partial class TeamLogoWindow : Window
    {
        private const string FOLDER = "\\Resources\\Img\\Default\\Teams\\";

        public delegate void Choose(string imgRout);

        public event Choose? Done;


        public TeamLogoWindow()
        {
            InitializeComponent();
            SetImagesPanel();
        }


        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void SetImagesPanel()
        {
            try
            {
                var images = GetImages();

                //var panel = GetContainer(images);

                //Images_Viewer.Content = panel;
            }
            catch (FolderAccessException ex)
            {
                ShowExceptionMessage(ex.Message);
            }
        }

        private IDictionary<string, string> GetImages()
        {
            var currentDir = Path.GetFullPath(@"..\..\..\");
            var folder = string.Concat(currentDir, FOLDER);
            if (Directory.Exists(folder))
            {
                var files = Directory.GetFiles(folder);
                var imgDictionary = GetPathDictionary(files);
                return imgDictionary;
            }
            
            throw new FolderAccessException("Can't get access to the images folder!");
        }

        static Dictionary<string, string> GetPathDictionary(string[] files)
        {
            var pathDictionary = new Dictionary<string, string>();

            foreach (string filePath in files)
            {
                string shortenedPath = GetShortenedPath(filePath);
                pathDictionary.Add(shortenedPath, filePath);
            }

            return pathDictionary;
        }

        private static string GetShortenedPath(string fullPath)
        {
            string normalizedPath = Path.GetFullPath(fullPath);

            string relativePath = normalizedPath.Replace(Path.GetPathRoot(normalizedPath), string.Empty);

            return relativePath;
        }

        //private StackPanel GetContainer(IDictionary<string, string> images)
        //{
        //    var panel = GetPanel();

        //    for (int i = 0; i < images.Count; i++)
        //    {
        //        string image1 = images[i];
        //        i++;
        //        string? image2 = i < images.Count ? images[i] : null;

        //        .var line = GetLine(image1, image2);
        //        panel.Children.Add(line);
        //    }

        //    return panel;
        //}

        private StackPanel GetPanel() => new()
        {
            Orientation = Orientation.Vertical,
        };

        private Grid GetLine((string, string) rout1, (string, string)? rout2)
        {
            var panel = new Grid();

            var btn1 = GetImageBtn(rout1);
            btn1.HorizontalAlignment = HorizontalAlignment.Left;
            panel.Children.Add(btn1);

            if (rout2 is not null)
            {
                var btn2 = GetImageBtn(rout2.Value);
                btn2.HorizontalAlignment = HorizontalAlignment.Right;
                panel.Children.Add(btn2);
            }

            return panel;
        }

        private Button GetImageBtn((string, string) rout)
        {
            var img = GetImage(rout.Item2);

            var btn = new Button()
            {
                Content = img,
                Tag = rout.Item1,
                Margin = new Thickness(5),
            };

            btn.Click += (sender, e) =>
            {
                Done?.Invoke(rout.Item1);
                this.Close();
            };

            return btn;
        }

        private Image GetImage(string rout)
        {
            var uri = new Uri(rout);

            var img = new Image()
            {
                Source = new BitmapImage(uri),
                Height = 200,
                Width = 200,
            };

            return img;
        }

        private void ShowExceptionMessage(string message)
        {
            Images_Viewer.Content = new TextBlock
            {
                Text = message,
                FontWeight = FontWeights.Bold,
                FontSize = 20,
                Foreground = new SolidColorBrush(Color.FromRgb(37, 86, 99)),
            };
        }
    }
}
