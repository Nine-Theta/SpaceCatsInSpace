using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class Asteroid : Sprite
	{
		Vec2 _position;
		Vec2 _velocity;

		public Asteroid(Vec2 pPosVec) : base("checkers.png")
		{
			_position = pPosVec;
			_velocity = new Vec2();
		}

		public void AddVelocity(Vec2 vec)
		{
			_velocity.Add(vec);
		}
	}
}
