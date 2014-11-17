using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    static class Validation
    {

        static Boolean id (String value, out Int32 result)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value", "value string containing id cannot be null");
            }
            Boolean returnValue = stringToPositiveSignedInt(value, out result);
            return returnValue;
        }

        static Boolean quantity (String value, out Int32 result)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value", "value string containing quantity cannot be null");
            }
            Boolean returnValue = stringToPositiveSignedInt(value, out result);
            return returnValue;
        }

        static Boolean name (String value, UInt16 charCount, out String result)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value", "value string containing name cannot be null");
            }
            String data = value.Trim();
            Boolean returnValue = false;

            if(data.Length>= Convert.ToInt32(charCount))
            {
                returnValue = false;
                result = String.Empty;
            }
            else
            {
                returnValue = true;
                result = data;
            }
            return returnValue;
        }

        static Boolean 


        //Helper Functions
        static private Boolean stringToPositiveSignedInt(String value, out Int32 result)
        {
            Boolean returnValue;
            Int32 data;

            if(Int32.TryParse(value.Trim(), out data))
            {
                if(data>=0)
                {
                    result = data;
                    returnValue =true;
                }
                else
                {
                    result = 0;
                    returnValue = false;
                }
            }
            else
            {
                result = 0;
                returnValue = false;
            }
            return returnValue;
    }
}
