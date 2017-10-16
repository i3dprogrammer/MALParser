using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class CharactersPage
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public CharactersPage(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public List<CharacterInfo> Characters { get; set; } = new List<CharacterInfo>();
        public List<PersonInfo> Staff { get; set; } = new List<PersonInfo>();
    }

    public class CharacterInfo
    {
        public string CharacterName { get; set; }
        public string CharacterRole { get; set; }
        public LinkInfo ImageLink { get; set; }
        public LinkInfo CharacterLink { get; set; }
        public List<PersonInfo> VoiceOvers { get; set; } = new List<PersonInfo>();
    }

    public class PersonInfo
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public LinkInfo ImageLink { get; set; }
        public LinkInfo Link { get; set; }
    }
}
