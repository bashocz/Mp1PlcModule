using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Business
{
	public class PromisException : Exception
	{
		#region ctors

		public PromisException(String message, Exception exception)
			: base(message, exception) { }

		public PromisException() 
		{
			ErrorCode = -1;
			ErrorMessage = "Unknwon error";
		}

		public PromisException(string message) 
		{ 
			ErrorCode = -1; 
			ErrorMessage = message; 
		}

		public PromisException(string state, string message)
		{
			ErrorCode = -1;
			ErrorState = state;
			ErrorMessage = message;
		}

		#endregion

		#region PromisException members

		public int ErrorCode { get; private set; }
		public string ErrorState { get; private set; }
		public string ErrorMessage { get; private set; }

		#endregion
	}
}
