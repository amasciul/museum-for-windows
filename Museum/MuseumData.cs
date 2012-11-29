using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Museum
{
	// Room
	// Holds info for a single room,, including a list of artworks (Artwork)
	class Room
	{
		public string Name { get; set; }
		public string Description { get; set; }
		private List<Artwork> _artworks = new List<Artwork>();
		public List<Artwork> Artworks
		{
			get
			{
				return this._artworks;
			}
		}
	}

	// Artwork
	// Holds info for a single artwork
	class Artwork
	{
		public string Title { get; set; }
		public string Artist { get; set; }
		public string Description { get; set; }
		public Uri Image { get; set; }
	}

	// MuseumSource
	// Holds a collection of rooms (Room), and contains methods needed to
	// retreive the rooms.
	class MuseumSource
	{
		private ObservableCollection<Room> _Rooms = new ObservableCollection<Room>();

		public ObservableCollection<Room> Rooms
		{
			get
			{
				return this._Rooms;
			}
		}

		public async Task GetRoomsAsync()
		{
			Stream contentStream = await GetDataStringAsync();
			try
			{
				XmlReader reader = XmlReader.Create(contentStream);

				Room currentRoom = null;
				Artwork currentArtwork = null;

				while (reader.Read())
				{
					if (reader.IsStartElement())
					{
						switch (reader.Name)
						{
							case "room":
								currentRoom = new Room();
								currentRoom.Name = reader["name"];
								currentRoom.Description = reader["description"];
								_Rooms.Add(currentRoom);
								break;
							case "artwork":
								currentArtwork = new Artwork();
								currentArtwork.Title = reader["title"];
								currentArtwork.Artist = reader["artist"];
								currentArtwork.Description = reader["desc"];
								currentArtwork.Image = new Uri(reader["image"]);
								if (currentRoom != null) currentRoom.Artworks.Add(currentArtwork);
								break;
						}
					}
				}
			}
			catch (XmlException e)
			{
				Debug.WriteLine("Error during XML reading");
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.ToString());
			}



		}

		public Room GetRoom(String roomName)
		{
			foreach(Room r in Rooms)
			{
				if(r.Name == roomName)return r;
			}
			return null;
		}

		public override string ToString()
		{
			string s = "";
			foreach (Room room in Rooms)
			{
				s += room.Name + "\n";
				s += room.Description + "\n";
				foreach (Artwork artwork in room.Artworks)
				{
					s += artwork.Title + "\n";
					s += artwork.Artist + "\n";
					s += artwork.Description + "\n";
					s += artwork.Image + "\n";
				}
			}
			return s;
		}

		private async Task<Stream> GetDataStringAsync()
		{
			try
			{
				WebRequest request = WebRequest.Create("http://localhost/museum/data.xml");
				WebResponse response = await request.GetResponseAsync();
				Stream stream = response.GetResponseStream();
				return stream;
			}
			catch (WebException)
			{
				Debug.WriteLine("Error during request");
				return null;
			}

		}
	}
}
