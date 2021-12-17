using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.webui.Identity;
using shopapp.webui.MailService;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IEmailSender emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender _emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = _emailSender;
        }
        public IActionResult login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                returnUrl = ReturnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //modele gelen bilgiler doğruysa ,veri tabanında böyle bir kullanıcı var mı sorgulayacaz.
            var user = await userManager.FindByNameAsync(model.userName);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile kullanıcı yoktur.");
                return View(model);
            }
            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                createMessage("LÜTFEN HESABINIZI ONAYLAYINIZ.", "danger");
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(user, model.passWord, false, false);
            if (result.Succeeded)
            {   //Doğru girilerse buraya yönlenir.
                return Redirect(model.returnUrl ?? "~/");
            }
            ModelState.AddModelError("", "Girilen bilgiler yanlış.");
            return View();
        }

        [HttpGet]
        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.firstName,
                LastName = model.lastName,
                Email = model.email,
                UserName = model.userName
            };
            //password field need hash.
            var res = await userManager.CreateAsync(user, model.password);
            if (res.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("confirmEmail", "Account", new { userId = user.Id, Token = token });
                createMessage("KAYIT BAŞARILI", "success");

                await emailSender.SendEmailAsync(model.email, "Hesap Onaylama İşlemi", $"Lütfen linke <a href='https://localhost:7298{url}'>tıklayınız.</a>");
                return RedirectToAction("login");
            }
            ModelState.AddModelError("", "Hata oluştu.");
            return View(model);
        }

        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult forgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> forgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                createMessage("Boş bırakmayınız!", "danger");
                return View();
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                createMessage("Bu mail adresinde kayıtlı kullanıcı bulunamadı!!", "danger");
                return View();
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("resetPassword", "account", new { userId = user.Id, Token = token });
            createMessage("ŞİFRE SIFIRLAMA LİNKİ E-POSTANIZA GÖNDERİLDİ.", "success");

            await emailSender.SendEmailAsync(email, "ŞİFRE SIFIRLAMA", $"Lütfen sıfırlamak için <a href='https://localhost:7298{url}'>tıklayınız.</a>");
            return RedirectToAction("login", "Account");
        }

        public IActionResult resetPassword(string userId, string Token)
        {
            if (userId == null || Token == null)
            {
                createMessage("Geçersiz Token", "danger");
                return View();
            }

            var model = new ResetModel { token = Token };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> resetPassword(ResetModel model)
        {
            if (!ModelState.IsValid)
            {
                createMessage("HATALI İSTEK", "danger");
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.eMail);
            if (user == null)
            {
                createMessage("Kullanıcı bulunamadı.", "danger");
                return View();
            }
            var result = await userManager.ResetPasswordAsync(user, model.token, model.password);
            if (result.Succeeded)
            {
                createMessage("PAROLA BAŞARIYLA SIFIRLANDI", "success");
                return RedirectToAction("login", "Account");
            }
            createMessage("BAŞARISIZ,YENİDEN DENEYİN", "info");
            return View(model);
        }
        public async Task<IActionResult> confirmEmail(string userId, string Token)
        {
            if (userId == null || Token == null)
            {
                createMessage("Geçersiz Token", "danger");
                return View();
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                createMessage("Böyle bir kullanıcı bulunamadı", "danger");
                return View();
            }
            var res = await userManager.ConfirmEmailAsync(user, Token);
            if (res.Succeeded)
            {
                createMessage("HESABINIZ ONAYLANDI", "success");
                return View();
            }
            createMessage("Hesabınız onaylanmadı", "danger");
            return View();
        }

        public void createMessage(string _message, string alertMessage)
        {
            var msg = new AlertMessage()
            {
                message = _message,
                alertType = alertMessage
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);
        }
    }
}