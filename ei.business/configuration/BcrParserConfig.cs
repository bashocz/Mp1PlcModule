using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace EI.Business
{
	public class BcrParserConfig : IBcrParserConfig
	{
		public string ResponseBeginSequence { get; set; }
		public string ResponseEndSequence { get; set; }
	}
}
