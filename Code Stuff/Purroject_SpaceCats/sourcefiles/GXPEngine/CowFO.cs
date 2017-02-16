using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class CowFO : Ball
	{
		private Vec2 _velocity;
		private Vec2 _acceleration;

		public CowFO(int pRadius, Vec2 pPosition) : base((int)(pRadius * 0.2), pPosition)
		{
			position = pPosition;
			_velocity = Vec2.zero;
			_acceleration = Vec2.zero;
		}

		public Vec2 velocity
		{
			set{
				_velocity = value ?? Vec2.zero;
			}
			get{
				return _velocity;
			}
		}
		public Vec2 acceleration
		{
			set{
				_acceleration = value ?? Vec2.zero;
			}
			get{
				return _acceleration;
			}
		}

		public void Step()
		{
			_velocity.Add(_acceleration);
			position.Add(_velocity);

			x = position.x;
			y = position.y;

		}
	}
}
