using System;
using GXPEngine;
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator

namespace Purroject_SpaceCats
{
	public class MenuScreen : Canvas
	{
		private const int LEVEL_SCREEN_OFFSET_PER_LEVEL = 200;
		private AnimSprite _titleScreen;
		private AnimSprite _background;
		private AnimSprite _mainScreen;
		private AnimSprite _ruleScreen;
		private AnimSprite _creditScreen;
		private AnimSprite _levelScreen;
		private Sprite _levelScreenBackground;
		private Sprite _levelScreenForeground;
		private AnimSprite _endScreen;
		private Digit _endDecaScore;
		private Digit _endFlatScore;
		private Digit _endHectaTime;
		private Digit _endDecaTime;
		private Digit _endFlatTime;
		private MyGame _gameRef;
		//inOtherScreen is for Screens that aren't the main menu. Pressing enter or space will take you back when true
		private bool _inOtherScreen = true;
		private int _selectedButton = 0;
		private int _backgroundTimer = 10;

		public MenuScreen(int pWidth, int pHeight) : base(pWidth, pHeight)
		{
			_titleScreen = new AnimSprite("Sprites/Menu/Title.png",  4, 1);
			AddChild(_titleScreen);
			_titleScreen.width = pWidth;
			_titleScreen.height = pHeight;
			_background = new AnimSprite("Sprites/Menu/Menu.png", 11, 1);
			AddChild(_background);
			_background.alpha = 0.0f;
			_mainScreen = new AnimSprite("Sprites/Menu/Menu Buttons.png", 4, 1);
			AddChild(_mainScreen);
			_mainScreen.alpha = 0.0f;
			_ruleScreen = new AnimSprite("Sprites/Menu/Rules.png", 8, 1);
			AddChild(_ruleScreen);
			_ruleScreen.alpha = 0.0f;
			_creditScreen = new AnimSprite("Sprites/Menu/Logo.png", 3, 1);
			AddChild(_creditScreen);
			_creditScreen.alpha = 0.0f;
			_endScreen = new AnimSprite("Sprites/Menu/Endscreen.png", 4, 1);
			AddChild(_endScreen);
			_endScreen.alpha = 0.0f;

			//Keep these 3 in order!
			_levelScreenBackground = new Sprite("Sprites/Menu/MapBackground.png");
			AddChild(_levelScreenBackground);
			_levelScreenBackground.alpha = 0.0f;
			_levelScreen = new AnimSprite("Sprites/Menu/Map.png", 7, 1);
			AddChild(_levelScreen);
			_levelScreen.alpha = 0.0f;
			_levelScreenForeground = new Sprite("Sprites/Menu/Menu Bars.png");
			AddChild(_levelScreenForeground);
			_levelScreenForeground.alpha = 0.0f;

			_endDecaScore = new Digit(260.0f, 547.0f);
			_endFlatScore = new Digit(282.0f, 547.0f);
			_endHectaTime = new Digit(460.0f, 547.0f);
			_endDecaTime = new Digit(482.0f, 547.0f);
			_endFlatTime = new Digit(504.0f, 547.0f);
			AddChild(_endDecaScore);
			AddChild(_endFlatScore);
			AddChild(_endHectaTime);
			AddChild(_endDecaTime);
			AddChild(_endFlatTime);
			_endDecaScore.alpha = 0.0f;
			_endFlatScore.alpha = 0.0f;
			_endHectaTime.alpha = 0.0f;
			_endDecaTime.alpha = 0.0f;
			_endFlatTime.alpha = 0.0f;
		}

		public void Step()
		{
			GetInput();
			AnimateBackground();
		}

		void GetInput()
		{
			if (_levelScreen.alpha == 1.0f)
			{
				if (Input.GetKeyDown(Key.DOWN) && _selectedButton > 0)
				{
					_selectedButton--;
					_levelScreen.SetFrame(_selectedButton);
					_levelScreen.y = -height + _levelScreen.currentFrame * LEVEL_SCREEN_OFFSET_PER_LEVEL;
				}
				if (Input.GetKeyDown(Key.UP) && _selectedButton < 6)
				{
					_selectedButton++;
					_levelScreen.SetFrame(_selectedButton);
					_levelScreen.y = -height + _levelScreen.currentFrame * LEVEL_SCREEN_OFFSET_PER_LEVEL;
				}
			}
			if (_mainScreen.alpha == 1.0f)
			{
				if (Input.GetKeyDown(Key.UP) && _selectedButton > 0)
				{
					_selectedButton--;
					_mainScreen.SetFrame(_selectedButton);
				}
				if (Input.GetKeyDown(Key.DOWN) && _selectedButton < 3)
				{
					_selectedButton++;
					_mainScreen.SetFrame(_selectedButton);
				}
			}
			if(Input.GetKeyDown(Key.SPACE) || Input.GetKeyDown(Key.ENTER))
			{
				if (!_inOtherScreen)
				{
					switch (_selectedButton)
					{
						case 0:
							_mainScreen.alpha = 0.0f;
							_levelScreen.alpha = 1.0f;
							_levelScreenBackground.alpha = 1.0f;
							_levelScreenForeground.alpha = 1.0f;
							_levelScreen.SetFrame(0);
							_levelScreen.y = -height + _levelScreen.currentFrame * LEVEL_SCREEN_OFFSET_PER_LEVEL;
							_inOtherScreen = true;
							break;
						case 1:
							Rules();
							break;
						case 2:
							Credits();
							break;
						case 3:
							Exit();
							break;
						default:
							Console.WriteLine("Shit be broke yo " + _selectedButton);
							break;
					}
				}
				else if (_levelScreen.alpha == 1.0f)
				{
					switch (_selectedButton)
					{
						case 0:
							StartGame(_selectedButton);
							break;
						case 1:
							StartGame(_selectedButton);
							break;
						case 2:
							StartGame(_selectedButton);
							break;
						case 3:
							StartGame(_selectedButton);
							break;
						case 4:
							StartGame(_selectedButton);
							break;
						case 5:
							StartGame(_selectedButton);
							break;
						case 6:
							StartGame(_selectedButton);
							break;
						default:
							Console.WriteLine("Shit be broke yo " + _selectedButton);
							break;
					}
				}
				else
				{
					_inOtherScreen = false;
					_titleScreen.alpha = 0.0f;
					_background.alpha = 1.0f;
					_mainScreen.alpha = 1.0f;
					_levelScreen.alpha = 0.0f;
					_levelScreenBackground.alpha = 0.0f;
					_levelScreenForeground.alpha = 0.0f;
					_ruleScreen.alpha = 0.0f;
					_creditScreen.alpha = 0.0f;
					_endScreen.alpha = 0.0f;
					_backgroundTimer = 5;
					_endDecaScore.alpha = 0.0f;
					_endFlatScore.alpha = 0.0f;
					_endHectaTime.alpha = 0.0f;
					_endDecaTime.alpha = 0.0f;
					_endFlatTime.alpha = 0.0f;
				}
			}
		}

		void AnimateBackground()
		{
			_backgroundTimer--;
			if (_backgroundTimer <= 0)
			{
				if (_mainScreen.alpha == 1.0f)
				{
					_backgroundTimer = 10;
					_background.NextFrame();
				}
				if (_ruleScreen.alpha == 1.0f)
				{
					_backgroundTimer = 10;
					_ruleScreen.NextFrame();
				}
				if (_creditScreen.alpha == 1.0f)
				{
					_creditScreen.NextFrame();
					_backgroundTimer = 5;
					if (_creditScreen.currentFrame == 0)
					{
						_backgroundTimer += 35;
					}
				}
				if (_titleScreen.alpha == 1.0f)
				{
					_titleScreen.NextFrame();
					_backgroundTimer = 10;
				}
			}
		}

		void StartGame(int pLevel)
		{
			this.alpha = 0.0f;
			_gameRef.InitializeGame(pLevel);
		}
		void Rules()
		{
			_ruleScreen.alpha = 1.0f;
			_inOtherScreen = true;
		}
		void Credits()
		{
			_creditScreen.alpha = 1.0f;
			_inOtherScreen = true;
		}
		void Exit()
		{
			_gameRef.Destroy();
		}

		public void SetGameRef(MyGame game)
		{
			_gameRef = game;
		}
		public void ShowEndScreen(int pScore, int pTime, bool pWon)
		{
			this.alpha = 1.0f;
			_inOtherScreen = true;
			_titleScreen.alpha = 0.0f;
			_background.alpha = 0.0f;
			_mainScreen.alpha = 0.0f;
			_levelScreen.alpha = 0.0f;
			_ruleScreen.alpha = 0.0f;
			_levelScreenForeground.alpha = 0.0f;
			_levelScreenBackground.alpha = 0.0f;
			_creditScreen.alpha = 0.0f;
			_endScreen.alpha = 1.0f;
			if (pWon)
			{
				if (pScore >= 12)
				{
					_endScreen.SetFrame(3);
				}
				else if (pScore >= 8)
				{
					_endScreen.SetFrame(2);
				}
				else
				{
					_endScreen.SetFrame(1);
				}
				AssignScores(pScore, pTime);
			}
			else
			{
				_endScreen.SetFrame(0);
			}
			_selectedButton = 0;
			
		}

		private void AssignScores(int pScore, int pTime)
		{
			_endDecaScore.alpha = 1.0f;
			_endFlatScore.alpha = 1.0f;
			UpdateScore(pScore);
			_endHectaTime.alpha = 1.0f;
			_endDecaTime.alpha = 1.0f;
			_endFlatTime.alpha = 1.0f;
			UpdateTime(pTime);
		}

		private void UpdateTime(int pTime)
		{
			int firstDigit = (pTime - (pTime % 100)) / 100;
			int secondDigit = (pTime - (pTime % 10) - ((pTime / 100) * 100)) / 10;
			int thirdDigit = pTime % 10;
			_endHectaTime.SetNumber(firstDigit);
			_endDecaTime.SetNumber(secondDigit);
			_endFlatTime.SetNumber(thirdDigit);
		}

		private void UpdateScore(int pScore)
		{
			int firstDigit = (pScore - (pScore % 10)) / 10;
			int secondDigit = pScore % 10;
			_endDecaScore.SetNumber(firstDigit);
			_endFlatScore.SetNumber(secondDigit);
		}
	}
}


#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator