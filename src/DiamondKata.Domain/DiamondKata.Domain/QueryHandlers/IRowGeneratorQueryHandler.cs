using DiamondKata.DomainService.Requests;

namespace DiamondKata.DomainService.QueryHandlers;

interface IRowGeneratorQueryHandler
{
    string Handle(RowQueryRequest request);
}