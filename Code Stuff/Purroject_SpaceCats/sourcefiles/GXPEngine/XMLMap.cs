using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Purroject_SpaceCats
{
	/// <summary>
	/// XML parser for object layers in tiled
	/// Be wary of the insanity ahead ye who traveleth here
	/// </summary>
	[XmlRoot("map")]
	public class XMLMap
	{
		public string[] levelFiles = { "Your level is in another variable", "/Levels/Level1.tmx", "Levels/Level2.tmx" };
		public const int PLAYER_RADIUS = 30;

		[XmlAttribute("width")]
		public int Width;
		[XmlAttribute("height")]
		public int Height;
		[XmlElement("tileset")]
		public TileSet tileSet;
		[XmlElement("objectgroup")]
		public ObjectGroup objectGroup;

		public XMLMap()
		{
		}

		public XMLMap ReadMap(int pLevel)
		{
			switch (pLevel)
			{
				case 0:
					Console.WriteLine(levelFiles[pLevel]);
					return new XMLMap();
				case 1:
					XmlSerializer serializer = new XmlSerializer(typeof(XMLMap));

					TextReader reader = new StreamReader(levelFiles[pLevel]);
					XMLMap map = serializer.Deserialize(reader) as XMLMap;
					return map;
				default:
					Console.WriteLine("Invalid map loaded, returning empty uninitialized map");
					return new XMLMap();
			}
		}
	}

	/// <summary>
	/// Object group (functionally identical to object layer).
	/// </summary>
	[XmlRoot("objectgroup")]
	public class ObjectGroup
	{
		[XmlAttribute("name")]
		public string Name;

		[XmlElement("object")]
		public TiledObject[] TiledObject;

		public ObjectGroup()
		{ 
		}
	}

	/// <summary>
	/// Tile set in Tiled.
	/// </summary>
	[XmlRoot("tileset")]
	public class TileSet
	{
		[XmlAttribute("name")]
		public string name;

		[XmlElement("image")]
		public Image image;

		[XmlAttribute("tilewidth")]
		public int tileWidth;

		[XmlAttribute("tileheight")]
		public int tileHeight;

		public override string ToString()
		{
			string stringdata = "";
			stringdata += "Name:" + name + Environment.NewLine;
			stringdata += "TileWidth:" + tileWidth + Environment.NewLine;
			stringdata += "TileHeight:" + tileHeight + Environment.NewLine;
			stringdata += "Image source:" + image + Environment.NewLine;
			return stringdata;
		}

		public TileSet()
		{ 
		}
	}

	/// <summary>
	/// Image of the tileset.
	/// </summary>
	[XmlRoot("image")]
	public class Image
	{
		[XmlAttribute("source")]
		public string source;

		[XmlAttribute("width")]
		public int width;

		[XmlAttribute("height")]
		public int height;

		public override string ToString()
		{
			string stringdata = "";
			stringdata += " Source:" + source + Environment.NewLine;
			stringdata += " FileWidth:" + width + Environment.NewLine;
			stringdata += " FileHeight:" + height + Environment.NewLine;
			return stringdata;
		}

		public Image()
		{ 
		}
	}

	/// <summary>
	/// Tiled objects in the layer.
	/// </summary>
	[XmlRoot("object")]
	public class TiledObject
	{
		[XmlAttribute("gid")]
		public int GID;
		[XmlAttribute("name")]
		public string Name;
		[XmlAttribute("x")]
		public int X;
		[XmlAttribute("y")]
		public int Y;
		[XmlAttribute("width")]
		public int Width;
		[XmlAttribute("height")]
		public int Height;


		[XmlElement("properties")]
		public Properties Properties;

		public TiledObject()
		{
		}
	}

	/// <summary>
	/// List of properties.
	/// </summary>
	[XmlRoot("properties")]
	public class Properties
	{
		[XmlElement("property")]
		public Property[] property;

		public override string ToString()
		{
			string stringdata = "";
			for (int i = 0; i < property.Length; i++)
			{
				stringdata += "  name: " + property[i] + Environment.NewLine;
			}
			return stringdata;
		}

		public Properties()
		{
		}
	}

	/// <summary>
	/// Property itself.
	/// </summary>
	[XmlRoot("property")]
	public class Property
	{
		[XmlAttribute("name")]
		public string Name;
		[XmlAttribute("value")]
		public string Value;

		public override string ToString()
		{
			string stringdata = "";
			stringdata += "   name: " + Name + Environment.NewLine;
			stringdata += "   value: " + Value + Environment.NewLine;
			return stringdata;
		}

		public Property()
		{
		}
	}
}
