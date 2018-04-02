namespace Cisco.Runbook
{
    public partial class Task<T>
    {
        public bool IsCancelable { get; set; }
        public bool JobDone { get; set; }
        public string LocalAlias { get; set; }
        public string ProgressDescription { get; set; }
        public bool ProgressError { get; set; }
    }
}