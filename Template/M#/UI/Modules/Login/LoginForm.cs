﻿using MSharp;

namespace Modules
{
    public class LoginForm : FormModule<Domain.User>
    {
        public LoginForm()
        {
            SupportsAdd(false).Using(new[] { "Olive.Security", "BotDetect.Web.Mvc" })
                .SupportsEdit(false)
                .HeaderText("احراز هویت")
                .IsViewComponent()
                .DataSource("await Domain.User.FindByEmail(info.Email)");

            Field(x => x.Email).WatermarkText("نام کاربری");
            Field(x => x.Password).Mandatory().WatermarkText("رمز عبور");
            CustomField().ControlMarkup(@"
                <div>
	                @{var loginCaptcha = Website.CaptchaHelper.GetLoginCaptcha();}
	                <captcha mvc-captcha=""loginCaptcha"" class=""center""/>
	                <div class=""actions"">
		                <input asp-for=""CaptchaCode"" placeholder=""Captcha code"" />
	                </div>
                </div>")
                .VisibleIf("info.ShowCaptcha");

            Button("Login").Text("ورود").ValidateAntiForgeryToken(false).CssClass("w-100 btn-login mb-2")
                .OnClick(x =>
                {
                    x.RunInTransaction(false);
                    x.ShowPleaseWait();

                    x.CSharp(@"var mvcCaptcha = Website.CaptchaHelper.GetLoginCaptcha();");
                    x.If("info.ShowCaptcha && !mvcCaptcha.Validate(info.CaptchaCode, Request.Param(mvcCaptcha.ValidatingInstanceKey))")
                      .CSharp(@"Notify(""کد امنیتی کپچا نامعتبر است، لطفا دوباره وارد نمایید"", ""error"");
                            info.ShowCaptcha = await LogonFailure.MustShowCaptcha(info.Email, Request.GetIPAddress());
                            return View(info);");

                    x.CSharp("var user = await Domain.User.FindByEmail(info.Email);");
                    x.If("user != null && !user.IsActive")
                     .CSharp(@"Notify(""اکانت شما غیرفعال شده است، لطفا با مدیر سیستم تماس بگیرید"", ""error"");
                        return View(info); ");
                    x.If("user == null || !SecurePassword.Verify(info.Password, user.Password, user.Salt)")
                     .CSharp(@"Notify(""نام کاربری یا رمز عبور نامعتبر است"", ""error"");
                        info.ShowCaptcha = await LogonFailure.MustShowCaptcha(info.Email, Request.GetIPAddress());
                        return View(info); ");
                    x.CSharp("await user.LogOn();");
                    x.CSharp("await LogonFailure.Remove(info.Email, Request.GetIPAddress());");
                    x.If("Url.ReturnUrl().HasValue() && Url.ReturnUrl() != Url.Index(\"Login\")").ReturnToPreviousPage();
                    x.Go<Login.DispatchPage>().RunServerSide();
                });

            Link("Forgot password?").Text("فراموشی رمز عبور").CssClass("text-info").OnClick(x => x.Go<Login.ForgotPasswordPage>());

            ViewModelProperty<bool>("ShowCaptcha").RetainInPost();
            ViewModelProperty<string>("CaptchaCode").NotReadOnly();
        }
    }
}
