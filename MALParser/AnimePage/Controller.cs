using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto;
using MALParser.Dto.Utility;

namespace MALParser.AnimePage
{
    public class Controller
    {
        private string m_pageLink;
        private BaseAnimeInfo m_header;
        private CharactersPageData m_characters;
        private DetailsPageData m_details;
        private EpisodesPageData m_episodes;
        private PicturesPageData m_pictures;
        private ReviewsPageData m_reviews;
        private StatsPageData m_stats;
        private VideosPageData m_videos;

        public BaseAnimeInfo AnimeHeader
        {
            get
            {
                if (m_header != null)
                    return m_header;
                throw new Exception("Cannot return anime header because it does not exist. (Somwhow)");
            }
        }

        public CharactersPageData CharactersPage
        {
            get
            {
                if (m_characters != null)
                    return m_characters;
                else
                    return GetCharactersPage();
            }
        }
        public DetailsPageData DetailsPage
        {
            get
            {
                if (m_details != null)
                    return m_details;
                else
                    return GetDetailsPage();
            }
        }
        public EpisodesPageData EpisodesPage
        {
            get
            {
                if (m_episodes != null)
                    return m_episodes;
                else
                    return GetEpisodesPage();
            }
        }
        public PicturesPageData PicturesPage
        {
            get
            {
                if (m_pictures != null)
                    return m_pictures;
                else
                    return GetPicturesPage();
            }
        }
        public ReviewsPageData ReviewsPage
        {
            get
            {
                if (m_reviews != null)
                    return m_reviews;
                else
                    return GetReviewsPage();
            }
        }
        public StatsPageData StatsPage
        {
            get
            {
                if (m_stats != null)
                    return m_stats;
                else
                    return GetStatsPage();
            }
        }
        public VideosPageData VideosPage
        {
            get
            {
                if (m_videos != null)
                    return m_videos;
                else
                    return GetVideosPage();
            }
        }

        public Controller(string animePageLink)
        {
            m_pageLink = animePageLink;

            //No idea how to do that async currently.
            //TODO: do this async also
            m_header = Header.Parse(animePageLink);
        }

        public async Task<CharactersPageData> GetCharactersPageAsync()
        {
            if (m_characters != null)
                return m_characters;

            m_characters = await Characters.ParseAsync(m_header.Link_CharactersAndStaff.Path);
            return m_characters;
        }

        private CharactersPageData GetCharactersPage()
        {
            if (m_characters != null)
                return m_characters;

            m_characters = Characters.Parse(m_header.Link_CharactersAndStaff.Path);
            return m_characters;
        }

        public async Task<DetailsPageData> GetDetailsPageAsync()
        {
            if (m_details != null)
                return m_details;
            m_details = await Details.ParseAsync(m_header.Link_Details.Path);
            return m_details;
        }

        private DetailsPageData GetDetailsPage()
        {
            if (m_details != null)
                return m_details;
            m_details = Details.Parse(m_header.Link_Details.Path);
            return m_details;
        }

        public async Task<EpisodesPageData> GetEpisodesPageAsync()
        {
            if (m_episodes != null)
                return m_episodes;
            m_episodes = await Episodes.ParseAsync(m_header.Link_Episodes.Path);
            return m_episodes;
        }

        private EpisodesPageData GetEpisodesPage()
        {
            if (m_episodes != null)
                return m_episodes;
            m_episodes = Episodes.Parse(m_header.Link_Episodes.Path);
            return m_episodes;
        }

        public async Task<PicturesPageData> GetPicturesPageAsync()
        {
            if (m_pictures != null)
                return m_pictures;
            m_pictures = await Pictures.ParseAsync(m_header.Link_Pictures.Path);
            return m_pictures;
        }

        private PicturesPageData GetPicturesPage()
        {
            if (m_pictures != null)
                return m_pictures;
            m_pictures = Pictures.Parse(m_header.Link_Pictures.Path);
            return m_pictures;
        }

        public async Task<ReviewsPageData> GetReviewsPageAsync()
        {
            if (m_reviews != null)
                return m_reviews;
            m_reviews = await Reviews.ParseAsync(m_header.Link_Reviews.Path);
            return m_reviews;
        }

        private ReviewsPageData GetReviewsPage()
        {
            if (m_reviews != null)
                return m_reviews;
            m_reviews = Reviews.Parse(m_header.Link_Reviews.Path);
            return m_reviews;
        }

        public async Task<StatsPageData> GetStatsPageAsync()
        {
            if (m_stats != null)
                return m_stats;
            m_stats = await Stats.ParseAsync(m_header.Link_Stats.Path);
            return m_stats;
        }

        private StatsPageData GetStatsPage()
        {
            if (m_stats != null)
                return m_stats;
            m_stats = Stats.Parse(m_header.Link_Stats.Path); ;
            return m_stats;
        }

        public async Task<VideosPageData> GetVideosPageAsync()
        {
            if (m_videos != null)
                return m_videos;
            m_videos = await Videos.ParseAsync(m_header.Link_Videos.Path);
            return m_videos;
        }

        private VideosPageData GetVideosPage()
        {
            if (m_videos != null)
                return m_videos;
            m_videos = Videos.Parse(m_header.Link_Videos.Path);
            return m_videos;
        }


    }
}
