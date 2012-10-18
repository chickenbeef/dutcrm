using System;
using System.Web.Security;
using Ext.Net;

namespace CRMUI.Client
{
	public partial class ChangePassword : System.Web.UI.Page
	{
		#region VARIABLES FOR VALIDATION
		private static bool _validold;
		private static bool _validNew;
		private static bool _validConf;
		#endregion


		protected void Page_Load(object sender, EventArgs e)
		{

		}

		#region CURRENT PASSWORD VALIDATION
		public void CurrentPassValidation()
		{

			var user = Membership.GetUser();
			string oldPass = null;

			if (user != null)
			{
				oldPass = user.GetPassword();
			}

			if (txtOldPassword.Text.Length == 0)
			{
				_validold = false;
				lblOldPasswordError.Icon = Icon.BulletRed;
				lblOldPasswordError.Text = "*Please enter current password";
			}

			else if (oldPass == txtOldPassword.Text)
			{
				_validold = true;
				lblOldPasswordError.Text = string.Empty;
				lblOldPasswordError.Icon = Icon.BulletGreen;
			}

			else
			{
				_validold = false;
				lblOldPasswordError.Icon = Icon.BulletRed;
				lblOldPasswordError.Text = "The password you entered is incorrect.";
			}

		}
		#endregion


		#region ALPHANUMERIC
		private bool CheckforNonAlphaNumeric(string p)
		{
			bool isNonAlphaNumeric = false;

			foreach (char ch in p)
			{
				if (!char.IsLetterOrDigit(ch))
				{
					isNonAlphaNumeric = true;
				}
			}
			return isNonAlphaNumeric;
		}
		#endregion


		#region NEW PASSWORD VALIDATION
		public void NewPassValidation()
		{

			if (txtNewPassword.Text.Length == 0)
			{
				_validNew = false;
				lblNewPasswordError.Icon = Icon.BulletRed;
				lblNewPasswordError.Text = "*Please enter new password";
			}

			else if (CheckforNonAlphaNumeric(txtNewPassword.Text))
			{
				_validNew = true;
				lblNewPasswordError.Text = string.Empty;
				lblNewPasswordError.Icon = Icon.BulletGreen;

			}

			else
			{
				_validNew = false;
				lblNewPasswordError.Icon = Icon.BulletRed;
				lblNewPasswordError.Text = "Minimum Non-alphanumeric characters required: 1";
			}

		}
		#endregion


		#region CONFIRM VALIDATION
		public void ConfirmPassValidation()
		{

			if (txtConfirmPassword.Text.Length == 0)
			{
				_validConf = false;
				lblConfirmPasswordError.Icon = Icon.BulletRed;
				lblConfirmPasswordError.Text = "*Please confirm password";
			}

			else if (txtConfirmPassword.Text == txtNewPassword.Text)
			{
				_validConf = true;
				lblConfirmPasswordError.Text = string.Empty;
				lblConfirmPasswordError.Icon = Icon.BulletGreen;
			}

			else
			{
				_validConf = false;
				lblConfirmPasswordError.Icon = Icon.BulletRed;
				lblConfirmPasswordError.Text = "Passwords do not match!";
			}

		}
		#endregion


		#region DIRECT EVENT SAVE
		protected void SavePassword(object sender, DirectEventArgs e)
		{

			try
			{
				CurrentPassValidation();
				NewPassValidation();
				ConfirmPassValidation();


				if (_validold && _validNew && _validConf)
				{
					var membershipUser = Membership.GetUser();
					if (membershipUser != null)
					{
						membershipUser.ChangePassword(txtOldPassword.Text, txtConfirmPassword.Text);


						ExtNet.Msg.Alert("Change Password", "Your Password Has Been Changed").Show();
						txtOldPassword.Reset();
						txtNewPassword.Reset();
						txtConfirmPassword.Reset();

						_validold = false;
						_validNew = false;
						_validConf = false;

					}
				}

			}

			catch (Exception ex)
			{

				ExtNet.Msg.Alert("Error", ex.Message).Show();

			}
		}
		#endregion


	}
}