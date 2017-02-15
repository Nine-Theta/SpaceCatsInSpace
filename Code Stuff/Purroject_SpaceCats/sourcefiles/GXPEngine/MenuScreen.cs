using System;
using GXPEngine;
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator

namespace Purroject_SpaceCats
{
	public class MenuScreen : Canvas
	{
		private AnimSprite _titleScreen;
		private AnimSprite _background;
		private AnimSprite _mainScreen;
		private AnimSprite _ruleScreen;
		private AnimSprite _creditScreen;
		private AnimSprite _levelScreen;
		private AnimSprite _endScreen;
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
			_levelScreen = new AnimSprite("Sprites/Menu/Map.png", 3, 1);
			AddChild(_levelScreen);
			_levelScreen.alpha = 0.0f;
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
				}
				if (Input.GetKeyDown(Key.UP) && _selectedButton < 2)
				{
					_selectedButton++;
					_levelScreen.SetFrame(_selectedButton);
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
							StartGame(1);
							break;
						case 1:
							StartGame(2);
							break;
						case 2:
							StartGame(3);
							break;
						case 3:
							Exit();
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
					_ruleScreen.alpha = 0.0f;
					_creditScreen.alpha = 0.0f;
					_endScreen.alpha = 0.0f;
					_backgroundTimer = 10;
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
					_backgroundTimer = 20;
					_ruleScreen.NextFrame();
				}
				if (_creditScreen.alpha == 1.0f)
				{
					_creditScreen.NextFrame();
					_backgroundTimer = 10;
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
		public void ShowEndScreen(int pScore, int pTime)
		{
			this.alpha = 1.0f;
			_inOtherScreen = true;
			_titleScreen.alpha = 0.0f;
			_background.alpha = 0.0f;
			_mainScreen.alpha = 0.0f;
			_levelScreen.alpha = 0.0f;
			_ruleScreen.alpha = 0.0f;
			_creditScreen.alpha = 0.0f;
			_endScreen.alpha = 1.0f;
			if (pScore >= 7)
			{
				_endScreen.SetFrame(3);
			}
			else if (pScore >= 3)
			{
				_endScreen.SetFrame(2);
			}
			else
			{
				_endScreen.SetFrame(1);
			}
			_selectedButton = 0;
			Console.WriteLine(_endScreen.y);
		}
	}
}


#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator