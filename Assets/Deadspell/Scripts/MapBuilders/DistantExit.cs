using System;

namespace Deadspell.MapBuilders
{
    public class DistantExit : IMetaMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            Build(ref data);
        }

        private void Build(ref MapBuilder.Data data)
        {
            throw new NotImplementedException();
        }
    }
}