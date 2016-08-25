namespace F23.DataAccessExtensions.Internal
{
    internal delegate TEntity EntityTranslator<out TEntity>(DataReaderValueProvider valueProvider);
}
