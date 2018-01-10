﻿using System.Web.Mvc;
using Core.Services;
using Interfaces.Core.Services;
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

            RegisterEntities(container);
            RegisterRepositories(container);
            RegisterServices(container);

            return container;
        }

        private static void RegisterEntities(UnityContainer container)
        {
            container.BindInRequestScope<IAnswer, Answer>();
            container.BindInRequestScope<IQuestion, Question>();
            container.BindInRequestScope<IUser, User>();
            container.BindInRequestScope<IFeedback, Feedback>();
            container.BindInRequestScope<IPublicFeedbackMessage, PublicFeedbackMessage>();
        }

        private static void RegisterRepositories(UnityContainer container)
        {
            container.BindInRequestScope<IQuestionRepository, QuestionRepository>();
            container.BindInRequestScope<IUserRepository, UserRepository>();
            container.BindInRequestScope<IFeedbackRepository, FeedbackRepository>();
            container.BindInRequestScope<IPublicFeedbackRepository, PublicFeedbackRepository>();
        }

        private static void RegisterServices(UnityContainer container)
        {
            container.BindInRequestScope<IQuestionService, QuestionService>();
            container.BindInRequestScope<IUserService, UserService>();
            container.BindInRequestScope<IFeedbackService, FeedbackService>();
            container.BindInRequestScope<IPublicFeedbackService, PublicFeedbackService>();
        }
    }
}