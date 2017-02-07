using System;
using System.Drawing;

namespace GXPEngine
{
	public class Ball : Canvas
	{
		public readonly int radius;
		protected Vec2 _position;
		//private Vec2 _velocity;
		//private Vec2 _acceleration;

		private Color _ballColor;

		public Ball(int pRadius, Vec2 pPosition = null, Color? pColor = null) : base(pRadius * 2, pRadius * 2)
		{
			radius = pRadius;
			position = pPosition;
			SetOrigin(radius, radius);
			_ballColor = pColor ?? Color.DeepPink;

			draw();
			x = position.x;
			y = position.y;
		}

		private void draw()
		{
			graphics.Clear(Color.Empty);
			graphics.FillEllipse(
				new SolidBrush(_ballColor),
				0, 0, 2 * radius, 2 * radius
			);
		}

		public Color ballColor
		{
			get{
				return _ballColor;
			}
			set{
				_ballColor = value;
				draw();
			}
		}

		public Vec2 position
		{
			set{
				_position = value ?? Vec2.zero;
			}
			get{
				return _position;
			}
		}
	}
}