namespace WebApi.Models
{
    public class ReturnValue
    {
        /// <summary>
        /// 输出执行结果，如果执行结果为True,错误信息为NULL，否则输出具体的错误信息。
        /// </summary>
        /// <value>
        ///   <c>true</c> 成功：错误信息为NULL，失败错误输出错误信息。 <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }

        public int ReturnCode { get; set; }
        public string ReturnMsg { get; set; }
    }
}