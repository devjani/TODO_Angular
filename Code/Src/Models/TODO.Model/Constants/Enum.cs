namespace TODO.Model
{
    using System.ComponentModel;

    /// <summary>
    /// Its a list of distichain enums
    /// </summary>
    public static class EQuoEnum
    {
        #region QueueType

        public enum QueueType
        {
            [Description("FundReceive")]
            FundReceive = 1
        }

        #endregion
    }
}
