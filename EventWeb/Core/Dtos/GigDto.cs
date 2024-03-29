using System;

namespace EventWeb.Core.Dtos
{
    public class GigDto
    {
        public int GigId { get; set; }
        public int Id { get; set; }
        public UserDto Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }
        public bool IsCanceled { get; set; }
    }
}