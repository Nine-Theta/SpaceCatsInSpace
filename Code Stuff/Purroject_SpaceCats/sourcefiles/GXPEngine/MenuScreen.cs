using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class MenuScreen : Canvas
	{
		//TODO: add titlescreen
		//private AnimSprite _titleScreen;
		private AnimSprite _background;
		private AnimSprite _mainScreen;
		private AnimSprite _ruleScreen;
		private AnimSprite _creditScreen;
		//TODO: Add Levelscreen
		//private AnimSprite _levelScreen;
		private AnimSprite _endScreen;
		private MyGame _gameRef;
		//inOtherScreen is for Screens that aren't the main menu. Pressing enter or space will take you back when true
		private bool _inOtherScreen = false;
		private int _selectedButton = 0;
		private int _backgroundTimer = 3;

		public MenuScreen(int pWidth, int pHeight) : base(pWidth, pHeight)
		{
			_background = new AnimSprite("Sprites/Menu/Menu.png", 11, 1);
			_mainScreen = new AnimSprite("Sprites/Menu/Menu Buttons.png", 4, 1);
			_ruleScreen = new AnimSprite("Sprites/Menu/Rules.png", 8, 1);
			_ruleScreen.alpha = 0.0f;
			_creditScreen = new AnimSprite("Sprites/Menu/Logo.png", 3, 1);
			_creditScreen.alpha = 0.0f;
			_endScreen = new AnimSprite("Sprites/Menu/Endscreen.png", 4, 1);
			_endScreen.alpha = 0.0f;
		}

		public void Step()
		{
			GetInput();
			AnimateBackground();
		}

		void GetInput()
		{
			if (Input.GetKeyDown(Key.UP) && _selectedButton > 0)
			{
				_selectedButton--;
				//_foreground.SetFrame(_selectedButton);
			}
			if (Input.GetKeyDown(Key.DOWN) && _selectedButton < 3)
			{
				_selectedButton++;
				//_foreground.SetFrame(_selectedButton);
			}
			if(Input.GetKeyDown(Key.SPACE) || Input.GetKeyDown(Key.ENTER))
			{
				if (!_inOtherScreen)
				{
					switch (_selectedButton)
					{
						case 0:
							StartGame();
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
				else
				{
					_inOtherScreen = false;
					//_titleScreen.alpha = 0.0f;
					_background.alpha = 1.0f;
					_mainScreen.alpha = 1.0f;
					//_levelScreen.alpha = 0.0f;
					_ruleScreen.alpha = 0.0f;
					_creditScreen.alpha = 0.0f;
					_endScreen.alpha = 0.0f;
				}
			}
		}

		void AnimateBackground()
		{
			_backgroundTimer--;
			if (_backgroundTimer <= 0)
			{
				_backgroundTimer = 3;
				_background.NextFrame();
			}
		}

		void StartGame()
		{
			
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
	}
}
