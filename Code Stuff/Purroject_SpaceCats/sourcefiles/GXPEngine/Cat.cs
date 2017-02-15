using System;

namespace GXPEngine
{
	public class Cat : Sprite
	{
		//This enum seems redundant, why not just use a boolean _disposable?
		public enum type { NORMAL, DISPOSABLE }

		//TODO: Implement new cat animation. It requires an Animsprite with 4 frames (4 col 1 row)
		private Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;
		private Vec2 _target;

		private type _catType;

		//private int _catID;

		public Cat(Player pTarget, type? pType = type.NORMAL, int? pCatID = -1) : base("Sprites/Cat.png")
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;

			_catType = pType ?? type.NORMAL;
			//_catID = pCatID ?? -1;

			_target = new Vec2(pTarget.x, pTarget.y);
			position = _target.Add(_target.Clone().Normalize().Scale(pTarget.radius));
			velocity = Vec2.zero;
			acceleration = Vec2.zero;
		}

		//public int GetCatID{
		//	get{
		//		return _catID;
		//	}
		//}

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

			if (_catType == type.DISPOSABLE)
			{

			}
		}
	}
}
