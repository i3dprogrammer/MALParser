using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.AnimePageModels;
using MALParser.Dto.Utility;
using MALParser.Dto;

namespace MALParser.AnimePage
{
    public class Controller
    {
        private string m_pageLink;
        private AnimePageHeader m_header;
        private CharactersPageData m_characters;
        private DetailsPageData m_details;
        private EpisodesPageData m_episodes;
        private PicturesPageData m_pictures;
        private ReviewsPageData m_reviews;
        private StatsPageData m_stats;
        private VideosPageData m_videos;

        public AnimePageHeader GetAnimeHeader
        {
            get
            {
                if (m_header != null)
                    return m_header;
                throw new Exception("Cannot return anime header because it does not exist. (Somehow)");
            }
        }

        public Controller(int animeId)
        {
            //TODO: Get link from anime id
            //m_pageLink = animePageLink;
        }

        public Controller(BaseAnimeEntry entry)
        {
            m_header = Header.Parse(entry.AnimeLink.Path);
        }

        public async void InitializeAsync()
        {
            m_header = await Header.ParseAsync(m_pageLink);
        }

        public void Initialize()
        {
            m_header = Header.Parse(m_pageLink);
        }

        public async Task<CharactersPageData> GetCharactersPageAsync()
        {
            if (m_characters != null)
                return m_characters;

            if (m_header.Link_CharactersAndStaff == null)
                throw new Exception("There's no characters & staff page for this anime.");

            m_characters = await Characters.ParseAsync(m_header.Link_CharactersAndStaff.Path);
            return m_characters;
        }

        public CharactersPageData GetCharactersPage()
        {
            if (m_characters != null)
                return m_characters;

            if (m_header.Link_CharactersAndStaff == null)
                throw new Exception("There's no characters & staff page for this anime.");

            m_characters = Characters.Parse(m_header.Link_CharactersAndStaff.Path);
            return m_characters;
        }

        public async Task<DetailsPageData> GetDetailsPageAsync()
        {
            if (m_details != null)
                return m_details;

            if (m_header.Link_Details == null)
                throw new Exception("There's no details page for this anime.");

            m_details = await Details.ParseAsync(m_header.Link_Details.Path);
            return m_details;
        }

        public DetailsPageData GetDetailsPage()
        {
            if (m_details != null)
                return m_details;

            if (m_header.Link_Details == null)
                throw new Exception("There's no details page for this anime.");

            m_details = Details.Parse(m_header.Link_Details.Path);
            return m_details;
        }

        public async Task<EpisodesPageData> GetEpisodesPageAsync()
        {
            if (m_episodes != null)
                return m_episodes;

            if (m_header.Episodes == null)
                throw new Exception("There's no episodes page for this anime.");

            m_episodes = await Episodes.ParseAsync(m_header.Link_Episodes.Path);
            return m_episodes;
        }

        public EpisodesPageData GetEpisodesPage()
        {
            if (m_episodes != null)
                return m_episodes;

            if (m_header.Episodes == null)
                throw new Exception("There's no episodes page for this anime.");

            m_episodes = Episodes.Parse(m_header.Link_Episodes.Path);
            return m_episodes;
        }

        public async Task<PicturesPageData> GetPicturesPageAsync()
        {
            if (m_pictures != null)
                return m_pictures;

            if (m_header.Link_Pictures == null)
                throw new Exception("There's no pictures page for this anime.");

            m_pictures = await Pictures.ParseAsync(m_header.Link_Pictures.Path);
            return m_pictures;
        }

        public PicturesPageData GetPicturesPage()
        {
            if (m_pictures != null)
                return m_pictures;

            if (m_header.Link_Pictures == null)
                throw new Exception("There's no pictures page for this anime.");

            m_pictures = Pictures.Parse(m_header.Link_Pictures.Path);
            return m_pictures;
        }

        public async Task<ReviewsPageData> GetReviewsPageAsync()
        {
            if (m_reviews != null)
                return m_reviews;

            if (m_header.Link_Reviews == null)
                throw new Exception("There's no reviews page for this anime.");

            m_reviews = await Reviews.ParseAsync(m_header.Link_Reviews.Path);
            return m_reviews;
        }

        public ReviewsPageData GetReviewsPage()
        {
            if (m_reviews != null)
                return m_reviews;

            if (m_header.Link_Reviews == null)
                throw new Exception("There's no reviews page for this anime.");

            m_reviews = Reviews.Parse(m_header.Link_Reviews.Path);
            return m_reviews;
        }

        public async Task<StatsPageData> GetStatsPageAsync()
        {
            if (m_stats != null)
                return m_stats;

            if (m_header.Link_Stats == null)
                throw new Exception("There's no stats page for this anime.");

            m_stats = await Stats.ParseAsync(m_header.Link_Stats.Path);
            return m_stats;
        }

        public StatsPageData GetStatsPage()
        {
            if (m_stats != null)
                return m_stats;

            if (m_header.Link_Stats == null)
                throw new Exception("There's no stats page for this anime.");

            m_stats = Stats.Parse(m_header.Link_Stats.Path); ;
            return m_stats;
        }

        public async Task<VideosPageData> GetVideosPageAsync()
        {
            if (m_videos != null)
                return m_videos;

            if (m_header.Link_Videos == null)
                throw new Exception("There's no videos page for this anime.");

            m_videos = await Videos.ParseAsync(m_header.Link_Videos.Path);
            return m_videos;
        }

        public VideosPageData GetVideosPage()
        {
            if (m_videos != null)
                return m_videos;

            if (m_header.Link_Videos == null)
                throw new Exception("There's no videos page for this anime.");

            m_videos = Videos.Parse(m_header.Link_Videos.Path);
            return m_videos;
        }


    }
}

//public CharactersPageData CharactersPage
//{
//    get
//    {
//        if (m_characters != null)
//            return m_characters;
//        else
//            return GetCharactersPage();
//    }
//}
//public DetailsPageData DetailsPage
//{
//    get
//    {
//        if (m_details != null)
//            return m_details;
//        else
//            return GetDetailsPage();
//    }
//}
//public EpisodesPageData EpisodesPage
//{
//    get
//    {
//        if (m_episodes != null)
//            return m_episodes;
//        else
//            return GetEpisodesPage();
//    }
//}
//public PicturesPageData PicturesPage
//{
//    get
//    {
//        if (m_pictures != null)
//            return m_pictures;
//        else
//            return GetPicturesPage();
//    }
//}
//public ReviewsPageData ReviewsPage
//{
//    get
//    {
//        if (m_reviews != null)
//            return m_reviews;
//        else
//            return GetReviewsPage();
//    }
//}
//public StatsPageData StatsPage
//{
//    get
//    {
//        if (m_stats != null)
//            return m_stats;
//        else
//            return GetStatsPage();
//    }
//}
//public VideosPageData VideosPage
//{
//    get
//    {
//        if (m_videos != null)
//            return m_videos;
//        else
//            return GetVideosPage();
//    }
//}