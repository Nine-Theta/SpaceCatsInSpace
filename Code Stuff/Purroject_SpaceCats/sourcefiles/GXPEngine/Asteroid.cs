using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class Asteroid : Ball
	{
		//Vec2 _position;
		Vec2 _velocity;
		Sprite _sprite;

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

			x = _position.x;
			y = _position.y;
		}
	}
}
