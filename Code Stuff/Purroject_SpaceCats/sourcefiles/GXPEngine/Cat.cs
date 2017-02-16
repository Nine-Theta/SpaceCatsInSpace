using System;

namespace GXPEngine
{
	public class Cat : AnimationSprite
	{
		//This enum seems redundant, why not just use a boolean _disposable?
		public enum type { NORMAL, DISPOSABLE }

		//TODO: Implement new cat animation. It requires an Animsprite with 4 frames (4 col 1 row)
		private Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;
		private Vec2 _target;
		private int _animTimer = 5;

		private type _catType;

		public Cat(Player pTarget, type? pType = type.NORMAL) : base("Cat-Spritesheet.png", 4, 1)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;
			SetFrame(0);

			_catType = pType ?? type.NORMAL;

			_target = new Vec2(pTarget.x, pTarget.y);
			position = Vec2.zero;
			rotation = position.Clone().GetAngleDegrees();
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

			if (_catType == type.DISPOSABLE && currentFrame < 3)
			{
				_animTimer--;
				if (_animTimer <= 0)
				{
					NextFrame();
					_animTimer = 5;
				}
			}
		}
	}
}
