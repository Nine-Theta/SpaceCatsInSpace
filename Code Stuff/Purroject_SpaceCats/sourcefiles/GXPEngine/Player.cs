using System;

namespace GXPEngine
{
	public class Player : Ball
	{
		//private Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;
		private float _gravityScale = 0.1f;
		private LevelManager _levelRef;

		public Player(int pRadius, Vec2 pPosition = null) : base(pRadius, pPosition)
		{
			position = pPosition;
			_velocity = Vec2.zero;
			_acceleration = Vec2.zero;
		}

		public Vec2 velocity{
			set{
				_velocity = value ?? Vec2.zero;
			}
			get{
				return _velocity;
			}
		}
		public Vec2 acceleration{
			set{
				_acceleration = value ?? Vec2.zero;
			}
			get{
				return _acceleration;
			}
		}
		public LevelManager levelRef
		{
			set{
				_levelRef = value;
			}
		}

		public void Step()
		{
			_velocity.Add(_acceleration);
			_position.Add(_velocity);

			x = _position.x;
			y = _position.y;

			_acceleration = Vec2.zero;

			//TODO: Fix this maybe some day in a near future (Wednesday pls)
			if (_levelRef != null &&_levelRef.planetList != null)
			{
				//foreach (Planet planet in _levelRef.planetList)
				for (int i = 0; i < _levelRef.planetList.Length; i++)
				{
					Planet planet = _levelRef.planetList[i];
					if (planet != null && planet.InRange(_position, radius))
					{
						Vec2 deltaVec = _position.Subtract(planet.posVec);
						_acceleration.Add(deltaVec.Normalize().Scale(_gravityScale));
					}
				}
			}

		}
	}
}
