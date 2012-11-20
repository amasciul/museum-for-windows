using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum
{
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

    class Artwork
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
    }
}
