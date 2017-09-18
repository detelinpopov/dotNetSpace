using System.Web.Mvc;
using Core.Services;
using Interfaces.Core.Services;
using Interfaces.Core.TestDataGenerators;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Sql.Entities;
using Sql.Repositories;

namespace InversionOfControl
{
    public class Register
    {
        public static void Start()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.BindInRequestScope<IAnswer, Answer>();
            container.BindInRequestScope<IQuestion, Question>();
            container.BindInRequestScope<IUser, User>();

            container.BindInRequestScope<IQuestionRepository, QuestionRepository>();
            container.BindInRequestScope<IUserRepository, UserRepository>();

            container.BindInRequestScope<IQuestionService, QuestionService>();
            container.BindInRequestScope<IUserService, UserService>();

            return container;
        }
    }
}