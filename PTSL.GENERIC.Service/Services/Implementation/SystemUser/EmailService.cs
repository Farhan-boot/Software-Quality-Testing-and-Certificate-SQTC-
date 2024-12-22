using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace PTSL.GENERIC.Service.Services.Implementation
{
    public class EmailService 
    {

        private readonly IConfiguration _configuration;
        public EmailService( IConfiguration config)
        {
            _configuration = config;   
        }


        public bool SendEmailAsync(List<string> ReceiverID, List<string> BccList, string subject, string body, string attach)
        {
            try
            {

                SendEmail sendEmail = new SendEmail();
                sendEmail.FromMail = "info@primetechbd.com";
                sendEmail.secretkey = "MjAwYTllZDEwZGRkM2YyODY0NzNmOWM4YjVjN2VhOGM=";

               var t =  _configuration.GetSection("email_send_url");

                sendEmail.Subject = subject;
                sendEmail.Body = body + "<br/><br/><b>Powered By<br/>TRINCOME</b>";
                sendEmail.attach = attach;
                List<string> listTo = new List<string>();
                foreach (var item in ReceiverID)
                {
                    listTo.Add(item);
                }

                //List<string> listBcc = new List<string>();
                //foreach (var item in BccList)
                //{
                //    listBcc.Add(item);
                //    //message.Bcc.Add(item);
                //}
                sendEmail.ToMail = listTo;
                sendEmail.BccList = BccList;

                using (var smtp = new SmtpClient())
                {
                    var result = SendEmailToUrl(sendEmail);
                    var result2 = JsonConvert.DeserializeObject<MailResponse>(result);
                    if (result2.msg == "success")
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string SendEmailToUrl(SendEmail model)
        {
            try
            {
                //PayloadResponse responseObj = new PayloadResponse();

                string path = "http://157.230.40.147/service/api/MailSender";

                string BaseUrl = path;
                var content2 = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(content2, Encoding.UTF8, "application/json");

                var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = new Uri(BaseUrl);

                HttpResponseMessage result = client.PostAsync(uri, content).Result;

                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();

                //responseObj = JsonConvert.DeserializeObject<PayloadResponse>(jsonString.Result);
                //if (responseObj.Success)
                //{
                //    return responseObj;
                //}

                return jsonString.Result;
            }
            catch (Exception ex)
            {
                return "Error " + ex.ToString();
            }
        }


        //Implement System Busniess Logic here




    }
}
