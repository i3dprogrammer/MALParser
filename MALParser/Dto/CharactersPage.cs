﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class CharactersPage : BaseAnimeInfo
    {
        public List<CharacterInfo> Characters { get; set; }
        public List<PersonInfo> Staff { get; set; }
    }

    public class CharacterInfo
    {
        public string CharacterName { get; set; }
        public string CharacterRole { get; set; }
        public LinkInfo Link { get; set; }
        public List<PersonInfo> VoiceOvers { get; set; }
    }

    public class PersonInfo
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public LinkInfo Link { get; set; }
    }
}
