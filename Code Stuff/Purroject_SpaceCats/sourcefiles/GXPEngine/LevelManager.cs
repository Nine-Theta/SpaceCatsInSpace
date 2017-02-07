using System;
namespace Purroject_SpaceCats
{
	public class LevelManager
	{
		private XMLMap _currentMap;
		public LevelManager()
		{
			_currentMap = _currentMap.ReadMap(1);
		}

		void InterpretObject(TiledObject pObject)
		{
			string objectName = pObject.Properties.property[0].Name;
			string[] splitNames = objectName.Split(' ');
			switch (splitNames[0])
			{
				case "spawn":

					break;
					
				default:
					Console.WriteLine("Unknown object in Object Layer");
					Console.WriteLine("Name: " + objectName);
					break;
			}
		}
	}
}
