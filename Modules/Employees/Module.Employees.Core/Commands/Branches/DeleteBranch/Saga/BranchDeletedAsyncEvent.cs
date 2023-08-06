namespace Module.Employees.Core.Commands.Branches.DeleteBranch.Saga
{
    public record BranchDeletedAsyncEvent
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
