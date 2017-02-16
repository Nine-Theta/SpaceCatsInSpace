using System;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine;
using Purroject_SpaceCats;

public class MyGame : Game
{
	private MouseHandler _catHandler = null; //playerhandler won the vote over "ballhandler" & "ballfondler" //Renamed playerhandler to cathandler
	private Player _player = null;
	private LevelManager _levelManager = null;
	private Cat _disposableCat = null; //Acceptable losses

	private List<Cat> _listDisposableCat;

	private Canvas _background = null;
	private Sprite _scrollTarget = null;
	private Sprite _screenSizeOverlay = null;
	private Sprite _backgroundSprite = null;

	private Vec2 _playerStartPosition = null;
	private Vec2 _mouseDelta = null;
	private Vec2 _playerLastPosition = null;
	private Vec2 _playerBouncePos = null;
	private Vec2 _playerPOI = null;
	private HUD _hud = null;

	private bool _started = false;
	private MenuScreen _menuScreen = null;

	private float _accelerationValue = 0.0f;
	//TODO? get an image file with the ball with decreasing amounts of cats
	//Already have the counter, extra files redundant. Possible though as they have time left
	private float _leftBoundary, _rightBoundary, _topBoundary, _bottomBoundary;
	private float _oldTime = 0;
	private float _time = 0.0f;

	private const int _scrollBoundary = 1600;
	private const int _gameWidth = 640; //Actual game width, regardless of screen width
	private const int _gameHeight = 6500;   //Actual game height, regardless of screen height

	//TODO: Implement these 3 variables fully
	private int _catCounter = 15;
	private int _scoreCounter = 0;
	private int _emporerSoulCounter = 0;
	//private ShankCounter _shankCounter; //counts the amount of times b*tches will get shanked, hypothetically that is. (for legal reasons).

	private bool _switchBoundaryCollision = false;
	private bool _switchScreenSizeOverlay = false;

	public MyGame() : base(640, 960, false, false) //Screen size should be 640x960. Don't overstep this boundary
	{
		targetFps = 60;
		_menuScreen = new MenuScreen(width, height);
		AddChild(_menuScreen);
		_menuScreen.SetGameRef(this);

	}

	public void InitializeGame(int pLevel)
	{
		_oldTime = Time.now;
		_levelManager = new LevelManager(pLevel, this);
		_backgroundSprite = new Sprite("Sprites/Background.png");
		_backgroundSprite.SetOrigin(_backgroundSprite.width / 2, _backgroundSprite.height / 2);
		_backgroundSprite.SetXY(width / 2, 0);
		AddChild(_backgroundSprite);

		_background = new Canvas(_gameWidth, _gameHeight);
		AddChild(_background);
		_background.graphics.FillRectangle(new SolidBrush(Color.Empty), new Rectangle(0, 0, _gameWidth, _gameHeight));
		AddChild(_levelManager);

		_player = _levelManager.GetPlayer();
		_scrollTarget = _player;
		_playerStartPosition = new Vec2(_player.x, _player.y);
		_playerLastPosition = new Vec2(_player.x, _player.y);
		_playerBouncePos = Vec2.zero;
		_playerPOI = Vec2.zero;
		_player.arrow.alpha = 0.0f;

		_listDisposableCat = new List<Cat>();

		_catHandler = new MouseHandler(_player.yarnSprite);
		_catHandler.OnMouseDownOnTarget += onCatMouseDown;

		_mouseDelta = new Vec2(Input.mouseX, Input.mouseY);

		float border = 0;
		_leftBoundary = border;
		_rightBoundary = _gameWidth - border;
		_topBoundary = border;
		_bottomBoundary = _gameHeight - border;

		DrawBorder(_leftBoundary, true);
		DrawBorder(_rightBoundary, true);
		DrawBorder(_topBoundary, false);
		DrawBorder(_bottomBoundary, false);

		_screenSizeOverlay = new Sprite("Sprites/screenSizeDebug.png");
		AddChild(_screenSizeOverlay);
		_screenSizeOverlay.SetOrigin(_screenSizeOverlay.width / 2, _screenSizeOverlay.height / 2);
		_screenSizeOverlay.alpha = 0.25f;

		_hud = new HUD(width, height);
		AddChild(_hud);
		_hud.SetCats(_catCounter);
		_started = true;
	}

	private void DrawBorder(float boundary, bool isXBoundary)
	{
		if (isXBoundary){
			_background.graphics.DrawLine(new Pen(Color.Lime), boundary, 0, boundary, _gameHeight);
		}
		else {
			_background.graphics.DrawLine(new Pen(Color.Lime), 0, boundary, _gameWidth, boundary);
		}
	}

	private void onCatMouseDown(GameObject target, MouseEventType type)
	{
		if (_catCounter > 0)
		{
			_catHandler.OnMouseMove += onCatMouseMove;
			_catHandler.OnMouseUp += onCatMouseUp;
			_catHandler.OnMouseRightDown += onCatRightMouseDown;
			_player.cat.alpha = 1.0f;
			_player.selected = true;
			_player.arrow.alpha = 1.0f;
			//_switchCatMoveToPlayer = false;
		}
		else
		{
			//Instant forfeit? Special Icon?
			Console.WriteLine("You cats are on another yarn flying through space.");
		}
	}

	private void onCatMouseMove(GameObject target, MouseEventType type)
	{
		_accelerationValue = _mouseDelta.Length() / 15;
	}

	private void onCatMouseUp(GameObject target, MouseEventType type)
	{
		_catHandler.OnMouseMove -= onCatMouseMove;
		_catHandler.OnMouseUp -= onCatMouseUp;
		_player.arrow.alpha = 0.0f;
		_player.selected = false;
		_player.cat.alpha = 0.0f;
		SpawnDisposableCat();
		_player.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(-_accelerationValue));
		_catCounter--;
	}

	private void onCatRightMouseDown(GameObject target, MouseEventType type)
	{
		_player.cat.velocity = Vec2.zero;
		_player.cat.alpha = 1.0f;
	}

	private void OnMouseEvent(GameObject target, MouseEventType type)
	{
		Console.WriteLine("Eventtype: " + type + " triggered on " + target);
	}

	void SpawnDisposableCat()
	{
		_disposableCat = new Cat(_player, Cat.type.DISPOSABLE);
		//_disposableCat.SetFrame(3);
		AddChild(_disposableCat);
		//_arrayDisposableCat[_arrayDisposableCat.Length - _catCounter] = _disposableCat;
		_listDisposableCat.Add(_disposableCat);
		_disposableCat.position.SetXY(_player.x + _player.cat.x, _player.y + _player.cat.y);
		_disposableCat.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(_accelerationValue));
		//Console.WriteLine(_disposableCat.acceleration);
	}

	private void BasicCollisionCheck(Planet other) //Very Basic
	{
		Vec2 deltaVec = _player.position.Clone().Subtract(other.posVec);

		if ((_player.radius + other.gravityRadius) > deltaVec.Length())
		{
			///_player.acceleration.Subtract(new Vec2(((_planet1.gravityForce  * Mathf.Cos(deltaVec.GetAngleRadians()))), ((_planet1.gravityForce * Mathf.Sin(deltaVec.GetAngleRadians())))).Normalize().Divide(deltaVec.Length()*0.04f));
			if ((_player.radius + other.hitball.radius) > deltaVec.Length())
			{
				//float degrees = deltaVec.GetAngleDegrees() + 180;
				_player.velocity.SetXY(0, 0);
				_player.acceleration.SetXY(0, 0);
				if (other is BlackHole)
				{
					_player.position = other.position;
				}
			}
			else{
				_player.acceleration.Subtract(deltaVec.Clone().Normalize().Scale(other.gravityForce));
			}
		}
		else {
			//Console.WriteLine("Collision = false");
		}
	}

	private void BasicAsteroidCheck(Asteroid other)
	{
		Vec2 deltaVec = _player.position.Clone().Subtract(other.position);
		if ((_player.radius + other.radius) > deltaVec.Length())
		{
			_player.velocity.Scale(0.5f);
			other.acceleration.Add(_player.velocity);
		}
	}

	private void scrollToTarget()
	{
		if (_scrollTarget != null)
		{
			y = (game.height / 2 - _scrollTarget.y);
			_backgroundSprite.y = _scrollTarget.y;
			_hud.y = _backgroundSprite.y - (height/2);
			if (_switchScreenSizeOverlay){
				_screenSizeOverlay.SetXY(_scrollTarget.x, _scrollTarget.y);
			}
			else {
				_screenSizeOverlay.SetXY(-2000, -2000);
			}
			//this.y = (_scrollBoundary - _scrollTarget.y);
		}
	}

	/// <summary>
	/// The boundary collision check that is currently being worked on.
	/// </summary>
	private void checkBoundaryCollisions()
	{
		bool leftHit = (_player.x - _player.radius) < _leftBoundary;
		bool rightHit = (_player.x + _player.radius) > _rightBoundary;
		bool topHit = (_player.y - _player.radius) < _topBoundary;
		bool bottomHit = (_player.y + _player.radius) > _bottomBoundary;

		if (leftHit || rightHit || topHit || bottomHit)
		{
			_playerBouncePos.x = _player.x;
			_playerBouncePos.y = _player.y;
			_player.ballColor = Color.Maroon;

			//_playerPOI.SetXY(_playerBouncePos.Clone().Subtract(_playerLastPosition));
			//Console.WriteLine("_playerBounce(" + _playerBouncePos + ") - _playerLastPosition(" + _playerLastPosition + ") = _playerPOI(" + _playerPOI + ")");

			//_playerPOI.SetXY(_playerLastPosition.Clone().Subtract(_playerBouncePos));
			//Console.WriteLine("_playerLastPosition(" + _playerLastPosition + ") - _playerBounce(" + _playerBouncePos + ") = _playerPOI(" + _playerPOI + ")");

			if (leftHit)
			{
				//Console.WriteLine("Last Pos PreCalc:D " + _playerLastPosition);
				_playerPOI.SetXY(_playerLastPosition.Clone().Normalize().Scale(_playerLastPosition.x - _leftBoundary));
				//Console.WriteLine("Last Pos PostCalc:D " + _playerPOI);
				_player.position.SetXY(_playerLastPosition.Add(_playerPOI.Clone().Normalize().Scale(_playerPOI.Length())));
				_player.velocity.Scale(-1, 1);
			}
			if (rightHit)
			{
				_playerPOI.SetXY(_playerLastPosition.Clone().Normalize().Scale(_rightBoundary - _playerLastPosition.x));
				_player.position.SetXY(_playerPOI.Add(_playerLastPosition));
				_player.velocity.Scale(-1, 1);
			}
			if (topHit)
			{
				_playerPOI.SetXY(_playerLastPosition.Clone().Normalize().Scale(_playerLastPosition.y - _topBoundary));
				_player.position.SetXY(_playerPOI.Add(_playerLastPosition));
				_player.velocity.Scale(1, -1);
			}
			if (bottomHit)
			{
				_playerPOI.SetXY(_playerLastPosition.Clone().Normalize().Scale(_bottomBoundary - _playerLastPosition.y));
				_player.position.SetXY(_playerPOI.Add(_playerLastPosition));
				_player.velocity.Scale(1, -1);
			}
		}
		else {
			_player.ballColor = Color.Pink;
		}
	}

	/// <summary>
	/// Any cat unfortunate enough to be "misplaced" out of the game area, will be fed to the god-emperor of mankind
	/// </summary>
	/// <param name="casualty">The Cat in question</param>
	/// <param name="index">The cat's index in the list</param>
	private void CheckForCatDestruction(Cat casualty, int index)
	{
		bool leftExit = (casualty.x < -100);
		bool rightExit = (casualty.x > (_gameWidth + 100));
		bool topExit = (casualty.y < -100);
		bool bottomExit = (casualty.y > (_gameHeight + 100));

		if (leftExit || rightExit || topExit || bottomExit)
		{
			_listDisposableCat.RemoveAt(index);
			casualty.Destroy();
			casualty = null;
			_emporerSoulCounter += 1;
			Console.WriteLine("Souls fed to the emperor today: [{0}/1000]", _emporerSoulCounter);
		}
	}

	private void Debug(){
		if (Input.GetKeyDown(Key.ONE)){
			targetFps = 60;
		}
		if (Input.GetKeyDown(Key.TWO)){
			targetFps = 12;
		}
		if (Input.GetKeyDown(Key.THREE)){
			targetFps = 2;
		}
		if (Input.GetKeyDown(Key.FOUR)){
			targetFps = 240;
		}
		if (Input.GetKeyDown(Key.FIVE)){
			targetFps = 999999999; 	//You have been in suspension for: 9 9 9 9 9 [/*&#%]		...9 
		}
		if (Input.GetKeyDown(Key.SIX)){
			targetFps = 1;
		}
		if (Input.GetKeyDown(Key.SEVEN)){
			_switchBoundaryCollision = !_switchBoundaryCollision;
		}
		if (Input.GetKeyDown(Key.EIGHT)){
			_switchScreenSizeOverlay = !_switchScreenSizeOverlay;
		}
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	//																									UPDATE																									//
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	void Update()
	{
		if (_started)
		{
			scrollToTarget();

			Debug();

			_player.Step();

			_player.arrow.position.SetXY(_mouseDelta.Clone().Normalize().Scale((_player.radius*-2) - (_accelerationValue*3)));
			_player.arrow.rotation = _mouseDelta.GetAngleDegrees() + 180;
			_player.arrow.scaleX = (0.5f + (_accelerationValue / 100));
			_player.arrow.scaleY = (0.5f + (_accelerationValue / 200));
			_player.cat.position.SetXY(_mouseDelta.Clone().Normalize().Scale(_player.radius*2));
			_player.cat.rotation = _mouseDelta.GetAngleDegrees()+90;

			if (_disposableCat != null && _listDisposableCat.Contains(_disposableCat))
			{
				for (int i = 0; i < _listDisposableCat.Count; i++)
				{
					_listDisposableCat[i].Step();

					CheckForCatDestruction(_listDisposableCat[i], i);
				}
			}

			_mouseDelta.SetXY((Input.mouseX - game.x) - _player.position.x, (Input.mouseY - game.y) - _player.position.y);

			//TODO: Replace this code by a for each loop (or just the levelmanager's system if the time is right)
			//BasicCollisionCheck(_planet1);
			//BasicCollisionCheck(_planet2);
			//BasicCollisionCheck(_planet3);
			//BasicCollisionCheck(_planet4);
			//BasicCollisionCheck(_blackhole);
			//BasicAsteroidCheck(_asteroid);
			//_asteroid.Step();

			_background.graphics.DrawLine(new Pen(Color.White), _playerLastPosition.x, _playerLastPosition.y, _player.x, _player.y);

			_playerLastPosition.x = _player.x;
			_playerLastPosition.y = _player.y;

			//Used for debug purposes
			if (_switchBoundaryCollision){
				checkBoundaryCollisions();
				_player.ballColor = Color.Red;
			}
			else {
				brokenBoundaryCollisionCheck();
				_player.ballColor = Color.Pink;
			}

			//Console.WriteLine(_time);
			float tTime = (Time.now - _oldTime) / 1000;
			_oldTime = Time.now;
			_time += tTime;
			_hud.SetCats(_catCounter);
			_hud.SetTime(_time);
			_hud.SetScore(_scoreCounter);
			_hud.Step();
		}
		else{
			_menuScreen.Step();
		}
	}

	static void Main()
	{
		new MyGame().Start();
	}

	/// <summary>
	/// The broken version of the boundary collision check.
	/// This will be used until the normal version works properly.
	/// Doesn't that make this the normal version and the other one the broken one?
	/// </summary>
	private void brokenBoundaryCollisionCheck()
	{
		bool leftHit = (_player.x - _player.radius) < _leftBoundary;
		bool rightHit = (_player.x + _player.radius) > _rightBoundary;
		bool topHit = (_player.y - _player.radius) < _topBoundary;
		bool bottomHit = (_player.y + _player.radius) > _bottomBoundary;

		if (leftHit || rightHit || topHit || bottomHit){
			_playerBouncePos.x = _player.x;
			_playerBouncePos.y = _player.y;
			_player.ballColor = Color.Maroon;

			_playerPOI.SetXY(_playerBouncePos.Clone().Subtract(_playerLastPosition));
			//Console.WriteLine("_playerBounce(" + _playerBouncePos + ") - _playerLastPosition(" + _playerLastPosition + ") = _playerPOI(" + _playerPOI + ")");

			//_playerPOI.SetXY(_playerLastPosition.Clone().Subtract(_playerBouncePos));
			//Console.WriteLine("_playerLastPosition(" + _playerLastPosition + ") - _playerBounce(" + _playerBouncePos + ") = _playerPOI(" + _playerPOI + ")");

			if (leftHit){
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(-1, 1);
			}
			if (rightHit){
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(-1, 1);
			}
			if (topHit){
				_player.velocity.Scale(0);
				_menuScreen.ShowEndScreen(_scoreCounter, (int)(_time), true);
				y = 0;
				_started = false;
				_player.cat.Destroy();
				_player.cat = null;
				_levelManager.UnloadLevel();
				_background.alpha = 0.0f;
				_backgroundSprite.alpha = 0.0f;
				_catCounter = 15;
				_time = 0;
				_scoreCounter = 0;
				_hud.Hide();
			}
			if (bottomHit){
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(1, -1);
			}
		}
		else {
			_player.ballColor = Color.Pink;
		}
	}

	public void AddScore(int pScore)
	{
		_scoreCounter += pScore;
	}

	/// <summary>
	/// Ollieses the private empty void.
	/// </summary>
	private void OlliesPrivateEmptyVoid()
	{

	}

	/// <summary>
	/// Ollieses the private empty void.
	/// Again.
	/// </summary>
	private void OlliesPrivateEmptyVoid2_EletricBoogalo()
	{

	}
}
