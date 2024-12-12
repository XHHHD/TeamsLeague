using System;

namespace TeamsLeague.UI.WPF.Exceptions
{
    public class TL_BaseException : Exception
    {
        public TL_BaseException(string message) : base(message) { }
    }
}
