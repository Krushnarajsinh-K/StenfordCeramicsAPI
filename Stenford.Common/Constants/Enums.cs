using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stenford.Common.Constants
{
	public class Enums
	{
        public static TEnum? GetEnumByDescription<TEnum>(string description) where TEnum : struct, Enum
        {
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                FieldInfo fi = typeof(TEnum).GetField(value.ToString());
                if (fi != null)
                {
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attributes.Length > 0 && attributes[0].Description == description)
                    {
                        return value;
                    }
                }
            }
            return null;
        }
        public enum StatusCode
		{
			Ok = 200,
			BadRequest = 400,
			NotFound = 404, // also use for data not found
			ServerError = 500,
			AccessDenied = 403,
			NotAllowed = 405,
			Conflict = 409,
			Unauthorized = 401,
			Expired = 410
		}

		public static string GetStatusCodeString(Enums.StatusCode code)
		{
			if (code == Enums.StatusCode.Ok)
				return "Ok";
			else if (code == Enums.StatusCode.BadRequest)
				return "Bad Request";
			else if (code == Enums.StatusCode.NotFound)
				return "Not Found";
			else if (code == Enums.StatusCode.ServerError)
				return "Server Error";
			else if (code == Enums.StatusCode.AccessDenied)
				return "Access Denied";
			else if (code == Enums.StatusCode.NotAllowed)
				return "Not Allowed";
			else if (code == Enums.StatusCode.Conflict)
				return "Conflict";
			else if (code == Enums.StatusCode.Unauthorized)
				return "Token Expired";

			return "";
		}

		public enum AttachmentType
		{
			//VoiceNote = 1,
			//VisitingCard = 2,
			ShowroomImage = 1
		}

        public enum UserType
        {
            [Description("Admin")]
            Admin = 1,
            [Description("SalesPerson")]
            SalesPerson = 2,
        }

        public enum EmailSmsTemplate
        {
            Login = 1,
            Registartion = 2,
            ForgotPassword = 3,
            RedeemAmountOtp = 4,
            AdminCreateNewShopKeeper = 5,
            PurchasedCRM_LeadmanagerReceiver = 6,
            PurchasedCRM_LeadmanagerSender = 7,
            SupportPhysicalCardPurchase = 8,
            SupportCRM_LeadmanagerCheckBalance = 9,
            SupportPhysicalCardLessInventory = 10,
            PartnerCreateVirtualCard = 11,
            VirtualCreditCardRequest = 12,
            SupportCRM_LeadmanagerPurchase = 13,
            SupportActivateCRM_Leadmanager = 14,
            ApiRegistration = 15,
            ApiTokenChange = 16
        }
    }
}
