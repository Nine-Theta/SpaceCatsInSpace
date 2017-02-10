using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Arrow : Sprite
	{
		private Vec2 _position;
		private Vec2 _target;
		public Arrow(Player pTarget) : base("Arrow.png")
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;

			_target = new Vec2(pTarget.x, pTarget.y);
			_position = _target.Add(_target.Clone().Normalize().Scale(pTarget.radius));
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
