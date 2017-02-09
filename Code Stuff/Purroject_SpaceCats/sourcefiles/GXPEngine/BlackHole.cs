using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class BlackHole : Planet
	{
		public BlackHole(Vec2 pPosVec, int pRadius, int pGravityRange) : base(pPosVec, "Black Hole.png", pRadius, 1, pGravityRange, 20)
		{
		}
	}
}
