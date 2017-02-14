using System;
using Purroject_SpaceCats;

namespace GXPEngine
{
	public class LevelManager : GameObject
	{
		private XMLMap _currentMap;
		private int _currentlevel;

		public LevelManager()
		{
			_currentMap = new XMLMap();
			LoadLevel(3);
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
			string objectName = pObject.Name;
			string[] splitNames = objectName.Split(' ');
			switch (splitNames[0])
			{
				case "Player":
					Player player = new Player(XMLMap.PLAYER_RADIUS, new Vec2(pObject.X, pObject.Y));
					player.levelRef = this;
					AddChild(player);
					player.rotation = pObject.Rotation;
					break;
				case "Planet":
					string fileSource = "Sprites/Planet ";
					string PartOfSource = "";
					switch (pObject.GID)
					{
						case 7:
							PartOfSource = "1";
							break;
						case 8:
							PartOfSource = "2";
							break;
						case 9:
							PartOfSource = "4";
							break;
						case 10:
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
					Planet planet = new Planet(new Vec2(pObject.X, pObject.Y), fileSource, 5, 0.2f, 300);
					planet.rotation = pObject.Rotation;
					//AddPlanetList(planet);
					AddChild(planet);
					break;
				case "Station":
					//Changes frame if frame is the starting station, else it uses another frame;
					SpaceStation station = new SpaceStation(pObject.X, pObject.Y, "Sprites/SpaceStation-v2.png");
					AddChild(station);
					station.rotation = pObject.Rotation;
					break;
				case "Black":
					BlackHole blackhole = new BlackHole(new Vec2(pObject.X, pObject.Y), 5, 300);
					AddChild(blackhole);
					blackhole.rotation = pObject.Rotation;
					break;
				case "Blackhole":
					BlackHole blackhole1 = new BlackHole(new Vec2(pObject.X, pObject.Y), 5, 300);
					AddChild(blackhole1);
					blackhole1.rotation = pObject.Rotation;
					break;
				case "Meteor":
					Asteroid asteroid = new Asteroid(350, new Vec2(pObject.X, pObject.Y));
					AddChild(asteroid);
					asteroid.rotation = pObject.Rotation;
					break;
				case "Asteroid":
					Asteroid asteroid1 = new Asteroid(350, new Vec2(pObject.X, pObject.Y));
					AddChild(asteroid1);
					asteroid1.rotation = pObject.Rotation;
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
