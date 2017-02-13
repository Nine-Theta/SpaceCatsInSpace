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
			LoadLevel(1);
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
					break;
				case "Planet":
					string fileSource = "Sprites/Planet ";
					string PartOfSource = "";
					if (pObject.Properties != null && pObject.Properties.property[0].Name == "PlanetType")
					{
						try
						{
							PartOfSource = pObject.Properties.property[0].Value;
						}
						catch
						{
							Console.WriteLine("InterpretObject failed to pass on value to the planet");
						}
					}
					if (PartOfSource == "")
					{
						PartOfSource = "1";
					}
					fileSource += PartOfSource + ".png";
					Planet planet = new Planet(new Vec2(pObject.X, pObject.Y), fileSource, 5, 0.2f, 300);

					//AddPlanetList(planet);
					AddChild(planet);
					break;
				case "Station":
					//Changes frame if frame is the starting station, else it uses another frame;
					SpaceStation station = new SpaceStation(pObject.X, pObject.Y, "Sprites/SpaceStationTemp.png");
					AddChild(station);
					break;
				case "Black":
					BlackHole blackhole = new BlackHole(new Vec2(pObject.X, pObject.Y), 5, 300);
					AddChild(blackhole);
					break;
				case "Blackhole":
					BlackHole blackhole1 = new BlackHole(new Vec2(pObject.X, pObject.Y), 5, 300);
					AddChild(blackhole1);
					break;
				case "Meteor":
					Asteroid asteroid = new Asteroid(350, new Vec2(pObject.X, pObject.Y));
					AddChild(asteroid);
					break;
				case "Asteroid":
					Asteroid asteroid1 = new Asteroid(350, new Vec2(pObject.X, pObject.Y));
					AddChild(asteroid1);
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
				foreach (Planet planet in this.GetChildren())
				{
					planetlist[index] = planet;
					index++;
				}
				return planetlist;
			}
		}
	}
}
