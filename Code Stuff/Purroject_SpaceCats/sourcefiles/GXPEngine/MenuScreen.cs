using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class MenuScreen : Canvas
	{
		private Sprite _background;
		//TODO: Make actual buttons that actually work, uncomment those below
		//private Button _startButton;
		//private Button _scoresButton;
		//private Button _optionsButton;
		//private Button _exitButton;

		public MenuScreen(int pWidth, int pHeight) : base(pWidth, pHeight)
		{
			_background = new Sprite("Background.png");
		}
	}
}
