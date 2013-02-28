using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Communication
{
    public abstract class BinaryLog
    {
        #region private members

        private string ConvertCharacterToPrintable(char c)
        {
            string ret = "";

            switch (c)
            {
                case (char)2: ret = "<STX>"; break;      // STX - start of text 
                case (char)3: ret = "<ETX>"; break;      // ETX - end of text 
                case (char)4: ret = "<EOT>"; break;      // EOT - end of transmission
                case (char)5: ret = "<ENQ>"; break;      // ENQ - enquiry 
                case (char)6: ret = "<ACK>"; break;      // ACK - acknowledge
                case (char)21: ret = "<NAK>"; break;      // NAK - negative acknowledge

                default: ret = string.Format("{0}", c); break;
            }

            return ret;
        }

        #endregion

        #region public methods

        protected string LogFormat(string data)
        {
            string ret = "";
            char[] arr = data.ToCharArray();

            foreach (char c in arr)
            {
                ret += ConvertCharacterToPrintable(c);
            }

            return ret;
        }

        protected string LogFormat(byte[] data)
        {
            string ret = "";

            for (int i = 0; i < data.Length; i++)
            {
                ret += ConvertCharacterToPrintable((char)data[i]);
            }

            return ret;
        }

        #endregion
    }
}
