
using PTSL.GENERIC.Web.Core.Enum.Documents;
using PTSL.GENERIC.Web.Core.Model;

namespace PTSL.GENERIC.Web.Core.Helper
{
    public static class DocumentHelper
    {
        public static string GetInitialFooterByDocumentType(DocumentType documentType, UserVM reportGenUser, string createdDate)
        {
            string returnString = "";
            if (documentType == DocumentType.Agreement)
            {
                returnString = $@"<!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Signature</title>
                        <!-- Bootstrap CSS -->
                        <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css"" rel=""stylesheet"">
                    </head>
                    <body>
                        <p><b style=""font-weight:normal;"" id=""docs-internal-guid-c56f0f9e-7fff-f32c-0e63-91040fff7d55""><br></b></p>
                        <p dir=""ltr"" style=""line-height:1.3800000000000001;margin-top:0pt;margin-bottom:0pt;"">
                        <span style=""font-size:10.5pt; font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">The terms and conditions laid down in this agreement are acceptable to both the parties.</span></p><p><br></p>
                        <div dir=""ltr"" style=""margin-left:0pt;"" align=""center""><table style=""border:none;border-collapse:collapse;"">
                            <colgroup><col width=""295""><col width=""295""></colgroup>
                            <tbody>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.2;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">FOR OWNER ORGANIZATIONS</span></p>
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.2;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">FOR SHQTC</span></p>
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;"">Name: {reportGenUser?.FirstName} {reportGenUser?.LastName}<br><br><br><br><br>
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;"">Name: <br><br><br><br><br>
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;text-align: justify;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">(Signature with Name &amp; Designation) Of Authorized Signatory with Official Seal)</span></p>
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;text-align: justify;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">(Signature with Name &amp; Designation) Of Authorized signatory with Official Seal)</span></p><br><br>
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><img src=""{reportGenUser?.SignatureUrl}"" alt=""Signature"" class=""img-fluid"" width=""150"" height=""100"">
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><img src="""" alt=""Signature"" class=""img-fluid"" width=""150"" height=""100"">
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">Date: {createdDate}</span></p></td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">Date:</span></p>
                                    </td>
                                </tr>
                            </tbody></table></div>

                        <!-- Bootstrap JS (Optional) -->
                        <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js""></script>
                    </body>
                    </html>";
            }
            else
            {
                returnString = $@"<!DOCTYPE html>
                                <html lang=""en"">

                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Document</title>
                                </head>

                                <body>
                                    <div class=""table-responsive"">
                                        <div style=""font-size: 18px;font-weight: 700;"">Report Generated By-</div>
                                        <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                <td style=""width: 50%;"">
                                                    <div><img src=""{reportGenUser.SignatureUrl}"" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;"">Date:</td>
                                                <td style=""width: 20%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">{createdDate}</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">{reportGenUser.FirstName} {reportGenUser.LastName}</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr style=""vertical-align: top;"">
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""> {reportGenUser.Designation}
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">Software & Hardware Quality Testing
                                                        and
                                                        Certification Centre (SHQTC),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                        Bangladesh Computer Council (BCC)
                                                    </div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">{reportGenUser.RoleName}</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class=""table-responsive"">
                                        <div style=""font-size: 18px;font-weight: 700;"">Verified By-</div>
                                        <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                <td style=""width: 50%;"">
                                                    <div><img src="""" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;"">Date:</td>
                                                <td style=""width: 20%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr style=""vertical-align: top;"">
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                    </div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class=""table-responsive"">
                                        <div style=""font-size: 18px;font-weight: 700;"">Approved By-</div>
                                        <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                <td style=""width: 50%;"">
                                                    <div><img src="""" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;"">Date:</td>
                                                <td style=""width: 20%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr style=""vertical-align: top;"">
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                    </div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                        </table>
                                    </div>
                                </body>
                        </html>";
            }
            
            return returnString;
        }

        public static string GetVerifiedFooterByDocumentType(DocumentType documentType, UserVM reportGenUser, List<UserVM> verifiedUsers, UserVM approvedUser)
        {
            string returnString = "";
            returnString = $@"<!DOCTYPE html>
                                <html lang=""en"">

                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Document</title>
                                </head>

                                <body>
                                    <div class=""table-responsive"">
                                        <div style=""font-size: 18px;font-weight: 700;"">Report Generated By-</div>
                                        <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                <td style=""width: 50%;"">
                                                    <div><img src=""{reportGenUser.SignatureUrl}"" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;"">Date:</td>
                                                <td style=""width: 20%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">{reportGenUser.CreatedAt.ToString("dd/MM/yyyy")}</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">{reportGenUser.FirstName} {reportGenUser.LastName}</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr style=""vertical-align: top;"">
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;""> {reportGenUser.Designation}
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">Software & Hardware Quality Testing
                                                        and
                                                        Certification Centre (SHQTC),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                        Bangladesh Computer Council (BCC)
                                                    </div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">{reportGenUser.RoleName}</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                        </table>
                                    </div>";
                                returnString += $@"<div class=""table-responsive"">
                                                                            <div style=""font-size: 18px;font-weight: 700;"">Verified By-</div>";

                                for (int i = 0; i < verifiedUsers.Count; i++)
                                {
                                    returnString += $@"<table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                                        <tr>
                                                            <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                            <td style=""width: 50%;"">
                                                                <div><img src=""{verifiedUsers[i].SignatureUrl}"" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                                <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                            </td>
                                                            <td style=""width: 10%;"">Date:</td>
                                                            <td style=""width: 20%;"">
                                                                <div style=""font-size: 18px;font-weight: 500;"">{verifiedUsers[i].CreatedAt.ToString("dd/MM/yyyyy")}</div>
                                                                <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                            <td style=""width: 50%;"">
                                                                <div style=""font-size: 18px;font-weight: 500;"">{verifiedUsers[i].FirstName} {verifiedUsers[i].LastName}</div>
                                                                <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                            </td>
                                                            <td style=""width: 10%;""></td>
                                                            <td style=""width: 20%;""></td>
                                                        </tr>
                                                        <tr style=""vertical-align: top;"">
                                                            <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                            <td style=""width: 50%;"">
                                                                <div style=""font-size: 18px;font-weight: 500;""> {verifiedUsers[i].Designation}
                                                                </div>
                                                                <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">Software & Hardware Quality Testing
                                                                    and
                                                                    Certification Centre (SHQTC),
                                                                </div>
                                                                <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                                    Bangladesh Computer Council (BCC)
                                                                </div>
                                                                <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                            </td>
                                                            <td style=""width: 10%;""></td>
                                                            <td style=""width: 20%;""></td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                            <td style=""width: 50%;"">
                                                                <div style=""font-size: 18px;font-weight: 500;"">{verifiedUsers[i].RoleName}</div>
                                                                <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                            </td>
                                                            <td style=""width: 10%;""></td>
                                                            <td style=""width: 20%;""></td>
                                                        </tr>
                                                    </table>";
                                };
                                                    returnString += $@" </div><div class=""table-responsive"">
                                                            <div style=""font-size: 18px;font-weight: 700;"">Approved By-</div>
                                                            <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                                                <tr>
                                                                    <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                                    <td style=""width: 50%;"">
                                                                        <div><img src=""{approvedUser.SignatureUrl}"" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                                        <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                                    </td>
                                                                    <td style=""width: 10%;"">Date:</td>
                                                                    <td style=""width: 20%;"">
                                                                        <div style=""font-size: 18px;font-weight: 500;"">{approvedUser.CreatedAt.ToString("dd/MM/yyyy")}</div>
                                                                        <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                                    <td style=""width: 50%;"">
                                                                        <div style=""font-size: 18px;font-weight: 500;"">{approvedUser.FirstName} {approvedUser.LastName}</div>
                                                                        <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                                    </td>
                                                                    <td style=""width: 10%;""></td>
                                                                    <td style=""width: 20%;""></td>
                                                                </tr>
                                                                <tr style=""vertical-align: top;"">
                                                                    <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                                    <td style=""width: 50%;"">
                                                                        <div style=""font-size: 18px;font-weight: 500;"">{approvedUser.Designation}
                                                                        </div>
                                                                        <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">Software & Hardware Quality Testing
                                                                            and
                                                                            Certification Centre (SHQTC),
                                                                        </div>
                                                                        <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                                            Bangladesh Computer Council (BCC)
                                                                        </div>
                                                                        <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                                    </td>
                                                                    <td style=""width: 10%;""></td>
                                                                    <td style=""width: 20%;""></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                                    <td style=""width: 50%;"">
                                                                        <div style=""font-size: 18px;font-weight: 500;"">{approvedUser.RoleName}</div>
                                                                        <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                                    </td>
                                                                    <td style=""width: 10%;""></td>
                                                                    <td style=""width: 20%;""></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </body>
                                                    </html>";
            return returnString;
        }

        public static string GetDynamicFooterByDocumentType(DocumentType documentType)
        {
            string returnString = "";
            returnString = $@"<!DOCTYPE html>
                                <html lang=""en"">

                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Document</title>
                                </head>

                                <body>
                                    <div class=""table-responsive"">
                                        <div style=""font-size: 18px;font-weight: 700;"">Report Generated By-</div>
                                        <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                <td style=""width: 50%;"">
                                                    <div><img src=""img/signature.PNG"" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;"">Date:</td>
                                                <td style=""width: 20%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">12/02/2023</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Md. Hossain Bin Amin</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr style=""vertical-align: top;"">
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Analyst (Certification) and
                                                        Additional Charge (Testing, Quality Control and
                                                        Certification),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">Software & Hardware Quality Testing
                                                        and
                                                        Certification Centre (SHQTC),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                        Bangladesh Computer Council (BCC)
                                                    </div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Test Analyst</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class=""table-responsive"">
                                        <div style=""font-size: 18px;font-weight: 700;"">Verified By-</div>
                                        <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                <td style=""width: 50%;"">
                                                    <div><img src=""img/signature.PNG"" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;"">Date:</td>
                                                <td style=""width: 20%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">12/02/2023</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Md. Hossain Bin Amin</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr style=""vertical-align: top;"">
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Analyst (Certification) and
                                                        Additional Charge (Testing, Quality Control and
                                                        Certification),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">Software & Hardware Quality Testing
                                                        and
                                                        Certification Centre (SHQTC),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                        Bangladesh Computer Council (BCC)
                                                    </div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Test Analyst</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class=""table-responsive"">
                                        <div style=""font-size: 18px;font-weight: 700;"">Approved By-</div>
                                        <table style=""width: 100%;padding-left: 50px;margin-bottom: 50px;"">
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Signature:</td>
                                                <td style=""width: 50%;"">
                                                    <div><img src=""img/signature.PNG"" style=""width: 100px;height: 40px;"" alt=""""></div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;"">Date:</td>
                                                <td style=""width: 20%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">12/02/2023</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Name:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Md. Hossain Bin Amin</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr style=""vertical-align: top;"">
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Title</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Analyst (Certification) and
                                                        Additional Charge (Testing, Quality Control and
                                                        Certification),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">Software & Hardware Quality Testing
                                                        and
                                                        Certification Centre (SHQTC),
                                                    </div>
                                                    <div style=""font-size: 18px;font-weight: 500;margin-top: 10px;"">
                                                        Bangladesh Computer Council (BCC)
                                                    </div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                            <tr>
                                                <td style=""width: 20%;font-size: 18px;font-weight: 500;"">Role:</td>
                                                <td style=""width: 50%;"">
                                                    <div style=""font-size: 18px;font-weight: 500;"">Test Analyst</div>
                                                    <div style=""width: 90%;height: 1px;background-color: #000;margin-top: 10px;""></div>
                                                </td>
                                                <td style=""width: 10%;""></td>
                                                <td style=""width: 20%;""></td>
                                            </tr>
                                        </table>
                                    </div>
                                </body>
                        </html>";
            return returnString;
        }

        public static string GetAgreementFooterByDocumentType(DocumentType documentType, UserVM clientUser, UserVM sqtcUser, string clientSign, string sqtcSign)
        {
            string returnString = "";
            //signatureUrl = "http://localhost:5283/uploads/SignatureUploadForUser/20240527193938_4349730a-d860-440b-9663-3f150d053d62.jpg";
            if (documentType == DocumentType.Agreement)
            {
                returnString = $@"<!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Signature</title>
                        <!-- Bootstrap CSS -->
                        <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css"" rel=""stylesheet"">
                    </head>
                    <body>
                        <p><b style=""font-weight:normal;"" id=""docs-internal-guid-c56f0f9e-7fff-f32c-0e63-91040fff7d55""><br></b></p>
                        <p dir=""ltr"" style=""line-height:1.3800000000000001;margin-top:0pt;margin-bottom:0pt;"">
                        <span style=""font-size:10.5pt; font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">The terms and conditions laid down in this agreement are acceptable to both the parties.</span></p><p><br></p>
                        <div dir=""ltr"" style=""margin-left:0pt;"" align=""center""><table style=""border:none;border-collapse:collapse;"">
                            <colgroup><col width=""295""><col width=""295""></colgroup>
                            <tbody>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.2;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">FOR OWNER ORGANIZATIONS</span></p>
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.2;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">FOR SHQTC</span></p>
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;"">Name: {clientUser?.FirstName} {clientUser?.LastName}<br><br><br><br><br>
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;"">Name: {sqtcUser.FirstName} {sqtcUser.LastName}<br><br><br><br><br>
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;text-align: justify;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">(Signature with Name &amp; Designation) Of Authorized Signatory with Official Seal)</span></p>
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;text-align: justify;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">(Signature with Name &amp; Designation) Of Authorized signatory with Official Seal)</span></p><br><br>
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><img src=""{clientSign}"" alt=""Signature"" class=""img-fluid"" width=""150"" height=""100"">
                                    </td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><img src=""{sqtcSign}"" alt=""Signature"" class=""img-fluid"" width=""150"" height=""100"">
                                    </td>
                                </tr>
                                <tr style=""height:0pt"">
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">Date:</span></p></td>
                                    <td style=""border-left:solid #000000 0.5pt;border-right:solid #000000 0.5pt;border-bottom:solid #000000 0.5pt;border-top:solid #000000 0.5pt;vertical-align:top;padding:0pt 5.4pt 0pt 5.4pt;overflow:hidden;overflow-wrap:break-word;""><p dir=""ltr"" style=""line-height:1.21;margin-right: 21.15pt;margin-top:0pt;margin-bottom:0pt;""><span style=""font-size:10.5pt;font-family:'Arial MT';color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"">Date:</span></p>
                                    </td>
                                </tr>
                            </tbody></table></div>

                        <!-- Bootstrap JS (Optional) -->
                        <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js""></script>
                    </body>
                    </html>";
            }
            return returnString;
        }

        public static string GetCertificateString(string projectName, string url)
        {
            string returnString = "";
            returnString = $@"<!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Document</title>
                            </head>
                            <body>
                                <main style=""position: relative;"">
                                    <img src=""{url}/img/shape-2.PNG"" alt="""" style=""position: fixed;top: 0;left: 0;"">
                                    <img src=""{url}/img/shape-3.PNG"" alt="""" style=""position: fixed;right: 0;bottom: 0;"">
                                    <div style=""padding: 30px;"">
                                        <div style=""text-align: right;font-size: 20px;font-weight: 700;margin-top: 40px;padding-right: 50px;"">CXA201CS</div>
                                        <div style=""text-align: center;margin-top: 20px;""><img style=""width: 200px;"" src=""{url}/img/bcc_logo.png"" alt=""""></div>
                                        <div style=""text-align: center;font-size: 16px;font-weight: 700;margin-top: 10px;"">SOFTWARE AND HARDWARE QUALITY TESTING & <br> CERTIFICATION CENTER</div>
                                        <div style=""text-align: center;font-size: 18px;font-weight: 700;margin-top: 20px;letter-spacing: 2px;color: #000 !important;""><span style=""font-size: 50px;color: #132F69;font-weight: 900;"">CERTIFICATE</span> <br> OF TESTING QUALIFICATION</div>
                                        <div style=""text-align: center;font-size: 20px;font-weight: 500;margin-top: 20px;letter-spacing: 1px;"">proudly awarded to :</div>
                                        <div style=""text-align: center;font-size: 25px;font-weight: 700;margin-top: 20px;"">Department of Inspection for Factories <br> and Establishments</div>
                                        <div>
                                           <p style=""width: 525px;text-align: justify; line-height: 25px; font-size: 20px; margin-top: 40px;margin-left: auto;margin-right: auto;"">
                                                This is to certify that, as of <strong>28 October 2023, {projectName}</strong> has passed
                                                the criteria for <strong>Functional, Security and Performance Testing</strong>
                                                from the <strong>SHQTC Center of Bangladesh Computer Council</strong>
                                           </p>
                                        </div>
                                        <div style=""text-align: center;margin: 30px 0;""><img style=""width: 150px;"" src=""{url}/img/Capture.PNG"" alt=""""></div>
                                        <div style=""text-align: center;margin-bottom: 60px;"">
                                            <table style=""width: 100%;"">
                                                <tr style=""vertical-align: top;"">
                                                    <td style=""height: 100px;"">
                                                        <div><img style=""height: 100px;"" src=""{url}/img/signature.PNG"" alt=""""></div>
                                                        <div style=""width: 250px; height: 2px; background-color: #000; margin: auto;""></div>
                                                        <div style=""font-size: 20px; font-weight: 700; margin: 10px 0 5px;"">Director</div>
                                                        <div style=""font-size: 18px; font-weight: 500; margin-top: 5px;"">Testing, Quality Control & Certification<br> <strong>Bangladesh Computer Council</strong></div>
                                                    </td>
                                                    <td style=""height: 100px;"">
                                                        <div><img style=""height: 100px;"" src=""{url}/img/signature-2.PNG"" alt=""""></div>
                                                        <div style=""width: 250px; height: 2px; background-color: #000; margin: auto;""></div>
                                                        <div style=""font-size: 20px; font-weight: 700; margin: 10px 0 5px;"">Executive Director (Grade-1)</div>
                                                        <div style=""font-size: 18px; font-weight: 700;"">Bangladesh Computer Council</div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </main>
                            </body>
                            </html>";
            return returnString;
        }
    }
}
