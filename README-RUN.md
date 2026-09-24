# 🥇 گلدن‌شک — راهنمای اجرای محلی روی سیستم خودتان

این پکیج شامل **بک‌اند ASP.NET Core 6** و **فرانت‌اند Angular 18** است.

```
GoldenCheque-full
├── GoldenChequeBack.Domain/         ← موجودیت‌ها
├── GoldenChequeBack.Persistence/    ← EF Core + Migration‌ها
├── GoldenChequeBack.Service/        ← سرویس‌ها و Repository‌ها
├── GoldenChequeBack.Infrastructure/ ← Middleware و Extension‌ها
├── GoldenChqeueBack/                ← پروژه اصلی Web API
├── golden-cheque-ui/                ← فرانت‌اند Angular (RTL فارسی)
├── GoldenChqeueBack.sln
└── README-RUN.md                    ← همین راهنما
```

---

## ۱) پیش‌نیازها

| ابزار | نسخه | لینک |
|---|---|---|
| .NET SDK | 6.0+ | https://dotnet.microsoft.com/download/dotnet/6.0 |
| Node.js | 18+ | https://nodejs.org |
| SQL Server | 2019+ (یا LocalDB/Express) | https://www.microsoft.com/sql-server |

---

## ۲) تنظیمات و ساخت خودکار دیتابیس

الف) مقادیر اتصال را **بدون کامیت در گیت** از طریق فایل `GoldenChqeueBack/appsettings.Development.json` بدهید (این فایل را به .gitignore اضافه کنید):

```json
{
  "ConnectionStrings": {
    "OnionArchConn": "Data Source=.\\SQLEXPRESS;Initial Catalog=GoldenCheque;Trusted_Connection=True;TrustServerCertificate=True",
    "IdentityConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=GoldenCheque;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "یک-کلید-تصادفی-حداقل-۳۲-کاراکتر-اینجا-بگذارید",
    "Issuer": "Identity",
    "Audience": "IdentityUser",
    "DurationInMinutes": 60
  }
}
```

ب) همین!

از نسخه فعلی، برنامه هنگام اجرا به‌صورت **خودکار** دیتابیس‌ها را می‌سازد:
- اگر دیتابیس وجود نداشته باشد، ساخته می‌شود (`Database.Migrate` روی هر دو کانتکست).
- مایگریشن‌ها اعمال و دیتای Seed (نقش‌ها، کاربر superadmin/basicuser، واحد «بسته» و دسته‌بندی‌ها) درج می‌شود.
- اگر خطای اتصال رخ دهد، برنامه با پیام واضح لاگ متوقف می‌شود تا مشکل connection string رفع شود.

پیش‌نیاز: فقط SQL Server باید در حال اجرا باشد؛ لازم نیست دیتابیس خالی بسازید.

اگر خواستید مایگریشن‌ها را دستی اعمال کنید (مثلاً برای عیب‌یابی):

```bash
dotnet ef database update --project GoldenChequeBack.Persistence --startup-project GoldenChqeueBack --context ApplicationDbContext
dotnet ef database update --project GoldenChequeBack.Persistence --startup-project GoldenChqeueBack --context IdentityContext
```

> اگر ابزار ef ندارید: `dotnet tool install --global dotnet-ef`

دیتای Seed پیش‌فرض:
- کاربر: `superadmin@gmail.com` / رمز: `Admin@12345`
- واحد پیش‌فرض «بسته» و دسته‌بندی‌های «الکترونیکی/غذایی»

---

## ۳) اجرای بک‌اند

```bash
dotnet run --project GoldenChqeueBack
```

بک‌اند روی `http://localhost:5082` بالا می‌آید.
Swagger (در حالت Development): `http://localhost:5082/OpenAPI`

---

## ۴) اجرای فرانت‌اند

در یک ترمینال جدید:

```bash
cd golden-cheque-ui
npm install
npm start
```

فرانت روی `http://localhost:4200` بالا می‌آید و از طریق proxy خودکار به بک‌اند روی 5082 وصل می‌شود.

ورود: `superadmin@gmail.com` / `Admin@12345`

---

## ۵) نکات مهم امنیتی ⚠️

- **هرگز** connection string و کلید JWT را در `appsettings.json` کامیت نکنید (قبلاً از این فایل حذف شده‌اند).
- App Password جیمیل قبلاً در گیت لو رفته — اگر استفاده می‌کنید آن را **لغو** کنید و از Gmail → App Passwords یکی جدید بسازید و فقط به‌صورت متغیر محیطی `MailSettings__SmtpPass` بدهید.
- تأیید ایمیل پیش‌فرض خاموش است (`FeatureManagement:EnableEmailService = false`) — کاربران بدون ایمیل هم با لینک فعال‌سازی مستقیم ساخته می‌شوند.

---

## عیب‌یابی سریع

| مشکل | راه‌حل |
|---|---|
| لاگین خطای `Cannot POST /api/...` یا `404` می‌دهد | dev-server بدون proxy اجرا شده. سرور فرانت را `Ctrl+C` کنید و دوباره `npm start` بزنید (proxy حالا داخل angular.json است و با هر `ng serve` اعمال می‌شود) |
| در صفحه‌های برنامه خطای 404 روی `/api/State` و مشابه آن می‌بینید | از نسخه بک‌اند قدیمی استفاده می‌کنید؛ مسیرهای بدون پسوند `Api` (مثل `/api/State`) و `/api/Shobe` فقط در نسخه جدید اضافه شده‌اند. zip جدید را بگیرید یا مسیر را با `/api/StateApi` صدا بزنید |
| بعد از ~۲۰ دقیقه همه درخواست‌ها خطای 500 می‌دهند ولی Postman سالم است | در نسخه‌های قدیمی، توکن منقضی باعث نوشتن استثنا در Response و کرش Kestrel می‌شد؛ در نسخه جدید این فیکس شده. فقط دوباره لاگین کنید (توکن ۲۰ دقیقه عمر دارد — `Jwt:DurationInMinutes`) |
| همان لحظه بعد از لاگین، همه درخواست‌های صفحه خطای 500 می‌دهند ولی Postman سالم جواب می‌گیرد | یعنی هنوز **باینری قدیمی** بک‌اند را اجرا می‌کنید (نسخه‌ای که در `OnAuthenticationFailed` استثنا را داخل Response می‌نویسد و کانکشن را خراب می‌کند). بک‌اند را Ctrl+C کنید و از سورس جدید دوباره اجرا کنید: `dotnet run --project GoldenChqeueBack`. سپس در مرورگر خروج بگیرید و دوباره لاگین کنید (توکن قدیمیِ خراب در localStorage فقط با لاگین مجدد عوض می‌شود). اگر توکن خراب مانده باشد، اینترسپتور جدید به‌طور خودکار روی 401 شما را به صفحه لاگین برمی‌گرداند |
| لاگین خطای `System.FormatException ... not a valid Base-64 string` می‌دهد | هش پسورد کاربر در دیتابیس قدیمی شما خراب است. بک‌اند را یک‌بار اجرا کنید — برنامه در استارتاپ هش کاربران seed (superadmin/basicuser) را خودکار تعمیر می‌کند و در لاگ اطلاع می‌دهد؛ سپس با `superadmin@gmail.com / Admin@12345` وارد شوید |
| `403 Forbidden` موقع باز کردن لینک preview | مخصوص لینک ابری است؛ روی localhost چنین مشکلی نیست |
| خطای اتصال SQL | connection string را در `appsettings.Development.json` چک کنید؛ `TrustServerCertificate=True` را فراموش نکنید |
| پورت 5082 اشغال | در `Properties/launchSettings.json` تغییر دهید و `proxy.conf.json` فرانت را هم همان کنید |
| فونت وزیرمتن لود نمی‌شود (آفلاین) | اینترنت برای Google Fonts لازم است؛ یا فونت را محلی کنید |
