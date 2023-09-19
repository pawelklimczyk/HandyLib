// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="UserMessage.cs" project="Gmtl.HandyLib" date="2019-03-28 07:36">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Models
{
    /// <summary>
    /// Provides a container for standard UI user messages
    /// </summary>
    public class UserMessage
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public UserMessageType Type { get; private set; }

        public static UserMessage CreateInfo(string message = "", string title = "")
        {
            return Create(title, message, UserMessageType.Info);
        }

        public static UserMessage CreateSuccess(string message = "", string title = "")
        {
            return Create(title, message, UserMessageType.Success);
        }

        public static UserMessage CreateWarning(string message = "", string title = "")
        {
            return Create(title, message, UserMessageType.Warning);
        }

        public static UserMessage CreateError(string message = "", string title = "")
        {
            return Create(title, message, UserMessageType.Error);
        }

        public static UserMessage Create(string message = "", string title = "",
            UserMessageType type = UserMessageType.Info)
        {
            return new UserMessage
            {
                Message = message,
                Title = title,
                Type = type
            };
        }
    }

    public enum UserMessageType
    {
        NotSet = 0,
        Info = 10,
        Warning = 20,
        Success = 30,
        Error = 100
    }
}