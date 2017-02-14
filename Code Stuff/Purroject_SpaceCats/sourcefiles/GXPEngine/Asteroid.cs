using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class Asteroid : Ball
	{
		//Vec2 _position;
		private Vec2 _velocity;
		private Sprite _sprite;
		private AnimSprite _crushedSprite;
		private int _counter = 3;
		private bool _crushed;

		public Asteroid(int pRadius, Vec2 pPosVec) : base((int)(pRadius * 0.2), pPosVec)
		{
			_sprite = new Sprite("Sprites/AsteroidTemp.png");
			_sprite.SetOrigin(_sprite.width / 2, _sprite.height / 2);
			AddChild(_sprite);
			SetScaleXY(0.2f);
			alpha = 0.1f;
			_position = pPosVec;
			_velocity = new Vec2();
		}

		public void AddVelocity(Vec2 vec)
		{
			_velocity.Add(vec);
		}

		public void Step()
		{
			_position.Add(_velocity);

			if (_velocity.Length() > 5.0f)
			{
				//Crush();
			}

			x = _position.x;
			y = _position.y;

			if (_crushed)
			{
				_counter--;
				if (_counter <= 0)
				{
					_counter = 3;
					if (_crushedSprite.currentFrame < _crushedSprite.frameCount)
					{
						_crushedSprite.NextFrame();
					}
					else
					{
						_sprite.Destroy();
						_crushedSprite.Destroy();
						this.Destroy();
					}
				}
			}
		}

		public void Crush()
		{
			_crushed = true;
			_crushedSprite = new AnimSprite("AsteroidTemp.png", 1, 1);
			_sprite.alpha = 0.0f;
		}
	}
}
