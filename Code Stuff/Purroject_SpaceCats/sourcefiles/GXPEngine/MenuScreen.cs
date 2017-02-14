using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class MenuScreen : Canvas
	{
		private AnimSprite _background;
		private AnimSprite _foreground;
		private AnimSprite _ruleScreen;
		private AnimSprite _creditScreen;
		private MyGame _gameRef;
		//inOtherScreen is for Credits and rulescreen. Pressing enter or space will take you back when true
		private bool _inOtherScreen = false;
		private int _selectedButton = 0;
		private int _backgroundTimer = 3;

		public MenuScreen(int pWidth, int pHeight) : base(pWidth, pHeight)
		{
			_background = new AnimSprite("Sprites/Menu/Background.png", 11, 1);
			_foreground = new AnimSprite("Sprites/Menu/Spritesheet.png", 4, 1);
			_ruleScreen = new AnimSprite("Sprites/Menu/Rules.png", 1, 1);
			_ruleScreen.alpha = 0.0f;
			_creditScreen = new AnimSprite("Sprites/Menu/Credits.png", 1, 1);
			_creditScreen.alpha = 0.0f;
		}

		void Update()
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
					_background.alpha = 1.0f;
					_foreground.alpha = 1.0f;
					_ruleScreen.alpha = 0.0f;
					_creditScreen.alpha = 0.0f;
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
