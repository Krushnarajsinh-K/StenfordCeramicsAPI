using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stenford.Common.Constants
{
	public class Enums
	{
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
	}
}
