using Hyperdrive.Identity.Application.ViewModels.Security;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.Security;

public class PhoneNumberChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityPhoneNumberChange ViewModel { get; set; }
}