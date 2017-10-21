﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser
{
    public enum SourceType
    {
        Anime,
        Manga,
        VisualNovel,
        LightNovel,
        Original,
        Novel,
        WebManga,
        DigitalManga,
        Game,
        Music,
        komaManga,
        CardGame,
        Book,
        PictureBook,
        Radio,
        Other,
    }

    public enum FieldName
    {
        English,
        Synonyms,
        Japanese,
        Type,
        Episodes,
        Status,
        Aired,
        Premiered,
        Broadcast,
        Producers,
        Licensors,
        Studios,
        Source,
        Genres,
        Duration,
        Rating,
        Score,
        Ranked,
        Popularity,
        Members,
        Favorites,
        None,
    }

    public enum RelatedAnime
    {
        Adaptation,
        Alternativeversion,
        Alternativesetting,
        Sidestory,
        Spinoff,
        Otherlinks,
        Other,
        Sequel,
        Prequel,
        Summary,
        Parentstory,
        Characters,
        None
    }

    public enum AnimeStatsType
    {
        Watching,
        Completed,
        OnHold,
        Dropped,
        PlantoWatch,
        Total,
    }

    public enum Season
    {
        Winter,
        Spring,
        Summer,
        Fall,
        Other,
    }
}
