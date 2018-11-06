using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sveit
{
    public static class AppSettings
    {
        private const string DefaultAuthEndpoint = "";
        private const string DefaultBaseEndpoint = "http://192.168.31.27:45455/";
        private const string DefaultAppliesEndpoint = "Apply";
        private const string DefaultCharactersEndpoint = "Character";
        private const string DefaultCharacterTypesEndpoint = "CharacterType";
        private const string DefaultCommentsEndpoint = "Comment";
        private const string DefaultContactsEndpoint = "Contact";
        private const string DefaultContactTypesEndpoint = "ContactType";
        private const string DefaultContentsEndpoint = "Content";
        private const string DefaultEndorsedSkillsEndpoint = "";
        private const string DefaultGamesEndpoint = "Game";
        private const string DefaultGendersEndpoint = "Gender";
        private const string DefaultGenresEndpoint = "Genre";
        private const string DefaultImagesEndpoint = "Image";
        private const string DefaultPlatformsEndpoint = "Platform";
        private const string DefaultPlayersEndpoint = "Player";
        private const string DefaultSkillsEndpoint = "Skill";
        private const string DefaultTagsEndpoint = "Tag";
        private const string DefaultTeamsEndpoint = "Team";
        private const string DefaultTokenEndpoint = "Token";
        private const string DefaultVacanciesEndpoint = "Vacancy";
        private const bool DefaultApiStatus = false;

        private static ISettings Settings => CrossSettings.Current;

        public static bool ApiStatus
        {
            get => Settings.GetValueOrDefault(nameof(ApiStatus), DefaultApiStatus);
            set => Settings.AddOrUpdateValue(nameof(ApiStatus), value);
        }

        public static string AuthEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(AuthEndpoint), DefaultAuthEndpoint);
            set => Settings.AddOrUpdateValue(nameof(AuthEndpoint), value);
        }

        public static string BaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(BaseEndpoint), DefaultBaseEndpoint);
            set => Settings.AddOrUpdateValue(nameof(BaseEndpoint), value);
        }

        public static string AppliesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(AppliesEndpoint), DefaultAppliesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(AppliesEndpoint), value);
        }

        public static string CharactersEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(CharactersEndpoint), DefaultCharactersEndpoint);
            set => Settings.AddOrUpdateValue(nameof(CharactersEndpoint), value);
        }

        public static string CharacterTypesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(CharacterTypesEndpoint), DefaultCharacterTypesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(CharacterTypesEndpoint), value);
        }

        public static string CommentsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(CommentsEndpoint), DefaultCommentsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(CommentsEndpoint), value);
        }

        public static string ContactsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(ContactsEndpoint), DefaultContactsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(ContactsEndpoint), value);
        }

        public static string ContactTypesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(ContactTypesEndpoint), DefaultContactTypesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(ContactTypesEndpoint), value);
        }

        public static string ContentsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(ContentsEndpoint), DefaultContentsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(ContentsEndpoint), value);
        }

        public static string EndorsedSkillsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(EndorsedSkillsEndpoint), DefaultEndorsedSkillsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(EndorsedSkillsEndpoint), value);
        }

        public static string GamesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(GamesEndpoint), DefaultGamesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(GamesEndpoint), value);
        }

        public static string GendersEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(GendersEndpoint), DefaultGendersEndpoint);
            set => Settings.AddOrUpdateValue(nameof(GendersEndpoint), value);
        }

        public static string GenresEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(GenresEndpoint), DefaultGenresEndpoint);
            set => Settings.AddOrUpdateValue(nameof(GenresEndpoint), value);
        }

        public static string ImagesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(ImagesEndpoint), DefaultImagesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(ImagesEndpoint), value);
        }

        public static string PlatformsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(PlatformsEndpoint), DefaultPlatformsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(PlatformsEndpoint), value);
        }

        public static string PlayersEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(PlayersEndpoint), DefaultPlayersEndpoint);
            set => Settings.AddOrUpdateValue(nameof(PlayersEndpoint), value);
        }

        public static string SkillsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(SkillsEndpoint), DefaultSkillsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(SkillsEndpoint), value);
        }

        public static string TagsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(TagsEndpoint), DefaultTagsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(TagsEndpoint), value);
        }

        public static string TeamsEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(TeamsEndpoint), DefaultTeamsEndpoint);
            set => Settings.AddOrUpdateValue(nameof(TeamsEndpoint), value);
        }
        
        public static string TokenEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(TokenEndpoint), DefaultTokenEndpoint);
            set => Settings.AddOrUpdateValue(nameof(TokenEndpoint), value);
        }


        public static string VacanciesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(VacanciesEndpoint), DefaultVacanciesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(VacanciesEndpoint), value);
        }
    }
}
