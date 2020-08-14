using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVIOperationsSystem.Controls
{
	public class MVINotifyBox : Label
	{
		private bool _isError;

		//public bool IsError
		//{
		//	get { return _isError; }
		//	set { _isError = value; }
		//}

		public MVINotifyBox(string message, bool isError)
		{
			this.Content = message;
		
		}


	}
}
