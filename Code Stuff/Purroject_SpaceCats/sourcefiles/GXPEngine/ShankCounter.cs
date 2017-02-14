using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace Purroject_SpaceCats
{
	[XmlRoot("shankcounter")]
	public class ShankCounter
	{
		[XmlAttribute("shanks")]
		public int shanks;

		private const string FILE_LOCATION = "Levels/ShankFiles.xml";

		public void AddShank()
		{
			ShankCounter shankCounter = ReadShanks();
			shankCounter.shanks++;
			shankCounter.WriteShanks();
			Console.WriteLine("Shank successfully added to counter");
		}

		public void WriteShanks()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ShankCounter));

			TextWriter writer = new StreamWriter(FILE_LOCATION);
			serializer.Serialize(writer, this);
			writer.Close();
		}
		public ShankCounter ReadShanks()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ShankCounter));

			TextReader reader = new StreamReader(FILE_LOCATION);
			ShankCounter shankCounter = serializer.Deserialize(reader) as ShankCounter;
			reader.Close();
			return shankCounter;
		}

		public int GetShanks()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ShankCounter));

			TextReader reader = new StreamReader(FILE_LOCATION);
			ShankCounter shankCounter = serializer.Deserialize(reader) as ShankCounter;
			reader.Close();
			reader.Dispose();
			return shankCounter.shanks;
		}
		public ShankCounter()
		{
		}
	}
}
