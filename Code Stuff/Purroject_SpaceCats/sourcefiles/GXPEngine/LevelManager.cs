using System;
using Purroject_SpaceCats;

namespace GXPEngine
{
	public class LevelManager : GameObject
	{
		private XMLMap _currentMap;
		private int _currentlevel;
		//private Planet[] this.GetChildren() = new Planet[20];

		public LevelManager()
		{
			_currentMap = new XMLMap();
			LoadLevel(1);
		}

		void LoadLevel(int pLevel)
		{
			//ClearPlanetList();
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
					string fileSource = "Planet ";
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
					Planet planet = new Planet(new Vec2(pObject.X, pObject.Y), fileSource, 30, 1, 100, 1);

					//AddPlanetList(planet);
					AddChild(planet);
					break;
				case "Station":
					//Changes frame if frame is the starting station, else it uses another frame;
					SpaceStation station = new SpaceStation(pObject.X, pObject.Y, "checkers.png");
					AddChild(station);
					break;
				case "Black":
					BlackHole blackhole = new BlackHole(new Vec2(pObject.X, pObject.Y), 100, 300);
					AddChild(blackhole);
					break;
				case "Meteor":
					Asteroid asteroid = new Asteroid(350, new Vec2(pObject.X, pObject.Y));
					AddChild(asteroid);
					break;
				default:
					Console.WriteLine("Unknown object in Object Layer");
					Console.WriteLine("Name: " + objectName);
					break;
			}
		}

		////TODO: SOMETHING WITH THIS MAYBE
		//PlanetList setup for accessibility
		//public void AddPlanetList(Planet pPlanet)
		//{
		//	int index = this.GetChildren().Count;
		//	this.GetChildren()[index - 1] = pPlanet;
		//}

		//public void ClearPlanetList()
		//{
		//	int index = 0;
		//	foreach (Planet planet in this.GetChildren()) //for (int i = 0; i < this.GetChildren().Length; i++)
		//	{
		//		//Planet planet = this.GetChildren()[i];
		//		planet.Destroy();
		//		this.GetChildren()[index] = null;
		//		index++;
		//	}
		//}

		//ReadOnly, add through AddPlanetList. Only clear when changing levels
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
