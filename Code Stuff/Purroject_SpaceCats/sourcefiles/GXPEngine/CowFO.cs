using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class CowFO : Asteroid //Our CFO needs RPC
	{
		private Vec2 _velocity;
		private Vec2 _acceleration;

		private float _rotationSpeed;

		private bool _isTouchable;

		public CowFO(int pRadius, Vec2 pPosition, float pRotationSpeed = 1.0f, bool pIsTouchable = false) : base(pRadius, pPosition, false)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.8f;

			position = pPosition;
			_velocity = Vec2.zero;
			_acceleration = Vec2.zero;
			_rotationSpeed = pRotationSpeed;
			_isTouchable = pIsTouchable;

			Sprite cow = new Sprite("Sprites/Cow.png");
			AddChild(cow);
			cow.SetOrigin(cow.width / 2, cow.height / 2);
		}

		public override void Step()
		{
			rotation += 1;
			base.Step();
		}
	}
}
