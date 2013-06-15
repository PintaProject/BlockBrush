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
		public override string Name {
			get { return AddinManager.CurrentLocalizer.GetString ("Block"); }
		}

		/// <summary>
		/// Event handler called when the mouse is moved. This method is where
		/// the brush should perform its drawing.
		/// </summary>
		/// <param name="g">The current Cairo drawing context.</param>
		/// <param name="strokeColor">The current stroke color.</param>
		/// <param name="surface">Image surface to draw on.</param>
		/// <param name="x">The current x coordinate of the mouse.</param>
		/// <param name="y">The current y coordinate of the mouse.</param>
		/// <param name="lastX">The previous x coordinate of the mouse.</param>
		/// <param name="lastY">The previous y coordinate of the mouse.</param>
		protected override Gdk.Rectangle OnMouseMove (Cairo.Context g, Cairo.Color strokeColor,
		                                              Cairo.ImageSurface surface,int x, int y,
		                                              int lastX, int lastY)
		{
			// Use the brush width as the width of the block.
			double width = g.LineWidth;

			// When moving the brush horizontally, avoid having a zero-height line.
			if (lastY == y)
				y++;

			// Draw a parallelogram.
			g.MoveTo (lastX - width, lastY);
			g.LineTo (lastX + width, lastY);
			g.LineTo (x + width, y);
			g.LineTo (x - width, y);
			g.LineTo (lastX - width, lastY);

			var dirty = g.FixedStrokeExtents ().ToGdkRectangle ();
			g.Fill ();
			return dirty;
		}
	}
}

