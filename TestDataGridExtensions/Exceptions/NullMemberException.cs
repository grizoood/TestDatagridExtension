using System;
using System.ComponentModel;

namespace TestDataGridExtensions.Exceptions
{
    [Localizable(false)]
    class NullMemberException : Exception
    {
        #region Constructors

        public NullMemberException(string memberName) : base(BuildMessage(memberName))
        {
        }

        #endregion

        #region Methods

        static string BuildMessage(string memberName)
        {
            return $@"Member {memberName} is null.";
        }

        #endregion
    }
}