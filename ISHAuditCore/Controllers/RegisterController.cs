// using System.Net;
// using System.Net.Mail;
// using ISHAuditCore.Context;
// using ISHAuditCore.Entities;
// using ISHAuditCore.Models;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ISHAuditCore.Controllers;
//
// public class RegisterController : baseController
// {
//     private readonly ISHAuditDbcontext _db;
//     
//     public RegisterController(ISHAuditDbcontext dbContext, Authority authorityClass, UserEditService userEditService)
//         : base(dbContext, authorityClass,userEditService) // 呼叫 baseController 的構造函數
//     {
//         _db = dbContext;
//     }
//     // GET
//     public IActionResult Index()
//     {
//         return View();
//     }
//     [HttpPost]
//     public async Task<IActionResult> SendVerificationEmail([FromBody] string email)
//     {
//         if (string.IsNullOrEmpty(email))
//         {
//             return BadRequest("Email address is required.");
//         }
//
//         try
//         {
//             // Generate a verification code (this could be stored in a database)
//             string verificationCode = GenerateVerificationCode();
//             
//             if (_db.review_queues.Any(u => u.email == email))
//             {
//                 var errorResponse = new { error = "此email已註冊" };
//                 return Json(errorResponse);
//             }
//             
//             review_queue reviewQueueEntry = new review_queue()
//             {
//                 email= email,
//                 verification_code = verificationCode,
//                 approval_time = DateTime.UtcNow
//             };
//             _db.review_queues.Add(reviewQueueEntry);
//             await _db.SaveChangesAsync();
//             
//             
//             await SendEmailAsync(email, verificationCode);
//
//             return Ok(new { message = "Verification email sent successfully." });
//         }
//         catch (System.Exception ex)
//         {
//             return StatusCode((int)HttpStatusCode.InternalServerError, $"Error sending email: {ex.Message}");
//         }
//     }
//     private string GenerateVerificationCode()
//     {
//         // Generate a simple 6-digit verification code
//         Random random = new Random();
//         return random.Next(100000, 999999).ToString();
//     }
//     
//     private async Task SendEmailAsync(string email, string verificationCode)
//     {
//         var smtpClient = new SmtpClient("smtp.gmail.com")
//         {
//             Port = 587, // Adjust port according to your provider
//             Credentials = new NetworkCredential("idddd555@gmail.com", "xlermwevjyndlxto"),
//             EnableSsl = true,
//         };
//
//         var mailMessage = new MailMessage
//         {
//             From = new MailAddress("idddd555@gmail.com"),
//             Subject = "Your Verification Code",
//             Body = $"Your verification code is: {verificationCode}",
//             IsBodyHtml = false,
//         };
//
//         mailMessage.To.Add(email);
//
//         await smtpClient.SendMailAsync(mailMessage);
//     }
// }