//
// BlockBrush.cs
//
// Author:
//       Cameron White <cameronwhite91@gmail.com>
//
// Copyright (c) 2013 Cameron White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Mono.Addins;
using Pinta.Core;

namespace BlockBrush
{
	public class BlockBrush : BasePaintBrush
	{
		/// <summary>
		/// The name of the brush.
		/// </summary>
		public override string Name => AddinManager.CurrentLocalizer.GetString ("Block");

		/// <summary>
		/// Event handler called when the mouse is moved. This method is where
		/// the brush should perform its drawing.
		/// </summary>
		/// <param name="g">The current Cairo drawing context.</param>
		/// <param name="surface">Image surface to draw on.</param>
		/// <param name="strokeArgs">Information about the current stroke and mouse movement.</param>
		/// <returns>A rectangle containing the area of the canvas that should be redrawn.</returns>
		protected override RectangleI OnMouseMove (Cairo.Context g,
							   Cairo.ImageSurface surface,
							   BrushStrokeArgs strokeArgs)
		{
			// Use the brush width as the width of the block.
			double width = g.LineWidth;

			// When moving the brush horizontally, avoid having a zero-height line.
			PointI last = strokeArgs.LastPosition;
			PointI pos = strokeArgs.CurrentPosition;
			if (strokeArgs.LastPosition.Y == pos.Y)
				pos = pos with { Y = pos.Y + 1 };

			// Draw a parallelogram.
			g.MoveTo (last.X - width, last.Y);
			g.LineTo (last.X + width, last.Y);
			g.LineTo (pos.X + width, pos.Y);
			g.LineTo (pos.X - width, pos.Y);
			g.LineTo (last.X - width, last.Y);

			var dirty = g.StrokeExtents ().ToInt ();
			g.Fill ();
			return dirty;
		}
	}
}

