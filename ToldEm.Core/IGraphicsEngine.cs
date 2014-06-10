using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public interface IGraphicsEngine
    {
        IScreenSize ScreenSize { get; }
        IScreenSize GetImageSize(string resourceName);

        void RegisterImageResource(string relativeUrl, string resourceName);
        void LoadImageResource(string resourceName);
        void UnloadImageResource(string resourceName);

        void DrawImage(string resourceName, IScreenRect position);
        void BeginFrame();
        void EndFrame();

        void DrawDebugRectangle(ScreenRect screenRect);
    }

    public interface IScreenRect
    {
        IScreenPoint Point { get; }
        IScreenSize Size { get; }
    }

    public interface IScreenPoint
    {
        double X { get; }
        double Y { get; }
    }

    public interface IScreenSize
    {
        double Width { get; }
        double Height { get; }
    }

    public class ScreenRect : IScreenRect
    {
        public IScreenPoint Point { get; set; }
        public IScreenSize Size { get; set; }
    }

    public class ScreenSize : IScreenSize
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class ScreenPoint : IScreenPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

}
