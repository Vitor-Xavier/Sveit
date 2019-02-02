using Autofac;
using Sveit.Services.Apply;
using Sveit.Services.Comment;
using Sveit.Services.Contact;
using Sveit.Services.ContactType;
using Sveit.Services.Content;
using Sveit.Services.Game;
using Sveit.Services.Gender;
using Sveit.Services.Genre;
using Sveit.Services.Image;
using Sveit.Services.Login;
using Sveit.Services.Navigation;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using Sveit.Services.Role;
using Sveit.Services.Skill;
using Sveit.Services.Tag;
using Sveit.Services.Team;
using Sveit.Services.Vacancy;
using System;

namespace Sveit.ViewModels.Base
{
    public class Locator
    {
        private IContainer container;
        private ContainerBuilder containerBuilder;

        public static Locator Instance { get; } = new Locator();

        public Locator()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<RequestService>().As<IRequestService>();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();

            if (AppSettings.ApiStatus)
            {
                containerBuilder.RegisterType<ApplyService>().As<IApplyService>();
                containerBuilder.RegisterType<CommentService>().As<ICommentService>();
                containerBuilder.RegisterType<ContactService>().As<IContactService>();
                containerBuilder.RegisterType<ContactTypeService>().As<IContactTypeService>();
                containerBuilder.RegisterType<ContentService>().As<IContentService>();
                containerBuilder.RegisterType<GameService>().As<IGameService>();
                containerBuilder.RegisterType<GenderService>().As<IGenderService>();
                containerBuilder.RegisterType<GenreService>().As<IGenreService>();
                containerBuilder.RegisterType<ImageService>().As<IImageService>();
                containerBuilder.RegisterType<LoginService>().As<ILoginService>();
                containerBuilder.RegisterType<PlayerService>().As<IPlayerService>();
                containerBuilder.RegisterType<RoleService>().As<IRoleService>();
                containerBuilder.RegisterType<SkillService>().As<ISkillService>();
                containerBuilder.RegisterType<TagService>().As<ITagService>();
                containerBuilder.RegisterType<TeamService>().As<ITeamService>();
                containerBuilder.RegisterType<VacancyService>().As<IVacancyService>();
            }
            else
            {
                containerBuilder.RegisterType<FakeApplyService>().As<IApplyService>();
                containerBuilder.RegisterType<FakeCommentService>().As<ICommentService>();
                containerBuilder.RegisterType<FakeContactService>().As<IContactService>();
                containerBuilder.RegisterType<FakeContactTypeService>().As<IContactTypeService>();
                containerBuilder.RegisterType<FakeContentService>().As<IContentService>();
                containerBuilder.RegisterType<FakeGameService>().As<IGameService>();
                containerBuilder.RegisterType<FakeGenderService>().As<IGenderService>();
                //containerBuilder.RegisterType<FakeGenreService>().As<IGenreService>();
                //containerBuilder.RegisterType<FakeImageService>().As<IImageService>();
                containerBuilder.RegisterType<FakeLoginService>().As<ILoginService>();
                containerBuilder.RegisterType<FakePlayerService>().As<IPlayerService>();
                containerBuilder.RegisterType<FakeRoleService>().As<IRoleService>();
                //containerBuilder.RegisterType<FakeSkillService>().As<ISkillService>();
                //containerBuilder.RegisterType<FakeTagService>().As<ITagService>();
                containerBuilder.RegisterType<FakeTeamService>().As<ITeamService>();
                containerBuilder.RegisterType<FakeVacancyService>().As<IVacancyService>();
            }
            

            containerBuilder.RegisterType<AppliesPlayerViewModel>();
            containerBuilder.RegisterType<AppliesTeamViewModel>();
            containerBuilder.RegisterType<ApplyViewModel>();
            containerBuilder.RegisterType<ApplyRegisterViewModel>();
            containerBuilder.RegisterType<CommentRegisterViewModel>();
            containerBuilder.RegisterType<ContactsPlayerRegisterViewModel>();
            containerBuilder.RegisterType<ContactsTeamRegisterViewModel>();
            containerBuilder.RegisterType<GameViewModel>();
            containerBuilder.RegisterType<GamesViewModel>();
            containerBuilder.RegisterType<HomeViewModel>();
            containerBuilder.RegisterType<LoginViewModel>();
            containerBuilder.RegisterType<MasterDetailMainViewModel>();
            containerBuilder.RegisterType<MasterMainViewModel>();
            containerBuilder.RegisterType<PlayerViewModel>();
            containerBuilder.RegisterType<PlayerRegisterViewModel>();
            containerBuilder.RegisterType<SettingsViewModel>();
            containerBuilder.RegisterType<TeamViewModel>();
            containerBuilder.RegisterType<TeamRegisterViewModel>();
            containerBuilder.RegisterType<VacanciesViewModel>();
            containerBuilder.RegisterType<VacanciesTeamViewModel>();
            containerBuilder.RegisterType<VacancyViewModel>();
            containerBuilder.RegisterType<VacancyRegisterViewModel>();
        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        //public object Resolve(Type type, params Autofac.Core.Parameter[] parameters) => container.Resolve(type, parameters);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => containerBuilder.RegisterType<T>();

        public void Build() => container = containerBuilder.Build();
    }
}
