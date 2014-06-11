using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToldEm.Core;

namespace ToldEm.WPF
{
    class WPFGraphicsProvider : IGraphicsProvider
    {
        private Canvas _target;
        private DrawingVisual _visual;
        private DrawingContext _context;
        private RenderTargetBitmap _buffer;
        private Image _bufferImage;
        private Pen _debugPen;

        public bool IsDebugEnabled { get; set; }

        private Dictionary<string, Uri> _imageUris;
        private Dictionary<string, ImageSource> _images;

        public WPFGraphicsProvider(Canvas target)
        {
            IsDebugEnabled = false;

            _imageUris = new Dictionary<string, Uri>();
            _images = new Dictionary<string, ImageSource>();

            _target = target;

            _visual = new DrawingVisual();


            _bufferImage = new Image();

            Action createImageBuffer = () =>
            {
                if (_target.ActualHeight < 1 || _target.ActualWidth < 1)
                {
                    return;
                }

                var pSource = PresentationSource.FromVisual(_target);
                var dpiX = 96.0 * pSource.CompositionTarget.TransformToDevice.M11;
                var dpiY = 96.0 * pSource.CompositionTarget.TransformToDevice.M22;

                _buffer = new RenderTargetBitmap((int)_target.ActualWidth, (int)_target.ActualHeight, dpiX, dpiY, PixelFormats.Pbgra32);
            };

            createImageBuffer();

            _target.SizeChanged += (sender, e) =>
            {
                createImageBuffer();
            };

            _target.Children.Add(_bufferImage);

            _debugPen = new Pen(Brushes.Red, 1);
        }

        public IScreenSize ScreenSize
        {
            get { return new ScreenSize() { Width = _target.ActualWidth, Height = _target.ActualHeight }; }
        }

        public void RegisterImageResource(string relativeUrl, string resourceName)
        {
            var baseUri = new Uri(System.Environment.CurrentDirectory + "/");
            var uri = new Uri(baseUri, relativeUrl);

            if (_imageUris.ContainsKey(resourceName))
            {
                if (_imageUris[resourceName] != uri)
                {
                    throw new ArgumentException(string.Format("Two resources with the same name '{0}' were added {1} and {2}", resourceName, uri.MakeRelativeUri(baseUri), relativeUrl));
                }
            }
            else
            {
                _imageUris.Add(resourceName, uri);
            }
        }

        public void LoadImageResource(string resourceName)
        {
            if (!_images.ContainsKey(resourceName))
            {
                var uri = _imageUris[resourceName];

                var s = new BitmapImage(uri);
                s.Freeze();
                _images.Add(resourceName, s);
            }
        }

        public void UnloadImageResource(string resourceName)
        {
            if (_images.ContainsKey(resourceName))
            {
                // TEST MEMORY: http://social.msdn.microsoft.com/Forums/vstudio/en-US/dee7cb68-aca3-402b-b159-2de933f933f1/disposing-a-wpf-image-or-bitmapimage-so-the-source-picture-file-can-be-modified?forum=wpf
                // The BitmapImage is a managed resource and will GC without calling dispose
                _images.Remove(resourceName);
            }
        }

        public void DrawImage(string resourceName, IScreenRect position)
        {
            var rect = new Rect(position.Point.X, position.Point.Y, position.Size.Width, position.Size.Height);

            var s = _images[resourceName];
            _context.DrawImage(s, rect);

            if (IsDebugEnabled)
            {
                _context.DrawRectangle(null, _debugPen, rect);
            }
        }

        public void DrawDebugRectangle(ScreenRect position)
        {
            var rect = new Rect(position.Point.X, position.Point.Y, position.Size.Width, position.Size.Height);
            _context.DrawRectangle(null, _debugPen, rect);
        }

        public void BeginFrame()
        {
            _context = _visual.RenderOpen();
            _context.DrawRectangle(Brushes.Black, null, new Rect(-10, -10, 10000, 10000));
        }

        public void EndFrame()
        {
            _context.Close();
            _context = null;

            _buffer.Render(_visual);

            if (_bufferImage.Source != _buffer)
            {
                _bufferImage.Source = _buffer;
            }

        }

        public IScreenSize GetImageSize(string resourceName)
        {
            var image = _images[resourceName] as BitmapImage;

            return new ScreenSize() { Width = image.PixelWidth, Height = image.PixelHeight };
        }



    }
}
