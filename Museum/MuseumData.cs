using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum
{
    // RoomData
    // Holds info for a single room,, including a list of artworks (Artwork)
    class RoomData
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
        public string Author { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
    }

    // MuseumSource
    // Holds a collection of rooms (RoomData), and contains methods needed to
    // retreive the rooms.
    class MuseumSource
    {
        private ObservableCollection<RoomData> _Rooms = new ObservableCollection<RoomData>();

    }
}
