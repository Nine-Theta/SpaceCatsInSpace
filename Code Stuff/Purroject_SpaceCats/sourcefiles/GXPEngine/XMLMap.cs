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
		[XmlAttribute("width")]
		public int Width = 20;
		[XmlAttribute("height")]
		public int Height = 20;
		[XmlElement("tileset")]
		public TileSet tileSet;
		[XmlElement("objectgroup")]
		public ObjectGroup objectGroup;

		public XMLMap()
		{
		}
	}

	/// <summary>
	/// Object group (functionally identical to object layer).
	/// </summary>
	[XmlRootAttribute("objectgroup")]
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
			stringdata += "Source:" + source + Environment.NewLine;
			stringdata += "FileWidth:" + width + Environment.NewLine;
			stringdata += "FileHeight:" + height + Environment.NewLine;
			return stringdata;
		}

		public Image()
		{ 
		}
	}

	/// <summary>
	/// Tiled objects in the layer.
	/// </summary>
	[XmlRootAttribute("object")]
	public class TiledObject
	{
		[XmlAttribute("gid")]
		public int GID;
		[XmlAttribute("x")]
		public int X;
		[XmlAttribute("y")]
		public int Y;

		[XmlElement("properties")]
		public Properties Properties;

		public TiledObject()
		{
		}
	}

	/// <summary>
	/// List of properties.
	/// </summary>
	[XmlRootAttribute("properties")]
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
	[XmlRootAttribute("property")]
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
