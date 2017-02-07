using System;

namespace GXPEngine
{
	public class Cat : Sprite
	{
		private Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;
		private Vec2 _target;

		public Cat(GameObject pTarget, int pTargetRadius) : base("square.png")
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;

			_target = new Vec2(pTarget.x, pTarget.y);
			position = _target.Add(_target.Clone().Normalize().Scale(pTargetRadius));
			velocity = Vec2.zero;
			acceleration = Vec2.zero;
		}

		public Vec2 position{
			set{
				_position = value ?? Vec2.zero;
			}
			get{
				return _position;
			}
		}

		public Vec2 velocity{
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

		public void Step(){
			_velocity.Add(_acceleration);
			_position.Add(_velocity);
			
			x = _position.x;
			y = _position.y;

			_acceleration = Vec2.zero;
		}
	}
}
