using System.Collections.Generic;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.ApplicationRole;

public class FindAllApplicationRoleQuery : IRequest<IList<ViewCatalog>>
{
}