using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Arrow : Sprite
	{
		private Vec2 _position;

		public Arrow(Player pTarget) : base("Sprites/Arrow.png")
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;

			_position = Vec2.zero;

			x = _position.x;
			y = _position.y;
		}

		public Vec2 position
		{
			set
			{
				_position = value ?? Vec2.zero;
			}
			get
			{
				return _position;
			}
		}

		public void Step()
		{
			x = _position.x;
			y = _position.y;
		}
	}
}
