using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Digit : AnimSprite
	{
		private int _number = 0;
		public Digit(int pNumber) : base("Sprites/Menu/Digits.png", 10, 0)
		{
			_number = pNumber;
			SetFrame(_number);
		}

		public void changeNumber(int pNumber)
		{
			_number = pNumber;
			SetFrame(_number);
		}
	}
}
