using System;
using System.Globalization;

namespace EI.Plc
{
    class StringConverter : IStreamConverter<string>
    {
        #region helper methods

        private string TextToPlcStream(string text)
        {
            string result = "";
            while (text.Length > 0)
            {
                text = text.PadRight(4, '0');
                result += text.Substring(0, 4);
                text = text.Remove(0, 4);
            }
            return result;
        }

        private string PlcStreamToText(string stream)
        {
            string result = "";
            while (stream.Length > 0)
            {
                result += stream.Substring(0, 4);
                stream = stream.Remove(0, 4);
            }
            return result;
        }

        private string TextToHexaDigit(string text)
        {
            string result = "";
            foreach (char character in text)
            {
                result += Convert.ToInt32(character).ToString("X2", CultureInfo.InvariantCulture);
            }
            return result;
        }

        private string HexaDigitToText(string stream)
        {
            string result = "";
            while (stream.Length > 0)
            {
                if (stream.Substring(0, 2) == "00")
                    break;

                result += Convert.ToChar(Convert.ToInt32(stream.Substring(0, 2), 16));
                stream = stream.Remove(0, 2);
            }
            return result;
        }

        #endregion

        #region ICommonConverter

        public string ToObject(string stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", string.Format(CultureInfo.InvariantCulture, "{0}: Parameter can't be null.", GetType().Name));

            return HexaDigitToText(PlcStreamToText(stream));
        }

        public string ToStream(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text", string.Format(CultureInfo.InvariantCulture, "{0}: Parameter can't be null.", GetType().Name));

            return TextToPlcStream(TextToHexaDigit(text));
        }

        #endregion
    }
}