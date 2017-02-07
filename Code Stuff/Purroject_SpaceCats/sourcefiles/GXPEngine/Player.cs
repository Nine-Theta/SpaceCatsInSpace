using System;

namespace GXPEngine
{
	public class Player : Ball
	{
		//private Vec2 _position;
		//private Vec2 _velocity;
		//private Vec2 _acceleration;

		public Player(int pRadius, Vec2 pPosition = null) : base(pRadius, pPosition)
		{
			
		}

		public override void Step()
		{
			base.Step();
		}
	}
}
