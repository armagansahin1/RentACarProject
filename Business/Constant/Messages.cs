using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constant
{
    public static class Messages
    {
        public static string AddMessage = "Data Added";
        public static string CarCharacterError = "Car name must be minimum 2 character";
        public static string DeleteMessage = "Data Deleted";
        public static string UpdateMessage = "Data Updated";
        public static string CarPriceError = "Car Price must be higher than 0";
        public static string CantRentMessage = "Not Available For Rent";
        public static string RentMessage = "Car Rented";

        public static string CantAddMessage = "Data can't Added!";
        public static string EmailError = "It is not an Email adress";
        public static string ColorNameExists = "Color Name Exists";
        public static string NumberOfPictureError = "The Number of Images Exceeded for this car !!!";
        public static string InvalidFileType = "Invalid File Type";
        internal static string UserNotFound = "User doesn't exist !!!";

        public static string PasswordError = "Password is wrong !!!";

        public static string SuccessfulLogin = "Successful Login";
        internal static string UserExistError = "User already exist!!!";
        internal static string UserRegistered = "The user has been successfully registered in the system";
        internal static string AccessTokenCreated = "Access Token has been created";
        internal static string AuthorizationDenied = "You have no authorization !!!";
    }
}
