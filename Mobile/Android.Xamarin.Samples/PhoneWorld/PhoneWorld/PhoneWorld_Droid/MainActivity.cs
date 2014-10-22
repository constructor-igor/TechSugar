using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Android.Provider;
using Android.Util;
//using System.Linq;

//using Xamarin.Contacts;

namespace PhoneWorld_Droid
{
	[Activity (Label = "PhoneWorld", MainLauncher = true, NoHistory = false)]
	public class MainActivity : Activity
	{
//		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Our code will go here

			// Get our UI controls from the loaded layout
			EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			EditText messageText = FindViewById<EditText>(Resource.Id.MessageText);
			Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
			Button callButton = FindViewById<Button>(Resource.Id.CallButton);
			Button sendButton = FindViewById<Button>(Resource.Id.SendSMSButton);
			Button contactsButton = FindViewById<Button>(Resource.Id.ContactsButton);

			// Disable the "Call" button
			callButton.Enabled = false;
			sendButton.Enabled = false;

			// Add code to translate number
			string translatedNumber = string.Empty;

			translateButton.Click += (object sender, EventArgs e) =>
			{
				// Translate user’s alphanumeric phone number to numeric
				translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
				if (String.IsNullOrWhiteSpace(translatedNumber))
				{
					callButton.Text = "Call";
					callButton.Enabled = false;
					sendButton.Enabled = false;
				}
				else
				{
					callButton.Text = "Call " + translatedNumber;
					callButton.Enabled = true;
					sendButton.Enabled = true;
				}
			};

			callButton.Click += (object sender, EventArgs e) =>
			{
				// On "Call" button click, try to dial phone number.
				var callDialog = new AlertDialog.Builder(this);
				callDialog.SetMessage("Call " + translatedNumber + "?");
				callDialog.SetNeutralButton("Call", delegate {
					// Create intent to dial phone
					var callIntent = new Intent(Intent.ActionCall);
					callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
					StartActivity(callIntent);
				});
				callDialog.SetNegativeButton("Cancel", delegate { });

				// Show the alert dialog to the user and wait for response.
				callDialog.Show();
			};

			sendButton.Click += (object sender, EventArgs e) => 
			{
				SmsManager.Default.SendTextMessage (translatedNumber, null, messageText.Text, null, null);
			};

			contactsButton.Click += (object sender, EventArgs e) => 
			{
				//Create a new intent for choosing a contact  
				var contactPickerIntent = new Intent(Intent.ActionPick, Android.Provider.ContactsContract.Contacts.ContentUri);
				contactPickerIntent.SetType(ContactsContract.CommonDataKinds.Phone.ContentType);//(Phone.CONTENT_TYPE); // Show user only contacts w/ phone numbers
				//Start the contact picker expecting a result  
				// with the resultCode '101'  
				StartActivityForResult(contactPickerIntent, 101);
			};
				

			string number;
			string message;
			LoadData (out number, out message);
			phoneNumberText.Text = number;
			messageText.Text = message;

//			// Get our button from the layout resource,
//			// and attach an event to it
//			Button button = FindViewById<Button> (Resource.Id.myButton);
			
//			button.Click += delegate {
//				button.Text = string.Format ("{0} clicks!", count++);
//			};
		}

		protected override void OnDestroy ()
		{
			Log.Debug ("OnDestroy", "entered");

			EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			EditText messageText = FindViewById<EditText>(Resource.Id.MessageText);

			SaveData (phoneNumberText.Text, messageText.Text);
			base.OnDestroy ();
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			//ShowDialog ("OnActivityResult");
			Log.Debug ("OnActivityResult", String.Format("entered, requestCode={0}, resultCode={1}", requestCode, resultCode));

			//EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			//phoneNumberText.Text = String.Format("request={0}, result={1}", requestCode, resultCode);

			base.OnActivityResult(requestCode, resultCode, data);

			if (requestCode == 101 && resultCode == Result.Ok) 
			{
				//Ensure we have data returned
				if (data == null || data.Data == null)          
					return;

				phoneNumberText.Text = "data != null";

				var id = data.Data.LastPathSegment;

				var contacts = ManagedQuery(ContactsContract.CommonDataKinds.Phone.ContentUri, null, "_id = ?", new string[] { id }, null); 
				//var contacts = ManagedQuery(ContactsContract.Contacts.ContentUri, null, "_id = ?", new string[] { id }, null); 
				contacts.MoveToFirst(); 
				string displayName = contacts.GetString(contacts.GetColumnIndex("display_name"));


				phoneNumberText.Text = String.Format("display name={0}", displayName);

/*
				var columnNames = contacts.GetColumnNames ();

				foreach (var columnName in columnNames) {
					int index = contacts.GetColumnIndex(columnName);
					var value = contacts.GetString (index);
					Console.WriteLine ("index = {0}, value = {1}", index, value);
				}					
*/

				int indexNumber = contacts.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number);
				string mobileNumber = contacts.GetString(indexNumber);

				if (!string.IsNullOrEmpty (mobileNumber)) {
					phoneNumberText.Text = mobileNumber;
				}

			}
		}

		void ShowDialog(string message)
		{
			Android.App.AlertDialog.Builder builder = new AlertDialog.Builder (this);
			AlertDialog alertDialog = builder.Create ();
			alertDialog.SetTitle ("Alert");
			alertDialog.SetMessage (message);
			alertDialog.SetButton("OK", (s, ev) =>
				{
				});
			alertDialog.Show ();
		}

		void SaveData(string number, string message)
		{
			var prefs = Application.Context.GetSharedPreferences("PhoneWorld_Droid", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutString("number", number);
			prefEditor.PutString("message", message);
			prefEditor.Commit();
		}

		void LoadData(out string number, out string message)
		{
			var prefs = Application.Context.GetSharedPreferences("PhoneWorld_Droid", FileCreationMode.Private);
			number = prefs.GetString("number", "default");
			message = prefs.GetString("message", "default");
		}
	}
}
