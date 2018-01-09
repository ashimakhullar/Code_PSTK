using System.Management.Automation;

namespace SP_powershell
{
    internal class HXParameterSet
    {
        [Parameter(Position = 0, Mandatory = true,
           ParameterSetName = "Test01")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string userName;

    }
}