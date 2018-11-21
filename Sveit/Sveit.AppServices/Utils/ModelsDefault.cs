using Sveit.Models;
using System;

namespace Sveit.AppServices.Utils
{
    /// <summary>
    /// Retorna os modelos com dados padrão inseridos.
    /// </summary>
    public static class ModelsDefault
    {
        public static Apply GetDefaultApply()
        {
            return new Apply
            {
                ApplyId = 0,
                Text = "Default",
                VacancyId = 0,
                PlayerId = 0,
                Approved = false,
                Deleted = false
            };
        }

        public static Comment GetDefaultComment()
        {
            return new Comment
            {
                CommentId = 0,
                Text = "Default",
                FromPlayerId = 0,
                ToPlayerId = 0,
                Deleted = false
            };
        }

        public static Contact GetDefaultContact()
        {
            return new Contact
            {
                ContactId = 0,
                Text = "Default",
                ContactTypeId = 0,
                Deleted = false
            };
        }

        public static ContactType GetDefaultContactType()
        {
            return new ContactType
            {
                ContactTypeId = 0,
                Name = "Default",
                Deleted = false
            };
        }

        public static Content GetDefaultContent()
        {
            return new Content
            {
                ContentId = 0,
                Title = "Default",
                Description = "Default",
                ContentUrl = "Default",
                ImageSource = "Default",
                Source = "Default",
                Deleted = false
            };
        }

        public static Game GetDefaultGame()
        {
            return new Game
            {
                GameId = 0,
                Name = "Default",
                BackgroundSource = "Default",
                IconSource = "Default",
                ImageSource = "Default",
                Deleted = false
            };
        }

        public static Gender GetDefaultGender()
        {
            return new Gender
            {
                GenderId = 0,
                Name = "Default",
                Deleted = false
            };
        }

        public static Genre GetDefaultGenre()
        {
            return new Genre
            {
                GenreId = 0,
                Name = "Default",
                Deleted = false
            };
        }

        public static Platform GetDefaultPlatform()
        {
            return new Platform
            {
                PlatformId = 0,
                Name = "Default",
                IconSource = "Default",
                Deleted = false
            };
        }

        public static Player GetDefaultPlayer()
        {
            return new Player
            {
                PlayerId = 0,
                Username = "Default",
                Email = "Default",
                Password = "Default",
                Name = "Default",
                Nickname = "Default",
                GenderId = 0,
                DateOfBirth = DateTime.Today,
                AvatarSource = "Default",
                Deleted = false
            };
        }

        public static Role GetDefaultRole()
        {
            return new Role
            {
                RoleId = 0,
                RoleTypeId = 0,
                Name = "Default",
                Deleted = false
            };
        }

        public static RoleType GetDefaultRoleType()
        {
            return new RoleType
            {
                RoleTypeId = 0,
                Name = "Default",
                IconSource = "Default",
                Deleted = false
            };
        }

        public static Skill GetDefaultSkill()
        {
            return new Skill
            {
                SkillId = 0,
                Name = "Default",
                Deleted = false
            };
        }

        public static Tag GetDefaultTag()
        {
            return new Tag
            {
                TagId = 0,
                Name = "Default",
                Deleted = false
            };
        }

        public static Team GetDefaultTeam()
        {
            return new Team
            {
                TeamId = 0,
                Name = "Default",
                GamePlatform_GameId = 0,
                GamePlatform_PlatformId = 0,
                IconSource = "Default",
                OwnerId = 0,
                Deleted = false
            };
        }

        public static Vacancy GetDefaultVacancy()
        {
            return new Vacancy
            {
                VacancyId = 0,
                Title = "Default",
                Description = "Default",
                MaxAge = 0,
                MinAge = 0,
                TeamId = 0,
                Available = true,
                Deleted = false
            };
        }
    }
}