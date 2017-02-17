using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class Asteroid : Ball
	{
		//Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;
		private AnimSprite _crushedSprite;
		private LevelManager _levelRef;

		private int _counter = 3;
		public bool crushed;

		public Asteroid(int pRadius, Vec2 pPosVec, bool isAsteroid = true) : base((int)(pRadius * 0.2), pPosVec)
		{
			if (isAsteroid){
				_crushedSprite = new AnimSprite("Sprites/astroid sprite.png", 10, 1);
				_crushedSprite.SetOrigin(_crushedSprite.width / 2, _crushedSprite.height / 2);
				AddChild(_crushedSprite);
			}
			SetScaleXY(0.2f);
			alpha = 0.1f;
			position = pPosVec;
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

		public LevelManager levelRef{
			set{
				_levelRef = value;
			}
		}

		public virtual void Step(){
			_velocity.Add(_acceleration);
			position.Add(_velocity);

			x = position.x;
			y = position.y;

			_acceleration = Vec2.zero;

			if (!_velocity.EqualsTo(Vec2.zero) && !crushed){
				ExtremelyBasicBoundaryCollsion();
			}

			if (_levelRef != null && _levelRef.planetList != null)
			{
				for (int i = 0; i < _levelRef.planetList.Length; i++)
				{
					Planet planet = _levelRef.planetList[i];
					if (planet != null)
					{
						Vec2 deltaVec = position.Clone().Subtract(planet.posVec);
						if (planet.hitball.radius + radius > deltaVec.Length())
						{
							Vec2 normalDelta = deltaVec.Clone().Normalize();
							_velocity.Reflect(normalDelta, 1).Scale(0.8f);
							Crush();
						}
					}
				}
			}

			if (crushed){
				_counter--;
				if (_counter <= 0){
					_counter = 3;
					if (_crushedSprite.currentFrame < _crushedSprite.frameCount - 1){
						_crushedSprite.NextFrame();
					}
					else{
						_crushedSprite.alpha = 0.0f;
						_crushedSprite.Destroy();
						this.Destroy();
					}
				}
			}
		}

		public void Crush(){
			crushed = true;
		}

		private void ExtremelyBasicBoundaryCollsion()//Asteroid Edition
		{
			bool leftHit = (x - radius) < -10;
			bool rightHit = (x + radius) > 650;
			bool topHit = (y - radius) < -1;
			bool bottomHit = (y + radius) > 6510;

			if (leftHit || rightHit || topHit || bottomHit){
				if (leftHit){
					velocity.Scale(-1, 1);
				}
				if (rightHit){
					velocity.Scale(-1, 1);
				}
				if (topHit){
					velocity.Scale(1, -1);
				}
				if (bottomHit){
					velocity.Scale(1, -1);
				}
			}
		}
	}
}
