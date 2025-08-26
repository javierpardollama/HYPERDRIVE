using Hyperdrive.Application.ViewModels.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.Security;

public class PhoneNumberChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityPhoneNumberChange ViewModel { get; set; }
}