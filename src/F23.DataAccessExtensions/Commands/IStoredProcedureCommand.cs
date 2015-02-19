namespace F23.DataAccessExtensions.Commands
{
    internal interface IStoredProcedureCommand<out TCommandResult>
    {
        TCommandResult Execute();
    }
}
