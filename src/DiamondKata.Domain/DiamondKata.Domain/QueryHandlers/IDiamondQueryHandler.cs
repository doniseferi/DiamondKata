using DiamondKata.DomainService.Requests;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IDiamondQueryHandler
{
    string Handle(DiamondRequest request);
}