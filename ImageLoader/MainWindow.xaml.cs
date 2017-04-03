using FCP2.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageLoader
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            StartFCP2Client();
        }

        private void StartFCP2Client()
        {
            Client.Instance.Protocol.NodeHelloEvent += OnNodeHello;
            Client.Instance.Protocol.ClientHello();
        }

        private void OnMenuExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private static bool IsImageKey(string line)
        {
            return line.StartsWith("CHK@") && line.EndsWith(".jpg");
        }

        private void OnPaste(object sender, RoutedEventArgs e)
        {
            using (var reader = new StringReader(Clipboard.GetText(TextDataFormat.UnicodeText)))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    if (IsImageKey(line))
                    {
                        var item = new ImageItem(line);
                        ImageList.Items.Add(item);
                        Dispatcher.Invoke(() => { CursorPositionLabel.Text = $"File Queued: {item.Filename}"; });
                    }
                }
            }
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Client.Instance.Protocol.Disconnect();
        }

        private void OnImageSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedImage = ImageList.SelectedItem as ImageItem;
            if (selectedImage != null && selectedImage.Image != null)
            {
                MainImage.Source = selectedImage.Image;
            }
            else
            {
                MainImage.Source = null;
            }
        }

        private void OnImageListMouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageList.Focus();
        }

        private void OnNodeHello(object sender, FCP2.EventArgs.NodeHelloEventArgs e)
        {
            Dispatcher.Invoke(() => { CursorPositionLabel.Text = $"Connection Established with: {e.Version}"; });
        }

    }
}
