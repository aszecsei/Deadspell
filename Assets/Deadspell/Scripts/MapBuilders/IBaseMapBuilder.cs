namespace Deadspell.MapBuilders
{
    public interface IBaseMapBuilder
    {
        void BuildMap(ref MapBuilder.Data data);
    }
}