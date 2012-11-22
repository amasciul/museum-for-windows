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
            string content = await GetDataStringAsync();

            Debug.WriteLine(content);

        }

        private async Task<string> GetDataStringAsync()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost/museum/data.xml");
                WebResponse response = await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (WebException)
            {
                Debug.WriteLine("Error during request");
                return null;
            }
            
        }
    }
}
