using System.Collections.Generic;

namespace Hyperdrive.Ai.Application.ViewModels.Views
{
    public class ViewAnswer
    {
        public string Answer { get; set; }

        public ICollection<ViewSource> Sources { get; set; }
    }
}
