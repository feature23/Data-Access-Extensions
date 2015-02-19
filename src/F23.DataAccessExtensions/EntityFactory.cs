namespace F23.DataAccessExtensions
{
    internal delegate object EntityFactory(DataReaderValueProvider valueProvider);

    internal delegate TEntity EntityFactory<out TEntity>(DataReaderValueProvider valueProvider);
}