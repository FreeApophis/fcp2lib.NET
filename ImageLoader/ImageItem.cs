using FCP2.Protocol;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using System;

namespace ImageLoader
{
    class ImageItem : INotifyPropertyChanged
    {
        private long _id;
        private long _progress;
        private long _size;
        private string _key;
        private FCP2Protocol _client;
        private BitmapImage _image;
        public event PropertyChangedEventHandler PropertyChanged;

        public ImageItem(string key)
        {
            _key = key;
            _id = Client.Instance.RequestID;
            _client = Client.Instance.Protocol;

            _client.SimpleProgressEvent += OnProgress;
            _client.AllDataEvent += OnData;

            StartDownload();
        }

        public void StartDownload()
        {
            _client.ClientGet(_key, ID, verbosity: VerbosityEnum.SimpleMessages);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnProgress(object sender, FCP2.EventArgs.SimpleProgressEventArgs e)
        {
            if (e.Identifier == ID)
            {
                Progress = e.Succeeded * 100 / e.Required;
            }
        }

        private void OnData(object sender, FCP2.EventArgs.AllDataEventArgs e)
        {
            if (e.Identifier == ID)
            {
                MemoryStream stream = LoadImage(e.GetStream(), (int)e.Datalength);
                _image = GetBitmapImage(stream);
                _image.Freeze();

                Size = e.Datalength;
                Progress = 100;

                _client.SimpleProgressEvent -= OnProgress;
                _client.AllDataEvent -= OnData;
            }
        }

        private MemoryStream LoadImage(Stream stream, int bytesToRead)
        {
            MemoryStream ms = new MemoryStream(bytesToRead);
            var buffer = new byte[1024];

            while (bytesToRead > 0)
            {
                var bytesRead = stream.Read(buffer, 0, Math.Min(bytesToRead, buffer.Length));
                ms.Write(buffer, 0, bytesRead);
                bytesToRead -= bytesRead;
            }

            ms.Position = 0L;
            return ms;
        }

        public static BitmapImage GetBitmapImage(Stream stream)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public string ID => $"ImageLoader-{_id}";
        public string Key => _key;
        public string Filename => ExtractFilename();
        public string HumanSize => $"{Format.ByteSize(Size)}";
        public BitmapImage Image => _image;

        public long Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
                OnPropertyChanged(nameof(HumanSize));
            }
        }

        private string ExtractFilename()
        {
            return _key.Split('/').Last();
        }

    }
}
