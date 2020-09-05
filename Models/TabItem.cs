using System;

namespace MVIOperationsSystem.Models
{
	public class TabItem
	{
        private string header;

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        private object content;

        public object Content
        {
            get { return content; }
            set { content = value; }
        }

    }
}
