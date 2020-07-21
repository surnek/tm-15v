using System;
using Android.App;
using DeepSoundClient;
using DeepSoundClient.Classes.Global;

namespace DeepSound.Helpers.Utils
{
    public static class DeepSoundTools
    {
        private static readonly string[] CountriesArray = Application.Context.Resources.GetStringArray(Resource.Array.countriesArray);

        public static string GetNameFinal(UserDataObject dataUser)
        {
            try
            {
                if (!string.IsNullOrEmpty(dataUser.Name) && !string.IsNullOrWhiteSpace(dataUser.Name))
                    return Methods.FunString.DecodeString(dataUser.Name);

                if (!string.IsNullOrEmpty(dataUser.Username) && !string.IsNullOrWhiteSpace(dataUser.Username))
                    return Methods.FunString.DecodeString(dataUser.Username);

                return Methods.FunString.DecodeString(dataUser.Username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
         
        public static string GetAboutFinal(UserDataObject dataUser)
        {
            try
            {
                if (!string.IsNullOrEmpty(dataUser.AboutDecoded) && !string.IsNullOrWhiteSpace(dataUser.AboutDecoded))
                    return Methods.FunString.DecodeString(dataUser.AboutDecoded);

                return Application.Context.Resources.GetString(Resource.String.Lbl_HasNotAnyInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Application.Context.Resources.GetString(Resource.String.Lbl_HasNotAnyInfo);
            }
        }
         
        public static string GetCountry(long codeCountry)
        {
            try
            { 
                if (codeCountry > -1)
                {
                    string name = CountriesArray[codeCountry];
                    return name;
                }
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        public static string GetGender(string type)
        {
            try
            {
                string text;
                switch (type)
                {
                    case "Male":
                    case "male":
                        text = Application.Context.GetText(Resource.String.Lbl_Male);
                        break;
                    case "Female":
                    case "female":
                        text = Application.Context.GetText(Resource.String.Lbl_Female);
                        break;
                    default:
                        text = "";
                        break;
                }
                return text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        /// <summary>
        /// var ImageUrl = !item.Thumbnail.Contains(DeepSoundClient.Client.WebsiteUrl) ? DeepSoundTools.GetTheFinalLink(item.Thumbnail) : item.Thumbnail;
        /// ['amazone_s3'] == 1   >> https://bucket.s3.amazonaws.com . '/' . $media;
        /// ['ftp_upload'] == 1   >> "http://".$wo['config']['ftp_endpoint'] . '/' . $media;
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public static string GetTheFinalLink(string media)
        {
            try
            {
                var path = media;
                var config = ListUtils.SettingsSiteList;
                if (!string.IsNullOrEmpty(config?.S3Upload) && config?.S3Upload == "on")
                {
                    path = "https://"+ config.S3BucketName + ".s3.amazonaws.com"  + "/" + media;
                    return path;
                }
                  
                if (!string.IsNullOrEmpty(config?.FtpUpload) && config?.FtpUpload == "on")
                {
                    path =  config.FtpEndpoint + "/" + media;
                    return path;
                }
                 
                if (!media.Contains(Client.WebsiteUrl))
                    path = Client.WebsiteUrl + "/" + media;

                return path;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return media;
            }
        }

    }
}