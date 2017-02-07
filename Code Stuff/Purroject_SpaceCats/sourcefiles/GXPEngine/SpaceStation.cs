using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class SpaceStation : Sprite
	{
		public SpaceStation(float pX, float pY) : base ("checkers.png")
		{
			SetXY(pX, pY);
			SetOrigin(width / 2, height / 2);
		}
	}
}
