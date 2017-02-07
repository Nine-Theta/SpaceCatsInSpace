using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class LevelManager : GameObject
	{
		private XMLMap _currentMap;
		private int _currentlevel;
		public LevelManager()
		{
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
			string objectName = pObject.Properties.property[0].Name;
			string[] splitNames = objectName.Split(' ');
			switch (splitNames[0])
			{
				case "Player":
					Player player = new Player(XMLMap.PLAYER_RADIUS ,new Vec2(pObject.X, pObject.Y));
					AddChild(player);
					break;
				case "Planet":
					Planet planet = new Planet(new Vec2(pObject.X, pObject.Y), "");
					AddChild(planet);
					break;
				case "Station":
					//Changes frame if frame is the start, else it uses another frame;
					int frame;
					if (splitNames[1] == "Start")
						frame = 0;
					else
						frame = 1;
					SpaceStation station = new SpaceStation(pObject.X, pObject.Y, frame);
					AddChild(station);
					break;
				default:
					Console.WriteLine("Unknown object in Object Layer");
					Console.WriteLine("Name: " + objectName);
					break;
			}
		}
	}
}
