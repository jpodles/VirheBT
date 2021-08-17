namespace VirheBT.State
{
    public interface IAppState
    {
        public int CurrentProjectId { get; set; }
        public int CurrentIssueId { get; set; }
    }
}