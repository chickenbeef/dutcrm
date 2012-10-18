using System;
using System.Text;
using System.Web.Security;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.Login
{
	public partial class PaswordRecovery : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		#region USERNAME VALIDATION

		protected void ValidateUserName(object sender, RemoteValidationEventArgs e)
		{
			var membership = Membership.GetUser(txtUsername.Text);


			if (txtUsername.Text.Length == 0)
			{
				e.Success = false;
				e.ErrorMessage = "Please enter a username";

				btnSubmit.Disabled = true;
				btnReset.Disabled = true;
			}

			else if (membership == null)
			{
				e.Success = false;
				e.ErrorMessage = "The username does not exist.";

				btnSubmit.Disabled = true;
				btnReset.Disabled = true;
			}

			else
			{
				e.Success = true;

				btnSubmit.Disabled = false;
				btnReset.Disabled = false;
			}

		}

		#endregion

		#region DIRECT EVENT

		protected void BtnSubmitClick(object sender, DirectEventArgs e)
		{
			btnReset.Disabled = true;
			try
			{
				var membershipUser = Membership.GetUser(txtUsername.Text);
				if (membershipUser != null)
				{
					var userPassword = membershipUser.GetPassword();
					var userEmail = membershipUser.Email;
					var username = membershipUser.UserName;
					var body = "Hi " + username + "<br/>You have requested for your password on our system, Please see below<br/>" +
					           "<br/>Username: " + username + "<br/>Password: " + userPassword +
					           "<br/><br/>Regards<br/>LawProperty Customer Support";
					var email = new EmailBl
					            	{
					            		To = userEmail,
					            		Subject = "Forgot Password",
					            		Body = body,
					            		IsHtml = true,
					            	};
					if (email.SendEmail())
					{
						ExtNet.Mask.Hide();
						ExtNet.Msg.Notify("Password Sent", "Your password has been sent.<br/>Please check your email.").Show();
					}
					else
					{
						ExtNet.Mask.Hide();
						ExtNet.Msg.Alert("Error", email.Error).Show();
					}

				}

				Disable();
			}
			catch (Exception ex)
			{
				ExtNet.Mask.Hide();
				ExtNet.Msg.Alert("Error", ex.Message).Show();

				Disable();
			}
		}

		#endregion



		#region DIRECT EVENT

		protected void ResetPassword(object sender, DirectEventArgs e)
		{
			btnSubmit.Disabled = true;

			try
			{
				var membership = Membership.GetUser(txtUsername.Text);

				if (membership != null)
				{
					var username = membership.UserName;
					var userEmail = membership.Email;
					var oldPassword = membership.GetPassword();
					var randomPassword = GeneratePassword();

					var resetPassword = membership.ChangePassword(oldPassword, randomPassword);

					var body = "Hi " + username + "<br/>You have requested for a password reset on our system, Please see below<br/>" +
					           "<br/>Username: " + username + "<br/> New Password: " + randomPassword +
					           "<br/><br/>Regards<br/>LawProperty Customer Support";

					var email = new EmailBl
					            	{
					            		To = userEmail,
					            		Subject = "Password Reset",
					            		Body = body,
					            		IsHtml = true,
					            	};

					if (email.SendEmail())
					{
						ExtNet.Mask.Hide();
						ExtNet.Msg.Notify("Password Sent", "Your password has been reset successfully.<br/>Please check your email.").Show
							();
					}
					else
					{
						ExtNet.Mask.Hide();
						ExtNet.Msg.Alert("Error", email.Error).Show();
					}

				}

				Disable();
			}

			catch (Exception ex)
			{
				ExtNet.Mask.Hide();
				ExtNet.Msg.Alert("Error", ex.Message).Show();
				Disable();
			}

		}

		#endregion

		#region DISABLE CONTROLS

		protected void Disable()
		{
			txtUsername.Reset();
			btnReset.Disabled = true;
			btnSubmit.Disabled = true;
		}

		#endregion

		#region RANDOMPASSWORD
		protected string GeneratePassword()
		{
			var pass = new StringBuilder();
			var rand = new Random();

			const string allowedChar = "ABCDEFGHJKLMNPQRSTUVXYZabcdefghjkmnpqrstuvxyz123456789";
			const string allowedPunc = "@!#$%";

			var chars = new char[6];
			var spchars = new char[2];
			var pos = rand.Next(1, 7);



			for (var i = 0; i < 6; i++)
			{
				chars[i] = allowedChar[rand.Next(0, allowedChar.Length)];

				pass.Append(chars[i]);
			}


			for (var j = 0; j < 2; j++)
			{
				spchars[j] = allowedPunc[rand.Next(0, allowedPunc.Length)];
				pass.Insert(pos, spchars[j]);
			}
			

			return pass.ToString();

		}
		#endregion

	}
}