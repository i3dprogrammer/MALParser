using MALParser.Dto;
using MALParser.Dto.ListModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Lists;

namespace MALParser.Lists
{
    public class AnimeListManager
    {
        private const string m_malSeasonLink = "https://myanimelist.net/anime/season/{0}/{1}";
        private const string m_malGenreLink = "https://myanimelist.net/anime/genre/{0}";
        private const string m_malScheduleLink = "https://myanimelist.net/anime/season/schedule";
        private const string m_malLaterLInk = "https://myanimelist.net/anime/season/later";
        public async Task<AnimeListData> GetSeasonListAsync(int year, Season season)
        {
            return await AnimeListParser.ParseAsync(string.Format(m_malSeasonLink, year, season.ToString().ToLower()));
        }

        public AnimeListData GetSeasonList(int year, Season season)
        {
            return AnimeListParser.Parse(string.Format(m_malSeasonLink, year, season.ToString().ToLower()));
        }

        public async Task<AnimeListData> GetGenreListAsync(Genres genre)
        {
            return await AnimeListParser.ParseAsync(string.Format(m_malGenreLink, (int)genre));
        }

        public AnimeListData GetGenreList(Genres genre)
        {
            return AnimeListParser.Parse(string.Format(m_malGenreLink, (int)genre));
        }

        public async Task<AnimeListData> GetAiringScheduleAsync(ScheduleDay day)
        {
            List<CoreAnimeEntry> list = new List<CoreAnimeEntry>();
            var ret = await AnimeListParser.ParseAsync(m_malScheduleLink);
            ret.Animes.ForEach(x =>
            {
                if (DateTime.TryParse(x.Aired, out DateTime res))
                {
                    if (day == ScheduleDay.Any || (int)day == (int)res.DayOfWeek)
                        list.Add(x);
                }
                else if (day == ScheduleDay.Unknown)
                    list.Add(x);
            });
            ret.Animes = list;

            return ret;
        }

        public AnimeListData GetAiringSchedule(ScheduleDay day)
        {
            List<CoreAnimeEntry> list = new List<CoreAnimeEntry>();
            var ret = AnimeListParser.Parse(m_malScheduleLink);
            ret.Animes.ForEach(x =>
            {
                if (DateTime.TryParse(x.Aired, out DateTime res))
                {
                    if (day == ScheduleDay.Any || (int)day == (int)res.DayOfWeek)
                        list.Add(x);
                }
                else if (day == ScheduleDay.Unknown)
                    list.Add(x);
            });
            ret.Animes = list;

            return ret;
        }

        public async Task<AnimeListData> GetLaterListAsync()
        {
            return await AnimeListParser.ParseAsync(m_malLaterLInk);
        }

        public AnimeListData GetLaterList()
        {
            return AnimeListParser.Parse(m_malLaterLInk);
        }
    }
}
