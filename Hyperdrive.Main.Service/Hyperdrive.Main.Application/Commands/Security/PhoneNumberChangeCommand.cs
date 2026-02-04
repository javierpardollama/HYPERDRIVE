using Hyperdrive.Main.Application.ViewModels.Security;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.Security;

public class PhoneNumberChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityPhoneNumberChange ViewModel { get; set; }
}