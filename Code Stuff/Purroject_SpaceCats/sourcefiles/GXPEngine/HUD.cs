using System;
using GXPEngine; 

namespace Purroject_SpaceCats
{
	public class HUD : Canvas
	{
		private Sprite _hudSprite;
		private Digit _hectaSecond;
		private Digit _decaSecond;
		private Digit _flatSecond;
		private Digit _decaCat;
		private Digit _flatCat;
		private Digit _decaScore;
		private Digit _flatScore;
		public HUD(int pWidth, int pHeight) : base(pWidth, pHeight)
		{
			_hudSprite = new Sprite("Sprites/HUD.png");
			AddChild(_hudSprite);
		}

		//TODO: Add actual info
		void Update()
		{

		}
	}
}
