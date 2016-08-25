namespace F23.DataAccessExtensions.Internal.Commands
{
    internal interface IStoredProcedureCommand<out TCommandResult>
    {
        TCommandResult Execute();
    }
}
