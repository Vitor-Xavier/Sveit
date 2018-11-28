using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Sveit
{
    public static class AppSettings
    {
        private const string DefaultLanguage = "";
        private const string DefaultAuthEndpoint = "Token";
        private const string DefaultBaseEndpoint = "https://sveit.azurewebsites.net/";
        private const string DefaultAppliesEndpoint = "Apply";
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
        private const string DefaultRolesEndpoint = "Role";
        private const string DefaultSkillsEndpoint = "Skill";
        private const string DefaultTagsEndpoint = "Tag";
        private const string DefaultTeamsEndpoint = "Team";
        private const double DefaultTokenDuration = 90.0;
        private const string DefaultVacanciesEndpoint = "Vacancy";
        private const bool DefaultApiStatus = true;
        private const bool DefaultCredentialStatus = true;

        private static ISettings Settings => CrossSettings.Current;

        public static string Language
        {
            get => Settings.GetValueOrDefault(nameof(Language), DefaultLanguage);
            set => Settings.AddOrUpdateValue(nameof(Language), value);
        }

        public static bool ApiStatus
        {
            get => Settings.GetValueOrDefault(nameof(ApiStatus), DefaultApiStatus);
            set => Settings.AddOrUpdateValue(nameof(ApiStatus), value);
        }

        public static bool CredentialStatus
        {
            get => Settings.GetValueOrDefault(nameof(CredentialStatus), DefaultCredentialStatus);
            set => Settings.AddOrUpdateValue(nameof(CredentialStatus), value);
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

        public static string RolesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(RolesEndpoint), DefaultRolesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(RolesEndpoint), value);
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

        public static double TokenDuration
        {
            get => Settings.GetValueOrDefault(nameof(TokenDuration), DefaultTokenDuration);
            set => Settings.AddOrUpdateValue(nameof(TokenDuration), value);
        }

        public static string VacanciesEndpoint
        {
            get => BaseEndpoint + Settings.GetValueOrDefault(nameof(VacanciesEndpoint), DefaultVacanciesEndpoint);
            set => Settings.AddOrUpdateValue(nameof(VacanciesEndpoint), value);
        }
    }
}
