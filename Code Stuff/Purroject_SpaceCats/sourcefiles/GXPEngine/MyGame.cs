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

	private Vec2 _playerStartPosition = null;
	private Vec2 _mouseDelta = null;

	private float _accelerationValue = 0.0f;
	//private int _catCounter = 5;
	private int _shankCounter = 10; //counts the amount of times b*tches will get shanked, hypothetically that is. (for legal reasons).

	public MyGame () : base(800, 600, false, false)
	{
		targetFps = 60;

		_player = new Player(30, new Vec2(width / 2, 200));
		AddChild(_player);

		_playerStartPosition = new Vec2(_player.x, _player.y);

		_cat = new Cat(_player, 30);
		AddChild(_cat);

		_catHandler = new MouseHandler(_cat);
		_catHandler.OnMouseDownOnTarget += onCatMouseDown;

		_planet = new Planet(new Vec2(200, 200), "circle.png", 5, 0.2f, 150);
		AddChild(_planet);

		_mouseDelta = new Vec2(Input.mouseX, Input.mouseY);

		_shankCounter += 1;
	}

	private void onCatMouseDown(GameObject target, MouseEventType type){

		_catHandler.OnMouseMove += onCatMouseMove;
		_catHandler.OnMouseUp += onCatMouseUp;
	}

	private void onCatMouseMove(GameObject target, MouseEventType type)
	{
		_cat.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));

		_accelerationValue = _mouseDelta.Length()/10;
		Console.WriteLine(_accelerationValue);
	}

	private void onCatMouseUp(GameObject target, MouseEventType type)
	{
		_catHandler.OnMouseMove -= onCatMouseMove;
		_catHandler.OnMouseUp -= onCatMouseUp;

		_cat.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(_accelerationValue));
		_player.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(-_accelerationValue));
	}

	private void OnMouseEvent(GameObject target, MouseEventType type)
	{
		Console.WriteLine("Eventtype: " + type + " triggered on " + target);
	}

	private void BasicCollisionCheck() //Very Basic
	{
		Vec2 deltaVec = _player.position.Clone().Subtract(_planet.posVec);

		if ((_player.radius + _planet.gravityRadius) > deltaVec.Length())
		{
			//Console.WriteLine("Collision = true");
			_player.acceleration.Subtract((_planet.gravityForce * Mathf.Cos(deltaVec.GetAngleRadians())), (_planet.gravityForce * Mathf.Sin(deltaVec.GetAngleRadians())));
		}
		else {
			//Console.WriteLine("Collision = false");
		}

		//if (_player.position.Clone().Subtract(_planet.
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
	}

	void Update()
	{
		Debug();
		
		_player.Step();

		_cat.Step();

		_mouseDelta.SetXY(Input.mouseX - _player.position.x, Input.mouseY - _player.position.y);

		BasicCollisionCheck();
	}

	static void Main() 
	{
		new MyGame().Start();
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
