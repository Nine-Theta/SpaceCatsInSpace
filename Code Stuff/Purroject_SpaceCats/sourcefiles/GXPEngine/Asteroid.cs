using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class Asteroid : Ball
	{
		//Vec2 _position;
		private Vec2 _velocity;
		private AnimSprite _crushedSprite;
		private int _counter = 3;
		private bool _crushed;

		public Asteroid(int pRadius, Vec2 pPosVec) : base((int)(pRadius * 0.2), pPosVec)
		{
			_crushedSprite = new AnimSprite("Sprites/astroid sprite.png", 10,1);
			_crushedSprite.SetOrigin(_crushedSprite.width / 2, _crushedSprite.height / 2);
			AddChild(_crushedSprite);
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

			if (_velocity.Length() > 3.0f && !_crushed)
			{
				Crush();
			}

			x = _position.x;
			y = _position.y;

			if (_crushed)
			{
				_counter--;
				if (_counter <= 0)
				{
					_counter = 3;
					if (_crushedSprite.currentFrame < _crushedSprite.frameCount - 1)
					{
						_crushedSprite.NextFrame();
					}
					else
					{
						_crushedSprite.alpha = 0.0f;
						_crushedSprite.Destroy();
						this.Destroy();
					}
				}
			}
		}

		public void Crush()
		{
			_crushed = true;
		}
	}
}
