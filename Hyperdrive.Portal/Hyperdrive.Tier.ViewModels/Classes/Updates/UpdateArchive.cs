using System.Collections.Generic;

using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    public class UpdateArchive : UpdateBase
    {
        public UpdateArchive()
        {
        }

        public virtual ViewApplicationUser By { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }

        public float Size { get; set; }

        public string Type { get; set; }

        public virtual ICollection<int> ApplicationUsersId { get; set; }
    }
}
