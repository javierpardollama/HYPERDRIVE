using System.Collections.Generic;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.DriveItem;

public class FindAllDriveItemVersionByDriveItemIdQuery : IRequest<IList<ViewDriveItemVersion>>
{
    public int Id { get; set; }
}
