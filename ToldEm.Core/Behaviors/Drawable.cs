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

    public interface IAlignment : IValue
    {
        [Default("HorizontalAlignment.Center")]
        HorizontalAlignment Horizontal { get; set; }
        [Default("VerticalAlignment.Middle")]
        VerticalAlignment Vertical { get; set; }
    }

    public enum FitType
    {
        Fit,
        Fill,
        //FillClip
    }

    public interface IDrawable : IBehavior
    {
        string ResourceUrl { get; }

        [Default("0")]
        int ZIndex { get; }

        [Default("Fit")]
        FitType FitType { get; }

        [Default("Center, Middle")]
        Alignment Alignment { get; }

        GameBounds GetBounds();
    }

    internal interface IDrawableInner : IDrawable
    {
        IScreenSize _ImageSize { get; set; }
    }


    // Tiling
    public enum TileDirection
    {
        Horizontal,
        Vertical,
    }

    public interface ITileable : IBehavior
    {
        TileDirection TileDirection { get; }
    }
}
