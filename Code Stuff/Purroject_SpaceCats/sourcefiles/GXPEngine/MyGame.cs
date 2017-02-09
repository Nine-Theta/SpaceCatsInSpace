using System;
using System.Drawing;
using GXPEngine;
using Purroject_SpaceCats;

//TODO Implement Nyarn cat (sic)
//TODO Shank a b*tch for this comment

public class MyGame : Game
{
	private MouseHandler _catHandler = null; //playerhandler won the vote over "ballhandler" & "ballfondler" //Renamed playerhandler to cathandler
	private Player _player = null;
	private Cat _cat = null;
	private Planet _planet = null;
	private Canvas _background = null;
	private Planet _planetTest = null;
	private Sprite scrollTarget;

	private Vec2 _playerStartPosition = null;
	private Vec2 _mouseDelta = null;
	private Vec2 _playerLastPosition = null;
	private Vec2 _playerBouncePos = null;
	private Vec2 _playerPOI = null;


	private float _accelerationValue = 0.0f;
	//private int _catCounter = 5;
	private float _leftBoundary, _rightBoundary, _topBoundary, _bottomBoundary;
	private float _bounceXPos, _bounceYPos;

	private const int _scrollBoundary = 700;
	private int _shankCounter = 10; //counts the amount of times b*tches will get shanked, hypothetically that is. (for legal reasons).

	private bool _switchBoundaryCollision = false;

	public MyGame() : base(640, 960, false, false)//Screen size should be 640x960, any other value is used for debugging;
	{
		targetFps = 60;

		_background = new Canvas(width, height);
		AddChild(_background);
		_background.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, width, height));

		_player = new Player(30, new Vec2(width / 2, 200));
		AddChild(_player);
		scrollTarget = _player;

		_playerStartPosition = new Vec2(_player.x, _player.y);
		_playerLastPosition = new Vec2(_player.x, _player.y);
		_playerBouncePos = Vec2.zero;
		_playerPOI = Vec2.zero;

		_cat = new Cat(_player, 30);
		AddChild(_cat);

		_catHandler = new MouseHandler(_cat);
		_catHandler.OnMouseDownOnTarget += onCatMouseDown;

		_planet = new Planet(new Vec2(300, 700), "circle.png", 5, 1.5f, 150);
		AddChild(_planet);

		_planetTest = new Planet(new Vec2(1000, 500), "circle.png", 5, 0.8f, 120);
		AddChild(_planetTest);

		_mouseDelta = new Vec2(Input.mouseX, Input.mouseY);

		float border = 50;
		_leftBoundary = border;
		_rightBoundary = width - border;
		_topBoundary = border - 550;
		_bottomBoundary = height - border;

		DrawBorder(_leftBoundary, true);
		DrawBorder(_rightBoundary, true);
		DrawBorder(_topBoundary, false);
		DrawBorder(_bottomBoundary, false);

		_shankCounter += 1;
		_shankCounter++;
	}

	private void DrawBorder(float boundary, bool isXBoundary)
	{
		if (isXBoundary)
		{
			_background.graphics.DrawLine(new Pen(Color.Lime), boundary, 0, boundary, height);
			Console.WriteLine("X? "+ boundary);
		}
		else {
			_background.graphics.DrawLine(new Pen(Color.Lime), 0, boundary, width, boundary);
			Console.WriteLine("Y? "+ boundary);
		}
	}

	private void onCatMouseDown(GameObject target, MouseEventType type)
	{

		_catHandler.OnMouseMove += onCatMouseMove;
		_catHandler.OnMouseUp += onCatMouseUp;
		_catHandler.OnMouseRightDown += onCatRightMouseDown;
	}

	private void onCatMouseMove(GameObject target, MouseEventType type)
	{
		_cat.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));

		_accelerationValue = _mouseDelta.Length() / 10;
		Console.WriteLine(_accelerationValue);
	}

	private void onCatMouseUp(GameObject target, MouseEventType type)
	{
		_catHandler.OnMouseMove -= onCatMouseMove;
		_catHandler.OnMouseUp -= onCatMouseUp;

		_cat.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(_accelerationValue));
		_player.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(-_accelerationValue));
	}

	private void onCatRightMouseDown(GameObject target, MouseEventType type)
	{
		_player.velocity = Vec2.zero;
		_cat.velocity = Vec2.zero;
		_player.position.SetXY(Input.mouseX, Input.mouseY);
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
			//Console.WriteLine("Collision = true");
			_player.acceleration.Subtract((_planet.gravityForce * Mathf.Cos(deltaVec.GetAngleRadians())), (_planet.gravityForce * Mathf.Sin(deltaVec.GetAngleRadians())));
		}
		else {
			//Console.WriteLine("Collision = false");
		}
	}

	private void scrollToTarget()
	{
		if (scrollTarget != null && scrollTarget.y < _scrollBoundary)
		{
			//y = (game.height / 2 - scrollTarget.y)*0.5f;
			this.y = (_scrollBoundary - scrollTarget.y);
			//x = (game.width / 2 - scrollTarget.x);
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
				Console.WriteLine("Last Pos PreCalc:D " + _playerLastPosition);
				_playerPOI.SetXY(_playerLastPosition.Clone().Normalize().Scale(_playerLastPosition.x - _leftBoundary));
				Console.WriteLine("Last Pos PostCalc:D " + _playerPOI);
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
		if (Input.GetKeyDown(Key.ONE))
		{
			targetFps = 60;
		}
		if (Input.GetKeyDown(Key.TWO))
		{
			targetFps = 12;
		}
		if (Input.GetKeyDown(Key.THREE))
		{
			targetFps = 2;
		}
		if (Input.GetKeyDown(Key.FOUR))
		{
			targetFps = 240;
		}
		if (Input.GetKeyDown(Key.FIVE))
		{
			targetFps = 999999999;
		}
		if (Input.GetKeyDown(Key.SIX))
		{
			targetFps = 1;
		}
		if (Input.GetKeyDown(Key.SEVEN))
		{
			_switchBoundaryCollision = !_switchBoundaryCollision;
		}
	}

	void Update()
	{
		scrollToTarget();

		Debug();

		_player.Step();

		_cat.Step();

		_mouseDelta.SetXY(Input.mouseX - _player.position.x, Input.mouseY - _player.position.y);

		BasicCollisionCheck(_planet);
		BasicCollisionCheck(_planetTest);

		_background.graphics.DrawLine(new Pen(Color.White), _playerLastPosition.x, _playerLastPosition.y, _player.x, _player.y);

		_playerLastPosition.x = _player.x;
		_playerLastPosition.y = _player.y;

		if (_switchBoundaryCollision){
			checkBoundaryCollisions();
			_player.ballColor = Color.Red;
		}else {
			brokenBoundaryCollisionCheck();
			_player.ballColor = Color.Pink;}

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
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(1, -1);
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
