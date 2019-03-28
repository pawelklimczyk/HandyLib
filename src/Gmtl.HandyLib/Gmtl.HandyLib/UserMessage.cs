// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="UserMessage.cs" project="Gmtl.HandyLib" date="2019-03-28 07:36">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------


namespace Gmtl.HandyLib
{
    /// <summary>
    /// Provides a container for standard UI user messages
    /// </summary>
    public class UserMessage
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public UserMessageType Type { get; private set; }

        public static UserMessage CreateInfo(string title = "", string message = "")
        {
            return Create(title, message, UserMessageType.Info);
        }

        public static UserMessage CreateWarning(string title = "", string message = "")
        {
            return Create(title, message, UserMessageType.Warning);
        }

        public static UserMessage CreateError(string title = "", string message = "")
        {
            return Create(title, message, UserMessageType.Error);
        }

        public static UserMessage Create(string title = "", string message = "",
            UserMessageType type = UserMessageType.Info)
        {
            return new UserMessage
            {
                Title = title,
                Message = message,
                Type = type
            };
        }
    }

    public enum UserMessageType
    {
        Info,
        Warning,
        Error
    }

}