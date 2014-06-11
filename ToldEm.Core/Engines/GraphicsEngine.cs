﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToldEm.Core
{
    public class GraphicsEngine
    {
        public void DrawGraphics(IHost _host, IGame _game, bool IsDebugEnabled)
        {
            var drawables = _game.Entities.Where(e => e.IsDrawable).OrderBy(d => d.ZIndex).Cast<IDrawableInner>().ToList();

            // Draw graphics
            _host.GraphicsProvider.BeginFrame();

            var sSize = _host.GraphicsProvider.ScreenSize;
            var squareSize = Math.Min(sSize.Width, sSize.Height);
            var unitSize = squareSize / 2.0;

            // Debug
            if (IsDebugEnabled)
            {
                _host.GraphicsProvider.DrawDebugRectangle(new ScreenRect()
                {
                    Point = new ScreenPoint()
                    {
                        X = (sSize.Width - squareSize) / 2.0,
                        Y = (sSize.Height - squareSize) / 2.0
                    },
                    Size = new ScreenSize()
                    {
                        Width = squareSize,
                        Height = squareSize
                    }
                });
            }

            drawables.ForEach(d =>
            {
                var name = d.ResourceUrl;

                if (d._ImageSize == null)
                {
                    d._ImageSize = _host.GraphicsProvider.GetImageSize(d.ResourceUrl);
                }

                var gameBounds = d.GetBounds();

                // Size
                var wTarget = gameBounds.Width * unitSize;
                var hTarget = gameBounds.Height * unitSize;

                var wRatio = wTarget / d._ImageSize.Width;
                var hRatio = hTarget / d._ImageSize.Height;
                var ratio = wRatio;

                if (d.FitType == FitType.Fit)
                {
                    ratio = Math.Min(wRatio, hRatio);
                }
                else
                {
                    ratio = Math.Max(wRatio, hRatio);
                }

                var wActual = d._ImageSize.Width * ratio;
                var hActual = d._ImageSize.Height * ratio;

                var s = new ScreenSize() { Width = wActual, Height = hActual };

                // Alignment
                var widthGap = wTarget - wActual;
                var heightGap = hTarget - hActual;

                var offsetLeft = d.Alignment.Horizontal == HorizontalAlignment.Left ? 0.0
                    : d.Alignment.Horizontal == HorizontalAlignment.Center ? (widthGap) * 0.5
                    : widthGap;

                var offsetTop = d.Alignment.Vertical == VerticalAlignment.Top ? 0.0
                    : d.Alignment.Vertical == VerticalAlignment.Middle ? (heightGap) * 0.5
                    : heightGap;

                // Top Left
                var screenLeft = (gameBounds.Left * unitSize) + (sSize.Width / 2.0);
                var screenTop = (-gameBounds.Top * unitSize) + (sSize.Height / 2.0);

                var p = new ScreenPoint() { X = screenLeft + offsetLeft, Y = screenTop + offsetTop };

                _host.GraphicsProvider.DrawImage(name, new ScreenRect() { Point = p, Size = s });

                // Debug
                if (IsDebugEnabled)
                {
                    // Position Rect
                    var pos = new ScreenPoint()
                    {
                        X = ((d as IPlaceable).Position.X * unitSize) + (sSize.Width / 2.0),
                        Y = (-(d as IPlaceable).Position.Y * unitSize) + (sSize.Height / 2.0)
                    };

                    _host.GraphicsProvider.DrawDebugRectangle(new ScreenRect() { Point = pos, Size = new ScreenSize() { Width = 1, Height = 10 } });
                    _host.GraphicsProvider.DrawDebugRectangle(new ScreenRect() { Point = pos, Size = new ScreenSize() { Width = 10, Height = 1 } });

                    // Image Rect
                    _host.GraphicsProvider.DrawDebugRectangle(new ScreenRect() { Point = p, Size = s });

                    // Box Rect
                    var boxTopLeft = new ScreenPoint() { X = screenLeft, Y = screenTop };
                    var boxSize = new ScreenSize() { Width = wTarget, Height = hTarget };
                    _host.GraphicsProvider.DrawDebugRectangle(new ScreenRect() { Point = boxTopLeft, Size = boxSize });
                }
            });

            _host.GraphicsProvider.EndFrame();
        }

        
    }
}