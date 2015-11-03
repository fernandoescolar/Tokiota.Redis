namespace Tokiota.Redis.Protocol
{
    using Net;
    using System;
    using System.Globalization;

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
