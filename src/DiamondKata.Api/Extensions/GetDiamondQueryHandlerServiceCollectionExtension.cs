using DiamondKata.DomainService.QueryHandlers.GetPadding;
using DiamondKata.DomainService.QueryHandlers.GetRowForChar;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.Api.GetDiamondResultQueryHandler;

namespace DiamondKata.Api.Extensions
{
    internal static class GetDiamondQueryHandlerServiceCollectionExtension
    {
        public static IServiceCollection AddGetDiamondQueryHandler(this IServiceCollection services)
        {
            services.AddSingleton<IGetDiamondQueryHandler, GetDiamondQueryHandler>();
            services.AddSingleton<IGetRowForCharQueryHandler, GetRowForCharQueryHandler>();
            services.AddSingleton<IGetOuterPaddingQueryHandler, GetOuterPaddingQueryHandler>();
            services.AddSingleton<IGetInnerPaddingQueryHandler, GetInnerPaddingQueryHandler>();
            services
                .AddSingleton<IGetDiamondResultQueryHandler,
                    GetDiamondResultQueryHandler.GetDiamondResultQueryHandler>();
            return services;
        }   
    }
}
