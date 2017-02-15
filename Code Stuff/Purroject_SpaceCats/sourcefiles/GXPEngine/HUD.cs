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
		private Digit _decaCats;
		private Digit _flatCats;
		private Digit _decaScore;
		private Digit _flatScore;
		private int _score;
		private int _cats;
		private int _time;


		public HUD(int pWidth, int pHeight) : base(pWidth, pHeight)
		{
			_hudSprite = new Sprite("Sprites/HUD.png");
			AddChild(_hudSprite);
			_hectaSecond = new Digit(144.0f);
			_decaSecond = new Digit(166.0f);
			_flatSecond = new Digit(188.0f);
			_decaCats = new Digit(350.0f);
			_flatCats = new Digit(372.0f);
			_decaScore = new Digit(570.0f);
			_flatScore = new Digit(592.0f);
			AddChild(_hectaSecond);
			AddChild(_decaSecond);
			AddChild(_flatSecond);
			AddChild(_decaCats);
			AddChild(_flatCats);
			AddChild(_decaScore);
			AddChild(_flatScore);
		}

		//TODO: Fix this so it doesn force it every time. Too much calculations
		public void Step()
		{
			UpdateScore();
			UpdateTime();
			UpdateCats();
		}

		public void SetTime(int pTime)
		{
			_time = pTime;
		}
		public void SetCats(int pCats)
		{
			_cats = pCats;
		}
		public void SetScore(int pScore)
		{
			_score = pScore;
		}

		private void UpdateScore()
		{
			int firstDigit = (_score - (_score % 10)) / 10;
			int secondDigit = _score % 10;
			_decaScore.SetNumber(firstDigit);
			_flatScore.SetNumber(secondDigit);
		}
		private void UpdateCats()
		{
			int firstDigit = (_cats - (_cats % 10)) / 10;
			int secondDigit = _cats % 10;
			_decaCats.SetNumber(firstDigit);
			_flatCats.SetNumber(secondDigit);
		}
		private void UpdateTime()
		{
			int firstDigit = (_time - (_time % 100)) / 100;
			Console.WriteLine("FirstDigit" + firstDigit);
			int secondDigit = (_time - (_time % 10) - ((_time / 100) * 100)) / 10;
			Console.WriteLine("secondDigit" + secondDigit);
			int thirdDigit = _time % 10;
			Console.WriteLine("thirdDigit" + thirdDigit);
			_hectaSecond.SetNumber(firstDigit);
			_decaSecond.SetNumber(secondDigit);
			_flatSecond.SetNumber(thirdDigit);
		}
	}
}
