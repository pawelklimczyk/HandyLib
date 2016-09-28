// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="ExceptionHelper.cs" project="Gmtl.HandyLib" date="2015-10-10 12:36">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Xml.Linq;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Handy methods related to System.Exception
    /// </summary>
    public static class HLExceptionHelper
    {
        /// <summary>
        /// Return XML serialized string of Exception provided in parameter
        /// </summary>
        public static string ToXmlString(this Exception exception, bool includeStackTrace)
        {
            XElement rootElement = AddException(exception, includeStackTrace);

            return rootElement.ToString();
        }

        private static XElement AddException(Exception exception, bool includeStackTrace)
        {
            //TODO handle exception.Data

            var rootElement = new XElement(exception.GetType().ToString());
            rootElement.Add(new XElement("Message", exception.Message));
            
            if (includeStackTrace && !String.IsNullOrWhiteSpace(exception.StackTrace))
            {
                XElement stackTraceXElement = new XElement("StackTrace");

                foreach (var stackFrame in exception.StackTrace.Split('\n'))
                {
                    stackTraceXElement.Add(new XElement("Frame", stackFrame.Trim()));
                }

                rootElement.Add(stackTraceXElement);
            }
            
            if (exception.InnerException != null)
            {
                rootElement.Add(AddException(exception.InnerException, includeStackTrace));
            }

            return rootElement;
        }
    }
}