using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public class Alignment
    {
        public Alignment(HorizontalAlignment horizontal = HorizontalAlignment.Center, VerticalAlignment vertical = VerticalAlignment.Middle)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }

        public HorizontalAlignment Horizontal { get; set; }
        public VerticalAlignment Vertical { get; set; }
    }

    public enum FitType
    {
        Fit,
        Fill,
        //FillClip
    }

    public interface IDrawable : IBehavior
    {
        IScreenSize _ImageSize { get; set; }

        string ResourceUrl { get; }
        string ResourceName { get; }

        [Default("0")]
        int ZIndex { get; }

        [Default("1, 1")]
        GameSize Size { get; }
        [Default("0, 0")]
        GamePoint Anchor { get; }
        [Default("0, 0")]
        GamePoint Position { get; }

        [Default("Fit")]
        FitType FitType { get; }
        [Default("Middle, Center")]
        Alignment Alignment { get; }
    }

}
