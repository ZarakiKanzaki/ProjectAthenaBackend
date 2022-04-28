using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace AthenaBackend.Domain.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        #region Properties
        public string Code { get; set; }
        public List<object> Parameters { get; set; } = new List<object>();
        public string ExceptionPaths { get; set; }
        #endregion

        public DomainException() : base() { }
        public DomainException(string message, string code, params object[] parameters)
            : base(message)
        {
            Code = code;
            Parameters = new List<object>(parameters);
            ExceptionPaths = GetStackFrames();
        }

        public DomainException(string code, params object[] parameters)
            : this(code)
        {
            Code = code;
            Parameters = new List<object>(parameters);
            ExceptionPaths = GetStackFrames();
        }

        public DomainException(Exception inner, string code, params object[] parameters)
            : this(code, inner)
        {
            Code = code;
            Parameters = new List<object>(parameters);
            ExceptionPaths = GetStackFrames();
        }

        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }


        private string GetStackFrames()
        {
            var st = new StackTrace(true);
            var frames = st.GetFrames();
            if (frames == null)
            {
                return string.Empty;
            }

            var fileInfo = new List<string>();

            frames.ToList().ForEach(f =>
            {
                if (f.GetFileLineNumber() < 1)
                {
                    return;
                }

                fileInfo.AddRange(new[] { "File: " + GetFileNameWithoutBase(f), "Number: " + f.GetFileLineNumber() });
            });
            return string.Join("\n", fileInfo);
        }

        private static string GetFileNameWithoutBase(StackFrame x)
        {
            var fileName = x.GetFileName();
            if (string.IsNullOrEmpty(fileName) == false)
            {
                var basePathIndex = fileName.IndexOf("\\src\\", StringComparison.Ordinal);
                if (basePathIndex > 0)
                {
                    return fileName.Substring(basePathIndex + 5);
                }
            }

            return string.Empty;
        }
    }
}
