namespace F23.DataAccessExtensions.Commands
{
    public interface IStoredProcedureCommand<out TCommandResult>
    {
        TCommandResult Execute();
    }
}
