using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrazyMelsWeb.Models;
using System.Text.RegularExpressions;

namespace CrazyMelsWeb.Models
{
    static public class Validation
    {
        static public class IsValid
        {
            static private SortedList<String, String> Invalids = new SortedList<string, string>();
            static private void ResetInvalids()
            {
                Invalids.Clear();
            }
            static private void AddInvalid(String source, String error)
            {
                Invalids.Add(source, error);
            }
            static public SortedList<String, String> getInvalid()
            {
                return Invalids;
            }



            static public Boolean NewEntry(C_Customer newCustomer)
            {
                Boolean returnValue = true;
                ResetInvalids();
                //QUES: If user indicates a custID do I ignore it or do I send it back as an error?(Assuming Ignore)
              

                //If Not ignore
                /*
                 if(newCustomer.custID != 0)
                 {
                     AddInvalid("custID", "Customer ID are automatically set for new entries and cannot be manually set");
                     returnValue &= false;
                 }
                */
                newCustomer.firstName = ValidationTools.cleanName(newCustomer.firstName);
                if (newCustomer.firstName != null)
                {
                    //QUES: is there any error checking on first names?
                }

                newCustomer.lastName = ValidationTools.cleanName(newCustomer.lastName);
                if (newCustomer.lastName == null)
                {
                    AddInvalid("lastName", "LastName required for new entries");
                    returnValue &= false;
                    //QUES: is there any error checking on first names?
                }

                newCustomer.phoneNumber = ValidationTools.cleanPhoneNumber(newCustomer.phoneNumber);
                if (newCustomer.phoneNumber == null)
                {
                    AddInvalid("phoneNumber", "phoneNumber required for new entries in the format XXX-XXX-XXXX");
                    returnValue &= false;
                    //QUES: is there any error checking on first names?
                }


                return returnValue;
            }

            static public Boolean MergeEntries(C_Customer oldEntry, C_Customer newEntry, out C_Customer mergedEntry)
            {
                Boolean returnValue = true;
                ResetInvalids();

                C_Customer tempMergedEntry = new C_Customer();

                tempMergedEntry.custID = 0;
                if(String.IsNullOrEmpty(newEntry.firstName))
                {
                    tempMergedEntry.firstName = oldEntry.firstName;
                }
                else
                {
                    tempMergedEntry.firstName = newEntry.firstName;
                }

                if (String.IsNullOrEmpty(newEntry.lastName))
                {
                    tempMergedEntry.lastName = oldEntry.lastName;
                }
                else
                {
                    tempMergedEntry.lastName = newEntry.lastName;
                }

                if (String.IsNullOrEmpty(newEntry.phoneNumber))
                {
                    tempMergedEntry.phoneNumber = oldEntry.phoneNumber;
                }
                else
                {
                    tempMergedEntry.phoneNumber = newEntry.phoneNumber;
                }

                returnValue = NewEntry(tempMergedEntry);

                tempMergedEntry.custID = oldEntry.custID;

                mergedEntry = tempMergedEntry;
                return returnValue;
            }


            static public Boolean MergeEntries(C_Customer oldEntry, C_Customer newEntry)
            {
                Boolean returnValue = true;
                ResetInvalids();

             


                if (!String.IsNullOrEmpty(newEntry.firstName))
                {
                    oldEntry.firstName = newEntry.firstName;
                }

                if (!String.IsNullOrEmpty(newEntry.lastName))
                {
                    oldEntry.lastName = newEntry.lastName;
                }

                if (!String.IsNullOrEmpty(newEntry.phoneNumber))
                {
                    oldEntry.phoneNumber = newEntry.phoneNumber;
                }

                returnValue = NewEntry(oldEntry);

             
                return returnValue;
            }



        }

        static private class ValidationTools
        {
            static public String cleanName(String name)
            {
                if (String.IsNullOrWhiteSpace(name))
                {
                    return null;
                }
                else
                {
                    name = name.Trim();
                    //QUES: Validation, Is there any other cleanup I should do on incoming data? Remove characters etc.
                    return name;
                }
            }


            static public String cleanPhoneNumber(String phone)
            {
                if (String.IsNullOrWhiteSpace(phone))
                {
                    return null;
                }
                else
                {
                    string userInput = phone.Trim();

                    Regex regexPhoneNumber = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

                    if (regexPhoneNumber.IsMatch(userInput))
                    {
                        return regexPhoneNumber.Replace(userInput, "$1-$2-$3");
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

    }



}
