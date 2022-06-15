
using FluentValidation.AspNetCore;
using MediatR;
using MtgLiveLifeCounter.Api.Authentication;
using MtgLiveLifeCounter.Core.Contracts;
using System.Reflection;
using System.Security.Principal;

namespace MtgLiveLifeCounter.Api
{
    public static class MtgLiveLifeCollectionExtensions
    {
        public static void AddApi(this IServiceCollection services, Action<MtgApiBuilder> configure)
        {
            var builer = new MtgApiBuilder();

            configure(builer);

            var configuration = builer.Build();

            if (configuration.UowType is Type uowType)
                services.AddScoped(typeof(IUow), uowType);

            if (configuration.HandlersAssembly is Assembly handlersAssembly)
                services.AddMediatR(opts => opts.AsScoped(), handlersAssembly);

            if (configuration.ValidatorsAssembly is Assembly validatorsAssembly)
                services.AddFluentValidation(opt => opt.RegisterValidatorsFromAssembly(validatorsAssembly));

            services.AddHttpContextAccessor();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            services.AddTransient<IPrincipal>(provider => provider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .User);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        }
    }

    internal class MtgApi
    {
        public Assembly HandlersAssembly { get; set; }
        public Assembly ValidatorsAssembly { get; set; }
        public Type UowType { get; set; }
    }

    public class MtgApiBuilder
    {
        private Assembly _validatorsAssembly;
        private Assembly _handlersAssembly;
        private Type _uowType;

        public MtgApiBuilder WithValidatorsFromAssemblyOf<T>()
        {
            _validatorsAssembly = typeof(T).GetTypeInfo().Assembly;
            return this;
        }

        public MtgApiBuilder WithCQRSFromAssemblyOf<T>()
        {
            _handlersAssembly = typeof(T).GetTypeInfo().Assembly;
            return this;
        }

        public MtgApiBuilder WithUow<T>() where T : IUow
        {
            _uowType = typeof(T);
            return this;
        }

        internal MtgApi Build() => new()
        {
            HandlersAssembly = _handlersAssembly,
            ValidatorsAssembly = _validatorsAssembly,
            UowType = _uowType
        };

    }

}
