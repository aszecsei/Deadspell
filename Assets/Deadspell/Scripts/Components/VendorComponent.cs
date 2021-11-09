using Deadspell.Core;
using Entitas;

namespace Deadspell.Components
{
    [Game]
    public class VendorComponent : IComponent
    {
        public VendorCategory Vends;
    }
}