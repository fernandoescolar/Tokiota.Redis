using System;
using System.Globalization;
using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal class DoubleResponseParser : IResponseParser<double>
    {
        public bool CheckExpetedHeader(int byteHeader)
        {
            return byteHeader == ':';
        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out double result)
        {
            return double.TryParse(textHeader, NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture.NumberFormat, out result);
        }
    }
}
