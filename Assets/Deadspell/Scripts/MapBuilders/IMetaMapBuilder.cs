namespace Deadspell.MapBuilders
{
    public interface IMetaMapBuilder
    {
        void BuildMap(ref MapBuilder.Data data);
    }
}