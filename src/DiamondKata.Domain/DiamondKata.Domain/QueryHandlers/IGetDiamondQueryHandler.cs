using DiamondKata.DomainService.ValueType;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DiamondKata.Domain.UnitTests"),
           InternalsVisibleTo("DiamondKata.Console"),
           InternalsVisibleTo("DiamondKata.Api"),
           InternalsVisibleTo("DiamondKata.Api.IntegrationTests")]
namespace DiamondKata.DomainService.QueryHandlers;

internal interface IGetDiamondQueryHandler
{
    string Handle(EnglishChar @char);
}