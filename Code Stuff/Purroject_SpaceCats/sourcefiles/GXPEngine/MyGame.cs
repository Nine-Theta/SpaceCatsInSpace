using System;
using System.Drawing;
using GXPEngine;
using Purroject_SpaceCats;

public class MyGame : Game
{
	private MouseHandler _catHandler = null; //playerhandler won the vote over "ballhandler" & "ballfondler" //Renamed playerhandler to cathandler
	private Player _player = null;
	private Cat _cat = null;
	private Arrow _arrow = null;
	private Planet _planet1 = null;
	private Planet _planet2 = null;
	private Planet _planet3 = null;
	private Planet _planet4 = null;
	private BlackHole _blackhole = null;
	private Asteroid _asteroid = null;

	private Canvas _background = null;
	private Sprite _scrollTarget = null;
	private Sprite _screenSizeOverlay = null;
	private Sprite _backgroundSprite = null;
	private SpaceStation _spaceStation = null;

	private Vec2 _playerStartPosition = null;
	private Vec2 _mouseDelta = null;
	private Vec2 _playerLastPosition = null;
	private Vec2 _playerBouncePos = null;
	private Vec2 _playerPOI = null;


	private float _accelerationValue = 0.0f;
	//TODO: get an image file with the ball with decreasing amounts of cats
	//private int _catCounter = 5;
	private float _leftBoundary, _rightBoundary, _topBoundary, _bottomBoundary;
	///private float _bounceX Pos, _bounceYPos; //Why did these exist again? I've only ever seen them as warnings in the error list

	private const int _scrollBoundary = 1600;
	private const int _gameWidth = 640;	//Actual game width, regardless of screen width
	private const int _gameHeight = 6500;	//Actual game height, regardless of screen height

	private int _shankCounter = 10; //counts the amount of times b*tches will get shanked, hypothetically that is. (for legal reasons).

	private bool _switchBoundaryCollision = false;
	private bool _switchScreenSizeOverlay = false;

	public MyGame() : base(640, 960, false, false) //Screen size should be 640x960. Don't overstep this boundary
	{
		targetFps = 60;

		_backgroundSprite = new Sprite("Background.png");
		_backgroundSprite.SetOrigin(_backgroundSprite.width / 2, _backgroundSprite.height / 2);
		_backgroundSprite.SetXY(width / 2, 0);
		AddChild(_backgroundSprite);

		_background = new Canvas(_gameWidth, _gameHeight);
		AddChild(_background);
		_background.graphics.FillRectangle(new SolidBrush(Color.Empty), new Rectangle(0, 0, _gameWidth, _gameHeight));

		_player = new Player(30, new Vec2(_gameWidth / 2, _gameHeight - 400));
		AddChild(_player);
		_scrollTarget = _player;

		_playerStartPosition = new Vec2(_player.x, _player.y);
		_playerLastPosition = new Vec2(_player.x, _player.y);
		_playerBouncePos = Vec2.zero;
		_playerPOI = Vec2.zero;

		_cat = new Cat(_player, 30);
		AddChild(_cat);
		//TODO: Get the arrow to point from the other side of the player's radius, opposing the cat
		_arrow = new Arrow(_player);
		AddChild(_arrow);
		_arrow.alpha = 0.0f;

		_catHandler = new MouseHandler(_cat);
		_catHandler.OnMouseDownOnTarget += onCatMouseDown;

		_asteroid = new Asteroid(350, new Vec2(_gameWidth / 2, _gameHeight - 600));
		AddChild(_asteroid);

		//Planets and black holes
		_planet1 = new Planet(new Vec2(100, 700), "Planet 1.png", 5, 0.5f, 300);
		AddChild(_planet1);
		_planet2 = new Planet(new Vec2(500, 4500), "Planet 2.png", 5, 1.0f, 300);
		AddChild(_planet2);
		_planet3 = new Planet(new Vec2(100, 2700), "Planet 3.png", 5, 0.5f, 300);
		AddChild(_planet3);
		_planet4 = new Planet(new Vec2(500, 5500), "Planet 4.png", 5, 1.0f, 300);
		AddChild(_planet4);

		_blackhole = new BlackHole(new Vec2(_gameWidth / 2, _gameHeight), 5, 300);
		AddChild(_blackhole);

		//SpaceStations (spawn and end should be here)
		_spaceStation = new SpaceStation(_gameWidth / 2, 0, "SpaceStationTemp.png");
		AddChild(_spaceStation);

		_mouseDelta = new Vec2(Input.mouseX, Input.mouseY);

		float border = 50;
		_leftBoundary = border;
		_rightBoundary = _gameWidth - border;
		_topBoundary = border;
		_bottomBoundary = _gameHeight - border;

		DrawBorder(_leftBoundary, true);
		DrawBorder(_rightBoundary, true);
		DrawBorder(_topBoundary, false);
		DrawBorder(_bottomBoundary, false);

		_screenSizeOverlay = new Sprite("screenSizeDebug.png");
		AddChild(_screenSizeOverlay);
		_screenSizeOverlay.SetOrigin(_screenSizeOverlay.width / 2, _screenSizeOverlay.height / 2);
		_screenSizeOverlay.alpha = 0.25f;

		//TODO: Add XML magic to actually keep track of the b*tches that are getting shanked
		_shankCounter += 1;
		_shankCounter++;
		_shankCounter += 6;
	}

	private void DrawBorder(float boundary, bool isXBoundary)
	{
		if (isXBoundary)
		{
			_background.graphics.DrawLine(new Pen(Color.Lime), boundary, 0, boundary, _gameHeight);
			//Console.WriteLine("X? "+ boundary);
		}
		else {
			_background.graphics.DrawLine(new Pen(Color.Lime), 0, boundary, _gameWidth, boundary);
			//Console.WriteLine("Y? "+ boundary);
		}
	}

	private void onCatMouseDown(GameObject target, MouseEventType type)
	{
		_catHandler.OnMouseMove += onCatMouseMove;
		_catHandler.OnMouseUp += onCatMouseUp;
		_catHandler.OnMouseRightDown += onCatRightMouseDown;
		_arrow.alpha = 1.0f;
		//_player.selected = true;
	}

	private void onCatMouseMove(GameObject target, MouseEventType type)
	{
		//TODO: Figure out how to get the arrow to move like the cat but on the other side
		_cat.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));
		_cat.rotation = _mouseDelta.GetAngleDegrees() + 180;
		_arrow.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));
		_arrow.rotation = _mouseDelta.GetAngleDegrees() + 180;

		_accelerationValue = _mouseDelta.Length() / 15;
		//Console.WriteLine(_accelerationValue);
	}

	private void onCatMouseUp(GameObject target, MouseEventType type)
	{
		_catHandler.OnMouseMove -= onCatMouseMove;
		_catHandler.OnMouseUp -= onCatMouseUp;
		_arrow.alpha = 0.0f;
		_player.selected = false;
		_cat.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(_accelerationValue));
		_player.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(-_accelerationValue));
	}

	private void onCatRightMouseDown(GameObject target, MouseEventType type)
	{
		/// These lines were commented out because the game is intended to work without them
		//_player.velocity = Vec2.zero;
		_cat.velocity = Vec2.zero;
		//_player.position.SetXY(Input.mouseX, Input.mouseY);
		_cat.position.SetXY(_player.position.Clone().Add(_player.position.Clone().Normalize().Scale(_player.radius)));
	}

	private void OnMouseEvent(GameObject target, MouseEventType type)
	{
		Console.WriteLine("Eventtype: " + type + " triggered on " + target);
	}

	private void BasicCollisionCheck(Planet other) //Very Basic
	{
		Vec2 deltaVec = _player.position.Clone().Subtract(other.posVec);

		if ((_player.radius + other.gravityRadius) > deltaVec.Length())
		{
			//Console.WriteLine(deltaVec);
			//TODO: Shank someone for this line commented out at the bottom of the following rant
			//Cos and Sin are very heavy on the CPU and will cause problems the more planets we make
			//And your collisioncheck already got the planet, why are you still using _planet1 in this formula?
			//Using other instead allows you to actually get the desired effect of the gravity force variable
			//Also it makes no sense to even use them in this particular line
			//We're adding acceleration towards a planet here, not trying to make a pythagoras formula to get degrees
			//Like for real you're not only using Cos and Sin but also calculating with Radians, do you have any idea what those numbers become
			//They're going to be below 0.0001, Cos and Sin are for degrees not Radians
			//And even if you have degrees what do you do with them? You're making a freaking vector here
			//Vectors take an X and a Y value, neither degrees nor radians make sense in this line
			//I'd understand if you're trying to fix some issues with gravitational pull here but this goes beyond the realms of reason
			//Why did you even make the divide function? The scale function is meant to be used in both directions and you have not used it anywhere but in this dumpsterfire of a line
			//Not to mention the fact that the x and y are calculated in extremely different ways entirely	
			//If this rant seems personal let me assure you it's not, but this single line of code has so many issues 
			//and it does not help that it's 23:40 on thursday and we have to show a functional prototype on friday
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
			else
			{
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
			other.AddVelocity(_player.velocity.Clone());
		}
	}

	private void scrollToTarget()
	{
		if (_scrollTarget != null)
		{
			y = (game.height / 2 - _scrollTarget.y);
			//x = (game.width / 2 - _scrollTarget.x); //Why are you even doing x? Didn't you see the level mockup on google Drive? Tall and Vertical, not horizontal

			//_backgroundSprite.x = _scrollTarget.x;
			_backgroundSprite.y = _scrollTarget.y;

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

	private void Debug()
	{
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
			targetFps = 999999999;
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

	void Update()
	{
		scrollToTarget();

		Debug();

		_player.Step();

		_cat.Step();

		_mouseDelta.SetXY((Input.mouseX - game.x) - _player.position.x, (Input.mouseY - game.y) - _player.position.y);

		//TODO: Replace this code by a for each loop (or just the levelmanager's system if the time is right)
		BasicCollisionCheck(_planet1);
		BasicCollisionCheck(_planet2);
		BasicCollisionCheck(_planet3);
		BasicCollisionCheck(_planet4);
		BasicCollisionCheck(_blackhole);
		BasicAsteroidCheck(_asteroid);
		_asteroid.Step();

		_background.graphics.DrawLine(new Pen(Color.White), _playerLastPosition.x, _playerLastPosition.y, _player.x, _player.y);

		_playerLastPosition.x = _player.x;
		_playerLastPosition.y = _player.y;

		if (_switchBoundaryCollision){
			checkBoundaryCollisions();
			_player.ballColor = Color.Red;
		}else {
			brokenBoundaryCollisionCheck();
			_player.ballColor = Color.Pink;}

		if (_cat.velocity.EqualsTo(Vec2.zero))
		{
			_cat.position.SetXY(_player.position.Clone().Add(_player.position.Clone().Normalize().Scale(_player.radius)));
		}
		//_playerLastPosition.x = _player.x;
		//_playerLastPosition.y = _player.y;
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

		if (leftHit || rightHit || topHit || bottomHit)
		{
			_playerBouncePos.x = _player.x;
			_playerBouncePos.y = _player.y;
			_player.ballColor = Color.Maroon;

			_playerPOI.SetXY(_playerBouncePos.Clone().Subtract(_playerLastPosition));
			//Console.WriteLine("_playerBounce(" + _playerBouncePos + ") - _playerLastPosition(" + _playerLastPosition + ") = _playerPOI(" + _playerPOI + ")");

			//_playerPOI.SetXY(_playerLastPosition.Clone().Subtract(_playerBouncePos));
			//Console.WriteLine("_playerLastPosition(" + _playerLastPosition + ") - _playerBounce(" + _playerBouncePos + ") = _playerPOI(" + _playerPOI + ")");

			if (leftHit)
			{
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(-1, 1);
			}
			if (rightHit)
			{
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(-1, 1);
			}
			if (topHit)
			{
				_player.velocity.Scale(0);
				//_player.position.Add(_playerPOI);
				//_player.velocity.Scale(1, -1);
			}
			if (bottomHit)
			{
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(1, -1);
			}
		}
		else {
			_player.ballColor = Color.Pink;
		}
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
