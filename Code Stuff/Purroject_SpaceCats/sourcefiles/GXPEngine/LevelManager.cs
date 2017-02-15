using System;
using Purroject_SpaceCats;

namespace GXPEngine
{
	public class LevelManager : GameObject
	{
		private XMLMap _currentMap;
		private int _currentlevel;

		public LevelManager(int pLevel)
		{
			_currentMap = new XMLMap();
			LoadLevel(pLevel);
		}

		void LoadLevel(int pLevel)
		{
			_currentlevel = pLevel;
			_currentMap = _currentMap.ReadMap(pLevel);
			foreach (TiledObject tObject in _currentMap.objectGroup.TiledObject)
			{
				InterpretObject(tObject);
			}
		}

		void InterpretObject(TiledObject pObject)
		{
			string objectName = pObject.Name.ToLower();
			string[] splitNames = objectName.Split(' ');
			switch (splitNames[0])
			{
				case "player":
					Player player = new Player(XMLMap.PLAYER_RADIUS, new Vec2(pObject.X, pObject.Y));
					player.levelRef = this;
					AddChild(player);
					player.rotation = pObject.Rotation;
					break;
				case "planet":
					string fileSource = "Sprites/Planet ";
					string PartOfSource = "";
					switch (pObject.GID)
					{
						case 1:
							PartOfSource = "1";
							break;
						case 2:
							PartOfSource = "2";
							break;
						case 3:
							PartOfSource = "4";
							break;
						case 4:
							PartOfSource = "3";
							break;
						default:
							Console.WriteLine("Faulty GID in planet code, GID: " + pObject.GID);
							break;
					}
					if (PartOfSource == "")
					{
						PartOfSource = "1";
					}
					fileSource += PartOfSource + ".png";
					Planet planet = new Planet(new Vec2(pObject.X + (pObject.Width/2), pObject.Y + (pObject.Height / 2)), fileSource, 5, 0.2f, 300);
					planet.rotation = pObject.Rotation;
					//planet.width = (int)(pObject.Width);
					//planet.height = (int)(pObject.Height);
					//AddPlanetList(planet);
					AddChild(planet);
					break;
				case "station":
					//Changes frame if frame is the starting station, else it uses another frame;
					SpaceStation station = new SpaceStation(pObject.X, pObject.Y, "Sprites/SpaceStation-v3.png");
					AddChild(station);
					station.rotation = pObject.Rotation;
					break;
				case "black":
					BlackHole blackhole = new BlackHole(new Vec2(pObject.X + (pObject.Width / 2), pObject.Y + (pObject.Height / 2)), 5, 300);
					AddChild(blackhole);
					blackhole.rotation = pObject.Rotation;
					break;
				case "blackhole":
					BlackHole blackhole1 = new BlackHole(new Vec2(pObject.X, pObject.Y), 5, 300);
					AddChild(blackhole1);
					blackhole1.rotation = pObject.Rotation;
					break;
				case "meteor":
					Asteroid asteroid = new Asteroid(350, new Vec2(pObject.X + (pObject.Width / 2), pObject.Y + (pObject.Height / 2)));
					AddChild(asteroid);
					asteroid.rotation = pObject.Rotation;
					break;
				case "asteroid":
					Asteroid asteroid1 = new Asteroid(350, new Vec2(pObject.X, pObject.Y));
					AddChild(asteroid1);
					asteroid1.rotation = pObject.Rotation;
					break;
				case "astroid":
					Asteroid asteroid2 = new Asteroid(350, new Vec2(pObject.X, pObject.Y));
					AddChild(asteroid2);
					asteroid2.rotation = pObject.Rotation;
					break;
				case "milk":
					Pickup milk = new Pickup((int)(pObject.Width), new Vec2(pObject.X + (pObject.Width / 2), pObject.Y + (pObject.Height / 2)), "Sprites/Milk.png");
					AddChild(milk);
					break;
				case "fish":
					Pickup fish = new Pickup((int)(pObject.Width), new Vec2(pObject.X + (pObject.Width / 2), pObject.Y + (pObject.Height / 2)), "Sprites/Fish.png");
					AddChild(fish);
					break;
				case "token":
					Pickup pickup = new Pickup((int)(pObject.Width), new Vec2(pObject.X + (pObject.Width / 2), pObject.Y + (pObject.Height / 2)), "Sprites/Pickup.png");
					AddChild(pickup);
					break;
				case "pick":
					Pickup pickup1 = new Pickup((int)(pObject.Width), new Vec2(pObject.X + (pObject.Width / 2), pObject.Y + (pObject.Height / 2)), "Sprites/Pickup.png");
					AddChild(pickup1);
					break;
				case "Coin":
					Pickup pickup2 = new Pickup((int)(pObject.Width), new Vec2(pObject.X + (pObject.Width / 2), pObject.Y + (pObject.Height / 2)), "Sprites/Pickup.png");
					AddChild(pickup2);
					break;
				default:
					Console.WriteLine("Unknown object in Object Layer");
					Console.WriteLine("Name: " + objectName);
					break;
			}
		}

		//ReadOnly, gets the planets from LevelManager's Children
		public Planet[] planetList
		{
			get
			{
				Planet[] planetlist = new Planet[this.GetChildren().Count];
				int index = 0;
				foreach (GameObject tObject in this.GetChildren())
				{
					if (tObject is Planet)
					{
						Planet planet = tObject as Planet;
						planetlist[index] = planet;
						index++;
					}
				}
				return planetlist;
			}
		}

		//ReadOnly, gets the asteroid from LevelManager's Children
		public Asteroid[] asteroidList
		{
			get
			{
				Asteroid[] asteroidlist = new Asteroid[this.GetChildren().Count];
				int index = 0;
				foreach (GameObject tObject in this.GetChildren())
				{
					if (tObject is Asteroid)
					{
						Asteroid asteroid = tObject as Asteroid;
						asteroidlist[index] = asteroid;
						index++;
					}
				}
				return asteroidlist;
			}
		}
		//ReadOnly, gets the asteroid from LevelManager's Children
		public Pickup[] pickupList
		{
			get
			{
				Pickup[] tPickupList = new Pickup[this.GetChildren().Count];
				int index = 0;
				foreach (GameObject tObject in this.GetChildren())
				{
					if (tObject is Pickup)
					{
						Pickup pickup = tObject as Pickup;
						tPickupList[index] = pickup;
						index++;
					}
				}
				return tPickupList;
			}
		}

		public Player GetPlayer()
		{
			foreach (GameObject tObject in GetChildren())
			{
				if (tObject is Player)
				{
					Player player = tObject as Player;
					return player;
				}
			}
			return null;
		}
	}
}
