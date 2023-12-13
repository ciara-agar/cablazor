using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace blazorappca.Models
{
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string AlbumUri { get; set; }
        public string ArtistUri { get; set; }
        public string CoverUrl { get; set; }
        public string ReleaseYear { get; set; }
        // Add other relevant properties for a song

        // Example of additional properties (you can customize this based on your needs)
        public string Duration { get; set; }
        public string TrackUri { get; set; }
        public List<string> Genres { get; set; }
        // ...
    }

    public class ApiResponse
    {
        public List<Song> Response { get; set; }
    }



    public class Data
    {
        public BrowseStart BrowseStart { get; set; }
    }

    public class BrowseData
    {
        public string Uri { get; set; }
        public string Title { get; set; }

        public string ArtworkSource { get; set; }

    }

    public class BrowseStart
    {
        public Sections Sections { get; set; }
        public string Uri { get; set; }
    }

    public class Sections
    {
        public List<SectionItem> Items { get; set; }
    }

    public class SectionItem
    {
        public string Uri { get; set; }
        public SectionItemContent Content { get; set; }
    }

    public class SectionItemContent
    {
        public SectionItemData Data { get; set; }
    }

    public class SectionItemData
    {
        public BrowseSectionContainerWrapper Container { get; set; }
    }

    public class BrowseSectionContainerWrapper
    {
        public BrowseSectionContainer Data { get; set; }
    }

    public class BrowseSectionContainer
    {
        public CardRepresentation CardRepresentation { get; set; }
    }

    public class CardRepresentation
    {
        public Title Title { get; set; }
        public Artwork Artwork { get; set; }
        public BackgroundColor BackgroundColor { get; set; }
    }

    public class Title
    {
        public string TransformedLabel { get; set; }
    }

    public class Artwork
    {
        public List<ArtworkSource> Sources { get; set; }
    }

    public class ArtworkSource
    {
        public string Url { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }

    public class BackgroundColor
    {
        public string Hex { get; set; }
    }

    public class ConcertData
    {
        public string Concert { get; set; }
        public string Artists { get; set; }
        public string UpcomingConcerts { get; set; }
        public string Color { get; set; }
        public string HeaderImageUri { get; set; }
        public string UpcomingConcertsSource { get; set; }
        public string UserLocation { get; set; }
        public string TicketAvailability { get; set; }
        public string TicketAvailabilityTranslated { get; set; }

    }

    public class UserData
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string FollowersCount { get; set; }
        public string FollowingCount { get; set; }
        public PublicPlaylistsInfo PublicPlaylists { get; set; }
        public bool IsVerified { get; set; }
        public bool ReportAbuseDisabled { get; set; }
        public bool HasSpotifyName { get; set; }
        public bool HasSpotifyImage { get; set; }
        public int Color { get; set; }
        public bool AllowFollows { get; set; }
        public bool ShowFollows { get; set; }
    }
     public class ApiUserResponse
    {
        public List<UserData> Response { get; set; }
    }
    public class PublicPlaylistsInfo
    {
        public int TotalPublicPlaylistsCount { get; set; }
    }

}